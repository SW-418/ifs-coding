using System.Collections.Generic;

#nullable enable
namespace ifs_coding.Question4.Models
{
    public class Instruction
    {
        public Operation Operation { get; set; }
        public List<string> Arguments { get; set; }
        public string Output { get; set; }
        
        public Instruction(Operation operation, List<string> args, string output)
        {
            Operation = operation;
            Arguments = args;
            Output = output;
        }

        public override string ToString()
        {
            return $"Op: {Operation} Args: {string.Join(",", Arguments)} Output: {Output}";
        }
    }
}