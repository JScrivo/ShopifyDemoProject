using System.ComponentModel.DataAnnotations;

namespace ShopifyDemoProject.Models
{
    public class Price
    {
        public int ItemID { get; set; }
        public int LocationID { get; set; }
        public float UnitPrice { get; set; }
    }
}
