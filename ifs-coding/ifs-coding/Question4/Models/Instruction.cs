using System.Collections.Generic;

#nullable enable
namespace ifs_coding.Question4.Models
{
    public class Instruction
    {
        public Operation Operation { get; }
        public List<string> Arguments { get; }
        public string Output { get; }
        
        public bool Processed { get; set; }
        
        public Instruction(Operation operation, List<string> args, string output)
        {
            Operation = operation;
            Arguments = args;
            Output = output;
            Processed = false;
        }
        public override string ToString()
        {
            return $"Op: {Operation} Args: {string.Join(",", Arguments)} Output: {Output} Processed: {Processed}";
        }
    }
}