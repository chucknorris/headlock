namespace headlock.disk
{
    using System.Text;

    public interface StringEncoding
    {
        Encoding CurrentEncoding { get; }
    }
}