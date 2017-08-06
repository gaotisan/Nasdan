namespace Nasdan.Core.Senses
{
    public class ImageMessage : Message
    {

        public string Url { get; set; }

        public override string GetMessage()
        {
            return this.Url;
        }
    }
}