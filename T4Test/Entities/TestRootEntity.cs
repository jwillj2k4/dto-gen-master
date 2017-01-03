using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace T4Test.Entities
{
    public class TestRootEntity : RootAggregate
    {
        public string StringProperty { get; set; }
        public int IntProperty { get; set; }

        private DateTime mCreateDate;
        public DateTime CreateDate { get;}

        public string ShortCreateDate
        {
            get
            {
                return this.CreateDate.ToShortDateString();
            }
        }

        public TestChildDependency ComplexType { get; set; }

        public TestRootEntity()
        {
            mCreateDate = DateTime.Now;
        }
    }
}
