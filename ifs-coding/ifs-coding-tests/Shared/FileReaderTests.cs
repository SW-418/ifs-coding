using System.IO;
using ifs_coding.Shared;
using Xunit;

namespace ifs_coding_tests.Shared
{
    public class FileReaderTests
    {
        private readonly IFileReader _fileReader;

        public FileReaderTests()
        {
            _fileReader = new FileReader();
        }
        
        [Fact]
        public void ReadSingleLineFile_ShouldThrowFileNotFoundException_WhenFileDoesntExist()
        {
            const string invalidFile = "not-real.txt";
            Assert.Throws<FileNotFoundException>(() => _fileReader.ReadSingleLineFile(invalidFile));
        }
        
        [Fact]
        public void ReadSingleLineFile_ReturnsExpectedString_WhenFileExists()
        {
            const string validFile = "TestData/reader-test-input.txt";
            const string expectedResult = "itsonlyatestrelax!";
            
            var actualResult = _fileReader.ReadSingleLineFile(validFile);
            
            Assert.Equal(expectedResult, actualResult);
        }
    }
}