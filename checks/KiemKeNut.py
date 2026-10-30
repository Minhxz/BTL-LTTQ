from pathlib import Path
import csv
import json
import re

goc = Path(__file__).resolve().parent.parent
du_an = goc / "QLyCuaHangSuaXe" / "QLyCuaHangSuaXe"
thu_muc_ra = goc / "checks" / "output" / "kiem-ke-nut"
thu_muc_ra.mkdir(parents=True, exist_ok=True)


def doc(tep):
    return tep.read_text(encoding="utf-8-sig")


def bo_chuoi_va_ghi_chu(noi_dung):
    mau = r'@"(?:""|[^"])*"|"(?:\\.|[^"\\])*"|\'(?:\\.|[^\'\\])*\'|//[^\n]*|/\*[\s\S]*?\*/'
    return re.sub(mau, lambda m: re.sub(r"[^\n]", " ", m.group()), noi_dung)


def pham_vi(noi_dung):
    chuoi = bo_chuoi_va_ghi_chu(noi_dung)
    ngan = []
    ket_qua = []
    for i, ky_tu in enumerate(chuoi):
        if ky_tu == "{":
            ngan.append(i)
        elif ky_tu == "}" and ngan:
            ket_qua.append((ngan.pop(), i))
    return sorted(ket_qua)


def phan_dinh_dang(noi_dung, cac_khoi):
    cac_bien = []
    for mau, vi_tri, kieu_mau in [
        (r"\b(?:var|[\w.]*frm\w+)\s+(\w+)\s*=\s*new\s+(?:[\w]+\.)*(frm\w+)\s*\(", 1, "tao"),
        (r"\b(?:var|[\w.]*frm\w+)\s+(\w+)\s*=\s*Application\.OpenForms\.OfType<(frm\w+)>\(\)", 1, "tao"),
        (r"\b(frm\w+)\s+(\w+)\s*[,)]", 2, "tham_so"),
    ]:
        for tim in re.finditer(mau, noi_dung):
            ten = tim.group(vi_tri)
            kieu = tim.group(2 if vi_tri == 1 else 1)
            dang_o = [k for k in cac_khoi if k[0] < tim.start() < k[1]]
            khoi_sau = next((k for k in cac_khoi if k[0] > tim.end()), None)
            truoc = noi_dung[max(0, tim.start() - 20):tim.start()]
            if kieu_mau == "tham_so" or re.search(r"\busing\s*\(\s*$", truoc):
                gioi_han = khoi_sau
            else:
                gioi_han = max(dang_o, default=None)
            if gioi_han:
                cac_bien.append((ten, kieu, tim.start(), gioi_han[1]))
    return cac_bien


bang_chung = {}
khong_nhan_dien = []
for tep in sorted((goc / "checks").glob("*.cs")):
    noi_dung = doc(tep)
    cac_khoi_tep = pham_vi(noi_dung)
    cac_bien = phan_dinh_dang(noi_dung, cac_khoi_tep)
    cac_hop_thoai = []
    for tim in re.finditer(r'BamMoHopThoai<(?:\w+\.)*(frm\w+)>\(\s*(\w+)\s*,\s*"(\w+)"\s*,\s*(\w+)\s*=>\s*\{', noi_dung):
        dau_khoi = tim.end() - 1
        khoi = next((k for k in cac_khoi_tep if k[0] == dau_khoi), None)
        if khoi:
            cac_bien.append((tim.group(4), tim.group(1), dau_khoi, khoi[1]))
            dong_nut = re.match(r'\s*,\s*"(\w+)"\s*\)', noi_dung[khoi[1] + 1:])
            cac_hop_thoai.append((tim, dong_nut.group(1) if dong_nut else None))

    def kieu_cua_bien(ten, vi_tri):
        cac_kieu = [b for b in cac_bien if b[0] == ten and b[2] <= vi_tri <= b[3]]
        return max(cac_kieu, key=lambda b: b[2])[1] if cac_kieu else None

    for tim, nut_dong in cac_hop_thoai:
        kieu_cha = kieu_cua_bien(tim.group(2), tim.start())
        neo = f"{tep.relative_to(goc).as_posix()}:{noi_dung.count(chr(10), 0, tim.start()) + 1} (BamMoHopThoai)"
        if kieu_cha:
            bang_chung.setdefault((kieu_cha, tim.group(3)), []).append(neo)
        if nut_dong:
            bang_chung.setdefault((tim.group(1), nut_dong), []).append(neo)

    nut_bien = []
    for tim in re.finditer(r'\bvar\s+(\w+)\s*=\s*Lay<Button>\(\s*(\w+)\s*,\s*"(\w+)"\s*\)', noi_dung):
        kieu = kieu_cua_bien(tim.group(2), tim.start())
        cac_khoi = [k for k in pham_vi(noi_dung) if k[0] < tim.start() < k[1]]
        if kieu and cac_khoi:
            nut_bien.append((tim.group(1), kieu, tim.group(3), tim.start(), max(cac_khoi)[1]))

    for tim in re.finditer(r'(?:Lay<Button>\(\s*(\w+)\s*,\s*"(\w+)"\s*\)|(\w+))\.PerformClick\s*\(', noi_dung):
        kieu = None
        ten_nut = None
        if tim.group(1):
            kieu = kieu_cua_bien(tim.group(1), tim.start())
            ten_nut = tim.group(2)
        else:
            cac_nut = [b for b in nut_bien if b[0] == tim.group(3) and b[3] <= tim.start() <= b[4]]
            if cac_nut:
                nut = max(cac_nut, key=lambda b: b[3])
                kieu, ten_nut = nut[1], nut[2]
        dong = noi_dung.count("\n", 0, tim.start()) + 1
        neo = f"{tep.relative_to(goc).as_posix()}:{dong}"
        if kieu and ten_nut:
            bang_chung.setdefault((kieu, ten_nut), []).append(neo)
        else:
            khong_nhan_dien.append(f"{neo}: {tim.group()}")

tep_thuc_chay = goc / "checks" / "output" / "smoke-all-buttons" / "su-kien.jsonl"
so_click_thuc_chay = set()
if tep_thuc_chay.exists():
    for so_dong, dong in enumerate(doc(tep_thuc_chay).splitlines(), 1):
        try:
            ket_qua = json.loads(dong)
        except json.JSONDecodeError:
            continue
        du_lieu = ket_qua.get("data", {})
        if ket_qua.get("kind") == "RESULT" and du_lieu.get("clicked"):
            cap = (du_lieu.get("form"), du_lieu.get("button"))
            so_click_thuc_chay.add(cap)
            bang_chung.setdefault(cap, []).append(f"{tep_thuc_chay.relative_to(goc).as_posix()}:{so_dong} RESULT {du_lieu.get('status')}")

cac_dong = []
for tep in sorted(du_an.rglob("frm*.Designer.cs")):
    noi_dung = doc(tep)
    kieu = re.search(r"partial\s+class\s+(frm\w+)", noi_dung).group(1)
    ma_chinh = tep.with_name(tep.name.replace(".Designer.cs", ".cs"))
    ma_chinh_text = doc(ma_chinh) if ma_chinh.exists() else ""
    for tim in re.finditer(r"this\.(\w+)\s*=\s*new\s+(?:System\.Windows\.Forms\.)?Button\s*\(", noi_dung):
        ten = tim.group(1)
        nhan = re.search(r'this\.' + re.escape(ten) + r'\.Text\s*=\s*"((?:\\.|[^"\\])*)"', noi_dung)
        noi_dung_nut = nhan.group(1) if nhan else ""
        su_kien = []
        for van_ban in (noi_dung, ma_chinh_text):
            for noi in re.finditer(r"(?:this\.)?" + re.escape(ten) + r"\.Click\s*\+=\s*([^;]+);", van_ban):
                su_kien.append(re.sub(r"\s+", " ", noi.group(1).strip()))
        noi_menu = re.search(r'NoiNutCoSan\(\s*' + re.escape(ten) + r'\s*,\s*"([^"]+)"', ma_chinh_text)
        if noi_menu:
            su_kien.append("NoiNutCoSan → MoTrang(" + noi_menu.group(1) + ")")
        thoat = re.search(r'(?:this\.)?' + re.escape(ten) + r'\.DialogResult\s*=\s*(?:System\.Windows\.Forms\.)?DialogResult\.(\w+)', noi_dung)
        if thoat:
            su_kien.append("DialogResult." + thoat.group(1))
        cac_dong.append({
            "Form": kieu,
            "TenNut": ten,
            "NoiDungDesigner": noi_dung_nut,
            "SuKien": " | ".join(su_kien),
            "DaPerformClick": "Có" if (kieu, ten) in bang_chung else "Chưa có bằng chứng",
            "BangChung": "; ".join(bang_chung.get((kieu, ten), [])),
            "NguonDesigner": f"{tep.relative_to(goc).as_posix()}:{noi_dung.count(chr(10), 0, tim.start()) + 1}",
        })

duong_dan_csv = thu_muc_ra / "BaoPhuNut.csv"
with duong_dan_csv.open("w", encoding="utf-8-sig", newline="") as tep:
    ghi = csv.DictWriter(tep, fieldnames=list(cac_dong[0]))
    ghi.writeheader()
    ghi.writerows(cac_dong)

da_click = sum(d["DaPerformClick"] == "Có" for d in cac_dong)
noi_dung_ra = [
    "# Kiểm kê nút theo mã nguồn hiện tại",
    "",
    "Phạm vi: các Button được khởi tạo trong Designer của 22 form sản phẩm; đối chiếu PerformClick trong checks/*.cs. Báo cáo này không tính việc gọi trực tiếp backend hay việc root bấm ngoài giao diện. Có PerformClick trong nguồn chỉ chứng minh đường kiểm thử có thao tác đó; không thay thế kết quả chạy test.",
    "",
    f"Tổng {len(cac_dong)} nút; {da_click} cặp form/nút có PerformClick; {len(cac_dong) - da_click} chưa có bằng chứng click trong nguồn kiểm thử.",
    f"Số cặp form/nút có RESULT clicked=true trong runner tương tác: {len(so_click_thuc_chay)}. Runner tổng quát có PerformClick không được tự tính là đã bấm cả 102 nút; chỉ tính từng RESULT thực tế.",
    "",
    "| Form | Tổng | Có PerformClick | Chưa có bằng chứng |",
    "|---|---:|---:|---:|",
]
for kieu in sorted({d["Form"] for d in cac_dong}):
    cac_nut = [d for d in cac_dong if d["Form"] == kieu]
    so = sum(d["DaPerformClick"] == "Có" for d in cac_nut)
    noi_dung_ra.append(f"| {kieu} | {len(cac_nut)} | {so} | {len(cac_nut) - so} |")
noi_dung_ra.extend(["", "## Từng nút", "", "| Form | Nút | Nội dung | Đối chiếu PerformClick |", "|---|---|---|---|"])
for dong in cac_dong:
    thong_tin = dong["BangChung"] if dong["BangChung"] else dong["DaPerformClick"]
    noi_dung_ra.append(f'| {dong["Form"]} | {dong["TenNut"]} | {dong["NoiDungDesigner"]} | {thong_tin} |')
noi_dung_ra.extend(["", "## Lời gọi chưa tự nhận diện", ""] + (khong_nhan_dien or ["Không có."]))
(thu_muc_ra / "BaoPhuNut.md").write_text("\n".join(noi_dung_ra) + "\n", encoding="utf-8-sig")
print(json.dumps({"so_form": len({d["Form"] for d in cac_dong}), "so_nut": len(cac_dong), "co_PerformClick": da_click, "chua_co_bang_chung": len(cac_dong) - da_click, "loi_goi_chua_nhan_dien": khong_nhan_dien, "bao_cao": str(duong_dan_csv)}, ensure_ascii=False, indent=2))
