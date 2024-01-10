using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication1.Models
{
    public class House
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public string Resim1 { get; set; }

        public string Resim2 { get; set; }
        
        public string PersonPhoneNumber { get; set; }

        public string Adress { get; set; }

        public bool Status { get; set; }

        [ForeignKey(nameof(City))]
        public int CityId { get; set; }

        public City City { get; set; }
    }
}

