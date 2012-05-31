using NUnit.Framework;

namespace headlock.specs
{
    using model;
    using processes;

    [TestFixture]
    public class IntegrationTests
    {
        [Test]
        public void Sign()
        {
            var p = new ProcessToPutInHeadlock();
            var ep = new ExecutionPoint(@".\test_folder");
            p.Go(ep);
            //assert file exists
        }

        [Test]
        public void Verify()
        {
            var p = new ProcessToVerifyHeadlock();
            var ep = new ExecutionPoint(@".\test_folder");
            //sign it?
            //run process
            p.Go(ep);
        }
    }
}