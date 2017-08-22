namespace Nasdan.Core.Senses
{
    public class StringMessage : Message
    {

        public string Msg { get; set; }
        public override string GetMessage()
        {
            return this.Msg;
        }
    }
}