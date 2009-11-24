namespace headlock.model
{
    using System.IO;

    public class ExecutionPoint
    {
        public ExecutionPoint(string path)
        {
            PathToPutInAHeadlock = path;
            PathToTheHeadlockFile = Path.Combine(path, Constants.Filename);
        }

        public string PathToPutInAHeadlock { get; private set; }
        public string PathToTheHeadlockFile { get; private set; }
    }
}