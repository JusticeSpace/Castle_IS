using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Castle.SomeCode
{
    public class Supplier
    {
        [Key]
        public int SupplierID { get; set; } // Было SuppliersID → исправлено

        public string SupplierName { get; set; }
        public string ContactPerson { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public int AddressID { get; set; }
    }
}
