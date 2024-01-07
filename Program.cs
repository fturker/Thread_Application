using Microsoft.Win32;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;

class Program
{
    static void Main()
    {
        
        ArrayList sayilar = new ArrayList();
        for (int i = 1; i <= 1000000; i++)
        {
            sayilar.Add(i);
        }

        
        int bolum = sayilar.Count / 4;
        List<ArrayList> bolumler = new List<ArrayList>();

        for (int i = 0; i < 4; i++)
        {
            int baslangicindex = i * bolum;
            int sonindex = (i + 1) * bolum - 1;

            ArrayList yigin = new ArrayList();

            for (int j = baslangicindex; j <= sonindex; j++)
            {
                yigin.Add(sayilar[j]);
            }

            bolumler.Add(yigin);
        }

       
        ArrayList ciftsayilar = new ArrayList();
        ArrayList teksayilar = new ArrayList();
        ArrayList asalsayilar = new ArrayList();

       
        Task[] tasks = new Task[4];

        for (int i = 0; i < 4; i++)
        {
            int index = i; 

            tasks[i] = Task.Run(() =>
            {
                FindNumbers(bolumler[index], ciftsayilar, teksayilar, asalsayilar);
            });
        }

        
        Task.WaitAll(tasks);

        
        Console.WriteLine("Çift Sayılar: " + string.Join(", ", ciftsayilar.ToArray()));
        Console.WriteLine("Tek Sayılar: " + string.Join(", ", teksayilar.ToArray()));
        Console.WriteLine("Asal Sayılar: " + string.Join(", ", asalsayilar.ToArray()));

        bool kosul = true;
        while (kosul)
        {
            Console.WriteLine("Liste Yazdırın\n1-Çift\n2-Tek\n3-Asal\n0-Çıkış");
            int input = int.Parse(Console.ReadLine());
            switch (input)
            {
                case 0:
                    kosul = false;
                    break;
                case 1:
                    for (int i = 0; i < ciftsayilar.Count; i++)
                    {
                        Console.WriteLine("Çift sayı : {0}", ciftsayilar[i].ToString());
                    }
                    break;
                case 2:
                    for (int i = 0; i < teksayilar.Count; i++)
                    {
                        Console.WriteLine("Çift sayı : {0}", teksayilar[i].ToString());
                    }
                    break;
                case 3:
                    for (int i = 0; i < asalsayilar.Count; i++)
                    {
                        Console.WriteLine("Çift sayı : {0}", asalsayilar[i].ToString());
                    }
                    break;
                default:
                    break;
            }
        }

        Console.ReadLine();
    }

    static void FindNumbers(ArrayList yigin, ArrayList ciftsayilar, ArrayList teksayilar, ArrayList asalsayilar)
    {
        foreach (int sayi in yigin)
        {
            if (Ciftmi(sayi))
            {
                lock (ciftsayilar)
                {
                    ciftsayilar.Add(sayi);
                }
            }
            else
            {
                lock (teksayilar)
                {
                    teksayilar.Add(sayi);
                }
            }

            if (Asalmi(sayi))
            {
                lock (asalsayilar)
                {
                    asalsayilar.Add(sayi);
                }
            }
        }
    }

    static bool Ciftmi(int number)
    {
        return number % 2 == 0;
    }

    static bool Asalmi(int number)
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