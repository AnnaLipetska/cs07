using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace task2
{
    class Program
    {
        // Создайте класс и примените к его методам атрибут Obsolete сначала в форме, просто
        // выводящей предупреждение, а затем в форме, препятствующей компиляции.
        // Продемонстрируйте работу атрибута на примере вызова данных методов.
        static void Main(string[] args)
        {
            var obsolete = new ClassWithObsoleteMethods();

            obsolete.ObsoleteMethod();

            // obsolete.ReallyObsoleteMethod();

            Console.ReadKey();
        }
    }
}
