using System.Collections.Generic;
using System.Linq;
using ifs_coding.Question4.Mapping;
using ifs_coding.Question4.Models;
using Xunit;
using Assert = Xunit.Assert;

namespace ifs_coding_tests.Question4.Mapping
{
    public class InstructionMapperTests
    {
        [Theory]
        [InlineData("NOT dq -> dr", Operation.Not, "dq", "dr")]
        [InlineData("NOT jy -> jz", Operation.Not, "jy", "jz")]
        [InlineData("NOT ft -> fu", Operation.Not, "ft", "fu")]
        public void Map_IndividualNot_MappedToInstructionAsExpected(string instruction, Operation operation,
            string input, string output)
        {
            var sut = new InstructionMapper();

            var expectedResult = new Instruction(operation, new List<string>{ input }, output);
            var actualResult = sut.Map(instruction);
            
            ValidateInstruction(expectedResult, actualResult);
        }
        
        [Theory]
        [InlineData("kg OR kf -> kh", Operation.Or, "kg", "kf", "kh")]
        [InlineData("ep OR eo -> eq", Operation.Or, "ep", "eo", "eq")]
        [InlineData("ld OR le -> lf", Operation.Or, "ld", "le", "lf")]
        public void Map_IndividualOr_MappedToInstructionAsExpected(string instruction, Operation operation,
            string input, string secondInput, string output)
        {
            var sut = new InstructionMapper();

            var expectedResult = new Instruction(operation, new List<string>{ input, secondInput }, output);
            var actualResult = sut.Map(instruction);
            
            ValidateInstruction(expectedResult, actualResult);
        }
        
        [Theory]
        [InlineData("gn AND gp -> gq", Operation.And, "gn", "gp", "gq")]
        [InlineData("cq AND cs -> ct", Operation.And, "cq", "cs", "ct")]
        [InlineData("kl AND kr -> kt", Operation.And, "kl", "kr", "kt")]
        public void Map_IndividualAnd_MappedToInstructionAsExpected(string instruction, Operation operation,
            string input, string secondInput, string output)
        {
            var sut = new InstructionMapper();

            var expectedResult = new Instruction(operation, new List<string>{ input, secondInput }, output);
            var actualResult = sut.Map(instruction);
            
            ValidateInstruction(expectedResult, actualResult);
        }
        
        [Theory]
        [InlineData("44430 -> b", Operation.Assign, "44430", "b")]
        [InlineData("lx -> a", Operation.Assign, "lx", "a")]
        public void Map_IndividualAssignment_MappedToInstructionAsExpected(string instruction, Operation operation,
            string input, string output)
        {
            var sut = new InstructionMapper();

            var expectedResult = new Instruction(operation, new List<string>{ input }, output);
            var actualResult = sut.Map(instruction);
            
            ValidateInstruction(expectedResult, actualResult);
        }
        
        [Theory]
        [InlineData("ap LSHIFT 1 -> bj", Operation.LShift, "ap", "1", "bj")]
        [InlineData("u LSHIFT 1 -> ao", Operation.LShift, "u", "1", "ao")]
        [InlineData("b LSHIFT 5 -> f", Operation.LShift, "b", "5", "f")]
        public void Map_IndividualLeftShift_MappedToInstructionAsExpected(string instruction, Operation operation,
            string input, string secondInput, string output)
        {
            var sut = new InstructionMapper();

            var expectedResult = new Instruction(operation, new List<string>{ input, secondInput }, output);
            var actualResult = sut.Map(instruction);
            
            ValidateInstruction(expectedResult, actualResult);
        }
        
        [Theory]
        [InlineData("hz RSHIFT 1 -> is", Operation.RShift, "hz", "1", "is")]
        [InlineData("as RSHIFT 1 -> bl", Operation.RShift, "as", "1", "bl")]
        [InlineData("b RSHIFT 5 -> f", Operation.RShift, "b", "5", "f")]
        public void Map_IndividualRightShift_MappedToInstructionAsExpected(string instruction, Operation operation,
            string input, string secondInput, string output)
        {
            var sut = new InstructionMapper();

            var expectedResult = new Instruction(operation, new List<string>{ input, secondInput }, output);
            var actualResult = sut.Map(instruction);
            
            ValidateInstruction(expectedResult, actualResult);
        }

        [Fact]
        public void Map_MultipleInstructions_ReturnsExpectedInstructionsCount()
        {
            var stringInstructions = new List<string>
            {
                "hz RSHIFT 1 -> is",
                "ap LSHIFT 1 -> bj",
                "44430 -> b",
                "kl AND kr -> kt",
                "ep OR eo -> eq",
                "NOT dq -> dr"
            };
            var sut = new InstructionMapper();

            var result = sut.Map(stringInstructions);
            
            Assert.Equal(stringInstructions.Count, result.Count());
        }

        private void ValidateInstruction(Instruction expected, Instruction actual)
        {
            Assert.Equal(expected.Operation, actual.Operation);
            Assert.Equal(expected.Arguments.Count, actual.Arguments.Count);
            Assert.Equal(expected.Arguments[0], actual.Arguments[0]);
            if(expected.Arguments.Count > 1) Assert.Equal(expected.Arguments[1], actual.Arguments[1]);
            Assert.Equal(expected.Output, actual.Output);
        }
    }
}