using System.Collections.Generic;

namespace ifs_coding.Shared
{
    public interface IFileReader
    {
        string ReadSingleLineFile(string fileName);
        IEnumerable<string> ReadMultiLineFile(string fileName);
    }
}
