using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ProductsWarehouse.Models
{
    public class Shipment
    {
        public int Id { get; set; }
        public string DocumentNumber { get; set; }   
        public DateTime DocumentDate { get; set; }   
        public string Status { get; set; }           
        public int CreatedByUserId { get; set; }     
        public DateTime CreatedAt { get; set; }     
        public List<ShipmentItem> Items { get; set; } = new List<ShipmentItem>();
    }

    public class ShipmentItem
    {
        public int Id { get; set; }
        public int ShipmentId { get; set; }
        public int ProductId { get; set; }
        public string ProductName { get; set; }       
        public string ProductArticle { get; set; }   
        public int Quantity { get; set; }            
        public decimal Price { get; set; }            
    }
}