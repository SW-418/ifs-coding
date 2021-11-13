using System;
using ifs_coding.Shared;

namespace ifs_coding
{
    class Program
    {
        static void Main(string[] args)
        {
            const string q1FileName = "Question1/question01_input.txt";
            const string q2FileName = "Question2/question02_input.txt";
            
            var fileReader = new FileReader();
            var question1 = new Question1.Question1(fileReader);
            var question2 = new Question2.Question2(fileReader);
            
            Console.WriteLine("Sam Wells - Answers");
            Console.WriteLine($"Q1 - Final Floor: {question1.FindFloor(q1FileName)}");
            Console.WriteLine($"Q2 - Houses with meters read at least once: {question2.CalculateTotalUniqueVisits(q2FileName)}");
        }
    }
}