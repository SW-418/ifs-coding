using System;
using ifs_coding.Shared;
using Moq;
using Xunit;

namespace ifs_coding_tests.Question2
{
    public class Question2Tests
    {
        private const string DUMMY_FILE = "dummy-file.txt";
        private readonly Mock<IFileReader> _fileReaderMock;

        public Question2Tests()
        {
            _fileReaderMock = new Mock<IFileReader>();
        }

        [Fact]
        public void CalculateTotalUniqueVisits_Returns1_WhenFileStringEmpty()
        {
            _fileReaderMock.Setup(reader => reader
                    .ReadSingleLineFile(DUMMY_FILE))
                .Returns("");
            
            var sut = new ifs_coding.Question2.Question2(_fileReaderMock.Object);
            var result = sut.CalculateTotalUniqueVisits(DUMMY_FILE);
            
            Assert.Equal(1, result);
        }
        
        [Fact]
        public void CalculateTotalUniqueVisits_ThrowsArgumentException_WhenFileStringContainsUnsupportedChar()
        {
            _fileReaderMock.Setup(reader => reader
                    .ReadSingleLineFile(DUMMY_FILE))
                .Returns("^v////");
            
            var sut = new ifs_coding.Question2.Question2(_fileReaderMock.Object);

            Assert.Throws<ArgumentException>(() => sut.CalculateTotalUniqueVisits(DUMMY_FILE));
        }
        
        [Theory]
        [InlineData(">", 2)]
        [InlineData("^>v<", 4)]
        [InlineData("^v^v^v", 2)]
        [InlineData("^^^^^^", 7)]
        [InlineData("^vvv", 4)]
        [InlineData(">><>>", 4)]
        public void CalculateTotalUniqueVisits_ReturnsExpectedResult_WhenInputValid(string input, int expectedResult)
        {
            _fileReaderMock.Setup(reader => reader
                    .ReadSingleLineFile(DUMMY_FILE))
                .Returns(input);

            var sut = new ifs_coding.Question2.Question2(_fileReaderMock.Object);
            var result = sut.CalculateTotalUniqueVisits(DUMMY_FILE);

            Assert.Equal(expectedResult, result);
        }
    }
}