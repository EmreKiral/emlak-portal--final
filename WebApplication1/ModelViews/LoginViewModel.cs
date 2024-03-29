﻿using System.ComponentModel.DataAnnotations;

namespace WebApplication1.ModelViews
{
	public class LoginViewModel
	{
		[Display(Name = "Kullanıcı Adı")]
		[Required(ErrorMessage = "Kullanıcı Adı Giriniz!")]
		public string UserName { get; set; }
		[Display(Name = "Parola")]
		[Required(ErrorMessage = "Parola Giriniz!")]
		public string Password { get; set; }

		[Display(Name = "Beni Hatırla")]
		public bool RememberMe { get; set; }
	}
}
