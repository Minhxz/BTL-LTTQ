using System;
using System.Data;
using System.Globalization;
using System.IO;
using System.IO.Compression;
using System.Text;
using System.Windows.Forms;
using System.Xml;

namespace QLyCuaHangSuaXe.LopDungChung
{
    public static class XuatExcel
    {
        public static void LuuExcel(DataTable bangDuLieu, string tieuDe)
        {
            if (bangDuLieu == null)
                throw new InvalidOperationException("Chưa có dữ liệu để xuất.");
            using (var hopThoai = new SaveFileDialog
            {
                Filter = "Tệp Excel (*.xlsx)|*.xlsx",
                Title = "Xuất dữ liệu ra Excel",
                FileName = "BangCong_" + DateTime.Today.ToString("yyyyMMdd") + ".xlsx"
            }

            )
                if (hopThoai.ShowDialog() == DialogResult.OK)
                {
                    GhiTep(bangDuLieu, hopThoai.FileName, tieuDe);
                    MessageBox.Show("Đã xuất Excel: " + hopThoai.FileName);
                }
        }

        static void GhiMucVanBan(ZipArchive tepNen, string ten, string giaTri)
        {
            var mucNen = tepNen.CreateEntry(ten);
            using (var boGhiVanBan = new StreamWriter(mucNen.Open(), new UTF8Encoding(false)))
                boGhiVanBan.Write(giaTri);
        }

        public static void GhiTep(DataTable bangDuLieu, string duongDan, string tieuDe)
        {
            using (var luongTep = new FileStream(duongDan, FileMode.Create, FileAccess.Write))
            using (var tepNen = new ZipArchive(luongTep, ZipArchiveMode.Create))
            {
                GhiMucVanBan(tepNen, "[Content_Types].xml", "<Types xmlns=\"http://schemas.openxmlformats.org/package/2006/content-types\"><Default Extension=\"rels\" ContentType=\"application/vnd.openxmlformats-package.relationships+xml\"/><Default Extension=\"xml\" ContentType=\"application/xml\"/><Override PartName=\"/xl/workbook.xml\" ContentType=\"application/vnd.openxmlformats-officedocument.spreadsheetml.sheet.main+xml\"/><Override PartName=\"/xl/worksheets/sheet1.xml\" ContentType=\"application/vnd.openxmlformats-officedocument.spreadsheetml.worksheet+xml\"/></Types>");
                GhiMucVanBan(tepNen, "_rels/.rels", "<Relationships xmlns=\"http://schemas.openxmlformats.org/package/2006/relationships\"><Relationship Id=\"rId1\" Type=\"http://schemas.openxmlformats.org/officeDocument/2006/relationships/officeDocument\" Target=\"xl/workbook.xml\"/></Relationships>");
                GhiMucVanBan(tepNen, "xl/_rels/workbook.xml.rels", "<Relationships xmlns=\"http://schemas.openxmlformats.org/package/2006/relationships\"><Relationship Id=\"rId1\" Type=\"http://schemas.openxmlformats.org/officeDocument/2006/relationships/worksheet\" Target=\"worksheets/sheet1.xml\"/></Relationships>");
                GhiMucVanBan(tepNen, "xl/workbook.xml", "<workbook xmlns=\"http://schemas.openxmlformats.org/spreadsheetml/2006/main\" xmlns:r=\"http://schemas.openxmlformats.org/officeDocument/2006/relationships\"><sheets><sheet name=\"Bao cao\" sheetId=\"1\" r:id=\"rId1\"/></sheets></workbook>");
                using (var boGhiXml = XmlWriter.Create(tepNen.CreateEntry("xl/worksheets/sheet1.xml").Open(), new XmlWriterSettings { Encoding = new UTF8Encoding(false), CloseOutput = true }))
                {
                    const string khongGianTen = "http://schemas.openxmlformats.org/spreadsheetml/2006/main";
                    boGhiXml.WriteStartElement("worksheet", khongGianTen);
                    boGhiXml.WriteStartElement("sheetViews");
                    boGhiXml.WriteStartElement("sheetView");
                    boGhiXml.WriteAttributeString("workbookViewId", "0");
                    boGhiXml.WriteStartElement("pane");
                    boGhiXml.WriteAttributeString("ySplit", "2");
                    boGhiXml.WriteAttributeString("topLeftCell", "A3");
                    boGhiXml.WriteAttributeString("state", "frozen");
                    boGhiXml.WriteEndElement();
                    boGhiXml.WriteEndElement();
                    boGhiXml.WriteEndElement();
                    boGhiXml.WriteStartElement("cols");
                    boGhiXml.WriteStartElement("col");
                    boGhiXml.WriteAttributeString("min", "1");
                    boGhiXml.WriteAttributeString("max", Math.Max(1, bangDuLieu.Columns.Count).ToString());
                    boGhiXml.WriteAttributeString("width", "23");
                    boGhiXml.WriteAttributeString("customWidth", "1");
                    boGhiXml.WriteEndElement();
                    boGhiXml.WriteEndElement();
                    boGhiXml.WriteStartElement("sheetData");
                    boGhiXml.WriteStartElement("row");
                    GhiO(boGhiXml, tieuDe);
                    boGhiXml.WriteEndElement();
                    boGhiXml.WriteStartElement("row");
                    foreach (DataColumn cot in bangDuLieu.Columns)
                        GhiO(boGhiXml, GiaoDien.TenCot(cot.ColumnName));
                    boGhiXml.WriteEndElement();
                    foreach (DataRow dong in bangDuLieu.Rows)
                    {
                        boGhiXml.WriteStartElement("row");
                        foreach (object giaTri in dong.ItemArray)
                            GhiO(boGhiXml, giaTri);
                        boGhiXml.WriteEndElement();
                    }

                    boGhiXml.WriteEndElement();
                    boGhiXml.WriteEndElement();
                }
            }
        }

        static void GhiO(XmlWriter boGhiXml, object giaTri)
        {
            boGhiXml.WriteStartElement("c");
            if (giaTri is decimal || giaTri is double || giaTri is float || giaTri is int || giaTri is long || giaTri is short)
                boGhiXml.WriteElementString("v", Convert.ToString(giaTri, CultureInfo.InvariantCulture));
            else
            {
                boGhiXml.WriteAttributeString("t", "inlineStr");
                boGhiXml.WriteStartElement("is");
                boGhiXml.WriteStartElement("t");
                boGhiXml.WriteAttributeString("xml", "space", null, "preserve");
                boGhiXml.WriteString(giaTri is DateTime ? ((DateTime)giaTri).ToString("dd/MM/yyyy HH:mm") : giaTri == DBNull.Value ? "" : Convert.ToString(giaTri));
                boGhiXml.WriteEndElement();
                boGhiXml.WriteEndElement();
            }

            boGhiXml.WriteEndElement();
        }
    }
}
