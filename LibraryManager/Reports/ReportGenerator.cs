using iTextSharp.text;
using iTextSharp.text.pdf;

namespace LibraryManager.Reports
{
    abstract class ReportGenerator
    {
        public static void AddHeader(Document doc, PdfWriter writer, string title, string subtitle)
        {
            Font titleFont = FontFactory.GetFont(FontFactory.HELVETICA, 22, Font.NORMAL);
            Font subtitleFont = FontFactory.GetFont(FontFactory.HELVETICA, 13, Font.ITALIC);
            doc.Add(new Paragraph(title, titleFont) { Leading = 0 });
            doc.Add(new Paragraph(subtitle, subtitleFont));
            Paragraph p = new Paragraph(new Chunk(new iTextSharp.text.pdf.draw.LineSeparator(0.0F, 100.0F, BaseColor.BLACK, Element.ALIGN_LEFT, 0)));
            doc.Add(p);
        }

        public static PdfPCell CreateHeaderCell(string header)
        {
            Font textFont = FontFactory.GetFont(FontFactory.HELVETICA, 11, Font.NORMAL);
            return new PdfPCell(new Phrase(header, textFont))
            {
                Border = 0,
                BorderColorBottom = new BaseColor(System.Drawing.Color.Black),
                BorderWidthBottom = 1f
            };
        }

        public static PdfPCell CreateStandardCell(string text)
        {
            Font textFont = FontFactory.GetFont(FontFactory.HELVETICA, 11, Font.NORMAL);
            return new PdfPCell(new Phrase(text, textFont))
            {

            };
        }
    }
}
