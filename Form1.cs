namespace Compresseur_de_fichier
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            progressBar1.Visible = false;
        }

        private void button1_Click(object sender, EventArgs e)
        {

            // Cr�ez une instance de OpenFileDialog
            OpenFileDialog openFileDialog1 = new OpenFileDialog();

            // D�finissez des propri�t�s de la bo�te de dialogue
            openFileDialog1.Title = "S�lectionnez un fichier";
            openFileDialog1.Filter = openFileDialog1.Filter = "Fichiers PDF (*.pdf)|*.pdf";

            openFileDialog1.FilterIndex = 1;
            openFileDialog1.RestoreDirectory = true;

            // Affichez la bo�te de dialogue et v�rifiez si l'utilisateur a s�lectionn� un fichier
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                // Le chemin du fichier s�lectionn� est dans openFileDialog1.FileName
                string selectedFile = openFileDialog1.FileName;

                // Faites quelque chose avec le fichier s�lectionn�, par exemple, affichez le chemin dans une zone de texte

                // Masquer le bouton
                //button1.Visible = false;

                Compress compress = new Compress(selectedFile, progressBar1);
                long originalsize = compress.GetFileSize(selectedFile);
                Aspose.Pdf.Document compressedDocument = compress.compressPdf();
                if (compress.CompressionSucceeded && compressedDocument != null)
                {

                    string destinationMessage = "Veuillez s�lectionner la destination du fichier";

                    MessageBox.Show($"{destinationMessage}", "Succ�s", MessageBoxButtons.OK, MessageBoxIcon.Information);


                    // Affichez une bo�te de dialogue pour permettre � l'utilisateur de choisir la destination et le nom du fichier
                    SaveFileDialog saveFileDialog = new SaveFileDialog();
                    saveFileDialog.Filter = "Fichiers PDF (*.pdf)|*.pdf";
                    if (saveFileDialog.ShowDialog() == DialogResult.OK)
                    {
                        string destinationPath = saveFileDialog.FileName;
                        compress.SaveFile(compressedDocument, destinationPath);
                        long resultsize = compress.GetFileSize(destinationPath);
                        string successMessage = $"Le fichier {selectedFile} a �t� compress�,";
                        string result = $"taille originale {originalsize} Ko >>> taille actuelle {resultsize} Ko";

                        MessageBox.Show($"{successMessage}\n{result}", "Succ�s", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    }


                }
                else
                {
                    MessageBox.Show($" Erreur compression du fichier {selectedFile}", "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }


            }
        }
    }
}