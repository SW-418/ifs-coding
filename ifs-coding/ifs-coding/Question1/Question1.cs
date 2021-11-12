using System;
using ifs_coding.Shared;

namespace ifs_coding.Question1
{
    public class Question1
    {
        private readonly IFileReader _fileReader;

        public Question1(IFileReader reader)
        {
            _fileReader = reader;
        }

        public int FindFloor(string fileName)
        {
            var input = _fileReader.ReadSingleLineFile(fileName);
            
            var currentFloor = 0;

            foreach (var character in input)
            {
                var i = character switch
                {
                    '(' => currentFloor++,
                    ')' => currentFloor--,
                    _ => throw new ArgumentException()
                };
            }
            return currentFloor;
        }
    }
}