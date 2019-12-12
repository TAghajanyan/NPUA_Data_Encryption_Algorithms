using System;
using System.Net.Sockets;
using System.Threading;
using System.Windows;

namespace Client
{
    public partial class MainWindow : Window
    {

        TcpClient clientSocket = new TcpClient();
        NetworkStream serverStream = default(NetworkStream);
        string readData = null;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button1_Click(object sender, RoutedEventArgs e)
        {
            readData = "Conected to Chat Server ...";
            try
            {
                clientSocket.Connect("127.0.0.1", 8001);
                serverStream = clientSocket.GetStream();
                byte[] outStream = System.Text.Encoding.ASCII.GetBytes(textBox1.Text + "$");
                serverStream.Write(outStream, 0, outStream.Length);
                serverStream.Flush();
                textBox1.Text = string.Empty;
                Thread ctThread = new Thread(GetMessage);
                ctThread.Start();
            }
            catch (Exception ex) { MessageBox.Show(ex.ToString()); }

        }

        private void Button2_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                byte[] outStream = System.Text.Encoding.ASCII.GetBytes(textBox3.Text + "$");
                serverStream.Write(outStream, 0, outStream.Length);
                serverStream.Flush();
                textBox3.Text = string.Empty;
            }
            catch (Exception ex) { MessageBox.Show(ex.ToString()); }
        }

        private void GetMessage()
        {
            while (true)
            {
                try
                {
                    serverStream = clientSocket.GetStream();
                    int buffSize = 0;
                    byte[] inStream = new byte[65536];
                    buffSize = clientSocket.ReceiveBufferSize;
                    int streamlength = serverStream.Read(inStream, 0, buffSize);

                    string returndata = System.Text.Encoding.ASCII.GetString(inStream, 0, streamlength);
                    readData = "" + returndata;
                    Msg();
                }
                catch (Exception ex) { MessageBox.Show(ex.ToString()); break; }
            }
        }

        private void Msg()
        {
            Dispatcher.Invoke(() => { textBlock2.Text = $"{textBlock2.Text}{Environment.NewLine} >> {readData}"; });
        }


    }
}
