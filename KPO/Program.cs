using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KPO
{
    class Program
    {

        static void Main(string[] args)
        {
            Console.WriteLine("Task 1");
            Task1 task1 = new Task1();
            task1.Start();
            //Console.ReadLine();

            Console.WriteLine("Task 2");
            Task2 task2 = new Task2();
            task2.Start();
            //Console.ReadLine();

            Console.WriteLine("Task 3");
            Task3 task3 = new Task3();
            task3.Start();
            //Console.ReadLine();

            Console.WriteLine("Task 4");
            Task4 task4 = new Task4();
            task4.Start();
            Console.ReadLine();

        }
    }
}
