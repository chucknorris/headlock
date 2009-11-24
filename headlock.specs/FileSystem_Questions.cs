namespace headlock.specs
{
    using System.IO;
    using System.Text;
    using MbUnit.Framework;

    [TestFixture]
    public class FileSystem_Questions
    {
        [Test]
        public void How_does_the_file_system_Save()
        {
            File.Delete(@".\test.txt");
            File.AppendAllText(@".\test.txt", "\n", Encoding.UTF8);
            File.AppendAllText(@".\test.txt", "dru", Encoding.UTF8);
            File.AppendAllText(@".\test.txt", "\n", Encoding.UTF8);

            var lines = File.ReadAllLines(@".\test.txt");
            Assert.AreEqual(lines.Length, 2);

        }
    }
}