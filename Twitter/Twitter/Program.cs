using System;
using System.Security.Cryptography;

class Program
{
    static void Main()
    {
        int userChoice = PrintMenu();

        while (userChoice != 3)
        {
            if (userChoice == 1)
            {
                CalculateRectangleArea();
            }
            else if (userChoice == 2)
            {
               
                
                CalculateTriangleProperties();
            }

            userChoice = PrintMenu();
        }
    }

    static int PrintMenu()
    {
        Console.WriteLine("menu:");
        Console.WriteLine("Select an option:");
        Console.WriteLine("1. Choosing a rectangle tower");
        Console.WriteLine("2. Choosing a triangle tower");
        Console.WriteLine("3.Exit the program");

        int userChoice = int.Parse(Console.ReadLine());
        return userChoice;
    }

    static void CalculateRectangleArea()
    {
        Console.Write("Enter length of rectangle: ");
        int length = int.Parse(Console.ReadLine());
        Console.Write("Enter width of rectangle: ");
        int width = int.Parse(Console.ReadLine());
        if  ((length - width) > 5)
        Console.WriteLine("The area of the rectangle is: " + (length * width));
        else Console.WriteLine((length *2+ width*2));
    }

    static void CalculateTriangleProperties()
    {
        Console.Write("Enter base length of triangle: ");
        int width = int.Parse(Console.ReadLine());
        Console.Write("Enter height of triangle: ");
        int height = int.Parse(Console.ReadLine());

        Console.WriteLine("Choose operation:");
        Console.WriteLine("1. Calculate perimeter");
        Console.WriteLine("2. Print triangle");
        int choice = int.Parse(Console.ReadLine());

        if (choice == 1)
        {
            int perimeter = (2 * height) + width;/*לבדוק*/
            Console.WriteLine("The perimeter of the triangle is: " + perimeter);
        }
        else if (choice == 2)
        {
            if (width % 2 == 0 || width >= (2 * height))
            {
                Console.WriteLine("Error: Cannot print triangle with given dimensions.");
            }
            else
            {
                    if(width>3) 
                         PrintTriangle(width, height);

                      else PrintTinyTriangle(height - 2, width); //טיפול בחריגים
            }
        }
    }

    static void PrintTriangle(int width, int height)   //הדפסת משולש
    {
        int cnt = 3;
        
        Console.WriteLine(new string(' ', (width - 1) / 2) + "*");

        int odd = (width - 2) / 2; //מספר האי זוגיים
        int row = (height - 2) / odd;//מספר החזרות על כל שורה
        int plus = (height - 2) % odd;//שארית החלוקה

        if (plus != 0)
        {
            for (int k = 0; k < plus; k++)
            {
                Console.WriteLine(new string(' ', ((width - 1) / 2) - 1) + new string('*', cnt));
            }
        }

        for (int i = 0; i < odd; i++)
        {
            for (int j = 0; j < row; j++)
            {
                Console.WriteLine(new string(' ', ((width - 1) / 2) - (i + 1)) + new string('*', cnt));
            }
            cnt += 2;
        }
        Console.WriteLine(new string('*', width));
    }



    static void PrintTinyTriangle(int n, int width)//הדפסת משולש חריג
    {
        if (width == 3) { 
            for (int i = 0; i <= n; i++)
            {
                Console.WriteLine(new string(' ', 1) + "*");
            }
        Console.WriteLine( new string('*', 3)); 
       }
        else

            for (int i = 0; i < n; i++)
            {
                Console.WriteLine("*");
            }


    }


}

