namespace Nasdan.Core.Senses
{


    public abstract class Message
    {
        public Message()
        {
            this.SessionToken = string.Empty;
            this.Sender = string.Empty;
            this.DateTime = string.Empty;
        }
        public string SessionToken { get; set; }
        public string Sender { get; set; }

        public string DateTime { get; set; }

        public abstract string GetMessage();

    }
}