using System;
using System.Collections.Generic;
using ifs_coding.Shared;

namespace ifs_coding.Question2
{
    public class Question2
    {
        private readonly IFileReader _fileReader;
        
        public Question2(IFileReader reader)
        {
            _fileReader = reader;
        }

        public int CalculateTotalUniqueVisits(string fileName)
        {
            var uniqueVisits = 1;
            var x = 0;
            var y = 0;

            var input = _fileReader.ReadSingleLineFile(fileName);
            var visited = new Dictionary<string, bool> { { $"{x}-{y}", true } };

            foreach (var character in input)
            {
                _ = character switch
                {
                    '^' => y++,
                    'v' => y--,
                    '>' => x++,
                    '<' => x--,
                    _ => throw new ArgumentException()
                };

                if (!visited.ContainsKey($"{x}-{y}"))
                {
                    uniqueVisits++;
                    visited.Add($"{x}-{y}", true);
                }
            }
            return uniqueVisits;
        }
    }
}