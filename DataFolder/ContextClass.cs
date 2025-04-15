using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Castle.DataFolder
{
    public partial class Amo_CastleEntities1 : DbContext
    {
        public static Amo_CastleEntities1 _context;
        public static Amo_CastleEntities1 GetContext()
        {
            if (_context == null)
            {
                _context = new Amo_CastleEntities1();
            }
            return _context;
        }
    }
}