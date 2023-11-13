	CREATE DATABASE PROSPERA
	USE PROSPERA

CREATE TABLE Usuario 

(
    IdUsuario INT PRIMARY KEY IDENTITY(1,1),
    NomeUsuario VARCHAR(50) NOT NULL,
    EmailUsuario VARCHAR(100) NOT NULL,
    SenhaUsuario VARCHAR(10) NOT NULL,
    CPFUsuario VARCHAR(15),
    CargoUsuario VARCHAR(20),
    DatCadastroUsuario DATE,
    DatUltimoAcesUsuario DATE,
    StatusUsuario VARCHAR(20),
	TpUsuario INT,
	 FOREIGN KEY (TpUsuario) REFERENCES TipoUsuario(IdTpUsuario)
);

	CREATE TABLE Terceiros 
	(
	  IdTerceiros INT PRIMARY KEY IDENTITY(1,1),
	  NomeTerceiros VARCHAR(50),
	  TelefoneTerceiros VARCHAR(20),
	  EmailTerceiros VARCHAR(50),
	  EnderecoTerceiros VARCHAR(80),
	  CidadeTerceiros VARCHAR(80),
	  BairroTerceiros VARCHAR(80),
	  UFTerceiros VARCHAR(2),
	  CEPTerceiros VARCHAR(10),
	  ObservacaoTerceiros VARCHAR(100),
	  DataCadastroTerceiros DATE,
	  DataUltimaMovimentacao DATE,
	  StatusTerceiros VARCHAR(20),
	  IdUsuario INT,
	  FOREIGN KEY (IdUsuario) REFERENCES Usuario(IdUsuario)

	);
		CREATE TABLE Extrato
	(
     IdExtrato INT PRIMARY KEY IDENTITY(1,1),
	 NomeExtrato VARCHAR(30),
	 ValorExtrato DECIMAL(10,2),
	 DestinatarioExtrato VARCHAR(100),
	 RemetenteExtrato VARCHAR(100),
	 DataExtrato DATE,
	 IdUsuario INT,
	 StatusExtrato VARCHAR(20),
	 ObservacaoExtrato VARCHAR(80),
	 FOREIGN KEY (IdUsuario) REFERENCES Usuario(IdUsuario)

	);

		CREATE TABLE Contas
	(
	IdContas INT PRIMARY KEY IDENTITY(1,1),
	CodigoCont INT,
	NomeCont VARCHAR(20),
	TipoCont VARCHAR(20),
	DatEmissaoCont DATE,
	DatVenciCont DATE,
	PessoaCont VARCHAR(30),
    RecebedorCont VARCHAR(30),
	PagadorCont VARCHAR(30),
	Descricaocont VARCHAR(80),
    IdUsuario INT,
	ValorCont DECIMAL(10,2),
	StatusCont VARCHAR(20),
	MetodoPgtoCont VARCHAR(20),
	ObservacaoCont VARCHAR(80),
	FOREIGN KEY (IdUsuario) REFERENCES Usuario(IdUsuario)

	);
		CREATE TABLE ContaBancaria
	(
	IdContaBancaria INT PRIMARY KEY IDENTITY(1,1),
	TitularContBan VARCHAR(80),
	NumContBan VARCHAR(10),
	AgenciaContBan VARCHAR(8),
	TipoContBan VARCHAR(50),
	SaldoContBan DECIMAL(10,2),
	ObsContBan VARCHAR(80),
	IdUsuario INT,
	IdTerceiros INT,
	FOREIGN KEY (IdUsuario) REFERENCES Usuario(IdUsuario),
	FOREIGN KEY (IdTerceiros)  REFERENCES Terceiros(IdTerceiros)

	);

	CREATE TABLE TipoUsuario
	(
	IdTpUsuario INT PRIMARY KEY IDENTITY(1,1),
	DescricaoTpUsuario VARCHAR(30)
	
	)