using System;
using System.Collections;
using System.Collections.Generic;

namespace Marcovs
{
    class Substitution
    {
        public string Prev { get; }
        public string Next { get; }
        public bool IsFinal { get; }

        public Substitution(string prev, string next, bool isFinal)
        {
            Prev = prev;
            Next = next;
            IsFinal = isFinal;
        }
    }
    
    internal class Program
    {
        static List<Substitution> ReadScheme()
        {
            var res = new List<Substitution>();
            
            while (true)
            {
                var substitution = Console.ReadLine();
                if (substitution == null)
                    return res;

                var subArr = substitution.Split();
                Substitution sub;
                switch (substitution.Length)
                {
                    case 2 : 
                        res.Add(new Substitution(subArr[0], subArr[1], false));
                        break;
                    case 3 :
                        res.Add(new Substitution(subArr[0], subArr[1], true));
                        break;
                    default :
                        Console.WriteLine("Неверный формат подстановки. Попробуйте ещё раз");
                        break;
                }
            }
        }

        static void ApplyScheme(List<Substitution> scheme, string word)
        {
            var close = false;
            while (!close)
            {
                foreach (var sub in scheme)
                {
                    if (word.IndexOf(sub.Prev, StringComparison.Ordinal) != -1)
                    {
                        word = word.Replace(sub.Prev, sub.Next);
                        Console.WriteLine(word);
                        if (sub.IsFinal)
                            close = true;
                        break;
                    }
                }
            }
        }
        
        public static void Main(string[] args)
        {
            Console.WriteLine("Пример ввода схемы нормального алгорифма:");
            Console.WriteLine("aaaa asd");
            Console.WriteLine("ss . dfs // заключительная подстановка");
            Console.WriteLine("asd (Eps) // замена на пустой символ");
            Console.WriteLine("(end) // окончание ввода");
            Console.WriteLine();
            
            Console.WriteLine("Введите схему нормального алгорифма");
            var scheme = ReadScheme();
            Console.WriteLine("Введите входное слово");
            var word = Console.ReadLine();
            Console.WriteLine("Результат применения схемы к слову:");
            
            ApplyScheme(scheme, word);
        }
    }
}