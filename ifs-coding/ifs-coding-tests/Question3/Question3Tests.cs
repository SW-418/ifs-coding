using System.Collections.Generic;
using ifs_coding.Shared;
using Moq;
using Xunit;

namespace ifs_coding_tests.Question3
{
    public class Question3Tests
    {
        private const string DUMMY_FILE = "dummy-file.txt";
        private readonly Mock<IFileReader> _fileReaderMock;

        public Question3Tests()
        {
            _fileReaderMock = new Mock<IFileReader>();
        }

        [Fact]
        public void FindGoodStrings_EmptyInput_Returns0()
        {
            SetupFileReaderMock(new List<string>());
            var sut = new ifs_coding.Question3.Question3(_fileReaderMock.Object);
            
            var result = sut.FindGoodStrings(DUMMY_FILE);
            
            Assert.Equal(0, result);
        }
        
        [Fact]
        public void FindGoodStrings_InputWithoutVowels_ReturnsExpectedResult()
        {
            var input = new List<string>
            {
                "bcd",
                "aac",
                "iif"
            };
            SetupFileReaderMock(input);
            var sut = new ifs_coding.Question3.Question3(_fileReaderMock.Object);
            
            var result = sut.FindGoodStrings(DUMMY_FILE);
            
            Assert.Equal(0, result);
        }
        
        [Fact]
        public void FindGoodStrings_InputWithVowels_ReturnsExpectedResult()
        {
            var input = new List<string>
            {
                "aaacc",
                "adioucc",
                "aeioucc",
                "aaeeiioouu"
            };
            SetupFileReaderMock(input);
            var sut = new ifs_coding.Question3.Question3(_fileReaderMock.Object);
            
            var result = sut.FindGoodStrings(DUMMY_FILE);
            
            Assert.Equal(input.Count, result);
        }
        
        [Fact]
        public void FindGoodStrings_InputWithoutRepeatingChars_ReturnsExpectedResult()
        {
            var input = new List<string>
            {
                "aei",
                "adiou",
                "aeic",
                "aeiou"
            };
            SetupFileReaderMock(input);
            var sut = new ifs_coding.Question3.Question3(_fileReaderMock.Object);
            
            var result = sut.FindGoodStrings(DUMMY_FILE);
            
            Assert.Equal(0, result);
        }
        
        [Fact]
        public void FindGoodStrings_InputWithRepeatingChars_ReturnsExpectedResult()
        {
            var input = new List<string>
            {
                "aaa",
                "adiouu",
                "aeicc",
                "aaeeiioouu"
            };
            SetupFileReaderMock(input);
            var sut = new ifs_coding.Question3.Question3(_fileReaderMock.Object);
            
            var result = sut.FindGoodStrings(DUMMY_FILE);
            
            Assert.Equal(input.Count, result);
        }
        
        [Fact]
        public void FindGoodStrings_InputWithDisallowedStrings_ReturnsExpectedResult()
        {
            var input = new List<string>
            {
                "aaaab",
                "abcdiouucd",
                "aeiccpq",
                "aaeeiioouuxy"
            };
            SetupFileReaderMock(input);
            var sut = new ifs_coding.Question3.Question3(_fileReaderMock.Object);
            
            var result = sut.FindGoodStrings(DUMMY_FILE);
            
            Assert.Equal(0, result);
        }

        private void SetupFileReaderMock(IEnumerable<string> expectedResponse)
        {
            _fileReaderMock.Setup(x => x
                    .ReadMultiLineFile(DUMMY_FILE))
                .Returns(expectedResponse);
        }
    }
}
