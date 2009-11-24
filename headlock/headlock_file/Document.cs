namespace headlock.headlock_file
{
    using System;
    using System.Collections.Generic;
    using System.Xml.Serialization;

    [Serializable]
    [XmlRoot("headlock")]
    public class Document
    {
        public Document()
        {
            Entries = new List<DocumentEntry>();
        }

        [XmlElement("entries")]
        public List<DocumentEntry> Entries { get; set; }

        [XmlElement("fingerprint")]
        public string Fingerprint { get; set; }
    }
}