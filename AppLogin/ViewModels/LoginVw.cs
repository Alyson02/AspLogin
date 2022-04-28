using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AppLogin.ViewModels
{
    public class LoginVw
    {
        public string UrlRetorno { get; set; }

        [Required(ErrorMessage = "Informe o loign")]
        [MaxLength(100, ErrorMessage = "O login deve ter até 50 caracteres")]
        public string Login { get; set; }

        [Required(ErrorMessage = "Informe a senha")]
        [MaxLength(100, ErrorMessage = "A senha deve ter pelo menos 6 caracteres")]
        [DataType(DataType.Password)] 
        public string Senha { get; set; }


    }
}