using Microsoft.Win32;
using System.IO;
using System.Windows;

namespace LZ77
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private static string fileName;
        private string input;
        private string compressed = string.Empty;
        private string decompressed = string.Empty;
        FileInfo fi;

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            if (openFileDialog.ShowDialog() == true)
                fileName = openFileDialog.FileName;
            fi = new FileInfo(fileName);
            _fileName.Text = fileName;

            input = File.ReadAllText(fileName);

            _inputValue.Text = input;
            _inputSize.Content = input.Length.ToString();
        }

        //Compress
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            compressed = LZ_77.CompressStr(input);
            _compressedValue.Text = compressed;
            _outputSize.Content = (compressed.Length).ToString();
            _difference.Content = (100 * (1 - compressed.Length / (float)input.Length)).ToString() + "%";
        }

        //Decompress
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            decompressed = LZ_77.DecompressStr(compressed);
            MessageBox.Show(decompressed);
        }
    }
}
