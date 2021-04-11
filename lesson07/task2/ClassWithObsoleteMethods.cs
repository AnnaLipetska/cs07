using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace task2
{
    class ClassWithObsoleteMethods
    {
        [Obsolete("This method is obsolete"/*, false*/)]
        public void ObsoleteMethod()
        {
            Console.WriteLine("Obsolete method warning");
        }

        [Obsolete("This method is not used anymore", true)]
        public void ReallyObsoleteMethod()
        {
            Console.WriteLine("Not compiling method");
        }
    }
}
