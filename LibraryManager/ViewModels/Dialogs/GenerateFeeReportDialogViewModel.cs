using iTextSharp.text;
using iTextSharp.text.pdf;
using LibraryManager.Reports;
using LibraryManager.ViewModels.Pages;
using System;
using System.IO;

namespace LibraryManager.ViewModels.Dialogs
{
    class GenerateFeeReportDialogViewModel : DialogViewModelBase
    {
        public override string FinalActionText => "Generate";

        protected override void AddPages()
        {
            PageViewModels.Add(new ChooseSaveLocationViewModel());
        }

        protected override void Complete()
        {
            try
            {
                string path = (PageViewModels[0] as ChooseSaveLocationViewModel).FilePath;
                Document doc = new Document();
                PdfWriter writer = PdfWriter.GetInstance(doc, new FileStream(path, FileMode.Create));
                doc.Open();
                {
                    FeeReportGenerator.BuildPDF(doc, writer);
                }
                doc.Close();
            }
            catch (Exception)
            {
                // An unexpected error occurred; fail gracefully
                return;
            }
        }
    }
}
