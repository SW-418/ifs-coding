using System;
using System.Collections.Generic;
using System.Linq;
using ifs_coding.Question4.Models;

namespace ifs_coding.Question4.Mapping
{
    public class InstructionMapper : IMapper<string, Instruction>
    {
        public IEnumerable<Instruction> Map(IEnumerable<string> input)
        {
            return input.Select(Map).ToList();
        }

        public Instruction Map(string input)
        {
            var splitInput = input.Split(" ");

            if (splitInput[0].Equals("NOT")) return new Instruction(Operation.Not, new List<string>{ splitInput[1] }, splitInput[3]);
            if (splitInput.Length == 3) return new Instruction(Operation.Assign, new List<string>{ splitInput[0] }, splitInput[2]);

            return splitInput[1] switch
            {
                "OR" => new Instruction(Operation.Or, new List<string>{ splitInput[0], splitInput[2] }, splitInput[4]),
                "AND" => new Instruction(Operation.And, new List<string>{ splitInput[0], splitInput[2] }, splitInput[4]),
                "LSHIFT" => new Instruction(Operation.LShift, new List<string>{ splitInput[0], splitInput[2] }, splitInput[4]),
                "RSHIFT" => new Instruction(Operation.RShift, new List<string>{ splitInput[0], splitInput[2] }, splitInput[4]),
                _ => throw new ArgumentException("Unsupported input")
            };
        }
    }
}