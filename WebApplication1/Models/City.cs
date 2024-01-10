using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication1.Models
{
    public class City
    {
        public int Id { get; set; }
        public string SehirAdi { get; set; }
        public ICollection<House> House { get; set;}
    }
}
