using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace task1
{
    class Employee
    {

    }

    [AccessLevel(AccessLevel.Min)]
    class Manager : Employee
    {

    }

    [AccessLevel(AccessLevel.Middle)]
    class Programmer : Employee
    {

    }

    [AccessLevel(AccessLevel.Max)]
    class Director : Employee
    {

    }
}
