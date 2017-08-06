namespace Nasdan.Core.Senses
{


    public abstract class Message
    {
        public string SessionToken { get; set; }
        public string Sender { get; set; }

        public string DateTime { get; set; }

        public abstract string GetMessage();

    }
}