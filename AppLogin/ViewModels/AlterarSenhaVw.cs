using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AppLogin.ViewModels
{
    public class AlterarSenhaVw
    {
        [Display(Name = "Senha Atual")]
        [Required(ErrorMessage = "Informe a senha")]
        [MinLength(6, ErrorMessage = "A senha deve ter ao menos 6 caracteres")]
        [DataType(DataType.Password)]
        public string SenhaAtual { get; set; }

        [Display(Name = "Nova Senha")]
        [Required(ErrorMessage = "Informe a senha")]
        [MinLength(6, ErrorMessage = "A senha deve ter ao menos 6 caracteres")]
        [DataType(DataType.Password)]
        public string NovaSenha { get; set; }

        [Display(Name = "Confirmar Senha")]
        [Required(ErrorMessage = "Confirme a senha")]
        [Compare(nameof(NovaSenha), ErrorMessage = "A senha são diferentes")]
        [DataType(DataType.Password)]
        public string ConfirmarSenha { get; set; }
    }
}