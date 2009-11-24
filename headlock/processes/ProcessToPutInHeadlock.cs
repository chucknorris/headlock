namespace headlock.processes
{
    using System.Collections.Generic;
    using disk;
    using model;

    public class ProcessToPutInHeadlock :
        Process
    {
        static FileSystem _fileSystem = new WindowsFileSystem(new Utf8StringEncoding());
        static Hasher _hasher = new Hasher();

        public void Go(ExecutionPoint point)
        {
            var files = _fileSystem.GetAllFilesInDir(point.PathToPutInAHeadlock);
            var entries = new List<Entry>();

            foreach (var file in files)
            {
                var e = new Entry {File = file.Name, FingerPrint = _hasher.Hash(_fileSystem.ReadBytes(file.FullName))};
                entries.Add(e);
            }

            _fileSystem.WriteEntriesToFile(point.PathToPutInAHeadlock, entries);
            _fileSystem.AttachFingerprintToHeadlockFile(point.PathToTheHeadlockFile, _hasher.Hash(_fileSystem.ReadBytes(point.PathToTheHeadlockFile)));
        }
    }
}