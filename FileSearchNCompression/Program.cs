using System;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Windows.Forms;

namespace FileSearchApp
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private void btnBrowse_Click(object sender, EventArgs e)
        {
            using (FolderBrowserDialog dialog = new FolderBrowserDialog())
            {
                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    txtDirectory.Text = dialog.SelectedPath;
                }
            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            string directory = txtDirectory.Text;
            string fileName = txtFileName.Text;

            if (Directory.Exists(directory) && !string.IsNullOrWhiteSpace(fileName))
            {
                string filePath = Directory.GetFiles(directory, fileName, SearchOption.AllDirectories).FirstOrDefault();
                
                if (!string.IsNullOrEmpty(filePath))
                {
                    txtResult.Text = filePath;
                    DisplayFileContent(filePath);
                }
                else
                {
                    MessageBox.Show("Файл не найден.", "Результат", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            else
            {
                MessageBox.Show("Введите корректные данные.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void DisplayFileContent(string filePath)
        {
            try
            {
                using (FileStream fs = new FileStream(filePath, FileMode.Open, FileAccess.Read))
                using (StreamReader reader = new StreamReader(fs))
                {
                    Console.WriteLine(reader.ReadToEnd());
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка чтения файла: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnCompress_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtResult.Text) && File.Exists(txtResult.Text))
            {
                string sourceFile = txtResult.Text;
                string compressedFile = sourceFile + ".gz";

                try
                {
                    using (FileStream originalFileStream = new FileStream(sourceFile, FileMode.Open, FileAccess.Read))
                    using (FileStream compressedFileStream = new FileStream(compressedFile, FileMode.Create))
                    using (GZipStream compressionStream = new GZipStream(compressedFileStream, CompressionMode.Compress))
                    {
                        originalFileStream.CopyTo(compressionStream);
                    }

                    MessageBox.Show($"Файл сжат: {compressedFile}", "Готово", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Ошибка сжатия: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Файл для сжатия не найден.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
