using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TestTarget
{
    class ObjectMethod
    {
        static string CallName { get; set; }


        public new static bool Equals(object obj)
        {
            CallName = "Equals-" + obj;
            return true;
        }

        public new static bool Equals(object objA, object objB)
        {
            CallName = "Equals-" + objA + "-" + objB;
            return true;
        }

        public new static int GetHashCode()
        {
            CallName = "GetHashCode";
            return 0;
        }

        public new static Type GetType()
        {
            CallName = "GetType";
            return typeof(int);
        }

        protected new static object MemberwiseClone()
        {
            CallName = "MemberwiseClone";
            return null;
        }

        public new static bool ReferenceEquals(object objA, object objB)
        {
            CallName = "ReferenceEquals-" + objA + "-" + objB;
            return false;
        }

        public new static string ToString()
        {
            CallName = "ToString";
            return string.Empty;
        }
    }
}
