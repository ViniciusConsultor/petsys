CREATE TABLE NCL_IDENTIFICADOR (
	NUMHIGH				INT				NOT NULL
)
;

CREATE TABLE NCL_MUNICIPIO (
	ID					BIGINT			NOT NULL,
	NOME				VARCHAR(30)		NOT NULL,
	UF					SMALLINT		NOT NULL,
	CEP					BIGINT				NULL
)
;

ALTER TABLE NCL_MUNICIPIO
	ADD CONSTRAINT PK_NCL_MUNIC
	PRIMARY KEY(ID)
;

CREATE INDEX IDX_NCL_MUNICIPIO1
	ON NCL_MUNICIPIO(NOME)
;

CREATE TABLE NCL_GRUPO (
	ID					BIGINT			NOT NULL,
	NOME				VARCHAR(30)		NOT NULL,
	STATUS				CHAR(1)			NOT NULL
)
;

ALTER TABLE NCL_GRUPO
	ADD CONSTRAINT PK_NCL_GRUPO
	PRIMARY KEY(ID)
;

CREATE INDEX IDX_NCL_GRUPO1
	ON NCL_GRUPO(NOME)
;

CREATE TABLE NCL_MODULO (
	IDMODULO			VARCHAR(7)		NOT NULL,
	NOME				VARCHAR(30)		NOT NULL
)
;

ALTER TABLE NCL_MODULO
	ADD CONSTRAINT PK_NCL_MODULO
	PRIMARY KEY(IDMODULO)
;

CREATE TABLE NCL_FUNCAO (
	IDMODULO			VARCHAR(7)		NOT NULL,
	IDFUNCAO			VARCHAR(11)		NOT NULL,
	NOME				VARCHAR(100)	NOT NULL
)
;

ALTER TABLE NCL_FUNCAO
	ADD CONSTRAINT PK_NCL_FUNCAO
	PRIMARY KEY(IDMODULO, IDFUNCAO)
;

ALTER TABLE NCL_FUNCAO
	ADD CONSTRAINT FK_NCL_FUNCAO
	FOREIGN KEY(IDMODULO)
	REFERENCES NCL_MODULO(IDMODULO)
;

CREATE TABLE NCL_OPERACAO (
	IDMODULO			VARCHAR(7)		NOT NULL,
	IDFUNCAO			VARCHAR(11)		NOT NULL,
	IDOPERACAO			VARCHAR(16)		NOT NULL,
	NOME				VARCHAR(30)		NOT NULL
)
;

ALTER TABLE NCL_OPERACAO
	ADD CONSTRAINT PK_NCL_OPERACAO
	PRIMARY KEY(IDMODULO, IDFUNCAO, IDOPERACAO)
;

ALTER TABLE NCL_OPERACAO
	ADD CONSTRAINT FK_NCL_OPERACAO
	FOREIGN KEY(IDMODULO, IDFUNCAO)
	REFERENCES NCL_FUNCAO(IDMODULO,IDFUNCAO)
;

CREATE TABLE NCL_MENUMODULO (
	IDMODULO			VARCHAR(7)		NOT NULL,
	IMAGEM				VARCHAR(255)	NOT NULL
)
;

ALTER TABLE NCL_MENUMODULO
	ADD CONSTRAINT PK_NCL_MENUMODULO
	PRIMARY KEY (IDMODULO)
;

ALTER TABLE NCL_MENUMODULO
	ADD CONSTRAINT FK_NCL_MENUMODULO
	FOREIGN KEY(IDMODULO)
	REFERENCES NCL_MODULO(IDMODULO)
;

CREATE TABLE NCL_MENUFUNCAO (
	IDMODULO			VARCHAR(7)		NOT NULL,
	IDFUNCAO			VARCHAR(11)		NOT NULL,
	IMAGEM				VARCHAR(100)	NOT NULL,
	URL					VARCHAR(100)	NOT NULL
)
;

ALTER TABLE NCL_MENUFUNCAO
	ADD CONSTRAINT PK_NCL_MENUFUNCAO
	PRIMARY KEY (IDMODULO,IDFUNCAO)
;

ALTER TABLE NCL_MENUFUNCAO
	ADD CONSTRAINT FK_NCL_MENUFUNCAO
	FOREIGN KEY(IDMODULO, IDFUNCAO)
	REFERENCES NCL_FUNCAO(IDMODULO, IDFUNCAO)
;

CREATE TABLE NCL_AUTORIZACAO (
	IDGRUPO				BIGINT			NOT NULL,
	IDDIRETIVA			VARCHAR(20)		NOT NULL
)
;

ALTER TABLE NCL_AUTORIZACAO
	ADD CONSTRAINT PK_NCL_AUTORIZACAO
	PRIMARY KEY (IDGRUPO, IDDIRETIVA)
;

ALTER TABLE NCL_AUTORIZACAO
	ADD CONSTRAINT FK_NCL_AUTORIZACAO
	FOREIGN KEY(IDGRUPO)
	REFERENCES NCL_GRUPO(ID)
;

CREATE TABLE NCL_PESSOA (
	ID					BIGINT			NOT NULL,
	NOME				VARCHAR(100)	NOT NULL,
	TIPO				SMALLINT		NOT NULL,
	ENDEMAIL			VARCHAR(255)		NULL,
	LOGRADOURO			VARCHAR(255)		NULL,
	COMPLEMENTO			VARCHAR(255)		NULL,
	IDMUNICIPIO			BIGINT				NULL,
	CEP					BIGINT				NULL,
	BAIRRO				VARCHAR(100)		NULL,
	SITE				VARCHAR(255)		NULL
)
;

ALTER TABLE NCL_PESSOA
	ADD CONSTRAINT PK_NCL_PESSOA
	PRIMARY KEY (ID)
;

CREATE INDEX IDX_NCL_PESSOA1
	ON NCL_PESSOA(NOME)
;

ALTER TABLE NCL_PESSOA
	ADD CONSTRAINT FK_NCL_PESSOA1
	FOREIGN KEY(IDMUNICIPIO)
	REFERENCES NCL_MUNICIPIO(ID)
;

CREATE TABLE NCL_PESSOAFISICA (
	IDPESSOA			BIGINT			NOT NULL,
	DATANASCIMENTO		INT					NULL,
	ESTADOCIVIL			CHAR(1)				NULL,
	NACIONALIDADE		CHAR(1)				NULL,
	RACA				CHAR(1)				NULL,
	SEXO				CHAR(1)				NULL,
	NOMEMAE				VARCHAR(100)		NULL,
	NOMEPAI				VARCHAR(100)		NULL,
	NUMERORG			VARCHAR(30)			NULL,
	ORGEXPEDITOR		VARCHAR(20)			NULL,
	DATAEXP				INT					NULL,
	UFEXP				SMALLINT			NULL,
	CPF					CHAR(11)			NULL,
	NATURALIDADE		BIGINT				NULL,
	FOTO				VARCHAR(100)		NULL
)
;

ALTER TABLE NCL_PESSOAFISICA
	ADD CONSTRAINT PK_NCL_PESSOAFISICA
	PRIMARY KEY (IDPESSOA)
;

ALTER TABLE NCL_PESSOAFISICA
	ADD CONSTRAINT FK_NCL_PESSOAFISICA1
	FOREIGN KEY(IDPESSOA)
	REFERENCES NCL_PESSOA(ID)
;

CREATE TABLE NCL_PESSOAJURIDICA (
	IDPESSOA			BIGINT			NOT NULL,
	NOMEFANTASIA		VARCHAR(100)		NULL,
	CNPJ				CHAR(14)			NULL,
	IE					VARCHAR(14)			NULL,
	IM					VARCHAR(20)			NULL
)
;

ALTER TABLE NCL_PESSOAJURIDICA
	ADD CONSTRAINT PK_NCL_PESSOAJURIDICA
	PRIMARY KEY (IDPESSOA)
;

ALTER TABLE NCL_PESSOAJURIDICA
	ADD CONSTRAINT FK_NCL_PESSOAJURIDICA1
	FOREIGN KEY(IDPESSOA)
	REFERENCES NCL_PESSOA(ID)
;

CREATE TABLE NCL_OPERADOR (
	IDPESSOA			BIGINT			NOT NULL,
	TIPOPESSOA			SMALLINT		NOT NULL,
	LOGIN				VARCHAR(255)	NOT NULL,
	STATUS				CHAR(1)			NOT NULL
)
;

ALTER TABLE NCL_OPERADOR
	ADD CONSTRAINT PK_NCL_OPERADOR
	PRIMARY KEY (IDPessoa)
;
CREATE INDEX IDX_NCL_OPERADOR1
	ON NCL_OPERADOR(LOGIN)
;

ALTER TABLE NCL_OPERADOR
	ADD CONSTRAINT FK_NCL_OPERADOR1
	FOREIGN KEY(IDPESSOA)
	REFERENCES NCL_PESSOA(ID)
;

CREATE TABLE NCL_PERFIL (
	IDUSUARIO			BIGINT			NOT NULL,
	IMAGEMDESKTOP		VARCHAR(100)	NOT NULL,
	TEMA				VARCHAR(20)		NOT NULL
)
;

ALTER TABLE NCL_PERFIL
	ADD CONSTRAINT PK_NCL_PERFIL
	PRIMARY KEY (IDUSUARIO)
;

ALTER TABLE NCL_PERFIL
	ADD CONSTRAINT FK_NCL_PERFIL
	FOREIGN KEY(IDUSUARIO)
	REFERENCES NCL_OPERADOR(IDPESSOA)
;

CREATE TABLE NCL_GRPOPE (
	IDOPERADOR			BIGINT			NOT NULL,
	IDGRUPO				BIGINT			NOT NULL
)
;

ALTER TABLE NCL_GRPOPE
	ADD CONSTRAINT PK_NCL_GRPOPE
	PRIMARY KEY (IDOPERADOR,IDGRUPO)
;

ALTER TABLE NCL_GRPOPE
	ADD CONSTRAINT FK_NCL_GRPOPE1
	FOREIGN KEY(IDOPERADOR)
	REFERENCES NCL_OPERADOR(IDPESSOA)
;

ALTER TABLE NCL_GRPOPE
	ADD CONSTRAINT FK_NCL_GRPOPE2
	FOREIGN KEY(IDGRUPO)
	REFERENCES NCL_GRUPO(ID)
;

CREATE TABLE NCL_SNHOP (
	IDOPERADOR			BIGINT			NOT NULL,
	SENHA				VARCHAR(255)	NOT NULL,
	DATACADASTRO		BIGINT			NOT NULL
)
;

ALTER TABLE NCL_SNHOP
	ADD CONSTRAINT PK_NCL_SNHOP
	PRIMARY KEY (IDOPERADOR)
;

ALTER TABLE NCL_SNHOP
	ADD CONSTRAINT FK_NCL_SNHOP
	FOREIGN KEY(IDOPERADOR)
	REFERENCES NCL_OPERADOR(IDPESSOA)
;

CREATE TABLE NCL_CLIENTE (
	IDPESSOA			BIGINT			NOT NULL,
	TIPOPESSOA			SMALLINT		NOT NULL
)
;

ALTER TABLE NCL_CLIENTE
	ADD CONSTRAINT PK_NCL_CLIENTE
	PRIMARY KEY (IDPessoa)
;

ALTER TABLE NCL_CLIENTE
	ADD CONSTRAINT FK_NCL_CLIENTE1
	FOREIGN KEY(IDPESSOA)
	REFERENCES NCL_PESSOA(ID)
;

CREATE TABLE NCL_ATALHO (
	ID					VARCHAR(100)	NOT NULL,
	NOME				VARCHAR(100)	NOT NULL,
	TIPO				CHAR(1)			NOT NULL,
	URL					VARCHAR(100)	NOT NULL,
	IMAGEM				VARCHAR(50)		NOT NULL,
	IDUSUARIO			BIGINT			NOT NULL,
)
;

ALTER TABLE NCL_ATALHO
	ADD CONSTRAINT PK_NCL_ATALHO
	PRIMARY KEY (ID,IDUSUARIO)
;

CREATE INDEX IDX_NCL_ATALHO
	ON NCL_ATALHO(IDUSUARIO)
;

CREATE TABLE NCL_BANCO (
	IDPESSOA			BIGINT			NOT NULL,
	NUMERO				SMALLINT		NOT NULL
)
;

ALTER TABLE NCL_BANCO
	ADD CONSTRAINT PK_NCL_BANCO
	PRIMARY KEY (IDPESSOA)
;

ALTER TABLE NCL_BANCO
	ADD CONSTRAINT FK_NCL_BANCO1
	FOREIGN KEY(IDPESSOA)
	REFERENCES NCL_PESSOA(ID)
;

CREATE INDEX IDX_NCL_BANCO
	ON NCL_BANCO(NUMERO)
;

CREATE TABLE NCL_AGENCIABANCO (
	IDPESSOA			BIGINT			NOT NULL,
	IDBANCO				BIGINT			NOT NULL,
	NUMERO				VARCHAR(50)		NOT NULL
)
;

ALTER TABLE NCL_AGENCIABANCO
	ADD CONSTRAINT PK_NCL_AGENCIABANCO
	PRIMARY KEY (IDPESSOA)
;

ALTER TABLE NCL_AGENCIABANCO
	ADD CONSTRAINT FK_NCL_AGENCIABANCO1
	FOREIGN KEY(IDPESSOA)
	REFERENCES NCL_PESSOA(ID)
;

ALTER TABLE NCL_AGENCIABANCO
	ADD CONSTRAINT FK_NCL_AGENCIABANCO2
	FOREIGN KEY(IDBANCO)
	REFERENCES NCL_BANCO(IDPESSOA)
;

CREATE INDEX IDX_NCL_AGENCIABANCO
	ON NCL_AGENCIABANCO(NUMERO)
;

CREATE TABLE NCL_PESSOATELEFONE (
	IDPESSOA			BIGINT			NOT NULL,
	DDD					SMALLINT		NOT NULL,
	NUMERO				BIGINT			NOT NULL,
	TIPO				SMALLINT		NOT NULL,
	INDICE				SMALLINT		NOT NULL
)
;

ALTER TABLE NCL_PESSOATELEFONE
	ADD CONSTRAINT PK_NCL_PESSOATELEFONE
	PRIMARY KEY (IDPESSOA, DDD, NUMERO, TIPO)
;

ALTER TABLE NCL_PESSOATELEFONE
	ADD CONSTRAINT FK_NCL_PESSOATELEFONE1
	FOREIGN KEY(IDPESSOA)
	REFERENCES NCL_PESSOA(ID)
;

CREATE TABLE NCL_PESSOADADOSBANC (
	IDPESSOA			BIGINT			NOT NULL,
	IDBANCO				BIGINT			NOT NULL,
	IDAGENCIA			BIGINT			NOT NULL,
	CONTA				VARCHAR(50)		NOT NULL,
	TIPO				SMALLINT			NULL
)
;

ALTER TABLE NCL_PESSOADADOSBANC
	ADD CONSTRAINT PK_NCL_PESSOADADOSBANC
	PRIMARY KEY (IDPESSOA, IDBANCO, IDAGENCIA, CONTA)
;

ALTER TABLE NCL_PESSOADADOSBANC
	ADD CONSTRAINT FK_NCL_PESSOADADOSBANC1
	FOREIGN KEY(IDPESSOA)
	REFERENCES NCL_PESSOA(ID)
;

ALTER TABLE NCL_PESSOADADOSBANC
	ADD CONSTRAINT FK_NCL_PESSOADADOSBANC2
	FOREIGN KEY(IDBANCO)
	REFERENCES NCL_BANCO(IDPESSOA)
;

ALTER TABLE NCL_PESSOADADOSBANC
	ADD CONSTRAINT FK_NCL_PESSOADADOSBANC3
	FOREIGN KEY(IDAGENCIA)
	REFERENCES NCL_AGENCIABANCO(IDPESSOA)	
;

CREATE TABLE NCL_AGENDA (
	IDPESSOA			BIGINT			NOT NULL,
	HORAINICO			SMALLINT		NOT NULL,
	HORAFIM				SMALLINT		NOT NULL,
	INTERVALO			SMALLINT		NOT NULL
)
;

ALTER TABLE NCL_AGENDA
	ADD CONSTRAINT PK_NCL_AGENDA
	PRIMARY KEY (IDPESSOA)
;

ALTER TABLE NCL_AGENDA
	ADD CONSTRAINT FK_NCL_AGENDA1
	FOREIGN KEY(IDPESSOA)
	REFERENCES NCL_PESSOA(ID)
;

CREATE TABLE NCL_COMPROMISSO (
	ID					BIGINT			NOT NULL,
	IDPESSOA			BIGINT			NOT NULL,
	INICIO				VARCHAR(20)		NOT NULL,
	FIM					VARCHAR(20)		NOT NULL,
	ASSUNTO				VARCHAR(100)		NOT NULL,
	LOCAL				VARCHAR(100)		NULL,
	DESCRICAO			VARCHAR(4000)		NULL,
	STATUS				CHAR(1)			NOT NULL
)
;

ALTER TABLE NCL_COMPROMISSO
	ADD CONSTRAINT PK_NCL_COMPROMISSO
	PRIMARY KEY (ID)
;

ALTER TABLE NCL_COMPROMISSO
	ADD CONSTRAINT FK_NCL_COMPROMISSO1
	FOREIGN KEY(IDPESSOA)
	REFERENCES NCL_PESSOA(ID)
;

CREATE INDEX IDX_NCL_COMPROMISSO1
	ON NCL_COMPROMISSO(INICIO)
;

CREATE INDEX IDX_NCL_COMPROMISSO2
	ON NCL_COMPROMISSO(FIM)
;

CREATE TABLE NCL_TAREFA (
	ID					BIGINT			NOT NULL,
	IDPESSOA			BIGINT			NOT NULL,
	INICIO				VARCHAR(20)		NOT NULL,
	FIM					VARCHAR(20)		NOT NULL,
	ASSUNTO				VARCHAR(100)		NOT NULL,
	DESCRICAO			VARCHAR(4000)		NULL,
	PRIORIDADE			CHAR(1)			NOT NULL,
	STATUS				CHAR(1)			NOT NULL
)
;

ALTER TABLE NCL_TAREFA
	ADD CONSTRAINT PK_NCL_TAREFA
	PRIMARY KEY (ID)
;

ALTER TABLE NCL_TAREFA
	ADD CONSTRAINT FK_NCL_TAREFA1
	FOREIGN KEY(IDPESSOA)
	REFERENCES NCL_PESSOA(ID)
;

CREATE INDEX IDX_NCL_TAREFA1
	ON NCL_TAREFA(INICIO)
;

CREATE INDEX IDX_NCL_TAREFA2
	ON NCL_TAREFA(FIM)
;

CREATE TABLE NCL_LEMBRETE (
	ID					BIGINT			NOT NULL,
	IDPESSOA			BIGINT			NOT NULL,
	INICIO				VARCHAR(20)		NOT NULL,
	FIM					VARCHAR(20)		NOT NULL,
	ASSUNTO				VARCHAR(100)		NOT NULL,
	LOCAL				VARCHAR(100)		NULL,
	DESCRICAO			VARCHAR(4000)		NULL,
	STATUS				CHAR(1)			NOT NULL
)
;

ALTER TABLE NCL_LEMBRETE
	ADD CONSTRAINT PK_NCL_LEMBRETE
	PRIMARY KEY (ID)
;

ALTER TABLE NCL_LEMBRETE
	ADD CONSTRAINT FK_NCL_LEMBRETE1
	FOREIGN KEY(IDPESSOA)
	REFERENCES NCL_PESSOA(ID)
;

CREATE INDEX IDX_NCL_LEMBRETE1
	ON NCL_LEMBRETE(INICIO)
;

CREATE INDEX IDX_NCL_LEMBRETE2
	ON NCL_LEMBRETE(FIM)
;