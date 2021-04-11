using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace task1
{
    enum AccessLevel
    {
        Max,
        Middle,
        Min
    }

    [AttributeUsage(AttributeTargets.Class, Inherited = false, AllowMultiple = false)]
    class AccessLevelAttribute : Attribute
    {
        readonly AccessLevel accessLevel;

        public AccessLevelAttribute(AccessLevel accessLevel)
        {
            this.accessLevel = accessLevel;
        }

        public AccessLevel AccessLevel
        {
            get { return accessLevel; }
        }
    }
}
