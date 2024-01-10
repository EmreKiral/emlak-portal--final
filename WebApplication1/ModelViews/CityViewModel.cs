using System.ComponentModel.DataAnnotations.Schema;
using WebApplication1.Models;

namespace WebApplication1.ModelViews
{
    public class CityViewModel
    {
        public int Id { get; set; }
        public string SehirAdi { get; set; }
        public string IlceAdi { get; set; }

    }
}
