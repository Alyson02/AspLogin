using AppLogin.Models;
using AppLogin.Utils;
using AppLogin.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Security.Claims;

namespace AppLogin.Controllers
{
    public class AutenticacaoController : Controller
    {
        // GET: Autenticacao get por padrao
        [HttpGet]
        public ActionResult Insert()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Insert(CadastroUsuarioViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(viewModel);
            }

            Usuario usuario = new Usuario{
                UsuNome = viewModel.UsuNome,
                Login = viewModel.Login,
                Senha = Hash.GerarHash(viewModel.Senha)
            };
            usuario.insert(usuario);

            TempData["MensagemLogin"] = "Cadastro realizado com sucesso! faça o login.";

            return RedirectToAction("Login","Autenticacao");
        }

        public ActionResult LoginBusca(string Login)
        {
            bool LoginExists;

            var usuario = new Usuario();
            string login = usuario.SelectLogin(Login);

            if (login.Length == 0)
                LoginExists = false;
            else
                LoginExists = true;

            return Json(!LoginExists, JsonRequestBehavior.AllowGet); 
        }

        public ActionResult Login(string ReturnUrl)
        {
            var viewmodel = new LoginVw
            {
                UrlRetorno = ReturnUrl
            };
            return View(viewmodel);
        }

        [HttpPost]
        public ActionResult Login(LoginVw viewmodel)
        {
            if (!ModelState.IsValid)
            {
                return View(viewmodel);
            }

            Usuario usuario = new Usuario();
            usuario = usuario.SelectUsuario(viewmodel.Login);

            if(usuario == null | usuario.Login != viewmodel.Login)
            {
                ModelState.AddModelError("Login", "Login Incorreto");
                return View(viewmodel);
            }
            if(usuario.Senha != Hash.GerarHash(viewmodel.Senha))
            {
                ModelState.AddModelError("Senha", "Senha incorreta");
                return View(viewmodel);
            }


            var identity = new ClaimsIdentity(new[]
            {
                new Claim(ClaimTypes.Name,usuario.Login),
                new Claim("Login",usuario.Login)
            }, "AppAplicationCookie");

            Request.GetOwinContext().Authentication.SignIn(identity);

            if (!String.IsNullOrWhiteSpace(viewmodel.UrlRetorno) || Url.IsLocalUrl(viewmodel.UrlRetorno))
                return Redirect(viewmodel.UrlRetorno);
            else
                return RedirectToAction("Index", "Home");
        }

        public ActionResult Logout()
        {
            Request.GetOwinContext().Authentication.SignOut("AppAplicationCookie");
            return RedirectToAction("Index", "Home");
        }

        public ActionResult AlterarSenha()
        {
            return View();
        }

        [Authorize]
        [HttpPost]
        public ActionResult AlterarSenha(AlterarSenhaVw viewmodel)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            var identity = User.Identity as ClaimsIdentity;
            var login = identity.Claims.FirstOrDefault(c => c.Type == "Login").Value;

            Usuario usuario = new Usuario();
            usuario = usuario.SelectUsuario(login);

            if(Hash.GerarHash(viewmodel.NovaSenha) == usuario.Senha)
            {
                ModelState.AddModelError("SenhaAtual", "Senha incorreta");
                return View();
            }

            usuario.Senha = Hash.GerarHash(viewmodel.NovaSenha);

            usuario.UpdateSenha(usuario);

            TempData["MensagemLogin"] = "Senha Alterada com sucesso";

            return RedirectToAction("Index", "Home");
        }

    }

       
}