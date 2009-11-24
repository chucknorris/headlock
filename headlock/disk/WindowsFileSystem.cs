namespace headlock.disk
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Text;
    using System.Xml.Serialization;
    using headlock_file;
    using model;

    public class WindowsFileSystem :
        FileSystem
    {
        private StringEncoding _encoding;

        public WindowsFileSystem(StringEncoding encoding)
        {
            _encoding = encoding;
        }

        public void AppendToFile(string path, string contents)
        {
            File.AppendAllText(path, contents, _encoding.CurrentEncoding);
        }

        public void AppendToFile(string path, byte[] contents)
        {
            using(var fs = File.OpenWrite(path))
            {
                fs.Position = fs.Length;
                foreach(var b in contents)
                    fs.WriteByte(b);
            }
        }
        
        public void OverwriteFile(string path, string contents)
        {
            File.WriteAllText(path, contents, _encoding.CurrentEncoding);
        }

        public string ReadString(string path)
        {
            return File.ReadAllText(path, _encoding.CurrentEncoding);
        }

        public byte[] ReadBytes(string path)
        {
            return File.ReadAllBytes(path);
        }

        public FileInfo[] GetAllFilesInDir(string path)
        {
            var info = new DirectoryInfo(path);
            return info.GetFiles("*.*", SearchOption.AllDirectories);
        }

        public void WriteEntriesToFile(string location, IList<Entry> entries)
        {
            var d = new Document();
            foreach (var entry in entries)
            {
                d.Entries.Add(new DocumentEntry { File = entry.File, Fingerprint = entry.FingerPrintString });
            }

            XmlSerializer xs = GetSerializer();
            var s = xs.SerializedString(d);
            var p = Path.Combine(location, Constants.Filename);
            File.AppendAllText(p, s);
        }

        public IList<Entry> ReadEntriesFromFile(string location)
        {
            XmlSerializer s = GetSerializer();
            var stream = new MemoryStream(GetHeadlockFileContents(location));
            var doc = (Document)s.Deserialize(stream);

            var entries = new List<Entry>();
            foreach (var entry in doc.Entries)
            {
                entries.Add(new Entry(){File = entry.File});
            }

            return entries;
        }

        public byte[] GetHeadlockFileContents(string path)
        {
            byte[] contents = File.ReadAllBytes(path);
            //subtract the original 88 bytes for the fingerprint and 1 byte for new line
            var result = new byte[contents.Length-89];
            Array.Copy(contents, 0, result, 0, result.Length);

            return result;
        }

        public byte[] GetHeadlockFileFingerprint(string path)
        {
            string fileContents = File.ReadAllText(path);
            int startPoint = fileContents.Length - 88;
            string fingerprint = fileContents.Substring(startPoint, 88);
            byte[] result = fingerprint.FlipToBytes();
            return result; //should be 64
        }

        public byte[] GetHeadlockFileFingerprintOld(string path)
        {
            var fileContents = new List<string>(File.ReadAllLines(path));
            var result = new List<string>();

            var e = fileContents.GetEnumerator();
            while (e.MoveNext())
            {
                if (e.Current.Equals(BEGIN))
                {
                    e.MoveNext();
                    while(!e.Current.Equals(END))
                    {
                        result.Add(e.Current);
                        e.MoveNext();
                    }

                    var sb = new StringBuilder();
                    foreach (var line in result)
                        sb.Append(line+"\r");

                    sb.Length = sb.Length - 1;

                    return _encoding.CurrentEncoding.GetBytes(sb.ToString());
                }
            }

            throw new Exception("didn't find fingerprint");
        }

        private readonly string BEGIN = "-----BEGIN FINGERPRINT-----";
        private readonly string END = "-----END FINGERPRINT-----";
        public void AttachFingerprintToHeadlockFile(string path, byte[] fingerprint)
        {
            AppendToFile(path, "\n");
            AppendToFile(path, fingerprint.FlipToString());
        }

        private XmlSerializer GetSerializer()
        {
            var s = new XmlSerializer(typeof (Document), new[] {typeof (DocumentEntry)});

            return s;
        }
    }
}