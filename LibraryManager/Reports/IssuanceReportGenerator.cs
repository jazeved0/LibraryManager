using iTextSharp.text;
using iTextSharp.text.pdf;
using LibraryManager.Data.Item;
using LibraryManager.Data.Item.Status;
using LibraryManager.Data.Member;
using LibraryManager.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;

namespace LibraryManager.Reports
{
    class IssuanceReportGenerator : ReportGenerator
    {
        public static void BuildPDF(Document doc, PdfWriter writer)
        {
            AddHeader(doc, writer, "Weekly Issuance Report", App.SCHOOL_NAME + " - Week of (" + DateTime.Now.ToShortDateString() + ")");
            doc.Add(Chunk.NEWLINE);

            IEnumerable<Member> ReportedMembers = MainWindowViewModel.Instance.MembersVM.Members.Where(m => m.IssuanceCount > 0);
            foreach(Member member in ReportedMembers)
            {
                AddMemberIssuanceDetails(doc, writer, member);
            }
        }

        public static void AddMemberIssuanceDetails(Document doc, PdfWriter writer, Member member)
        {
            Font subheadingFont = FontFactory.GetFont(FontFactory.HELVETICA, 11, Font.BOLD);
            Font textFont = FontFactory.GetFont(FontFactory.HELVETICA, 11, Font.NORMAL);
            doc.Add(new Paragraph(String.Format("• {0}  ({1};  ID:{2})", member.Name, member.Type, member.ID), subheadingFont) { SpacingAfter = 2f });

            PdfPTable memberDetails = new PdfPTable(5)
            {
                WidthPercentage = 100
            };
            memberDetails.SetWidths(new float[] { 1, 1, 3, 1, 1.2f });
            memberDetails.AddCell(CreateHeaderCell("ID"));
            memberDetails.AddCell(CreateHeaderCell("Type"));
            memberDetails.AddCell(CreateHeaderCell("Title"));
            memberDetails.AddCell(CreateHeaderCell("Loan Date"));
            memberDetails.AddCell(CreateHeaderCell("Return By Date"));

            IEnumerable<IssuableItem> ReportedItems = member.Items.Where(i => i.Status.Type == ItemStatus.StatusType.Issued || i.Status.Type == ItemStatus.StatusType.Overdue);
            foreach(IssuableItem item in ReportedItems)
            {
                memberDetails.AddCell(CreateStandardCell(item.ID));
                memberDetails.AddCell(CreateStandardCell(item.Type.ToString()));
                memberDetails.AddCell(CreateStandardCell(item.Title));
                memberDetails.AddCell(CreateStandardCell(item.Status.InitialDate.ToShortDateString()));
                memberDetails.AddCell(CreateStandardCell(item.Status.DueDate.ToShortDateString()));
            }

            doc.Add(memberDetails);
            doc.Add(Chunk.NEWLINE);
        }
    }
}