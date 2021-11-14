using System.Collections.Generic;
using System.IO;
using System.Linq;
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
        
        [Fact]
        public void ReadMultiLineFile_ShouldThrowFileNotFoundException_WhenFileDoesntExist()
        {
            const string invalidFile = "not-real.txt";
            Assert.Throws<FileNotFoundException>(() => _fileReader.ReadMultiLineFile(invalidFile));
        }
        
        [Fact]
        public void ReadMultiLineFile_ReturnsExpectedString_WhenFileExists()
        {
            const string validFile = "TestData/multiline-reader-test-input.txt";
            var expectedResult = new List<string>{ "hello", "hey", "hi" };
            
            var actualResult = _fileReader.ReadMultiLineFile(validFile).ToArray();
            
            Assert.Collection(expectedResult, 
                item => Assert.Equal(item, actualResult[0]),
                item => Assert.Equal(item, actualResult[1]),
                item => Assert.Equal(item, actualResult[2]));
        }
    }
}
