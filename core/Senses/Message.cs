using System;

namespace Nasdan.Core.Senses
{


    public abstract class Message
    {
        public Message()
        {
            this.Guid =  Guid.NewGuid();
            this.SessionToken = string.Empty;
            this.Sender = string.Empty;
            this.DateTime = string.Empty;
        }
         public Guid Guid { get; set; }
        public string SessionToken { get; set; }
        public string Sender { get; set; }

        public string DateTime { get; set; }

        public abstract string GetMessage();

    }
}