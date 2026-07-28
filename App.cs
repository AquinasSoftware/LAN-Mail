using System;
using System.Net;
using System.Net.Sockets;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace LAN_Mail
{
    public partial class App : Form
    {

        string sysIP = "";
        const int port = 1440;
        Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);


        public App()
        {
            InitializeComponent();
        }

        private void appendText(string text, Color color)
        {
            convoTbx.SelectionStart = convoTbx.TextLength;
            convoTbx.SelectionLength = 0;
            convoTbx.SelectionColor = color;
            convoTbx.AppendText(text);
        }

        private void App_Load(object sender, EventArgs e)
        {
            this.FormBorderStyle = FormBorderStyle.FixedSingle; // Or Fixed3D, FixedDialog
            this.MaximizeBox = false;
            IPHostEntry host = Dns.GetHostEntry(Dns.GetHostName());
            foreach (var ip in host.AddressList)
            {
                if (ip.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork)
                {
                    sysIP = ip.ToString();
                    return;
                }
            }

            MessageBox.Show("Network Adapter Error", "Error Starting Application", MessageBoxButtons.OK, MessageBoxIcon.Error);
            Application.Exit();
        }



        void writeInput(Socket listener)
        {
            byte[] buffer = new byte[sizeof(char) * 131 + 1];
            int bytesReceived = listener.Receive(buffer);

            string text = Encoding.UTF8.GetString(buffer, 0, bytesReceived);

            convoTbx.Invoke(() => appendText(text, Color.Blue));

            listener.Shutdown(SocketShutdown.Both);
            listener.Close();
        }

        private async void startBtn_Clicked(object sender, EventArgs e)
        {
            if (ipTbx.Text.Equals(sysIP) && nameTbx.Text.Length > 0)
            {
                // Disable Step 1 Text
                step1Lbl.Enabled = false;
                nameLbl.Enabled = false;
                ipLbl.Enabled = false;

                // Disable Step 1 Buttons
                nameTbx.Enabled = false;
                ipTbx.Enabled = false;
                startBtn.Enabled = false;

                // Enable Messaging
                recipientLbl.Enabled = true;
                recipientTbx.Enabled = true;
                convoTbx.Enabled = true;
                messageTbx.Enabled = true;
                sendBtn.Enabled = true;

                // Start Listening
                IPEndPoint localEndPoint = new IPEndPoint(IPAddress.Any, port);
                try
                {
                    socket.Bind(localEndPoint);
                    appendText("Now Listening\n", Color.Green);
                    socket.Listen(100);
                    while (true)
                    {
                        Socket receiver = await socket.AcceptAsync();
                        writeInput(receiver);
                    }
                }
                catch { }
            }

            else
            {
                MessageBox.Show("The IPv4 Address you entered is Incorrect", "Incorrect Info Entered", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void sendBtn_Click(object sender, EventArgs e)
        {
            try
            {
                Socket writer = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                string text = nameTbx.Text + ": " + messageTbx.Text + '\n';
                byte[] data = Encoding.UTF8.GetBytes(text);
                writer.Connect(new IPEndPoint(IPAddress.Parse(recipientTbx.Text), port));
                writer.Send(data);
                writer.Shutdown(SocketShutdown.Send);
                writer.Close();
                appendText(text, Color.Black);
            }

            catch { }
        }

        private void App_FormClosed(object sender, FormClosedEventArgs e)
        {
            try
            {
                if (!socket.Connected) socket.Shutdown(SocketShutdown.Both);
            }

            catch { }

            socket.Close();
        }
    }
}
