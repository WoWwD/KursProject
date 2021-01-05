﻿using System;
using System.Windows.Forms;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Apteka
{
    class Program
    {
        public static void Menu()
        {
            Console.WriteLine("1. Добавление препаратов");
            Console.WriteLine("2. Вывод всех препаратов");
            Console.WriteLine("3. Добавить один препарат");
            Console.WriteLine("4. Удалить последний добавленный препарат");
            Console.WriteLine("5. Добавить список препаратов в файл");
            Console.WriteLine("6. Добавить препараты из файла в список");
            Console.WriteLine("7. Поиск препаратов по производителю");
            Console.WriteLine("8. Сортировать методом подсчёта препараты по цене");
            Console.WriteLine("9. Построить диаграмму");
            Console.WriteLine("Escape. Выход");
        }
        [STAThread]
        static void Main(string[] args)
        {
            int N = 0;
            Apteka[] d;
            Apteka[] p;
            d = new Apteka[N];
          Menu:
            try
            {
                Menu();
                switch (Console.ReadKey(true).Key)
                {
                    case ConsoleKey.D1:{
                        Console.Clear();
                        Console.Write("Количество добавляемых препаратов: ");
                        N = Convert.ToInt32(Console.ReadLine());
                        d = new Apteka[N];
                        Data.InputData(N, d);
                        Data.Show("Препараты добавлены!", "Сообщение", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        Console.Clear();
                        goto Menu;
                    }
                    case ConsoleKey.D2:{
                        Console.Clear();
                        if (N != 0)
                        {
                            Data.OutputData(N, d);
                            Console.WriteLine("Нажмите Enter для возврата в меню");
                            Console.ReadKey();
                            Console.Clear();
                            goto Menu;
                        }
                        Data.Show("Список препаратов пуст!", "Сообщение", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        Console.Clear();
                        goto Menu;
                    }
                    case ConsoleKey.D3:{
                        Console.Clear();
                        N++;
                        p = new Apteka[N];
                        for (int i = 0; i < N - 1; i++)
                            p[i] = d[i];
                        d = new Apteka[N];
                        Data.InputDataOne(N, p);
                        for (int i = 0; i < N; i++)
                            d[i] = p[i];
                        Data.Show("Препарат добавлен!", "Сообщение", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        Console.Clear();
                        goto Menu;
                    }
                    case ConsoleKey.D4:{
                        Console.Clear();
                        if (N != 0)
                        {
                                N--;
                            p = new Apteka[N];
                            for (int i = 0; i < N; i++)
                                p[i] = d[i];
                            d = new Apteka[N];
                            for (int i = 0; i < N; i++)
                                d[i] = p[i];
                            Data.Show("Последний добавленный препарат был удалён!", "Сообщение", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            Console.Clear();
                            goto Menu;
                        }
                        else
                        {
                            Data.Show("Список препаратов пуст!", "Сообщение", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            goto Menu;
                        }
                    }
                    case ConsoleKey.D5:{
                        Console.Clear();
                        if (N != 0)
                        {
                            Data.WriteToFile(d, @"D:\\apteka.txt");
                            Data.Show("Препараты были сохранены в файл 'Apteka.txt' !", "Сообщение", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            Console.Clear();
                            goto Menu;
                        }
                        else
                        {
                            Data.Show("Список препаратов пуст!", "Сообщение", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            goto Menu;
                        }
                    }
                    case ConsoleKey.D6:{
                        Console.Clear();
                        Data.ReadFrFile(out N, out d, @"D:\\apteka.txt");
                        if (new FileInfo(@"D:\\apteka.txt").Length == 0)
                        {
                            Data.Show("Файл пустой!", "Сообщение", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            goto Menu;
                        }
                        Data.Show("Препараты были добавлены в список!", "Сообщение", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        Console.Clear();
                        goto Menu;
                    }
                    case ConsoleKey.D7:{
                        Console.Clear();
                        if (N != 0)
                        {
                            Data.SearchData(N, d);
                            Console.WriteLine("Нажмите Enter для возврата в меню");
                            Console.ReadKey();
                            Console.Clear();
                            goto Menu;
                        }
                        else
                        {
                            Data.Show("Список препаратов пуст!", "Сообщение", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            goto Menu;
                        }
                    }
                    case ConsoleKey.D8:{
                        Console.Clear();
                        if (N != 0)
                        {
                            Data.Sort(d, 10000, N);
                            Data.Show("Сортировка выполнена!", "Сообщение", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            Console.Clear();
                            goto Menu;
                        }
                        else
                        {
                            Data.Show("Список препаратов пуст!", "Сообщение", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            goto Menu;
                        }
                    }
                    case ConsoleKey.D9:{
                        Application.EnableVisualStyles();
                        Application.Run(new KP.AptekaChart());
                        Console.Clear();
                        goto Menu;
                    }
                    case ConsoleKey.Escape: { break; }
                }
            }
            catch (Exception ex)
            {
                Data.Show(ex.Message, "Исключение", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Console.Clear();
                goto Menu;
            }
        }
        
    }
}
