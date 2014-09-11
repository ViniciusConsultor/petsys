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
	URL					VARCHAR(100)	NOT NULL,
	AGRUPADOR			VARCHAR(100)		NULL
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
	SITE				VARCHAR(255)		NULL,
	IDBANCO				CHAR(3)				NULL,
	IDAGENCIA			BIGINT				NULL,
	CNTACORRENTE		VARCHAR(50)			NULL,
	TIPOCNTACORRENTE	SMALLINT			NULL
)
;

ALTER TABLE NCL_PESSOA
	ADD CONSTRAINT PK_NCL_PESSOA
	PRIMARY KEY (ID)
;

CREATE INDEX IDX_NCL_PESSOA1
	ON NCL_PESSOA(NOME)
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
	FOTO				VARCHAR(4000)		NULL
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
	IM					VARCHAR(20)			NULL,
	LOGOMARCA			VARCHAR(4000)		NULL
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

CREATE TABLE NCL_EMPRESA (
	IDPESSOA			BIGINT			NOT NULL,
	DTATIVACAO			INTEGER			NOT NULL
)
;

ALTER TABLE NCL_EMPRESA
	ADD CONSTRAINT PK_NCL_EMPRESA
	PRIMARY KEY (IDPessoa)
;

ALTER TABLE NCL_EMPRESA
	ADD CONSTRAINT FK_NCL_EMPRESA1
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
	SENHA				VARCHAR(4000)	NOT NULL,
	DATACADASTRO		VARCHAR(20)		NOT NULL
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

CREATE TABLE NCL_GRUPO_DE_ATIVIDADE (
	ID					BIGINT			NOT NULL,
	NOME				VARCHAR(100)	NOT NULL
)
;

ALTER TABLE NCL_GRUPO_DE_ATIVIDADE
	ADD CONSTRAINT PK_NCL_GRUPO_DE_ATIVIDADE
	PRIMARY KEY(ID)
;

CREATE INDEX IDX_NCL_GRUPO_DE_ATIVIDADE1
	ON NCL_GRUPO_DE_ATIVIDADE(NOME)
;

CREATE TABLE NCL_CLIENTE (
	IDPESSOA			BIGINT			NOT NULL,
	TIPOPESSOA			SMALLINT		NOT NULL,
	DTCADASTRO			INT				NOT NULL,
	INFOADICIONAL		VARCHAR(4000)		NULL,
	FAIXASALARIAL		FLOAT				NULL,
	DESCONTOAUTOMATICO	FLOAT				NULL,
	VLRMAXCOMPRAS		FLOAT				NULL,
	SALDOCOMPRAS		FLOAT				NULL,
	IDEMPRESA			BIGINT			NOT NULL,
	NUMREGISTRO			VARCHAR(20)			NULL,
	DTREGISTRO			INT					NULL,
	IDGRPATIVIDADE		BIGINT				NULL
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

ALTER TABLE NCL_CLIENTE
	ADD CONSTRAINT FK_NCL_CLIENTE2
	FOREIGN KEY(IDGRPATIVIDADE)
	REFERENCES NCL_GRUPO_DE_ATIVIDADE(ID)
;

ALTER TABLE NCL_CLIENTE
	ADD CONSTRAINT FK_NCL_CLIENTE3
	FOREIGN KEY(IDEMPRESA)
	REFERENCES NCL_EMPRESA(IDPESSOA)
;

CREATE TABLE NCL_ATALHO (
	ID					VARCHAR(100)	NOT NULL,
	NOME				VARCHAR(100)	NOT NULL,
	TIPO				CHAR(1)			NOT NULL,
	URL					VARCHAR(4000)	NOT NULL,
	IMAGEM				VARCHAR(4000)	NOT NULL,
	IDUSUARIO			BIGINT			NOT NULL
)
;

ALTER TABLE NCL_ATALHO
	ADD CONSTRAINT PK_NCL_ATALHO
	PRIMARY KEY (ID,IDUSUARIO)
;

CREATE INDEX IDX_NCL_ATALHO
	ON NCL_ATALHO(IDUSUARIO)
;


CREATE TABLE NCL_AGENCIABANCO (
	ID					BIGINT			NOT NULL,
	IDBANCO				CHAR(3)			NOT NULL,
	NUMERO				VARCHAR(50)		NOT NULL,
	NOME				VARCHAR(255)	NOT NULL
)
;

ALTER TABLE NCL_AGENCIABANCO
	ADD CONSTRAINT PK_NCL_AGENCIABANCO
	PRIMARY KEY (ID)
;

CREATE INDEX IDX_NCL_AGENCIABANCO
	ON NCL_AGENCIABANCO(NUMERO)
;

CREATE TABLE NCL_TIPO_ENDERECO (
	ID					BIGINT			NOT NULL,
	NOME				VARCHAR(100)	NOT NULL
)
;

ALTER TABLE NCL_TIPO_ENDERECO
	ADD CONSTRAINT PK_NCL_TIPO_ENDERECO
	PRIMARY KEY (ID)
;

CREATE INDEX IDX_NCL_TIPO_ENDERECO1
	ON NCL_TIPO_ENDERECO(NOME)
;

CREATE TABLE NCL_PESSOAENDERECO (
	IDPESSOA			BIGINT			NOT NULL,
	TIPO				BIGINT			NOT NULL,
	INDICE				SMALLINT		NOT NULL,
	LOGRADOURO			VARCHAR(255)		NULL,
	COMPLEMENTO			VARCHAR(255)		NULL,
	IDMUNICIPIO			BIGINT				NULL,
	CEP					BIGINT				NULL,
	BAIRRO				VARCHAR(100)		NULL
)
;

ALTER TABLE NCL_PESSOAENDERECO
	ADD CONSTRAINT PK_NCL_PESSOAENDERECO
	PRIMARY KEY (IDPESSOA, TIPO, INDICE)
;

ALTER TABLE NCL_PESSOAENDERECO
	ADD CONSTRAINT FK_NCL_PESSOAENDERECO1
	FOREIGN KEY(IDPESSOA)
	REFERENCES NCL_PESSOA(ID)
;

ALTER TABLE NCL_PESSOAENDERECO
	ADD CONSTRAINT FK_NCL_PESSOAENDERECO2
	FOREIGN KEY(TIPO)
	REFERENCES NCL_TIPO_ENDERECO(ID)
;

CREATE TABLE NCL_PESSOATELEFONE (
	IDPESSOA			BIGINT			NOT NULL,
	DDD					SMALLINT		NOT NULL,
	NUMERO				BIGINT			NOT NULL,
	TIPO				SMALLINT		NOT NULL,
	INDICE				SMALLINT		NOT NULL,
	CONTATO				VARCHAR(255)    NULL
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

CREATE TABLE NCL_PESSOACONTATO (
	IDPESSOA			BIGINT			NOT NULL,
	NOMECONTATO			VARCHAR(255)	NOT NULL,
	INDICE				SMALLINT		NOT NULL
)
;

ALTER TABLE NCL_PESSOACONTATO
	ADD CONSTRAINT PK_NCL_PESSOACONTATO
	PRIMARY KEY (IDPESSOA, NOMECONTATO)
;

ALTER TABLE NCL_PESSOACONTATO
	ADD CONSTRAINT FK_NCL_PESSOACONTATO1
	FOREIGN KEY(IDPESSOA)
	REFERENCES NCL_PESSOA(ID)
;

CREATE TABLE NCL_CNFAGENDAUSU (
	IDPESSOA			BIGINT			NOT NULL,
	HORAINICO			SMALLINT		NOT NULL,
	HORAFIM				SMALLINT		NOT NULL,
	INTERVALO			SMALLINT		NOT NULL,
	IDPESSOAPADRAO		BIGINT			NOT NULL
)
;

ALTER TABLE NCL_CNFAGENDAUSU
	ADD CONSTRAINT PK_NCL_CNFAGENDAUSU
	PRIMARY KEY (IDPESSOA)
;

ALTER TABLE NCL_CNFAGENDAUSU
	ADD CONSTRAINT FK_NCL_CNFAGENDAUSU1
	FOREIGN KEY(IDPESSOA)
	REFERENCES NCL_PESSOA(ID)
;

ALTER TABLE NCL_CNFAGENDAUSU
	ADD CONSTRAINT FK_NCL_CNFAGENDAUSU2
	FOREIGN KEY(IDPESSOAPADRAO)
	REFERENCES NCL_PESSOA(ID)
;

CREATE TABLE NCL_COMPROMISSO (
	ID					BIGINT			NOT NULL,
	IDPESSOA			BIGINT			NOT NULL,
	INICIO				VARCHAR(20)		NOT NULL,
	FIM					VARCHAR(20)		NOT NULL,
	ASSUNTO				VARCHAR(4000)	NOT NULL,
	LOCAL				VARCHAR(4000)		NULL,
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
	ASSUNTO				VARCHAR(4000)	NOT NULL,
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
	ASSUNTO				VARCHAR(4000)	NOT NULL,
	LOCAL				VARCHAR(4000)		NULL,
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

CREATE TABLE NCL_CNFGERAL (
	NOTIFERROSREMAIL		CHAR(1)			NOT NULL,
	EMAILREMETNOTIFERROS	VARCHAR(255)		NULL,
	REMETENTEPADRAO			VARCHAR(255)		NULL,
	HABILITARSSL			CHAR(1)				NULL,
	PORTA					SMALLINT			NULL,
	REQUERAUTENTICACAO		CHAR(1)				NULL,
	SHNUSUSERVSAIDA			VARCHAR(255)		NULL,
	USUSERVSAIDA			VARCHAR(255)		NULL,
	SERVSAIDA				VARCHAR(255)		NULL,
	TIPOSERVSAIDA			CHAR(1)				NULL,
	TXTCOMPRO				VARCHAR(50)			NULL,
	TXTCOMPROENTRELNH		CHAR(1)			NOT NULL,
	TXTLEMBRE				VARCHAR(50)			NULL,
	TXTLEMBREENTRELNH		CHAR(1)			NOT NULL,
	TXTTARE					VARCHAR(50)			NULL,
	TXTTAREENTRELNH			CHAR(1)			NOT NULL,
	TXTCABAGEN				VARCHAR(50)		NOT NULL,
	APRELNHCABAGEN			CHAR(1)			NOT NULL,
	APRELNHRODAGEN			CHAR(1)			NOT NULL
)
;

INSERT INTO NCL_CNFGERAL (NOTIFERROSREMAIL, EMAILREMETNOTIFERROS, REMETENTEPADRAO, HABILITARSSL, PORTA, REQUERAUTENTICACAO, SHNUSUSERVSAIDA, USUSERVSAIDA, SERVSAIDA, TIPOSERVSAIDA, TXTCOMPRO, TXTCOMPROENTRELNH, TXTLEMBRE, TXTLEMBREENTRELNH, TXTTARE, TXTTAREENTRELNH, TXTCABAGEN, APRELNHCABAGEN, APRELNHRODAGEN) VALUES('S', 'HERMES@SIMPLETI.COM.BR', 'HERMES@SIMPLETI.COM.BR', 'N', 25, 'S', 'CJORP8Z5JEWM4ECX+ENUAG==', 'HERMES@SIMPLETI.COM.BR', 'MAIL.SIMPLETI.COM.BR', '0', 'Compromissos ', 'N', 'Lembretes ', 'N', 'Tarefas ', 'N', 'Agenda ', 'N', 'N');


CREATE TABLE NCL_FORNECEDOR (
	IDPESSOA			BIGINT			NOT NULL,
	TIPOPESSOA			SMALLINT		NOT NULL,
	DTCADASTRO			INTEGER			NOT NULL,
	INFOADICIONAL		VARCHAR(4000)		NULL,
	IDEMPRESA			BIGINT			NOT NULL
)
;

ALTER TABLE NCL_FORNECEDOR
	ADD CONSTRAINT PK_NCL_FORNECEDOR
	PRIMARY KEY (IDPessoa)
;

ALTER TABLE NCL_FORNECEDOR
	ADD CONSTRAINT FK_NCL_FORNECEDOR1
	FOREIGN KEY(IDPESSOA)
	REFERENCES NCL_PESSOA(ID)
;

CREATE TABLE NCL_CONTATOFORNECEDOR (
	IDPFORNECEDOR		BIGINT			NOT NULL,
	IDPESSOACONTATO		BIGINT			NOT NULL,
	IDEMPRESA			BIGINT			NOT NULL
)
;

ALTER TABLE NCL_CONTATOFORNECEDOR
	ADD CONSTRAINT PK_NCL_CONTATOFORNECEDOR
	PRIMARY KEY (IDPFORNECEDOR,IDPESSOACONTATO)
;

ALTER TABLE NCL_CONTATOFORNECEDOR
	ADD CONSTRAINT FK_NCL_CONTATOFORNECEDOR1
	FOREIGN KEY(IDPFORNECEDOR)
	REFERENCES NCL_FORNECEDOR(IDPESSOA)
;

ALTER TABLE NCL_CONTATOFORNECEDOR
	ADD CONSTRAINT FK_NCL_CONTATOFORNECEDOR2
	FOREIGN KEY(IDPESSOACONTATO)
	REFERENCES NCL_PESSOA(ID)
;



CREATE TABLE NCL_VISOPEEMP (
	IDOPERADOR			BIGINT			NOT NULL,
	IDEMPRESA			BIGINT			NOT NULL
)
;

ALTER TABLE NCL_VISOPEEMP
	ADD CONSTRAINT PK_NCL_VISOPEEMP
	PRIMARY KEY (IDOPERADOR,IDEMPRESA)
;

ALTER TABLE NCL_VISOPEEMP
	ADD CONSTRAINT FK_NCL_VISOPEEMP1
	FOREIGN KEY(IDOPERADOR)
	REFERENCES NCL_OPERADOR(IDPESSOA)
;

ALTER TABLE NCL_VISOPEEMP
	ADD CONSTRAINT FK_NCL_VISOPEEMP2
	FOREIGN KEY(IDEMPRESA)
	REFERENCES NCL_EMPRESA(IDPESSOA)
;

CREATE TABLE NCL_REDEFSNH (
	ID					BIGINT			NOT NULL,
	IDOPERADOR			BIGINT			NOT NULL,
	DTSOLICITACAO		BIGINT				NOT NULL
)
;

ALTER TABLE NCL_REDEFSNH
	ADD CONSTRAINT PK_NCL_REDEFSNH
	PRIMARY KEY (ID)
;

CREATE INDEX IDX_NCL_REDEFSNH1
	ON NCL_REDEFSNH(IDOPERADOR)
;

CREATE INDEX IDX_NCL_REDEFSNH2
	ON NCL_REDEFSNH(DTSOLICITACAO)
;

CREATE TABLE NCL_PAIS (
	ID					BIGINT			NOT NULL,
	NOME				VARCHAR(100)	NOT NULL,
	SIGLA				VARCHAR(5)		NOT NULL
)
;

ALTER TABLE NCL_PAIS
	ADD CONSTRAINT PK_NCL_PAIS
	PRIMARY KEY(ID)
;

CREATE INDEX IDX_NCL_PAIS1
	ON NCL_PAIS(NOME)
;

CREATE TABLE NCL_CEDENTE (
	IDPESSOA			BIGINT			NOT NULL,
	TIPOPESSOA			SMALLINT		NOT NULL,
	IMAGEMBOLETO VARCHAR(255) NULL,
	TIPODECARTEIRA VARCHAR(50) NULL,
	INICIONOSSONUMERO BIGINT NULL,
	NUMEROAGENCIA VARCHAR(10) NULL,
	NUMEROCONTA VARCHAR(20) NULL,
	TIPOCONTA INT NULL,
	PADRAO CHAR(1),
	NUMERODOBANCO varchar(10)
)
;

ALTER TABLE NCL_CEDENTE
	ADD CONSTRAINT PK_NCL_CEDENTE
	PRIMARY KEY (IDPessoa)
;

ALTER TABLE NCL_CEDENTE
	ADD CONSTRAINT FK_NCL_CEDENTE1
	FOREIGN KEY(IDPESSOA)
	REFERENCES NCL_PESSOA(ID)
;

CREATE TABLE NCL_PESSOAEVENTO (
	IDPESSOA			BIGINT			NOT NULL,
	DATA				INT				NOT NULL,
	DESCRICAO			VARCHAR(4000)	NOT NULL
)
;

ALTER TABLE NCL_PESSOAEVENTO
	ADD CONSTRAINT FK_NCL_PESSOAEVENTO1
	FOREIGN KEY(IDPESSOA)
	REFERENCES NCL_PESSOA(ID)
;

CREATE INDEX IDX_NCL_PESSOAEVENTO1
	ON NCL_PESSOAEVENTO(IDPESSOA)
;


CREATE TABLE NCL_HISTORICOEMAIL (
	ID					BIGINT			NOT NULL,
	DATA				VARCHAR(30)		NOT NULL,
	POSSUIANEXO			CHAR(1)			NOT NULL,
	ASSUNTO				VARCHAR(4000)		NULL,
	REMETENTE			VARCHAR(1000)	NOT NULL,
	MENSAGEM			VARCHAR(4000)		NULL,
	DESTINATARIOSCC		VARCHAR(4000)	NOT NULL,
	DESTINATARIOSCCO	VARCHAR(4000)		NULL,
	CONTEXTO			VARCHAR(1000)		NULL
);


ALTER TABLE NCL_HISTORICOEMAIL
	ADD CONSTRAINT PK_NCL_HISTORICOEMAIL
	PRIMARY KEY(ID)
;

CREATE TABLE NCL_HISTORICOEMAILANEXO (
	IDHISTORICO			BIGINT			NOT NULL,
	NOMEANEXO			VARCHAR(1000)	NOT NULL,
	INDICE				INT				NOT NULL,
	BINARIOANEXO		IMAGE			NOT NULL
);	

ALTER TABLE NCL_HISTORICOEMAILANEXO
	ADD CONSTRAINT PK_NCL_HISTORICOEMAILANEXO
	PRIMARY KEY(IDHISTORICO, INDICE)
;

ALTER TABLE NCL_HISTORICOEMAILANEXO
	ADD CONSTRAINT FK_NCL_HISTORICOEMAILANEXO1
	FOREIGN KEY(IDHISTORICO)
	REFERENCES NCL_HISTORICOEMAIL(ID)
;

CREATE TABLE NCL_PESSOAEMAIL(
	IDPESSOA BIGINT NOT NULL,
	ENDEMAIL VARCHAR(255) NOT NULL);

ALTER TABLE NCL_PESSOAEMAIL
	ADD CONSTRAINT PK_NCL_PESSOAEMAIL
	PRIMARY KEY(IDPESSOA, ENDEMAIL);

ALTER TABLE NCL_PESSOAEMAIL
	ADD CONSTRAINT FK_NCL_PESSOAEMAIL1
	FOREIGN KEY(IDPESSOA)
	REFERENCES NCL_PESSOA(ID);

ALTER TABLE NCL_PESSOAEMAIL
	ADD CONSTRAINT UC_NCL_PESSOAEMAIL1 UNIQUE (IDPESSOA, ENDEMAIL); 