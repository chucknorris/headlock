namespace headlock
{
    using System;
    using System.IO;
    using System.Text;
    using System.Xml.Serialization;

    public static class Extensions
    {
        public static string SerializedString(this XmlSerializer serializer, object o)
        {
            var sb = new StringBuilder();
            var tw = new StringWriter(sb);
            serializer.Serialize(tw,o);

            return sb.ToString();
        }

        public static byte[] FlipToBytes(this string input)
        {
            return Convert.FromBase64String(input);
        }

        public static string FlipToString(this byte[] input)
        {
            return Convert.ToBase64String(input);
        }

        public static byte[] GetFileContents(this string path)
        {
            if(!File.Exists(path))
                throw new Exception("can't find file");
            

            return File.ReadAllBytes(path);
        }
    }
}