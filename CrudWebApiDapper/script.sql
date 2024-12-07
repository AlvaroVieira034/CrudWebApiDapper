create table Usuarios (
	Id int IDENTITY(1,1) PRIMARY KEY,
	NomeCompleto varchar(100),
	Email varchar(100),
	Cargo varchar(100),
	Salario decimal,
	CPF varchar(11),
	Situacao bit,
	Senha varchar(100)
)

select * from usuarios






