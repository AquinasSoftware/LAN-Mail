using System;
using System.Net;
using System.Net.Sockets;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace LAN_Mail
{
    public partial class App : Form
    {

        string sysIP = "";
        const int port = 1440;
        Socket listener = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        Socket active = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        bool connecting;


        public App()
        {
            InitializeComponent();
        }

        private void appendText(string text, Color color)
        {
            convoTbx.Invoke(() =>
            {
                convoTbx.SelectionStart = convoTbx.TextLength;
                convoTbx.SelectionLength = 0;
                convoTbx.SelectionColor = color;
                convoTbx.AppendText(text);
            });
        }

        private void App_Load(object sender, EventArgs e)
        {
            this.FormBorderStyle = FormBorderStyle.FixedSingle; // Or Fixed3D, FixedDialog
            this.MaximizeBox = false;
            connecting = false;
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
            System.Windows.Forms.Application.Exit();
        }



        bool writeInput()
        {
            try
            {
                byte[] buffer = new byte[sizeof(char) * 131 + 1];
                int bytesReceived = active.Receive(buffer);

                if (bytesReceived > 0)
                {
                    string text = Encoding.UTF8.GetString(buffer, 0, bytesReceived);

                    appendText(text, Color.Blue);
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch { return false; }
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
                connectBtn.Enabled = true;
                convoTbx.Enabled = true;

                // Start Listening
                IPEndPoint localEndPoint = new IPEndPoint(IPAddress.Any, port);
                try
                {
                    listener.Bind(localEndPoint);
                    appendText("Now Listening\n", Color.Green);
                    listener.Listen(100);

                    while (true)
                    {
                            Socket incoming = await listener.AcceptAsync();
                            if (incoming.RemoteEndPoint != null && !active.Connected && !connecting)
                            {
                                active = incoming;
                                this.Invoke(() =>{
                                    recipientTbx.Text = ((IPEndPoint)active.RemoteEndPoint).Address.ToString();
                                    on_Connected();
                                });
                            }
                            else
                            {
                                try
                                {
                                    incoming.Shutdown(SocketShutdown.Both);
                                }
                                catch { }
                                incoming.Close();
                            }
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
                string text = nameTbx.Text + ": " + messageTbx.Text + '\n';
                byte[] data = Encoding.UTF8.GetBytes(text);
                active.Send(data);

                messageTbx.Clear();
                appendText(text, Color.Black);
            }

            catch { }
        }

        private void App_FormClosed(object sender, FormClosedEventArgs e)
        {
            try
            {
                if (!listener.Connected) listener.Shutdown(SocketShutdown.Both);
                listener.Close();

                active.Shutdown(SocketShutdown.Both);
                active.Close();
            }

            catch { }

            
        }

        private void connectBtn_Click(object sender, EventArgs e)
        {
            if (!active.Connected) // Attempt Connection
            {
                connecting = true;
                try
                {
                    active = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                    active.Connect(new IPEndPoint(IPAddress.Parse(recipientTbx.Text), port));
                    connecting = false;
                    on_Connected();
                }
                catch
                {
                    appendText("Connection Failed\nMake sure you entered the address correctly, the other person is listening and is not connected to someone else\n", Color.Purple);
                }
                finally
                {
                    connecting = false;
                }
            }
            else // Disconnect
            {
                on_Disconnected();
            }
        }

        private async void on_Connected()
        {   
            messageTbx.Enabled = true;
            sendBtn.Enabled = true;
            recipientTbx.Enabled = false;
            connectBtn.Text = "Disconnect";
            appendText("Connection Established\n", Color.Green);

            while (await Task.Run(() => writeInput()))
            {
                // Loop
            }

            on_Disconnected();
        }

        private void on_Disconnected()
        {
            if (!active.Connected && connectBtn.Text == "Connect")
            {
                return;
            }

            appendText("Connection Closed\n", Color.Green);

            // Flush Socket
            try{
                active.Shutdown(SocketShutdown.Both);
                active.Close();
            }
            catch { }
            active = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);


            messageTbx.Enabled = false;
            sendBtn.Enabled = false;
            recipientTbx.Enabled = true;
            connectBtn.Text = "Connect";

        }
    }
}
