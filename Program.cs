using Microsoft.Win32;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;

class Program
{
    static void Main()
    {
        
        ArrayList numbers = new ArrayList();
        for (int i = 1; i <= 1000000; i++)
        {
            numbers.Add(i);
        }

        
        int division = numbers.Count / 4;
        List<ArrayList> divisions = new List<ArrayList>();

        for (int i = 0; i < 4; i++)
        {
            int firstindex = i * division;
            int lastindex = (i + 1) * division - 1;

            ArrayList stack = new ArrayList();

            for (int j = firstindex; j <= lastindex; j++)
            {
                stack.Add(numbers[j]);
            }

            divisions.Add(stack);
        }

       
        ArrayList evennumbers = new ArrayList();
        ArrayList oddnumbers = new ArrayList();
        ArrayList primenumbers = new ArrayList();

       
        Task[] tasks = new Task[4];

        for (int i = 0; i < 4; i++)
        {
            int index = i; 

            tasks[i] = Task.Run(() =>
            {
                FindNumbers(divisions[index], evennumbers, oddnumbers, primenumbers);
            });
        }

        
        Task.WaitAll(tasks);

        
        Console.WriteLine("Çift Sayılar: " + string.Join(", ", evennumbers.ToArray()));
        Console.WriteLine("Tek Sayılar: " + string.Join(", ", oddnumbers.ToArray()));
        Console.WriteLine("Asal Sayılar: " + string.Join(", ", primenumbers.ToArray()));

        bool condition = true;
        while (condition)
        {
            Console.WriteLine("Liste Yazdırın\n1-Çift\n2-Tek\n3-Asal\n0-Çıkış");
            int input = int.Parse(Console.ReadLine());
            switch (input)
            {
                case 0:
                    condition = false;
                    break;
                case 1:
                    for (int i = 0; i < evennumbers.Count; i++)
                    {
                        Console.WriteLine("Çift sayı : {0}", evennumbers[i].ToString());
                    }
                    break;
                case 2:
                    for (int i = 0; i < oddnumbers.Count; i++)
                    {
                        Console.WriteLine("Tek sayı : {0}", oddnumbers[i].ToString());
                    }
                    break;
                case 3:
                    for (int i = 0; i < primenumbers.Count; i++)
                    {
                        Console.WriteLine("Asal sayı : {0}", primenumbers[i].ToString());
                    }
                    break;
                default:
                    break;
            }
        }

        Console.ReadLine();
    }

    static void FindNumbers(ArrayList stack, ArrayList evennumbers, ArrayList oddnumbers, ArrayList primenumbers)
    {
        foreach (int number in stack)
        {
            if (isEven(number))
            {
                lock (evennumbers)
                {
                    evennumbers.Add(number);
                }
            }
            else
            {
                lock (oddnumbers)
                {
                    oddnumbers.Add(number);
                }
            }

            if (isPrime(number))
            {
                lock (primenumbers)
                {
                    primenumbers.Add(number);
                }
            }
        }
    }

    static bool isEven(int number)
    {
        return number % 2 == 0;
    }

    static bool isPrime(int number)
    {
        if (number < 2)
            return false;

        for (int i = 2; i <= Math.Sqrt(number); i++)
        {
            if (number % i == 0)
                return false;
        }

        return true;
    }
}