using iTextSharp.text;
using iTextSharp.text.pdf;
using LibraryManager.Data.Item;
using LibraryManager.Data.Member;
using LibraryManager.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;

namespace LibraryManager.Reports
{
    class FeeReportGenerator : ReportGenerator
    {
        public static void BuildPDF(Document doc, PdfWriter writer)
        {
            AddHeader(doc, writer, "Weekly Overdue Fee Report", App.SCHOOL_NAME + " - Week of (" + DateTime.Now.ToShortDateString() + ")");
            doc.Add(Chunk.NEWLINE);

            IEnumerable<Member> ReportedMembers = MainWindowViewModel.Instance.MembersVM.Members.Where(m => m.Items.Where(i => i.Status.Fee > 0m).Count() > 0);
            foreach (Member member in ReportedMembers)
            {
                AddMemberFeeDetails(doc, writer, member);
            }
        }

        public static void AddMemberFeeDetails(Document doc, PdfWriter writer, Member member)
        {
            Font subheadingFont = FontFactory.GetFont(FontFactory.HELVETICA, 11, Font.BOLD);
            Font textFont = FontFactory.GetFont(FontFactory.HELVETICA, 11, Font.NORMAL);
            doc.Add(new Paragraph(String.Format("• {0}  ({1};  ID:{2})     Total Fees: {3:C}", member.Name, member.Type, member.ID, member.Fee), subheadingFont) { SpacingAfter = 2f });

            PdfPTable memberDetails = new PdfPTable(5)
            {
                WidthPercentage = 100
            };
            memberDetails.SetWidths(new float[] { 1.12f, 3, 1, 1, 1.15f });
            memberDetails.AddCell(CreateHeaderCell("Fee Incurred"));
            memberDetails.AddCell(CreateHeaderCell("Title"));
            memberDetails.AddCell(CreateHeaderCell("Type"));
            memberDetails.AddCell(CreateHeaderCell("Issued On"));
            memberDetails.AddCell(CreateHeaderCell("Overdue Since"));

            IEnumerable<IssuableItem> ReportedItems = member.Items.Where(i => i.Status.Fee > 0m);
            foreach (IssuableItem item in ReportedItems)
            {
                memberDetails.AddCell(CreateStandardCell(String.Format("{0:C}", item.Status.Fee)));
                memberDetails.AddCell(CreateStandardCell(item.Title));
                memberDetails.AddCell(CreateStandardCell(item.Type.ToString()));
                memberDetails.AddCell(CreateStandardCell(item.Status.InitialDate.ToShortDateString()));
                memberDetails.AddCell(CreateStandardCell(item.Status.InitialDate.AddDays(App.Instance.Config.DiscreteValues[item.Status.Owner.Type]["issuance_max_duration"].CurrentValue).ToShortDateString()));
            }

            doc.Add(memberDetails);
            doc.Add(Chunk.NEWLINE);
        }
    }
}