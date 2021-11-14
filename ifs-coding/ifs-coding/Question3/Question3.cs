using System;
using System.Collections.Generic;
using ifs_coding.Shared;

namespace ifs_coding.Question3
{
    public class Question3
    {
        private readonly IFileReader _fileReader;
        private readonly Dictionary<char, bool> _vowels = new()
        {
            { 'a', true },
            { 'e', true },
            { 'i', true },
            { 'o', true },
            { 'u', true }
        };
        private readonly Dictionary<string, bool> _disallowedStrings = new()
        {
            { "ab", true },
            { "cd", true },
            { "pq", true },
            { "xy", true }
        };

        public Question3(IFileReader reader)
        {
            _fileReader = reader;
        }

        public int FindGoodStrings(string fileName)
        {
            var goodStringCount = 0;
            var input = _fileReader.ReadMultiLineFile(fileName);
            
            foreach (var s in input)
            {
                var vowelCount = 0;
                var repeatingChar = false;
                var containsDisallowedString = false;
                var fastCounter = 1;
                
                for (var i = 0; i < s.Length; i++)
                {
                    if (_vowels.ContainsKey(s[i])) vowelCount++;
                    if (i < s.Length - 1)
                    {
                        if(s[i].Equals(s[fastCounter])) repeatingChar = true;
                        if (_disallowedStrings.ContainsKey($"{s[i]}{s[fastCounter]}")) containsDisallowedString = true;
                    }
                    fastCounter++;
                }

                if (vowelCount >= 3 && repeatingChar && !containsDisallowedString) goodStringCount++;
            }
            
            return goodStringCount;
        }
    }
}
