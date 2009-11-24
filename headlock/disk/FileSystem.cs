namespace headlock.disk
{
    using System.Collections.Generic;
    using System.IO;
    using model;

    public interface FileSystem
    {
        void AppendToFile(string path, string contents);
        void OverwriteFile(string path, string contents);
        string ReadString(string path);
        byte[] ReadBytes(string path);
        FileInfo[] GetAllFilesInDir(string path);
        void WriteEntriesToFile(string location, IList<Entry> entries);
        IList<Entry> ReadEntriesFromFile(string location);
        byte[] GetHeadlockFileContents(string path);
        byte[] GetHeadlockFileFingerprint(string path);
        void AttachFingerprintToHeadlockFile(string path, byte[] fingerprint);
    }
}