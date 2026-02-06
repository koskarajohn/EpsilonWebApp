// See https://aka.ms/new-console-template for more information

using EpsilonWebApp.Console;

var employee = new Employee()
{
    Name = "Employee 1"
};

var manager = new Manager()
{
    Name = "Manager 1"
};

Person.PrintName(employee);
Person.PrintName(manager);

Console.WriteLine("Hello, World!");