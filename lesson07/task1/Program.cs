using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace task1
{
    class Program
    {
        // Создайте пользовательский атрибут AccessLevelAttribute, позволяющий определить
        // уровень доступа пользователя к системе.Сформируйте состав сотрудников некоторой фирмы
        // в виде набора классов, например, Manager, Programmer, Director.При помощи атрибута
        // AccessLevelAttribute распределите уровни доступа персонала и отобразите на экране
        // реакцию системы на попытку каждого сотрудника получить доступ в защищенную секцию.
        static void Main(string[] args)
        {

            var employees = new Employee[] { new Manager(), new Programmer(), new Director() };

            foreach (var employee in employees)
            {
                Access(employee);
            }

            Console.ReadKey();
        }

        static void Access(Employee employee)
        {
            Type employeeType = employee.GetType();
            var attributes = employeeType.GetCustomAttributes(typeof(AccessLevelAttribute), false);

            foreach (AccessLevelAttribute attribute in attributes)
            {
                Console.WriteLine($"This employee is a {employeeType.Name}, access level: {attribute.AccessLevel}");
            }

        }
    }
}

