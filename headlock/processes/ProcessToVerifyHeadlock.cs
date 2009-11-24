namespace headlock.processes
{
    using System;
    using System.IO;
    using disk;
    using model;

    public class ProcessToVerifyHeadlock :
        Process
    {
        private readonly FileSystem _fileSystem = new WindowsFileSystem(new Utf8StringEncoding());
        private readonly Hasher _hasher = new Hasher();

        public void Go(ExecutionPoint point)
        {
            var p = point.PathToTheHeadlockFile;

            VerifyFile(p);
            VerifyEntries(new FileInfo(p));
        }

        public void VerifyFile(string path)
        {
            var fi = new FileInfo(path);
            var bytes = _fileSystem.GetHeadlockFileContents(fi.FullName);

            var currentFingerprint = _hasher.Hash(bytes);
            var previousFingerprint = _fileSystem.GetHeadlockFileFingerprint(fi.FullName);

            if (CompareByteArrays(previousFingerprint, currentFingerprint))
                Console.WriteLine("File is Valid");


        }

        public void VerifyEntries(FileInfo fi)
        {
            var entries = _fileSystem.ReadEntriesFromFile(fi.FullName);
            foreach (var entry in entries)
            {
                var bytes = _fileSystem.ReadBytes(entry.File);
                var currentFingerprint = _hasher.Hash(bytes);
                var pastFingerprint = entry.FingerPrint;

                var result = CompareByteArrays(currentFingerprint, pastFingerprint);

                if(result) Console.WriteLine("{0} '{1}'", entry.File, "Is Good");
            }
            
        }

        public bool CompareByteArrays(byte[] left, byte[] right)
        {
            bool match = true;
            for (int i = 0; i < left.Length - 1; i++)
            {
                if (left[i] != right[i])
                {
                    match = false;
                    break;
                }
            }
            return match;
        }
    }
}