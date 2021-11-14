using System.Collections.Generic;
using System.Linq;

namespace ifs_coding.Shared
{
    public class FileReader : IFileReader
    {
        public string ReadSingleLineFile(string fileName)
        {
            return System.IO.File.ReadAllText(fileName);
        }

        public IEnumerable<string> ReadMultiLineFile(string fileName)
        {
            return System.IO.File.ReadAllLines(fileName).ToList();
        }
    }
}
