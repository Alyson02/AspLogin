create database dbAutorizacaoNoite;

use dbAutorizacaoNoite;


create table tbUsuario(
UsuarioID int primary key auto_increment,
UsuNome varchar(100) not null unique,
Login varchar(50)  not null unique,
Senha varchar(100) not null
);





select UsuarioID, UsuNome, Login, Senha from tbusuario;

delimiter $$
create procedure Insertusuario(vUsuNome varchar(100), vLogin varchar(50), vSenha varchar(100))
begin 
insert into tbUsuario(UsuNome, Login, Senha) 
			values(vUsuNome, vLogin, vSenha);
end$$

delimiter $$
create procedure SelectLogin(vLogin varchar(50))
begin 
select Login from tbUsuario where Login = vLogin;
end$$

delimiter $$
create procedure SelectUsuario(vLogin varchar(50))
begin 
select * from tbUsuario where Login = vLogin;
end$$

delimiter $$
create procedure UpdateSenha(vLogin varchar(50), vSenha varchar(100))
begin 
update tbUsuario set Senha = vSenha where Login = vLogin;
end$$

