using System.Collections.Generic;
using ifs_coding.Shared;
using Moq;
using Xunit;

namespace ifs_coding_tests.Question5
{
    public class Question5Tests
    {
        private const string DummyFile = "dummy-file.txt";
        
        private readonly Mock<IFileReader> _fileReaderMock;

        public Question5Tests()
        {
            _fileReaderMock = new Mock<IFileReader>();
        }

        [Theory]
        [InlineData("R75,D30,R83,U83,L12,D49,R71,U7,L72", "U62,R66,U55,R34,D71,R55,D58,R83", 159)]
        [InlineData("R98,U47,R26,D63,R33,U87,L62,D20,R33,U53,R51", "U98,R91,D20,R16,D67,R40,U7,R15,U6,R7", 135)]
        public void CalculateClosestIntersection_ReturnsExpectedResult(string line1, string line2, int expectedResult)
        {
            var input = new List<string> { line1, line2 };
            _fileReaderMock.Setup(x => x
                    .ReadMultiLineFile(DummyFile))
                .Returns(input);
            var sut = new ifs_coding.Question5.Question5(_fileReaderMock.Object);

            var actualResult = sut.CalculateClosestIntersection(DummyFile);
            
            Assert.Equal(expectedResult, actualResult);
        }
    }
}
