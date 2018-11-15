GO
USE MASTER

GO
IF EXISTS (SELECT name FROM master.dbo.sysdatabases WHERE name = 'testeChat')
DROP DATABASE testeChat

go
CREATE DATABASE testeChat

go
use testeChat

--create table message(
--	id int identity primary key,
--	text varchar(300)
--)

--insert into message values ('s')


--insert into message values ('uaaau')

--select * from message where id > 0

create table usuario(
	idusuario int identity primary key,
	username varchar(50)
)

create table mensagem(
	idmensagem int identity primary key,
	id_sender int foreign key
		references usuario(idusuario),
	id_reciever int foreign key
		references usuario(idusuario),
	text varchar(300)
)


insert into usuario values('root'),('perrella'),('asonso')

insert into mensagem values(1,2,'saaalve')

select * from mensagem where idmensagem >1