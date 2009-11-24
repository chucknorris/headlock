namespace headlock
{
    using System.IO;
    using System.Security.Cryptography;

    public class Hasher
    {
        public byte[] Hash(byte[] contents)
        {
            byte[] output;
            using (HashAlgorithm hash = new SHA512Managed())
            {
                using (Stream str = new MemoryStream(contents))
                {
                    output = hash.ComputeHash(str);
                }
            }
            return output;
        }
    }
}