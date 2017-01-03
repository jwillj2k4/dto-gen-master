using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace T4Test
{
    using System.Reflection;

    class Program
    {
        static void Main(string[] args)
        {
            GetEntities();
        }

        private static Assembly GetLoadedAssembly()
        {
            var asm = Assembly.GetExecutingAssembly();

            return asm;
        }

        private static List<string> GetEntities()
        {
            List<string> tmpReturn = new List<string>();
            Assembly proj = GetLoadedAssembly();

            foreach (Type tmpEnt in proj.GetTypes())
            {
                if (tmpEnt.BaseType.Name == "T4Test.RootAggregate")
                    tmpReturn.Add(tmpEnt.Name);
            }
            return tmpReturn;
        }
    }
}
