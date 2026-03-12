using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductsWarehouse.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string Article { get; set; }        
        public string Name { get; set; }          
        public string Category { get; set; }        
        public string Unit { get; set; }            
        public decimal PurchasePrice { get; set; } 
        public int CurrentStock { get; set; }      
        public bool IsActive { get; set; }         
    }
}
