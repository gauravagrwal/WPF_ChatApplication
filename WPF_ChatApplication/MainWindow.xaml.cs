using System;
using System.Windows;
using System.Windows.Input;
using System.Windows.Threading;

namespace WPF_ChatApplication
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private ChatProxy _cp { get; set; }
        public MainWindow()
        {
            InitializeComponent();
        }

        private void sendMessage(Message m)
        {
            _cp.SendMessage(m);
            txt_msginput.Clear();
        }

        private void txt_msginput_TextChanged(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                if (_cp != null)
                {
                    if (!string.IsNullOrEmpty(txt_username.Text) && !string.IsNullOrEmpty(txt_msginput.Text))
                        sendMessage(new Message(txt_username.Text, txt_msginput.Text));
                    else
                        Status("Nothing to send.");
                }
                else
                    Status("Chat not started.");
            }
        }

        private void btn_SendMsg_Click(object sender, RoutedEventArgs e)
        {
            if (_cp != null)
            {
                if (!string.IsNullOrEmpty(txt_username.Text) && !string.IsNullOrEmpty(txt_msginput.Text))
                    sendMessage(new Message(txt_username.Text, txt_msginput.Text));
                else
                    Status("Nothing to send.");
            }
            else
                Status("Chat not started.");
        }

        public void ShowMessage(Message m)
        {
            chats.Dispatcher.Invoke(
                new Action(delegate ()
                {
                    chats.Text += ("[" + m.Sent + "] " + m.Username + ": " + m.Msg);
                    chats.Text += Environment.NewLine;
                    chats.ScrollToEnd();
                }),
                DispatcherPriority.Normal);
        }

        public void Status(string str)
        {
            chats.Dispatcher.Invoke(
                new Action(delegate () { MessageBox.Show(str); }),
                DispatcherPriority.Normal);
        }

        private void btn_StartChat_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(txt_sendersPort.Text) && !string.IsNullOrEmpty(txt_receiverAddress.Text))
            {
                _cp = new ChatProxy(this.ShowMessage, this.Status, txt_sendersPort.Text, txt_receiverAddress.Text);
                if (_cp.Status)
                {
                    chats.Text += "Connection Initiated...";
                    chats.Text += Environment.NewLine;
                }
                else
                    Status("Please provide details");
            }
        }
    }
}
