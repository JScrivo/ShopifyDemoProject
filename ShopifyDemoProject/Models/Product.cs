using System.ComponentModel.DataAnnotations;

namespace ShopifyDemoProject.Models
{
    public class Product
    {
        [Key]
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public float DefaultPrice { get; set; }

        public float VolPerUnit { get; set; }
    }
}
