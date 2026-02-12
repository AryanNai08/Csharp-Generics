// Demonstrates use of GENERICS (List<T>) instead of ArrayList
// Shows type safety, performance improvement and no boxing/unboxing required

using System;
using System.Collections.Generic;

namespace GenricBasics
{
    class Program
    {
        static void Main(string[] args)
        {
            // Create object of Salaries class
            Salaries salaries = new Salaries();

            // Get list of salaries (generic List<float>)
            List<float> salaryList = salaries.GetSalaries();

            // Access second salary from list (index 1)
            // No casting required because List<float> is strongly typed
            float salary = salaryList[1];

            // Increase salary by 2%
            salary = salary + (salary * 0.02f);

            // Print updated salary
            Console.WriteLine(salary);

            Console.ReadKey();
        }
    }

    public class Salaries
    {
        // Generic list that stores only float values
        // Using List<float> avoids boxing/unboxing problems of ArrayList
        List<float> _salaryList = new List<float>();

        // Constructor runs automatically when object is created
        public Salaries()
        {
            // Add salary values into generic list
            // 'f' indicates float value
            _salaryList.Add(60000.34f);
            _salaryList.Add(40000.51f);
            _salaryList.Add(20000.23f);
        }

        // Method returns the salary list
        public List<float> GetSalaries()
        {
            return _salaryList;
        }
    }
}
