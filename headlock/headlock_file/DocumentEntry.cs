namespace headlock.headlock_file
{
    using System;

    [Serializable]
    public class DocumentEntry
    {
        public string File { get; set; }
        //todo: can this be a byte array?
        public string Fingerprint { get; set; }
    }
}