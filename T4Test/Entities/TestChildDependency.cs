using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace T4Test.Entities
{
    public class TestChildDependency
    {
        public string Name { get; set; }

        public string foo { get; }

        public TestChildChildDependency ChildOfAChild { get; set; }
    }
}
