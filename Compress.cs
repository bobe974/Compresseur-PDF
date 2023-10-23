using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using Aspose.Pdf;
using Aspose.Pdf.Optimization;



namespace Compresseur_de_fichier
{
    internal class Compress
    {
        private string filePath;
        private ProgressBar progressBar;
      public bool CompressionSucceeded { get; private set; }
        

        public Compress(String FilePath, ProgressBar progressBar)
        {

            this.filePath = FilePath;
            this.progressBar = progressBar;
            CompressionSucceeded = false;
           
        }

        public Aspose.Pdf.Document compressPdf()
        {
            try
            {
                // Load the PDF file for compression
                Aspose.Pdf.Document CompressPdfDocument = new Aspose.Pdf.Document(filePath);

                // Optimize PDF
                OptimizationOptions PdfoptimizeOptions = new OptimizationOptions();

                // Enable image compression
                PdfoptimizeOptions.ImageCompressionOptions.CompressImages = true;

                // Set the image quality
                PdfoptimizeOptions.ImageCompressionOptions.ImageQuality = 50;

                // Appy optimization
                CompressPdfDocument.OptimizeResources(PdfoptimizeOptions);

                // Save the compressed PDF
                //string repertoire = System.IO.Directory.GetCurrentDirectory();
               // CompressPdfDocument.Save(repertoire + "/compressed.pdf");
                CompressionSucceeded = true; // La compression a réussi
                return CompressPdfDocument;
      
            }
            catch (Exception ex)
            {
                return null; // En cas d'erreur, retourne null
            }
        }

        public void SaveFile(Aspose.Pdf.Document CompressPdfDocument, String destPath)
        {
            try
            {
                CompressPdfDocument.Save(destPath);
            }
            catch (Exception ex)
            {

            }
           
        }

        public long GetFileSize(string filePath)
        {
            if (File.Exists(filePath))
            {
                FileInfo fileInfo = new FileInfo(filePath);
                long fileSize = fileInfo.Length;
                return fileSize / 1024; // La taille du fichier en kilo

            }
            else
            {
                // Le fichier n'existe pas
                return -1;
            }
        }
    }
}
