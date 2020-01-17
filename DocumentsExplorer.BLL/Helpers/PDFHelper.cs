using iTextSharp.text;
using iTextSharp.text.pdf;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace DocumentsExplorer.BLL.Helpers
{
    public static class PdfMerger
    {


        public static bool MergeFiles(string outputPdfPath, List<string> lstFiles)
        {


            try
            {
                PdfReader reader = null;
                Document sourceDocument = null;
                PdfCopy pdfCopyProvider = null;
                PdfImportedPage importedPage;
                //string outputPdfPath = @"C:/new.pdf";


                sourceDocument = new Document();
                pdfCopyProvider = new PdfCopy(sourceDocument, new System.IO.FileStream(outputPdfPath, System.IO.FileMode.Create));

                //Open the output file
                sourceDocument.Open();
                //Loop through the files list
                for (int i = 0; i < lstFiles.Count; ++i)
                {
                    reader = new PdfReader(lstFiles[i]);
                    // loop over the pages in that document
                    int n = reader.NumberOfPages;
                    for (int page = 0; page < n;)
                    {
                        pdfCopyProvider.AddPage(pdfCopyProvider.GetImportedPage(reader, ++page));
                    }
                }
                reader.Close();
                sourceDocument.Close();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
