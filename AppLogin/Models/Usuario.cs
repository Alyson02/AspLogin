//using MySql.Data.MySqlClient;
using MySqlConnector;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Configuration;
using System.Linq;
using System.Web;

namespace AppLogin.Models
{
    public class Usuario
    {
        public int UsuarioId { get; set; }

        [Required]
        [MaxLength(100)]
        public string UsuNome  { get; set; }

        [Required]
        [MaxLength(50)]
        public string Login { get; set; }

        [Required]
        [MaxLength(100)]
        public string Senha { get; set; }

        MySqlConnection conexao = new MySqlConnection(ConfigurationManager.ConnectionStrings["conexao"].ConnectionString);
        MySqlCommand command = new MySqlCommand();

        public void insert(Usuario usuario)
        {
            conexao.Open();
            command.CommandText = "call Insertusuario(@UsuNome, @Login, @Senha);";
            command.Parameters.Add("@UsuNome", MySqlDbType.VarChar).Value = usuario.UsuNome;
            command.Parameters.Add("@Login", MySqlDbType.VarChar).Value = usuario.Login;
            command.Parameters.Add("@Senha", MySqlDbType.VarChar).Value = usuario.Senha;
            command.Connection = conexao;
            command.ExecuteNonQuery();
            conexao.Close();
        }

        public string SelectLogin(string vLogin)
        {
            conexao.Open();
            command.CommandText = "call SelectLogin(@Login);";
            command.Parameters.Add("@Login", MySqlDbType.String).Value = vLogin;
            command.Connection = conexao;
            string Login = (string)command.ExecuteScalar();
            conexao.Close();
            if (Login == null)
                Login = "";
            return Login;
        }

        public Usuario SelectUsuario(string vLogin)
        {
            conexao.Open();
            command.CommandText = "call SelectUsuario(@Login);";
            command.Parameters.Add("@Login", MySqlDbType.String).Value = vLogin;
            command.Connection = conexao;

            var redUsuario = command.ExecuteReader();
            var TempUsuario = new Usuario();
            if (redUsuario.Read())
            {
                TempUsuario.UsuarioId = int.Parse(redUsuario["UsuarioID"].ToString());
                TempUsuario.UsuNome = redUsuario["UsuNome"].ToString();
                TempUsuario.Login = redUsuario["Login"].ToString();
                TempUsuario.Senha = redUsuario["Senha"].ToString();
            };
            redUsuario.Close();
            conexao.Close();
            return TempUsuario;
        }

        public void UpdateSenha(Usuario usuario)
        {
            conexao.Open();
            command.CommandText = "call UpdateSenha(@Login, @Senha);";
            command.Parameters.Add("@Login", MySqlDbType.VarChar).Value = usuario.Login;
            command.Parameters.Add("@Senha", MySqlDbType.VarChar).Value = usuario.Senha;
            command.Connection = conexao;
            command.ExecuteNonQuery();
            conexao.Close();
        }



    }
}