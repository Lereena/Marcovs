using System;
using System.Collections;
using System.Collections.Generic;

namespace Marcovs
{
    class Substitution
    {
        public string Replaceable { get; }
        public string Replacement { get; }
        public bool IsFinal { get; }

        public Substitution(string replaceable, string replacement, bool isFinal)
        {
            Replaceable = replaceable;
            Replacement = replacement;
            IsFinal = isFinal;
        }
    }
    
    static class Program
    {
        static List<Substitution> ReadScheme()
        {
            var res = new List<Substitution>();
            var substitution = Console.ReadLine();
            
            while (substitution != "(end)")
            {
                if (substitution == null)
                    return res;

                var subArr = substitution.Split();
                switch (subArr.Length)
                {
                    case 1 :
                        if (substitution == "(end)")
                            break;
                        else
                        {
                            Console.WriteLine("Неверный формат подстановки. Попробуйте ещё раз");
                            break;
                        }
                    case 2 : 
                        res.Add(new Substitution(EmptyIfEps(subArr[0]), EmptyIfEps(subArr[1]), false));
                        break;
                    case 3 :
                        res.Add(new Substitution(EmptyIfEps(subArr[0]), EmptyIfEps(subArr[2]), true));
                        break;
                    default :
                        Console.WriteLine("Неверный формат подстановки. Попробуйте ещё раз");
                        break;
                }
                
                substitution = Console.ReadLine();
            }

            return res;
        }

        static string EmptyIfEps(string str) => str == "(Eps)" ? "" : str;
        
        static void ApplyScheme(List<Substitution> scheme, string word)
        {
            var close = false;
            while (!close)
            {
                foreach (var sub in scheme)
                {
                    if (word.IndexOf(sub.Replaceable, StringComparison.Ordinal) != -1)
                    {
                        word = word.ReplaceFirst(sub.Replaceable, sub.Replacement);

                        Console.WriteLine(word == "" ? "(Eps)" : word);

                        if (sub.IsFinal)
                            close = true;
                        
                        break;
                    }
                }
            }
        }
        
        static string ReplaceFirst(this string text, string search, string replace)
        {
            int pos = text.IndexOf(search, StringComparison.Ordinal);
            if (pos < 0)
            {
                return text;
            }
            return text.Substring(0, pos) + replace + text.Substring(pos + search.Length);
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