﻿CREATE TABLE MP_TIPO_PATENTE (
	IDTIPO_PATENTE 					BIGINT 				NOT NULL,
	DESCRICAO_TIPO_PATENTE 			VARCHAR(4000) 		NOT	NULL,
	SIGLA_TIPO 						CHAR(2) 			NOT	NULL,
	TEMPO_INICIO_ANOS 				INT 				NOT	NULL,
	QUANTIDADE_PAGTO 				INT					NOT	NULL,
	TEMPO_ENTRE_PAGTO 				INT	 				NOT	NULL,
	SEQUENCIA_INICIO_PAGTO 			INT 				NOT	NULL,
	TEM_PAGTO_INTERMEDIARIO 		CHAR(1) 				NULL,
	INICIO_INTERMED_SEQUENCIA 		INT 				NOT	NULL,
	QUANTIDADE_PAGTO_INTERMED 		INT 				NOT	NULL,
	TEMPO_ENTRE_PAGTO_INTERMED 		INT					NOT	NULL,
	DESCRICAO_PAGTO 				VARCHAR(4000) 		NOT	NULL,
	DESCRICAO_PAGTO_INTERMED 		VARCHAR(4000) 		NOT	NULL,
	TEM_PED_EXAME 					CHAR(1) 			    NULL
)
;
ALTER TABLE MP_TIPO_PATENTE
	ADD CONSTRAINT PK_TIPO_PATENTE
	PRIMARY KEY(IDTIPO_PATENTE)
;

ALTER TABLE MP_TIPO_PATENTE
ADD CONSTRAINT TIPO_PATENTE_UNIQUE UNIQUE (DESCRICAO_TIPO_PATENTE, SIGLA_TIPO)
;

CREATE TABLE MP_INVENTOR (
	IDPESSOA						BIGINT				NOT NULL,
	TIPOPESSOA						SMALLINT			NOT NULL,
	DTCADASTRO						INT					NOT NULL,
	INFOADICIONAL					VARCHAR(4000)		NULL
);

ALTER TABLE MP_INVENTOR
	ADD CONSTRAINT PK_MP_INVENTOR
	PRIMARY KEY (IDPessoa)
;

ALTER TABLE MP_INVENTOR
	ADD CONSTRAINT FK_MP_INVENTOR1
	FOREIGN KEY(IDPESSOA)
	REFERENCES NCL_PESSOA(ID)
;

CREATE TABLE MP_TIPO_PROCEDIMENTO_INTERNO(
	IDTIPO_PROCEDIMENTO_INTERNO		BIGINT				NOT NULL,
	DESCRICAO_TIPO					VARCHAR(255)		NOT NULL
);
	
ALTER TABLE MP_TIPO_PROCEDIMENTO_INTERNO
	ADD CONSTRAINT PK_MP_TIPO_PROCEDIMENTO_INTERNO
	PRIMARY KEY(IDTIPO_PROCEDIMENTO_INTERNO)
;

CREATE TABLE MP_PROCURADORES
(
	IDPESSOA						BIGINT				NOT NULL,
	TIPOPESSOA						SMALLINT			NOT NULL,
	MATRICULAAPI					VARCHAR(22)				NULL,
	SIGLAORGAO						CHAR(10)				NULL,
	NRREGISTROORGAO					VARCHAR(17)				NULL,
	DATAREGISTROORGAO				INT						NULL,
	OBSCONTATO						VARCHAR(255)			NULL
);

ALTER TABLE MP_PROCURADORES
	ADD CONSTRAINT PK_MP_PROCURADORES
	PRIMARY KEY (IDPessoa);

ALTER TABLE MP_PROCURADORES
	ADD CONSTRAINT FK_MP_PROCURADORES1
	FOREIGN KEY(IDPESSOA)
	REFERENCES NCL_PESSOA(ID);


CREATE TABLE MP_DESPACHO_MARCA(
	IDDESPACHO						BIGINT				NOT NULL,
	CODIGO_DESPACHO					VARCHAR(50)				NOT NULL,
	DETALHE_DESPACHO				VARCHAR(4000)			NULL,
	IDSITUACAO_PROCESSO				BIGINT					NULL,
	REGISTRO						CHAR(1)					NULL
);
	
ALTER TABLE MP_DESPACHO_MARCA
	ADD CONSTRAINT PK_DESPACHO
	PRIMARY KEY(IDDESPACHO)
;

CREATE TABLE MP_PATENTE
(
	IDPATENTE				BIGINT				NOT NULL,
	TITULOPATENTE			VARCHAR(500)			NULL,
	IDTIPOPATENTE			BIGINT				NOT NULL,
	OBRIGACAOGERADA			CHAR(1)				NOT NULL,
	DATACADASTRO			INT						NULL,
	OBSERVACAO				VARCHAR(250)			NULL,
	RESUMO_PATENTE			VARCHAR(500)			NULL,
	QTDEREINVINDICACAO		INT						NULL
);

ALTER TABLE MP_PATENTE
	ADD CONSTRAINT PK_MP_PATENTE
	PRIMARY KEY (IDPATENTE);

ALTER TABLE MP_PATENTE
	ADD CONSTRAINT FK_MP_PATENTE2
	FOREIGN KEY (IDTIPOPATENTE) REFERENCES MP_TIPO_PATENTE(IDTIPO_PATENTE);

CREATE TABLE MP_PATENTEANUIDADE
(
	IDPATENTEANUIDADE				BIGINT				NOT NULL,
	IDPATENTE						BIGINT				NOT NULL,
	DESCRICAOANUIDADE				VARCHAR(45)				NULL,
	DATALANCAMENTO					INT						NULL,
	DATAVENCIMENTO					INT						NULL,
	DATAPAGAMENTO					INT						NULL,
	VALORPAGAMENTO					FLOAT					NULL,
	ANUIDADEPAGA					CHAR(1)				NOT NULL,
	PEDIDOEXAME						CHAR(1)					NULL,	
	DATAVENCTO_SEM_MULTA			INT						NULL,
	DATAVENCTO_COM_MULTA			INT						NULL
);

ALTER TABLE MP_PATENTEANUIDADE
	ADD CONSTRAINT PK_MP_PATENTEANUIDADE
	PRIMARY KEY (IDPATENTEANUIDADE)
;
	
ALTER TABLE MP_PATENTEANUIDADE
	ADD CONSTRAINT FK_MP_PATENTEANUIDADE
	FOREIGN KEY (IDPATENTE) REFERENCES MP_PATENTE(IDPATENTE)
;	

CREATE TABLE MP_PATENTECLASSIFICACAO
(
	IDPATENTECLASSIFICACAO			BIGINT				NOT NULL,
	CLASSIFICACAO					VARCHAR(20)			NOT NULL,
	DESCRICAO_CLASSIFICACAO			VARCHAR(100)			NULL,
	IDPATENTE						BIGINT				NOT NULL,
	TIPO_CLASSIFICACAO				INT					NOT NULL
);

ALTER TABLE MP_PATENTECLASSIFICACAO
	ADD CONSTRAINT PK_MP_PATENTECLASSIFICACAO
	PRIMARY KEY (IDPATENTECLASSIFICACAO)
;
	
ALTER TABLE MP_PATENTECLASSIFICACAO
	ADD CONSTRAINT FK_MP_PATENTECLASSIFICACAO
	FOREIGN KEY (IDPATENTE) REFERENCES MP_PATENTE(IDPATENTE)
;	

CREATE TABLE MP_PATENTEPRIORIDADEUNIONISTA
(
	IDPRIORIDADEUNIONISTA			BIGINT				NOT NULL,
	DATA_PRIORIDADE					INT						NULL,		
	NUMERO_PRIORIDADE				VARCHAR(20)				NULL,
	IDPATENTE						BIGINT				NOT NULL,
	IDPAIS							BIGINT					NULL
);

ALTER TABLE MP_PATENTEPRIORIDADEUNIONISTA
	ADD CONSTRAINT PK_MP_PATENTEPRIORIDADEUNIONISTA
	PRIMARY KEY (IDPRIORIDADEUNIONISTA)
;
	
ALTER TABLE MP_PATENTEPRIORIDADEUNIONISTA
	ADD CONSTRAINT FK_MP_PATENTEPRIORIDADEUNIONISTA
	FOREIGN KEY (IDPATENTE) REFERENCES MP_PATENTE(IDPATENTE)
;	
	
ALTER TABLE MP_PATENTEPRIORIDADEUNIONISTA
	ADD CONSTRAINT FK_MP_PATENTEPRIORIDADEUNIONISTA2
	FOREIGN KEY (IDPAIS) REFERENCES NCL_PAIS(ID)
;	

CREATE TABLE MP_PATENTETITULARINVENTOR
(
	IDPATENTETITULARINVENTOR		BIGINT				NOT NULL,
	IDPATENTE						BIGINT				NOT NULL,
	CONTATO_TITULAR					CHAR(10)				NULL
);

ALTER TABLE MP_PATENTETITULARINVENTOR
	ADD CONSTRAINT PK_MP_PATENTETITULARINVENTOR
	PRIMARY KEY (IDPATENTETITULARINVENTOR)
;
	
ALTER TABLE MP_PATENTETITULARINVENTOR
	ADD CONSTRAINT FK_MP_PATENTETITULARINVENTOR
	FOREIGN KEY (IDPATENTE) REFERENCES MP_PATENTE(IDPATENTE)
;


CREATE TABLE MP_MARCAS(
	IDMARCA							BIGINT				NOT NULL,
	CODIGONCL						INT					NOT NULL,
	CODIGOAPRESENTACAO				INT					NOT NULL,
	IDCLIENTE						BIGINT				NOT NULL,
	CODIGONATUREZA					INT					NOT NULL,
	DESCRICAO_MARCA					VARCHAR(255)			NULL,
	ESPECIFICACAO_PROD_SERV			VARCHAR(4000)			NULL,
	IMAGEM_MARCA					VARCHAR(255)			NULL,
	OBSERVACAO_MARCA				VARCHAR(4000)			NULL,
	CODIGOCLASSE					INT						NULL,
	CODIGOCLASSE_SUBCLASSE1			INT						NULL,
	CODIGOCLASSE_SUBCLASSE2			INT						NULL,
	CODIGOCLASSE_SUBCLASSE3			INT						NULL
);

ALTER TABLE MP_MARCAS
	ADD CONSTRAINT PK_MARCAS
	PRIMARY KEY (IDMARCA)
;	
	
ALTER TABLE MP_MARCAS
	ADD CONSTRAINT FK_MARCAS_CONTATO
	FOREIGN KEY (IDCLIENTE) REFERENCES NCL_CLIENTE(IDPESSOA)
;

CREATE INDEX IDX_MP_MARCAS1
	ON MP_MARCAS(CODIGONCL)
;

CREATE INDEX IDX_MP_MARCAS2
	ON MP_MARCAS(CODIGOAPRESENTACAO)
;

CREATE INDEX IDX_MP_MARCAS3
	ON MP_MARCAS(CODIGONATUREZA)
;

CREATE TABLE MP_PROCESSOMARCA (
	IDPROCESSO						BIGINT				NOT NULL,
	IDMARCA							BIGINT				NOT NULL,
	PROCESSO						BIGINT				NOT NULL,
	DATAENTRADA						INT					NOT NULL,
	DATACONCESSAO					INT						NULL,
	PROCESSOEHTERCEIRO				CHAR(1)				NOT NULL,
	IDDESPACHO						BIGINT					NULL,
	IDPROCURADOR					BIGINT				NOT	NULL,
	SITUACAO						INT						NULL
);

ALTER TABLE MP_PROCESSOMARCA
	ADD CONSTRAINT PK_MP_PROCESSOMARCA
	PRIMARY KEY (IDPROCESSO)
;	

ALTER TABLE MP_PROCESSOMARCA
	ADD CONSTRAINT FK_MP_PROCESSOMARCA1
	FOREIGN KEY (IDMARCA) REFERENCES MP_MARCAS(IDMARCA)
;

ALTER TABLE MP_PROCESSOMARCA
	ADD CONSTRAINT FK_MP_PROCESSOMARCA2
	FOREIGN KEY (IDDESPACHO) REFERENCES MP_DESPACHO_MARCA(IDDESPACHO)
;

ALTER TABLE MP_PROCESSOMARCA
	ADD CONSTRAINT FK_MP_PROCESSOMARCA3
	FOREIGN KEY (IDPROCURADOR) REFERENCES MP_PROCURADORES(IDPESSOA)
;

CREATE INDEX IDX_MP_PROCESSOMARCA2
	ON MP_PROCESSOMARCA(PROCESSO)
;

CREATE INDEX IDX_MP_PROCESSOMARCA3
	ON MP_PROCESSOMARCA(DATAENTRADA)
;

CREATE TABLE MP_RADICAL_MARCA (
	IDRADICAL						BIGINT				NOT NULL,
	DESCRICAORADICAL				VARCHAR(50)			NOT NULL,
	IDMARCA							BIGINT				NOT NULL,
	CODIGONCL						INT						NULL
);

ALTER TABLE MP_RADICAL_MARCA
	ADD CONSTRAINT PK_MP_RADICAL_MARCA
	PRIMARY KEY (IDRADICAL)
;

CREATE TABLE MP_PROCESSOPATENTE
(
	IDPROCESSOPATENTE				BIGINT				NOT NULL,
	NUMEROPROCESSO					VARCHAR(25)				NULL,
	PROTOOCOLO						INT						NULL,
	DATAENTRADA						INT						NULL,
	PROCESSODETERCEIRO				CHAR(1)				NOT NULL,
	DATACONCESSAO_REGISTRO			INT						NULL,	
	IDDESPACHO						BIGINT					NULL,
	IDPROCURADOR					BIGINT					NULL,
	EHESTRANGEIRO					CHAR(1)					NULL,
	PCT								CHAR(1)					NULL,
	NUMEROPCT						VARCHAR(20)				NULL,
	NUMEROWO						VARCHAR(20)				NULL,
	DATA_DEPOSITOPCT				INT						NULL,
	DATA_PUBLICACAOPCT				INT						NULL,
	ATIVO							CHAR(1)				NOT NULL
)
;

ALTER TABLE MP_PROCESSOPATENTE
	ADD CONSTRAINT PK_MP_PROCESSOPATENTE
	PRIMARY KEY (IDPROCESSOPATENTE)
;

ALTER TABLE MP_PROCESSOPATENTE
	ADD CONSTRAINT FK_MP_PROCESSOPATENTE
	FOREIGN KEY (IDPROCURADOR) REFERENCES MP_PROCURADORES(IDPESSOA)
;
	

CREATE TABLE MP_DESPACHO_PATENTE
(
	IDDESPACHOPATENTE				BIGINT				NOT NULL,
	CODIGO_SITUACAOPROCESSOPATENTE	VARCHAR(50)			NOT NULL,
	CODIGO_DESPACHO					VARCHAR(50)			NOT NULL,
	DESCRICAO						VARCHAR(255)			NULL,
	DETALHE_DESPACHO				VARCHAR(1000)			NULL
)
;
	
ALTER TABLE MP_DESPACHO_PATENTE
	ADD CONSTRAINT PK_MP_DESPACHO_PATENTE
	PRIMARY KEY(IDDESPACHOPATENTE)
;