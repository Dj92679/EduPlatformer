using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NumberSpawn : MonoBehaviour
{
    //pre-set variables
    public GameObject nums;
    public GameObject spawns;
    public GameObject chestLocations;
    public GameObject equationText;
    //Random rand = new Random();
    private string[] operations = { "+", "-", "x", "/" };
    public int difficulty;
    private int[] numbers = new int[151];
    public int solution;
    private int[] spawned_numbers = new int[10];
    public string operation;
    public int first;
    public int second;

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Level started");
        Spawn();
        //this.GetComponent<Text>.text = "__ " + operation + " __ = " + solution;
    }


    // Update is called once per frame
    void Update()
    {

    }

    public void Spawn() {
        switch(difficulty)
{
    case 1:
        {
            //switch case for different operators
            operation = operations[Random.Range(0, 2)];
            switch(operation)
            {
                case "+":
                    {
                        //reporting the equation type to make it more obvious on the console output
                        //Console.WriteLine("Equation type: " + operation);

                        //making solution and setting the answers
                        solution = Random.Range(2, 21);
                        first = Random.Range(0, solution + 1);
                        second = solution - first;

                        //checking the answer and solutions are working
                        //Console.WriteLine("Solution: " + solution + "\n" + "Num1: " + first + "\n" + "Num2: " + second);

                        spawned_numbers[0] = first;
                        spawned_numbers[1] = second;

                        //setting the rest of the numbers besides the correct answers
                        for (int i = 2; i < spawned_numbers.Length; i++)
                        {
                            //loop does not break until new numbers have been checked to not allow for extra solutions
                            bool check = true;
                            while (check)
                            {
                                int checks_failed = 0;
                                int num = Random.Range(0, 21);
                                for (int j = 0; j < spawned_numbers.Length; j++)
                                {
                                    if (num + spawned_numbers[j] == solution)
                                    {
                                        //Console.WriteLine("check failed");
                                        checks_failed++;
                                    }
                                }
                                //will only happen if the new number cannot make the answer with any of the other numbers
                                if (checks_failed == 0)
                                {
                                    spawned_numbers[i] = num;
                                    check = false;
                                }
                            }
                        }

                        //checking the list of numbers is working
                        for (int k = 0; k < spawned_numbers.Length; k++)
                        {
                            //Console.WriteLine(spawned_numbers[k]);
                        }
                        break;
                    }

                case "-":
                    {
                        //Console.WriteLine("Equation type: " + operation);

                        solution = Random.Range(0, 11);
                        first = Random.Range(solution, 21);
                        second = first - solution;

                        //Console.WriteLine("Solution: " + solution + "\n" + "Num1: " + first + "\n" + "Num2: " + second);

                        spawned_numbers[0] = first;
                        spawned_numbers[1] = second;

                        for (int i = 2; i < spawned_numbers.Length; i++)
                        { 
                            bool check = true;
                            while (check)
                            {
                                int checks_failed = 0;
                                int num = Random.Range(0, 21);
                                for (int j = 0; j < spawned_numbers.Length; j++)
                                {
                                    if (num - spawned_numbers[j] == solution || spawned_numbers[j] - num == solution)
                                    {
                                        //Console.WriteLine("check failed");
                                        checks_failed++;
                                    }
                                }
                                if (checks_failed == 0)
                                {
                                    spawned_numbers[i] = num;
                                    check = false;
                                }
                            }
                        }

                        for(int k = 0; k < spawned_numbers.Length; k++)
                        {
                            //Console.WriteLine(spawned_numbers[k]);
                        }
                        break;
                    }
            }
            break;
        }
    case 2:
        {
            string operation = operations[Random.Range(0, 4)];

            switch (operation)
            {
                case "+":
                    {
                        //Console.WriteLine("Equation type: " + operation);

                        solution = Random.Range(10, 101);
                        first = Random.Range(0, solution + 1);
                        second = solution - first;

                        //Console.WriteLine("Solution: " + solution + "\n" + "Num1: " + first + "\n" + "Num2: " + second);

                        spawned_numbers[0] = first;
                        spawned_numbers[1] = second;

                        for (int i = 2; i < spawned_numbers.Length; i++)
                        {
                            bool check = true;
                            while (check)
                            {
                                int checks_failed = 0;
                                int num = Random.Range(0, 101);
                                for (int j = 0; j < spawned_numbers.Length; j++)
                                {
                                    if (num + spawned_numbers[j] == solution)
                                    {
                                        //Console.WriteLine("check failed");
                                        checks_failed++;
                                    }
                                }
                                if (checks_failed == 0)
                                {
                                    spawned_numbers[i] = num;
                                    check = false;
                                }
                            }
                        }

                        for (int k = 0; k < spawned_numbers.Length; k++)
                        {
                            //Console.WriteLine(spawned_numbers[k]);
                        }
                        break;
                    }

                case "-":
                    {
                        //Console.WriteLine("Equation type: " + operation);

                        solution = Random.Range(0, 51);
                        first = Random.Range(solution, 101);
                        second = first - solution;

                        //Console.WriteLine("Solution: " + solution + "\n" + "Num1: " + first + "\n" + "Num2: " + second);

                        spawned_numbers[0] = first;
                        spawned_numbers[1] = second;

                        for (int i = 2; i < spawned_numbers.Length; i++)
                        {
                            bool check = true;
                            while (check)
                            {
                                int checks_failed = 0;
                                int num = Random.Range(0, 101);
                                for (int j = 0; j < spawned_numbers.Length; j++)
                                {
                                    if (num - spawned_numbers[j] == solution || spawned_numbers[j] - num == solution)
                                    {
                                        //Console.WriteLine("check failed");
                                        checks_failed++;
                                    }
                                }
                                if (checks_failed == 0)
                                {
                                    spawned_numbers[i] = num;
                                    check = false;
                                }
                            }
                        }

                        for (int k = 0; k < spawned_numbers.Length; k++)
                        {
                            //Console.WriteLine(spawned_numbers[k]);
                        }
                        break;
                    }

                case "x":
                    {
                        //Console.WriteLine("Equation type: " + operation);

                        solution = Random.Range(0, 101);
                        first = Random.Range(0, 101);

                        //list of all the possiblle factors of the solution
                        List<int> factors = new List<int>();
                        //checking the number isnt a prime number
                        while (factors.Count  < 1)
                        {
                            solution = Random.Range(0, 101);
                            for (int i = 1; i < 11; i++)
                            {
                                if(solution == 0)
                                {
                                    factors.Add(0);
                                }
                                else if(solution % i == 0)
                                {
                                    //checking if the other product of this equation would be over 10
                                    if(solution / i < 11)
                                    {
                                        factors.Add(i);
                                    }
                                }
                                //Console.WriteLine("inner loop played");
                            }
                            //Console.WriteLine("outer loop played");
                        }
                        //if the number generated is 0 it will just give another random number from 0-10
                        if(solution == 0)
                        {
                            first = 0;
                            second = Random.Range(0, 11);
                        }
                        //otherwise choose a random factor and the quotient of it and the solution
                        else
                        {
                            first = factors[Random.Range(0, factors.Count)];
                            second = solution / first;
                        }

                        //Console.WriteLine("Solution: " + solution + "\n" + "Num1: " + first + "\n" + "Num2: " + second);

                        spawned_numbers[0] = first;
                        spawned_numbers[1] = second;

                        for (int i = 2; i < spawned_numbers.Length; i++)
                        {
                            bool check = true;
                            while (check)
                            {
                                int checks_failed = 0;
                                int num = Random.Range(0, 11);
                                for (int j = 0; j < spawned_numbers.Length; j++)
                                {
                                    if (num * spawned_numbers[j] == solution)
                                    {
                                        //Console.WriteLine("check failed");
                                        checks_failed++;
                                    }
                                }
                                if (checks_failed == 0)
                                {
                                    spawned_numbers[i] = num;
                                    check = false;
                                }
                            }
                        }

                        for (int k = 0; k < spawned_numbers.Length; k++)
                        {
                            //Console.WriteLine(spawned_numbers[k]);
                        }
                        break;
                    }

                case "/":
                    {
                        //Console.WriteLine("Equation type: " + operation);

                        //lower limit is 1 becuase you cannot divive by 0
                        solution = Random.Range(1, 11);
                        first = Random.Range(1, 11);
                        second = solution * first;

                        //Console.WriteLine("Solution: " + solution + "\n" + "Num1: " + first + "\n" + "Num2: " + second);

                        spawned_numbers[0] = first;
                        spawned_numbers[1] = second;

                        for (int i = 2; i < spawned_numbers.Length; i++)
                        {
                            bool check = true;
                            while (check)
                            {
                                int checks_failed = 0;
                                int num = Random.Range(1, 11);
                                for (int j = 0; j < spawned_numbers.Length; j++)
                                {
                                    if (num == 0 || num / spawned_numbers[j] == solution || spawned_numbers[j] / num == solution)
                                    {
                                        //Console.WriteLine("check failed");
                                        checks_failed++;
                                    }
                                }
                                if (checks_failed == 0)
                                {
                                    spawned_numbers[i] = num;
                                    check = false;
                                }
                            }
                        }

                        for (int k = 0; k < spawned_numbers.Length; k++)
                        {
                            //Console.WriteLine(spawned_numbers[k]);
                        }
                        break;
                    }
            }
            break;
        }
    case 3:
        {
            string operation = operations[Random.Range(0, 4)];
            switch (operation)
            {
                case "+":
                    {
                        //Console.WriteLine("Equation type: " + operation);

                        solution = Random.Range(72, 145);
                        first = Random.Range(0, solution + 1);
                        second = solution - first;

                        //Console.WriteLine("Solution: " + solution + "\n" + "Num1: " + first + "\n" + "Num2: " + second);

                        spawned_numbers[0] = first;
                        spawned_numbers[1] = second;

                        for (int i = 2; i < spawned_numbers.Length; i++)
                        {
                            bool check = true;
                            while (check)
                            {
                                int checks_failed = 0;
                                int num = Random.Range(0, 145);
                                for (int j = 0; j < spawned_numbers.Length; j++)
                                {
                                    if (num + spawned_numbers[j] == solution)
                                    {
                                        //Console.WriteLine("check failed");
                                        checks_failed++;
                                    }
                                }
                                if (checks_failed == 0)
                                {
                                    spawned_numbers[i] = num;
                                    check = false;
                                }
                            }
                        }

                        for (int k = 0; k < spawned_numbers.Length; k++)
                        {
                            //Console.WriteLine(spawned_numbers[k]);
                        }
                        break;
                    }

                case "-":
                    {
                        //Console.WriteLine("Equation type: " + operation);

                        solution = Random.Range(0, 73);
                        first = Random.Range(solution, 145);
                        second = first - solution;

                        //Console.WriteLine("Solution: " + solution + "\n" + "Num1: " + first + "\n" + "Num2: " + second);

                        spawned_numbers[0] = first;
                        spawned_numbers[1] = second;

                        for (int i = 2; i < spawned_numbers.Length; i++)
                        {
                            bool check = true;
                            while (check)
                            {
                                int checks_failed = 0;
                                int num = Random.Range(0, 145);
                                for (int j = 0; j < spawned_numbers.Length; j++)
                                {
                                    if (num - spawned_numbers[j] == solution || spawned_numbers[j] - num == solution)
                                    {
                                        //Console.WriteLine("check failed");
                                        checks_failed++;
                                    }
                                }
                                if (checks_failed == 0)
                                {
                                    spawned_numbers[i] = num;
                                    check = false;
                                }
                            }
                        }

                        for (int k = 0; k < spawned_numbers.Length; k++)
                        {
                            //Console.WriteLine(spawned_numbers[k]);
                        }
                        break;
                    }

                case "x":
                    {
                        //Console.WriteLine("Equation type: " + operation);

                        solution = Random.Range(0, 145);
                        first = Random.Range(0, 145);

                        List<int> factors = new List<int>();

                        while (factors.Count < 1)
                        {
                            solution = Random.Range(0, 145);
                            for (int i = 1; i < 13; i++)
                            {
                                if (solution == 0)
                                {
                                    factors.Add(0);
                                }
                                else if (solution % i == 0)
                                {
                                    if (solution / i < 13)
                                    {
                                        factors.Add(i);
                                    }
                                }
                            }
                        }
                        if (solution == 0)
                        {
                            first = 0;
                            second = Random.Range(0, 13);
                        }
                        else
                        {
                            first = factors[Random.Range(0, factors.Count)];
                            second = solution / first;
                        }

                        //Console.WriteLine("Solution: " + solution + "\n" + "Num1: " + first + "\n" + "Num2: " + second);

                        spawned_numbers[0] = first;
                        spawned_numbers[1] = second;

                        for (int i = 2; i < spawned_numbers.Length; i++)
                        {
                            bool check = true;
                            while (check)
                            {
                                int checks_failed = 0;
                                int num = Random.Range(0, 13);
                                for (int j = 0; j < spawned_numbers.Length; j++)
                                {
                                    if (num * spawned_numbers[j] == solution)
                                    {
                                        //Console.WriteLine("check failed");
                                        checks_failed++;
                                    }
                                }
                                if (checks_failed == 0)
                                {
                                    spawned_numbers[i] = num;
                                    check = false;
                                }
                            }
                        }

                        for (int k = 0; k < spawned_numbers.Length; k++)
                        {
                            //Console.WriteLine(spawned_numbers[k]);
                        }
                        break;
                    }

                case "/":
                    {
                        //Console.WriteLine("Equation type: " + operation);

                        solution = Random.Range(1, 13);
                        first = Random.Range(1, 13);
                        second = solution * first;

                        //Console.WriteLine("Solution: " + solution + "\n" + "Num1: " + first + "\n" + "Num2: " + second);

                        spawned_numbers[0] = first;
                        spawned_numbers[1] = second;

                        for (int i = 2; i < spawned_numbers.Length; i++)
                        {
                            bool check = true;
                            while (check)
                            {
                                int checks_failed = 0;
                                int num = Random.Range(1, 13);
                                for (int j = 0; j < spawned_numbers.Length; j++)
                                {
                                    if (num == 0 || num / spawned_numbers[j] == solution || spawned_numbers[j] / num == solution)
                                        {
                                            //Console.WriteLine("check failed");
                                            checks_failed++;
                                        }
                                }
                                if (checks_failed == 0)
                                {
                                    spawned_numbers[i] = num;
                                    check = false;
                                }
                            }
                        }

                        for (int k = 0; k < spawned_numbers.Length; k++)
                        {
                            //Console.WriteLine(spawned_numbers[k]);
                        }
                        break;
                    }
            }
            break;
        }
    }
    for(int i = 0; i < spawned_numbers.Length; i++) {
        Instantiate(nums.transform.GetChild(spawned_numbers[i]), spawns.transform.GetChild(i).transform.position, Quaternion.identity);
    }
    //this.GetComponent<Text>.text = "";
    }
}
