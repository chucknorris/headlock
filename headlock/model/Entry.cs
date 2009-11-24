namespace headlock.model
{
    public class Entry
    {
        public string File { get; set; }
        public byte[] FingerPrint { get; set; }
        public string FingerPrintString
        {
            get
            {
                return FingerPrint.FlipToString();
            }
        }
    }
}