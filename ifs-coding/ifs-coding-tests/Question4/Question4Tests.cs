using System.Collections.Generic;
using ifs_coding.Question4.Mapping;
using ifs_coding.Question4.Models;
using ifs_coding.Shared;
using Moq;
using Xunit;
using Assert = Xunit.Assert;

namespace ifs_coding_tests.Question4
{
    public class Question4Tests
    {
        private const string DummyFile = "dummy-file.txt";

        private readonly List<string> _testCircuit = new List<string>
        {
            "123 -> x",
            "456 -> y",
            "x AND y -> d",
            "x OR y -> e",
            "x LSHIFT 2 -> f",
            "y RSHIFT 2 -> g",
            "NOT x -> h",
            "NOT y -> i"
        };
        
        private readonly Mock<IFileReader> _fileReaderMock;
        private readonly IMapper<string, Instruction> _mapper;

        public Question4Tests()
        {
            _fileReaderMock = new Mock<IFileReader>();
            _mapper = new InstructionMapper();
        }
        
        [Fact]
        private void CalculateWireResult_ReturnsExpectedResult()
        {
            _fileReaderMock.Setup(x => x
                    .ReadMultiLineFile(DummyFile))
                .Returns(_testCircuit);
            var sut = new ifs_coding.Question4.Question4(_fileReaderMock.Object, _mapper);
        
            var actualResult = sut.CalculateWireResult(DummyFile);
        
            Assert.Equal(72, actualResult["d"]);
            Assert.Equal(507, actualResult["e"]);
            Assert.Equal(492, actualResult["f"]);
            Assert.Equal(114, actualResult["g"]);
            Assert.Equal(65412, actualResult["h"]);
            Assert.Equal(65079, actualResult["i"]);
            Assert.Equal(123, actualResult["x"]);
            Assert.Equal(456, actualResult["y"]);
        }

        [Xunit.Theory]
        [InlineData("123 -> x", "x", 123)]
        [InlineData("456 -> y", "y", 456)]
        private void CalculateWireResult_HandlesNumericAssignAsExpected(string instruction, string wireLetter, ushort expectedResult)
        {
            _fileReaderMock.Setup(x => x
                    .ReadMultiLineFile(DummyFile))
                .Returns(new List<string>{ instruction });
            var sut = new ifs_coding.Question4.Question4(_fileReaderMock.Object, _mapper);

            var actualResult = sut.CalculateWireResult(DummyFile);
            
            Assert.Equal(expectedResult, actualResult[wireLetter]);
        }
        
        [Fact]
        private void CalculateWireResult_HandlesNumericAssignOverwriteAsExpected()
        {
            const string instruction = "-> y";
            const int firstValue = 456;
            const int secondValue = 900;
            
            _fileReaderMock.Setup(x => x
                    .ReadMultiLineFile(DummyFile))
                .Returns(new List<string>{ $"{firstValue} {instruction}", $"{secondValue} {instruction}"  });
            var sut = new ifs_coding.Question4.Question4(_fileReaderMock.Object, _mapper);

            var actualResult = sut.CalculateWireResult(DummyFile);
            
            Assert.Equal(secondValue, actualResult["y"]);
        }
        
        [Fact]
        private void CalculateWireResult_HandlesWireValueAssignmentAsExpected()
        {
            var instructions = new List<string>
            {
                "453 -> x",
                "x -> y"
            };
            
            _fileReaderMock.Setup(x => x
                    .ReadMultiLineFile(DummyFile))
                .Returns(instructions);
            var sut = new ifs_coding.Question4.Question4(_fileReaderMock.Object, _mapper);

            var actualResult = sut.CalculateWireResult(DummyFile);
            
            Assert.Equal(453, actualResult["x"]);
            Assert.Equal(453, actualResult["y"]);
        }
        
        [Fact]
        private void CalculateWireResult_HandlesWireValueAndAsExpected()
        {
            var instructions = new List<string>
            {
                "453 -> x",
                "999 -> y",
                "x AND y -> z"
            };
            
            _fileReaderMock.Setup(x => x
                    .ReadMultiLineFile(DummyFile))
                .Returns(instructions);
            var sut = new ifs_coding.Question4.Question4(_fileReaderMock.Object, _mapper);

            var actualResult = sut.CalculateWireResult(DummyFile);
            
            Assert.Equal(453, actualResult["x"]);
            Assert.Equal(999, actualResult["y"]);
            Assert.Equal(453 & 999, actualResult["z"]);
        }
        
        [Fact]
        private void CalculateWireResult_HandlesWireValueOrAsExpected()
        {
            var instructions = new List<string>
            {
                "453 -> x",
                "999 -> y",
                "x OR y -> z"
            };
            
            _fileReaderMock.Setup(x => x
                    .ReadMultiLineFile(DummyFile))
                .Returns(instructions);
            var sut = new ifs_coding.Question4.Question4(_fileReaderMock.Object, _mapper);

            var actualResult = sut.CalculateWireResult(DummyFile);
            
            Assert.Equal(453, actualResult["x"]);
            Assert.Equal(999, actualResult["y"]);
            Assert.Equal(453 | 999, actualResult["z"]);
        }
        
        [Fact]
        private void CalculateWireResult_HandlesWireValueNotAsExpected()
        {
            var instructions = new List<string>
            {
                "453 -> x",
                "999 -> y",
                "NOT y -> z"
            };
            
            _fileReaderMock.Setup(x => x
                    .ReadMultiLineFile(DummyFile))
                .Returns(instructions);
            var sut = new ifs_coding.Question4.Question4(_fileReaderMock.Object, _mapper);

            var actualResult = sut.CalculateWireResult(DummyFile);
            
            Assert.Equal(453, actualResult["x"]);
            Assert.Equal(999, actualResult["y"]);
            Assert.Equal(unchecked((ushort)~999), actualResult["z"]);
        }
        
        [Fact]
        private void CalculateWireResult_HandlesWireValueLShiftAsExpected()
        {
            var instructions = new List<string>
            {
                "453 -> x",
                "999 -> y",
                "y LSHIFT 3 -> z"
            };
            
            _fileReaderMock.Setup(x => x
                    .ReadMultiLineFile(DummyFile))
                .Returns(instructions);
            var sut = new ifs_coding.Question4.Question4(_fileReaderMock.Object, _mapper);

            var actualResult = sut.CalculateWireResult(DummyFile);
            
            Assert.Equal(453, actualResult["x"]);
            Assert.Equal(999, actualResult["y"]);
            Assert.Equal(999 << 3, actualResult["z"]);
        }
        
        [Fact]
        private void CalculateWireResult_HandlesWireValueRShiftAsExpected()
        {
            var instructions = new List<string>
            {
                "453 -> x",
                "999 -> y",
                "y RSHIFT 3 -> z"
            };
            
            _fileReaderMock.Setup(x => x
                    .ReadMultiLineFile(DummyFile))
                .Returns(instructions);
            var sut = new ifs_coding.Question4.Question4(_fileReaderMock.Object, _mapper);

            var actualResult = sut.CalculateWireResult(DummyFile);
            
            Assert.Equal(453, actualResult["x"]);
            Assert.Equal(999, actualResult["y"]);
            Assert.Equal(999 >> 3, actualResult["z"]);
        }
    }
}
