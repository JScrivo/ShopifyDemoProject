using System.ComponentModel.DataAnnotations;

namespace ShopifyDemoProject.Models
{
    public class Inventory
    {
        public int ProductID { get; set; }
        public int LocationID { get; set; }
        public int Quantity { get; set; }
    }
}
