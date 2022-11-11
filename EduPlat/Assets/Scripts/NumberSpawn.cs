using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class NumberSpawn : MonoBehaviour
{
    //pre-set variables
    public GameObject nums; //this is a gameObject containing all the prefabs for the numbers in the game. this is important because the prefabs all have a script containing the int value of the number itself which is referred to in CharacterController2D script.
    public GameObject spawns; //this is the gameObject containing information on where the numbers can spawn
    public GameObject chestLocations; //this is a gameobject that contains all the chestlocations so the chest can be moved between the positions
    public GameObject spawnedNums; //this is a gameobject which the spawned numbers will all be children of. they are grouped for easier deletion and re-implementation
    public GameObject equationText; //this gameobject is the object which has the textbox that displays the equation
    private string[] operations = { "+", "-", "x", "/" }; //simple list of mathermical operations which is used to decide which operation to use
    public int difficulty; //this is an int value derived from a manually assigned value on each level
    public int solution; //this is the value of the solution that the player will have to work towards
    private List<int> positions; //this is a temp lsit used to decide which position each new number will take
    private int[] spawned_numbers; //this is the list of numbers that will be spawned
    public string operation; //this is the operation type that will be decided via a random number generator
    public int first; //this is the first number that will make the solution
    public int second; //this is the second number to make the solution

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Level started");
        Spawn();
    }

    void Addition(int max) { 
    //making solution and setting the answers
    solution = Random.Range(2, 21);
    first = Random.Range(0, solution + 1);
    second = solution - first;

    //adding first and second to array
    spawned_numbers[0] = first;
    spawned_numbers[1] = second;

    //setting the rest of the numbers besides the correct answers
    for (int i = 2; i < spawned_numbers.Length; i++)
    {
        //loop does not break until new numbers have been checked to not allow for extra solutions
        bool check = true;
        while (check)
        {
            int checks_failed = 0; //resetting checks

            int num = Random.Range(0, max + 1); //making new random number to test
            for (int j = 0; j < spawned_numbers.Length; j++)
            {
                //enters if statement if number can make solution with any other number
                if (num + spawned_numbers[j] == solution)
                {
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
    }

    //called to decide on what type of equation will happen. max decides how many operations to consider
    void OperationGenerator(int max, string[] operations) {
        operation = operations[Random.Range(0, max)];
    }

    //this method is the main function of the script which chooses the operation, 
    public void Spawn() {
        if (SceneManager.GetActiveScene().name == "Tutorial")
        {
            //tutorial only has 2 number spawn gameobjects so array can only have size of 2 otherwise indexoutofarrayerror will occur
            positions = new List<int>() { 0, 1}; 
            spawned_numbers = new int[2]; 
            Debug.Log("Tutorial Level");
        } else {
            positions = new List<int>() { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9 };
            spawned_numbers = new int[10];
        }
    switch(difficulty)
    {
        case 1:
        {
            //switch case for different operators
            OperationGenerator(2, operations);
            switch(operation)
            {
                case "+":
                {
                    Addition(20);
                    break; 
                }

                case "-":
                {
                    Addition(20);
                    break;
                }
            }
            break;
        }
        case 2:
        {
            operation = operations[Random.Range(0, 4)];

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
                        second = Random.Range(1, 11);
                        first = solution * second;

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
                                    try {
                                        if (num / spawned_numbers[j] == solution || spawned_numbers[j] / num == solution)
                                        {
                                            //Console.WriteLine("check failed");
                                            checks_failed++;
                                        }
                                    }
                                    catch(System.DivideByZeroException) {
                                        continue;
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
            operation = operations[Random.Range(0, 4)];
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
                        second = Random.Range(1, 13);
                        first = solution * second;

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
                                    try {
                                        if (num / spawned_numbers[j] == solution || spawned_numbers[j] / num == solution)
                                        {
                                            //Console.WriteLine("check failed");
                                            checks_failed++;
                                        }
                                    }
                                    catch(System.DivideByZeroException) {
                                        continue;
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
        if (SceneManager.GetActiveScene().name == "Tutorial")
        {  if (spawnedNums.transform.childCount > 1)
            {
                Destroy(spawnedNums.transform.GetChild(0).gameObject);
                Destroy(spawnedNums.transform.GetChild(1).gameObject);
            }

        } else if (spawnedNums.transform.childCount > 1) {
        Destroy(spawnedNums.transform.GetChild(0).gameObject);
        Destroy(spawnedNums.transform.GetChild(1).gameObject);
        Destroy(spawnedNums.transform.GetChild(2).gameObject);
        Destroy(spawnedNums.transform.GetChild(3).gameObject);
        Destroy(spawnedNums.transform.GetChild(4).gameObject);
        Destroy(spawnedNums.transform.GetChild(5).gameObject);
        Destroy(spawnedNums.transform.GetChild(6).gameObject);
        Destroy(spawnedNums.transform.GetChild(7).gameObject);
        Destroy(spawnedNums.transform.GetChild(8).gameObject);
        Destroy(spawnedNums.transform.GetChild(9).gameObject);
    }
    for(int i = 0; i < spawned_numbers.Length; i++) {
        int choice = Random.Range(0, positions.Count);
        int pos = positions[choice];
        positions.RemoveAt(choice);
        Instantiate(nums.transform.GetChild(spawned_numbers[i]), spawns.transform.GetChild(pos).transform.position, Quaternion.identity, spawnedNums.transform);
    }
    equationText.GetComponent<TMP_Text>().text = "Equation:   " + operation + "   = " + solution.ToString();
    }
}
