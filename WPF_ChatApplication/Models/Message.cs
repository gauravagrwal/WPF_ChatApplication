using System;
using System.Collections.Generic;

namespace WPF_ChatApplication.Models
{
    public class Message
    {
        public string Username { get; set; }
        public string Msg { get; set; }
        public DateTime Sent { get; set; }
        public FormUrlEncodedContent serializedMessage { get; set; }

        public Message(MessageReceiver m)
        {
            Username = m.Username;
            Msg = m.Msg;
            Sent = DateTime.Now;
        }

        public Message(string username, string msg)
        {
            Username = username;
            Msg = msg;
            Sent = DateTime.Now;

            var dict = new Dictionary<string, string>();
            dict.Add("Text", msg);
            dict.Add("Username", username);
            dict.Add("Sent", DateTime.Now.ToString());

            serializedMessage = new FormUrlEncodedContent(dict);
        }
    }
}
