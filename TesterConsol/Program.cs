using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TesterConsol
{
    internal class Program
    {
        public struct TestElement
        {
            public string QuestionName;
            public List<string> AnswerName;
            public int CorrectAnswer;
        }
        private static List<TestElement> Test;
        public static string NameTest;
        public static int CountAnswers = 0;
        public static void Menu()
        {
            Console.WriteLine("-----------------------");
            Console.WriteLine("\tTESTER");
            Console.WriteLine("-----------------------\n");
            Console.WriteLine("Menu\n");
            Console.WriteLine("Create test - 1");
            Console.WriteLine("Passing test - 2\n");
            Console.WriteLine("Введите значение");
            int Key = 0;
            try
            {
                Key = int.Parse(Console.ReadLine());
            }
            catch (Exception)
            {
                Console.Clear();
                Console.WriteLine("\nОШИБКА 001 <Введено некорректное значение, попробуйте снова>\n");
                Menu();
            }
            switch (Key)
            {
                case 1:
                    Console.Clear();
                    Console.WriteLine("Введите название теста");
                    Create(Console.ReadLine());
                    break;
                case 2:
                    Console.Clear();
                    Passing();
                    break;
                default:
                    Console.Clear();
                    Console.WriteLine("\nОШИБКА 002 <Введено неверное значение, попробуйте снова>\n");
                    Menu();
                    break;
            }
        }


        public static void Create(string NameTest)
        {
            Console.Clear();
            int CountQuestions = 0;

            for (int i = 0; i < 2; i++)
            {
                switch (i)
                {
                    case 0: //Количество вопросов
                        Console.WriteLine("Введите количество вопросов");
                        if (!int.TryParse(Console.ReadLine(), out CountQuestions))
                        {
                            Console.Clear();
                            Console.WriteLine("\nОШИБКА 003 <Введено некорректное количество вопросов, попробуйте снова>\n");
                            i--;
                        }
                        Test = new List<TestElement>(CountQuestions);
                        break;
                    case 1: //Количество ответов
                        Console.WriteLine("Введите количество ответов");
                        if (!int.TryParse(Console.ReadLine(), out CountAnswers))
                        {
                            Console.Clear();
                            Console.WriteLine("\nОШИБКА 004 <Введено некорректное количество ответов, попробуйте снова>\n");
                            i--;
                        }
                        else if (2 > CountAnswers)
                        {
                            Console.Clear();
                            Console.WriteLine("\nОШИБКА 005 < Введите количество ответов более 1-го >\n");
                            i--;
                        }
                        break;
                    default:
                        break;
                }
            }
            for (int i = 0; i < CountQuestions; i++)
            {
                Console.Clear();
                Console.WriteLine("Название вопроса - " + i);
                TestElement testElement = new TestElement();
                testElement.QuestionName = Console.ReadLine();
                testElement.AnswerName = new List<string>();
                for (int j = 0; j < CountAnswers; j++)
                {
                    Console.Clear();
                    Console.WriteLine("Название ответа - " + j);
                    testElement.AnswerName.Add(Console.ReadLine());
                }
                Console.Clear();
                for (int t = 0; t < CountAnswers; t++)
                {
                    switch (t)
                    {
                        case 0: //Правильный ответ
                            Console.WriteLine("Индекс правильного ответа, индексы начинаются с 0");
                            if (!int.TryParse(Console.ReadLine(), out testElement.CorrectAnswer))
                            {
                                Console.Clear();
                                Console.WriteLine("\nОШИБКА 005 <Введен неверный индекс правильного ответа, попробуйте снова>\n");
                                t--;
                            }
                            break;
                        default:
                            break;
                    }
                }
            }
            Console.Clear();
            Console.WriteLine("Тест готов и сохранен в файл\n");
            Save();
            Menu();
        }


        public static void Passing()
        {
            Console.WriteLine("Введите название теста");
            NameTest = Console.ReadLine();
            int correct_answer = 0;
            Load();
            for (int i = 0; i < Test.Count; i++)
            {
                Console.Clear();
                Console.WriteLine($"Вопрос {i} - { Test[i].QuestionName} \n");
                for (int j = 0; j < Test[i].AnswerName.Count; j++)
                {
                    Console.WriteLine($"{j}) {Test[i].AnswerName[j]} \n");
                }
                Console.WriteLine("Введите правильный ответ");
                int answer = 0;
                for (int t = 0; t < 1; t++)
                {
                    switch (t)
                    {
                        case 0: //Правильный ответ
                            if (!int.TryParse(Console.ReadLine(), out answer))
                            {
                                Console.WriteLine("\nОШИБКА 006 <Введен неверный индекс правильного ответа, попробуйте снова>\n");
                                t--;
                            }
                            else if (answer > CountAnswers)
                            {
                                Console.WriteLine("\nОШИБКА 007 <Такого варианта нет>\n");
                                t--;
                            }
                            break;
                        default:
                            break;
                    }
                    if (Test[i].CorrectAnswer == answer)
                    {
                        correct_answer++;
                    }
                }
            }
            Console.WriteLine($"Количество правильных ответов - {correct_answer} \n");
            Console.WriteLine("Вернуться в меню - 1");
            Console.WriteLine($"Закрыть {NameTest} - 2");
            int Final = int.Parse(Console.ReadLine()); 
            switch (Final)
            {
                case 1:
                    Menu();
                    break;
                case 2:
                    return;
                    break;
                default:
                    Console.Clear();
                    Console.WriteLine("\nОШИБКА 008 <Такого пункта нет>\n");
                    Menu();
                    break;
            }
        }


        public static void Save()
        {
            string json = JsonConvert.SerializeObject(Test);
            File.WriteAllText($@"C:\1\{NameTest}.txt", json);
        }


        public static void Load()
        {
            if (File.Exists($@"C:\1\{NameTest}.txt"))
            {
                string json = File.ReadAllText($@"C:\1\{NameTest}.txt");

                Test = JsonConvert.DeserializeObject<List<TestElement>>(json);
            }
            else
            {
                Console.WriteLine("\nОШИБКА 006 <Тест не найден, попробуйте снова>\n");
                Passing();
            }
        }


        static void Main(string[] args)
        {
            Menu();
        }
    }
}
