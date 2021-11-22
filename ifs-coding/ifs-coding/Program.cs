using System;
using ifs_coding.Question4.Mapping;
using ifs_coding.Shared;

namespace ifs_coding
{
    class Program
    {
        static void Main(string[] args)
        {
            const string q1FileName = "Question1/question01_input.txt";
            const string q2FileName = "Question2/question02_input.txt";
            const string q3FileName = "Question3/question03_input.txt";
            const string q4FileName = "Question4/question04_input.txt";
            const string q5FileName = "Question5/question05_input.txt";
            
            var fileReader = new FileReader();
            var instructionMapper = new InstructionMapper();
            
            var question1 = new Question1.Question1(fileReader);
            var question2 = new Question2.Question2(fileReader);
            var question3 = new Question3.Question3(fileReader);
            var question4 = new Question4.Question4(fileReader, instructionMapper);
            var question5 = new Question5.Question5(fileReader);
            
            Console.WriteLine("Sam Wells - Answers");
            Console.WriteLine($"Q1 - Final Floor: {question1.FindFloor(q1FileName)}");
            Console.WriteLine($"Q2 - Houses with meters read at least once: {question2.CalculateTotalUniqueVisits(q2FileName)}");
            Console.WriteLine($"Q3 - Good string count: {question3.FindGoodStrings(q3FileName)}");
            Console.WriteLine($"Q4 - Wire A's final signal: {question4.CalculateWireResult(q4FileName)}");
            Console.WriteLine($"Q5 - MH distance from central port to closest intersection: {question5.CalculateClosestIntersection(q5FileName)}");
        }
    }
}
