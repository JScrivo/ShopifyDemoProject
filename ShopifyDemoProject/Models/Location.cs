using System.ComponentModel.DataAnnotations;

namespace ShopifyDemoProject.Models
{
    public class Location
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public float Capacity { get; set; }
    }
}
