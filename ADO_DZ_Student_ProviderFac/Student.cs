using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ADO_DZ_Student
{
    class Student
    {
        public int Id { set; get; }
        public string Name { set; get; }
        public string Surname { set; get; }
        public int? IdGroup { set; get; }

        public override string ToString()
        {
            return Id + "  " + Name + "   " + Surname + "   " + IdGroup;
        }

    }
}
