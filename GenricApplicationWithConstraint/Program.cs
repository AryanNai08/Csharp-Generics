namespace GenricApplicationWithConstraint
{



    // ===============================================================
    // GENERIC BUBBLE SORT PROGRAM
    // Demonstrates:
    // 1. Generic class with constraint (where T : IComparable<T>)
    // 2. Sorting custom objects using Bubble Sort
    // 3. Interface IComparable for comparison logic
    // ===============================================================


    class Program
    {
        static void Main(string[] args)
        {
            // Create array of Employee objects
            // Each employee has Id and Name
            Employee[] arr = new Employee[]
            {
                new Employee { Id = 4, Name = "Aryan" },
                new Employee { Id = 2, Name = "Kartik" },
                new Employee { Id = 3, Name = "Vivek" },
                new Employee { Id = 1, Name = "ansh" }
            };

            // Create generic sorter object for Employee type
            SortArray<Employee> sortArray = new SortArray<Employee>();

            // Call bubble sort method
            sortArray.BubbleSort(arr);

            // Print sorted array
            foreach (object ar in arr)
            {
                Console.Write(ar + ", ");
            }

            Console.ReadKey();
        }
    }


    // ===============================================================
    // EMPLOYEE CLASS
    // Implements IComparable<Employee> so that objects can be compared
    // ===============================================================
    public class Employee : IComparable<Employee>
    {
        public int Id { get; set; }
        public string Name { get; set; }

        // CompareTo method defines HOW to compare employees
        // Here sorting is based on Name (alphabetically)
        public int CompareTo(Employee? other)
        {
            // Compare current object's Name with other object's Name
            return this.Name.CompareTo(other.Name);
        }

        // Override ToString() so printing object shows meaningful output
        public override string ToString()
        {
            return $"{Id} {Name}";
        }
    }


    // ===============================================================
    // GENERIC SORT CLASS
    // where T : IComparable<T>  → constraint
    // Means T must implement IComparable<T> interface
    // So we can call CompareTo() method
    // ===============================================================
    public class SortArray<T> where T : IComparable<T>
    {
        // Generic Bubble Sort method
        public void BubbleSort(T[] arr)
        {
            int n = arr.Length;

            // Outer loop for number of passes
            for (int i = 0; i < n - 1; i++)
            {
                // Inner loop for comparison
                for (int j = 0; j < n - i - 1; j++)
                {
                    // Compare two adjacent elements
                    // CompareTo returns:
                    // >0 → greater
                    // <0 → smaller
                    // 0  → equal
                    if (arr[j].CompareTo(arr[j + 1]) > 0)
                    {
                        // If current element greater → swap
                        Swap(arr, j);
                    }
                }
            }
        }

        // Swap two elements in array
        private void Swap(T[] arr, int j)
        {
            T temp = arr[j];
            arr[j] = arr[j + 1];
            arr[j + 1] = temp;
        }
    }
}

