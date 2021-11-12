namespace ifs_coding.Shared
{
    public class FileReader : IFileReader
    {
        public string ReadSingleLineFile(string fileName)
        {
            return System.IO.File.ReadAllText(fileName);
        }
    }
}