//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан по шаблону.
//
//     Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//     Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Castle
{
    using System;
    using System.Collections.Generic;
    
    public partial class Product
    {
        public int ProductID { get; set; }
        public string ProductName { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public int CategoryID { get; set; }
        public int SuppliersID { get; set; }
        public string Comment { get; set; }
        public Nullable<int> PhotoID { get; set; }
    
        public virtual Categories Categories { get; set; }
        public virtual Photos Photos { get; set; }
        public virtual Suppliers Suppliers { get; set; }
    }
}
