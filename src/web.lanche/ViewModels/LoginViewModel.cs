using System.ComponentModel.DataAnnotations;

namespace web.lanche.ViewModels
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "informe o nome")]
        [Display(Name ="Usuário")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Informe a senha")]
        [DataType(DataType.Password)]
        [Display(Name = "Senha")]
        public string Password { get; set; }
        
        public string ReturnUrl { get; set; }
    }
}