namespace EpsilonWebApp.Console;

public class Person
{
    public string Name { get; set; }

    public static void PrintName(Person? person)
    {
        if (person is null) return;
        System.Console.WriteLine("Person name is " + person.Name);
    }
}

public class Employee : Person
{
    
}

public class Manager : Person
{
 
}