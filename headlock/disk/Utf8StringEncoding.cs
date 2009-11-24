namespace headlock.disk
{
    using System.Text;

    public class Utf8StringEncoding :
        StringEncoding
    {
        private readonly Encoding _encoding = Encoding.UTF8;

        public Encoding CurrentEncoding
        {
            get { return _encoding; }
        }
    }
}