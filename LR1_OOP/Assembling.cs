using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace LR1_OOP
{ 
    class Assembling
    {
        public static class ReflectiveEnumerator
        {
            public static List<Type> GetEnumerableOfType<T>(Assembly asm) where T : class
            {
                List<Type> classes = new List<Type>();
                foreach (Type type in asm.GetTypes().Where(myType => myType.IsClass && !myType.IsAbstract && myType.IsSubclassOf(typeof(T))))
                {
                    classes.Add(type);
                }
                return classes;
            }
        }

    }
}
