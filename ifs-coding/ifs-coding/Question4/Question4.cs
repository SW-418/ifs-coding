using System;
using System.Collections.Generic;
using ifs_coding.Question4.Mapping;
using ifs_coding.Question4.Models;
using ifs_coding.Shared;

namespace ifs_coding.Question4
{
    public class Question4
    {
        private readonly IFileReader _fileReader;
        private readonly IMapper<string, Instruction> _instructionMapper;

        public Question4(IFileReader reader, IMapper<string, Instruction> mapper)
        {
            _fileReader = reader;
            _instructionMapper = mapper;
        }

        public int CalculateWireResult(string fileName)
        {
            var instructions = _fileReader.ReadMultiLineFile(fileName);
            var wires = new Dictionary<string, int>();
            var mappedInstructions = _instructionMapper.Map(instructions);

            foreach (var operation in mappedInstructions)
            {
                Console.WriteLine(operation.ToString());
                
                
            }

            return wires["a"];
        }
    }
}