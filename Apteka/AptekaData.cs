﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Apteka
{
    public struct Apteka
    {
        private string _name;
        private string _manufact;
        private int _price;
        private double _discount;
        public string name
        {
            set 
            {
                foreach (char c in value)
                {
                    if (!Char.IsLetter(c))
                    {
                        throw new Exception("Строка должна содержать только буквы!");
                    }
                    else
                    {
                        _name = value;
                    } 
                }
            } 
            get { return _name; } 
        }
        public string manufact
        {
            set 
            {
                foreach (char c in value)
                {
                    if (!Char.IsLetter(c))
                    {
                        throw new Exception("Строка должна содержать только буквы!");
                    }
                    {
                        _manufact = value;
                    }
                        
                }
            } 
            get { return _manufact; } 
        }
        public int price
        {
            set
            {
                if (value > 10000 || value < 1)
                {
                    throw new Exception("Цена должна быть в пределах от 1 до 10000 (руб.)!");
                }
                else
                {
                    _price = value;
                }
            }
            get { return _price; }
        }
        public double discount
        {
            set
            {
                if (value > 100 || value < 0)
                {
                    throw new Exception("Скидка должна быть в пределах от 0 до 100 (%)!");
                }
                else
                {
                    _discount = value;
                }
            }
            get { return _discount; }
        }
        public double discountedPrice;
        public void OutputDataStruct()
        {
            Console.WriteLine($"Название препарата: {name}, Производитель: {manufact}, Цена: {price} руб., Скидка: {Math.Round(discount, 2)} %, Цена со скидкой: {Math.Round(discountedPrice, 2)} руб.");
        }
    }
    public static class Data
    {
        public static void InputData(int N, Apteka[] d)
        {
            for (int i = 0; i < N; i++)
            {
                Console.WriteLine();
                Console.Write("Название препарата: ");
                d[i].name = Console.ReadLine();
                Console.Write("Производитель: ");
                d[i].manufact = Console.ReadLine();
                Console.Write("Цена (руб.): ");
                d[i].price = Convert.ToInt32(Console.ReadLine());
                Console.Write("Скидка (%): ");
                d[i].discount = Convert.ToDouble(Console.ReadLine());
                d[i].discountedPrice = d[i].price - (d[i].price * d[i].discount) / 100;
                Console.Write($"Цена со скидкой (руб.): {d[i].discountedPrice}");
                Console.WriteLine();
            }
        }
        public static void InputDataOne(int N, Apteka[] d)
        {
            int i = N - 1;
            Console.WriteLine();
            Console.Write("Название препарата: ");
            d[i].name = Console.ReadLine();
            Console.Write("Производитель: ");
            d[i].manufact = Console.ReadLine();
            Console.Write("Цена (руб.): ");
            d[i].price = Convert.ToInt32(Console.ReadLine());
            Console.Write("Скидка (%): ");
            d[i].discount = Convert.ToDouble(Console.ReadLine());
            d[i].discountedPrice = d[i].price - (d[i].price * d[i].discount) / 100;
            Console.Write($"Цена со скидкой (руб.): {d[i].discountedPrice}");
            Console.WriteLine();
        }
        public static void OutputData(int N, Apteka[] d)
        {
            Console.Write("Список товаров: ");
            for (int i = 0; i < N; i++)
            {
                Console.WriteLine();
                d[i].OutputDataStruct();
            }
        }
        public static void WriteToFile(Apteka[] d, string path)
        {
            using (StreamWriter writer = new StreamWriter(path, false))
            {
                foreach (Apteka s in d)
                {
                    writer.WriteLine($"Название препарата: {s.name}");
                    writer.WriteLine($"Производитель: {s.manufact}");
                    writer.WriteLine($"Цена (руб.): {s.price}");
                    writer.WriteLine($"Скидка (%): {s.discount}");
                    writer.WriteLine($"Цена со скидкой (руб.): {s.discountedPrice}\n/");
                }
                writer.Close();
            }
        }
        public static void ReadFrFile(out int N, out Apteka[] d, string path)
        {
            using (StreamReader reader1 = new StreamReader(path))
            {
                N = 0;
                string str1;
                while ((str1 = reader1.ReadLine()) != null)
                {
                    if (str1.Contains('/'))
                    {
                        N++;
                    }
                }
                reader1.Close();
            }
            d = new Apteka[N];
            using (StreamReader reader2 = new StreamReader(path))
            {
                string str2, line;
                for (int i = 0; i < N; i++)
                {
                    while ((line = reader2.ReadLine()) != null)
                    {
                        if (line.Contains("Название препарата:"))
                        {
                            str2 = line;
                            str2 = str2.Substring(str2.IndexOf(':') + 1);
                            d[i].name = str2.Replace(" ", "");
                        }
                        if (line.Contains("Производитель:"))
                        {
                            str2 = line;
                            str2 = str2.Substring(str2.IndexOf(':') + 1);
                            d[i].manufact = str2.Replace(" ", "");
                        }
                        if (line.Contains("Цена (руб.):"))
                        {
                            str2 = line;
                            str2 = str2.Substring(str2.IndexOf(':') + 1);
                            d[i].price = Convert.ToInt32(str2);
                        }
                        if (line.Contains("Скидка (%):"))
                        {
                            str2 = line;
                            str2 = str2.Substring(str2.IndexOf(':') + 1);
                            d[i].discount = Convert.ToDouble(str2);
                        }
                        if (line.Contains("Цена со скидкой (руб.):"))
                        {
                            str2 = line;
                            str2 = str2.Substring(str2.IndexOf(':') + 1);
                            d[i].discountedPrice = d[i].price - (d[i].price * d[i].discount) / 100;
                        }
                        if (line.Contains('/'))
                        {
                            break;
                        }
                    }
                }
                reader2.Close();
            }
        }
        public static void SearchData(int N, Apteka[] d)
        {
            string atr;
            int n = 0;
            Console.Write("Введите производителя: ");
            atr = Console.ReadLine();
            for (int i = 0; i < N; i++)
            {
                if (atr == d[i].manufact)
                {
                    Console.Write("Найденные товары: ");
                    d[i].OutputDataStruct();
                    n++;
                }
            }
            if (n == 0)
            {
                Console.WriteLine("Таких товаров не найдено!");
            }
        }
        public static void Sort(Apteka[] d, int k, int N)
        {
            var index = 0;
            var count = new int[k + 1];
            for (int j = 0; j < N; j++)
            {
                count[d[j].price]++;
            }
            for (int t = 0; t < count.Length; t++)
            {
                for (int w = 0; w < count[t]; w++)
                {    
                    for (int i = N - 1; i > 0; i--)
                    {
                        for (int j = 0; j < i; j++)
                        {
                            if (d[j].price == t)
                            {
                                string n = d[index].name;
                                d[index].name = d[j].name;
                                d[j].name = n;

                                string m = d[index].manufact;
                                d[index].manufact = d[j].manufact;
                                d[j].manufact = m;

                                int p = d[index].price;
                                d[index].price = d[j].price;
                                d[j].price = p;

                                double dis = d[index].discount;
                                d[index].discount = d[j].discount;
                                d[j].discount = dis;

                                double dp = d[index].discountedPrice;
                                d[index].discountedPrice = d[j].discountedPrice;
                                d[j].discountedPrice = dp;
                            }
                        }
                    }
                    index++;
                }
            }
        }
        public static DialogResult Show(string text, string caption, MessageBoxButtons buttons, MessageBoxIcon icon)
        {
            return MessageBox.Show(text, caption, buttons, icon);
        }
    }
}