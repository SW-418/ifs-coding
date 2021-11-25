using System;
using System.Collections.Generic;
using System.Linq;
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

        public Dictionary<string, ushort> CalculateWireResult(string fileName)
        {
            var instructions = _fileReader.ReadMultiLineFile(fileName);
            var wires = new Dictionary<string, ushort>(); // ushort - 0->65535

            var mappedInstructions = _instructionMapper.Map(instructions);
            
            var unprocessedInstructions = mappedInstructions.Where(x => !x.Processed);
            var previousCount = Int32.MaxValue;
            while(unprocessedInstructions.Any() && unprocessedInstructions.Count() < previousCount)
            {
                previousCount = unprocessedInstructions.Count();
                foreach (var unprocessedInstruction in unprocessedInstructions)
                {
                    ProcessInstruction(unprocessedInstruction, wires);
                }
                unprocessedInstructions = mappedInstructions.Where(x => !x.Processed);
                
            }
            return wires;
        }

        private void ProcessInstruction(Instruction instruction, IDictionary<string, ushort> wires)
        {
            if (instruction.Operation == Operation.Assign)
            {
                var processed = false;
                if (ushort.TryParse(instruction.Arguments[0], out var val))
                {
                    wires[instruction.Output] = val;
                    processed = true;
                }
                else
                {
                    if (wires.ContainsKey(instruction.Arguments[0]))
                    {
                        wires[instruction.Output] = wires[instruction.Arguments[0]];
                        processed = true;
                    }
                }
                instruction.Processed = processed;
            }

            if (instruction.Operation == Operation.And)
            {
                // Check there is a signal on both wires
                if (wires.ContainsKey(instruction.Arguments[0]) && wires.ContainsKey(instruction.Arguments[1]))
                {
                    wires[instruction.Output] =
                        (ushort)(wires[instruction.Arguments[0]] & wires[instruction.Arguments[1]]);
                    instruction.Processed = true;
                }
            }
            if (instruction.Operation == Operation.Or)
            {
                // Check there is a signal on both wires
                if (wires.ContainsKey(instruction.Arguments[0]) && wires.ContainsKey(instruction.Arguments[1]))
                {
                    wires[instruction.Output] =
                        (ushort)(wires[instruction.Arguments[0]] | wires[instruction.Arguments[1]]);
                    instruction.Processed = true;
                }
            }

            if (instruction.Operation == Operation.Not)
            {
                // Check there is a signal on both wires
                if (wires.ContainsKey(instruction.Arguments[0]))
                {
                    wires[instruction.Output] =
                        (ushort)~wires[instruction.Arguments[0]];
                    instruction.Processed = true;
                }
            }
            
            if (instruction.Operation == Operation.LShift)
            {
                // Check there is a signal on both wires
                if (wires.ContainsKey(instruction.Arguments[0]))
                {
                    wires[instruction.Output] =
                        (ushort)(wires[instruction.Arguments[0]] << int.Parse(instruction.Arguments[1]));
                    instruction.Processed = true;
                }
            }
            
            if (instruction.Operation == Operation.RShift)
            {
                // Check there is a signal on both wires
                if (wires.ContainsKey(instruction.Arguments[0]))
                {
                    wires[instruction.Output] =
                        (ushort)(wires[instruction.Arguments[0]] >> int.Parse(instruction.Arguments[1]));
                    instruction.Processed = true;
                }
            }
        }
    }
}
