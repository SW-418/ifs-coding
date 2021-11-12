using System;
using ifs_coding.Shared;
using Moq;
using Xunit;

namespace ifs_coding_tests.Question1
{
    public class Question1Tests
    {
        private const string DUMMY_FILE = "dummy-file.txt";
        private readonly Mock<IFileReader> _fileReaderMock;

        public Question1Tests()
        {
            _fileReaderMock = new Mock<IFileReader>();
        }

        [Fact]
        public void FindFloor_Returns0_WhenFileStringEmpty()
        {
            _fileReaderMock.Setup(reader => reader
                .ReadSingleLineFile(DUMMY_FILE))
                .Returns("");
            
            var sut = new ifs_coding.Question1.Question1(_fileReaderMock.Object);
            var result = sut.FindFloor(DUMMY_FILE);
            
            Assert.Equal(0, result);
        }
        
        [Fact]
        public void FindFloor_ThrowsArgumentException_WhenFileStringContainsUnsupportedChar()
        {
            _fileReaderMock.Setup(reader => reader
                .ReadSingleLineFile(DUMMY_FILE))
                .Returns("((z+");
            
            var sut = new ifs_coding.Question1.Question1(_fileReaderMock.Object);

            Assert.Throws<ArgumentException>(() => sut.FindFloor(DUMMY_FILE));
        }

        [Theory]
        [InlineData("(())", 0)]
        [InlineData("()()", 0)]
        [InlineData("(((", 3)]
        [InlineData("(()(()(", 3)]
        [InlineData("))(((((", 3)]
        [InlineData("())", -1)]
        [InlineData("))(", -1)]
        [InlineData(")))", -3)]
        [InlineData(")())())", -3)]
        public void FindFloor_ReturnsExpectedResult_WhenInputValid(string input, int expectedResult)
        {
            _fileReaderMock.Setup(reader => reader
                    .ReadSingleLineFile(DUMMY_FILE))
                .Returns(input);

            var sut = new ifs_coding.Question1.Question1(_fileReaderMock.Object);
            var result = sut.FindFloor(DUMMY_FILE);

            Assert.Equal(expectedResult, result);
        }
    }
}