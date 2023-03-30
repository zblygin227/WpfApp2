using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp2
{
    internal class StudentsContext : StudentsEntities
    {
        private static StudentsContext _context;

        public static StudentsContext GetContext()
        {
            if(_context == null) _context = new StudentsContext();
            return _context;
        }
    }
}
