using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using WebApplication1.Models;

namespace WebApplication1.ModelViews
{
    public class HouseViewModel
    {
        public int Id { get; set; }


        [Display(Name = "Ev İsmini Giriniz :")]
        [Required(ErrorMessage = "Ev İsmini Giriniz!")]
        public string Name { get; set; }

        [Display(Name = "Ev Açıklamsını Giriniz :")]
        [Required(ErrorMessage = "Ev Açıklamsını Giriniz!")]
        public string Description { get; set; }


        [Display(Name = "1.Ev Resmini Seçiniz :")]
        [Required(ErrorMessage = "1.Ev Resmini Seçiniz!")]
        public IFormFile Resim1 { get; set; }


        [Display(Name = "2.Ev Resmini Seçiniz :")]
        [Required(ErrorMessage = "2.Ev Resmini Seçiniz!")]
        public IFormFile Resim2 { get; set; }





        public string Resim1imgsrc { get; set; }

        public string Resim2imgsrc { get; set; }


        [Display(Name = "Ev Satan Telefon Numarısını Giriniz :")]
        [Required(ErrorMessage = "Ev Satan Telefon Numarısını Giriniz !")]
        public string PersonPhoneNumber { get; set; }



        [Display(Name = "Adresini Giriniz :")]
        [Required(ErrorMessage = "Adresini Giriniz !")]
        public string Adress { get; set; }


        [Display(Name = "İlan Durumunu Seçiniz:")]
        [Required(ErrorMessage = "İlan Durumunu Seçiniz !")]
        public bool Status { get; set; }

       
        [Display(Name = "İlan Bölgesini Seçiniz:")]
        [Required(ErrorMessage = "İlan Bölgesini Seçiniz !")]
        public int CityId { get; set; }
    }
}
