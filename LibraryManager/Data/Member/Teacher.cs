using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManager.Data.Member
{
    public class Teacher : Member
    {
        public Teacher()
        {
            this.Type = MemberType.Teacher;
        }
    }
}
