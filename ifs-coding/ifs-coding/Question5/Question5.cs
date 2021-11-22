using System;
using System.Collections.Generic;
using System.Linq;
using ifs_coding.Shared;

namespace ifs_coding.Question5
{
    public class Question5
    {
        private const int Wire1Value = 1;
        private const int Wire2Value = 2;
        private const int IntersectionValue = -1;
        
        private readonly IFileReader _fileReader;
        private int _shortestDistance;

        public Question5(IFileReader reader)
        {
            _fileReader = reader;
            _shortestDistance = int.MaxValue;
        }

        public int CalculateClosestIntersection(string fileName)
        {
            var inputs = _fileReader.ReadMultiLineFile(fileName);
            var dictGrid = new Dictionary<int, Dictionary<int, int>>();
            
            var wire1Instructions = inputs.First();
            var wire2Instructions = inputs.Last();

            ProcessWireInstructions(wire1Instructions, dictGrid, Wire1Value);
            ProcessWireInstructions(wire2Instructions, dictGrid, Wire2Value);

            return _shortestDistance;
        }

        private void ProcessWireInstructions(string wire1Instructions, IDictionary<int, Dictionary<int, int>> dictGrid, int wireValue)
        {
            var x = 0;
            var y = 0;
            foreach (var instruction in wire1Instructions.Split(","))
            {
                var direction = instruction[0];
                var distance = int.Parse(instruction[1..instruction.Length]);
        
                if (direction.Equals('R'))
                {
                    var startingPoint = x;
                    for (var i = startingPoint; i < startingPoint + distance; i++)
                    {
                        UpdateAxisValues(dictGrid, i, y, wireValue);
                        x++;
                    }
                }
                if (direction.Equals('L'))
                {
                    var startingPoint = x;
                    for (var i = startingPoint; i > startingPoint - distance; i--)
                    {
                        UpdateAxisValues(dictGrid, i, y, wireValue);
                        x--;
                    }
                }
                if (direction.Equals('U'))
                {
                    var startingPoint = y;
                    for (var i = startingPoint; i < startingPoint + distance; i++)
                    {
                        UpdateAxisValues(dictGrid, x, i, wireValue);
                        y++;
                    }
                }
                if (direction.Equals('D'))
                {
                    var startingPoint = y;
                    for (var i = startingPoint; i > startingPoint - distance; i--)
                    {
                        UpdateAxisValues(dictGrid, x, i, wireValue);
                        y--;
                    }
                }
            }
        }

        private void UpdateAxisValues(IDictionary<int, Dictionary<int, int>> grid, int x, int y, int value)
        {
            if (grid.TryGetValue(x, out var yDict))
            {
                if (yDict.TryGetValue(y, out var val))
                {
                    // If there is an intersection and it isn't the starting position
                    if (val != 1 || value != 2 || x == 0 && y == 0) return;
                    yDict[y] = IntersectionValue;
                    var distance = Math.Abs(0 - x) + Math.Abs(0 - y);
                    if (distance < _shortestDistance) _shortestDistance = distance;
                }
                else { yDict.Add(y, value); }
            }
            else { grid[x] = new Dictionary<int, int> { { y, value } }; }
        }
    }
}
