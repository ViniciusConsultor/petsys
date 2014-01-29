﻿CREATE TABLE MP_NATUREZA_PATENTE (
	IDNATUREZA_PATENTE 			NUMBER(11) 				NOT NULL,
	DESCRICAO_NATUREZA_PATENTE 	VARCHAR2(255) 			NOT	NULL,
	SIGLA_NATUREZA 				CHAR(2) 				NOT	NULL,
	TEMPO_INICIO_ANOS 			NUMBER(9) 				NOT	NULL,
	QUANTIDADE_PAGTO 			NUMBER(9) 				NOT	NULL,
	TEMPO_ENTRE_PAGTO 			NUMBER(9) 	 			NOT	NULL,
	SEQUENCIA_INICIO_PAGTO 		NUMBER(9)  				NOT	NULL,
	TEM_PAGTO_INTERMEDIARIO 	CHAR(1) 				    NULL,
	INICIO_INTERMED_SEQUENCIA 	NUMBER(9)  				NOT	NULL,
	QUANTIDADE_PAGTO_INTERMED 	NUMBER(9)  				NOT	NULL,
	TEMPO_ENTRE_PAGTO_INTERMED 	NUMBER(9) 				NOT	NULL,
	DESCRICAO_PAGTO 			VARCHAR2(255) 			NOT	NULL,
	DESCRICAO_PAGTO_INTERMED 	VARCHAR2(255) 			NOT	NULL,
	TEM_PED_EXAME 				CHAR(1) 				    NULL
);

ALTER TABLE MP_NATUREZA_PATENTE
	ADD CONSTRAINT PK_NATUREZA_PATENTE
	PRIMARY KEY(IDNATUREZA_PATENTE);

ALTER TABLE MP_NATUREZA_PATENTE
ADD CONSTRAINT NATUREZA_PATENTE_UNIQUE UNIQUE (DESCRICAO_NATUREZA_PATENTE, SIGLA_NATUREZA);


CREATE TABLE MP_INVENTOR (
	IDPESSOA					NUMBER(11)				NOT NULL,
	TIPOPESSOA					NUMBER(5)				NOT NULL,
	DTCADASTRO					NUMBER(5)				NOT NULL,
	INFOADICIONAL				VARCHAR2(4000)				NULL
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

CREATE TABLE MP_TIPO_ANDAMENTO_INTERNO(
	IDTIPO_ANDAMENTO_INTERNO NUMBER(11) NOT NULL,
	DESCRICAO_TIPO varchar(255) NOT NULL
	);
	
	ALTER TABLE MP_TIPO_ANDAMENTO_INTERNO
	ADD CONSTRAINT PK_TIPO_ANDAMENTO_INTERNO
	PRIMARY KEY(IDTIPO_ANDAMENTO_INTERNO);

CREATE TABLE MP_PROCURADORES
(
	IDPESSOA NUMBER(11) NOT NULL,
	TIPOPESSOA	NUMBER(5) NOT NULL,
	MATRICULAAPI VARCHAR(22),
	SIGLAORGAO CHAR(10),
	NRREGISTROORGAO VARCHAR(17),
	DATAREGISTROORGAO NUMBER(5),
	OBSCONTATO VARCHAR(255)
);

ALTER TABLE MP_PROCURADORES
	ADD CONSTRAINT PK_MP_PROCURADORES
	PRIMARY KEY (IDPessoa);

ALTER TABLE MP_PROCURADORES
	ADD CONSTRAINT FK_MP_PROCURADORES1
	FOREIGN KEY(IDPESSOA)
	REFERENCES NCL_PESSOA(ID);

CREATE TABLE MP_DESPACHO(
	IDDESPACHO NUMBER(11) NOT NULL,
	CODIGO_DESPACHO VARCHAR(50) NOT NULL,
	DETALHE_DESPACHO varchar(4000) NULL,
	IDSITUACAO_PROCESSO NUMBER(11) NULL,
	REGISTRO char(1) NULL);
	
	ALTER TABLE MP_DESPACHO
	ADD CONSTRAINT PK_DESPACHO
	PRIMARY KEY(IDDESPACHO);

CREATE TABLE MP_PATENTE
(
	IDPATENTE				NUMBER				NOT NULL,
	TITULOPATENTE			VARCHAR(4000)			NULL,
	IDNATUREZAPATENTE		NUMBER				NOT NULL,
	OBRIGACAOGERADA			CHAR(1)				NOT NULL,
	DATACADASTRO			NUMBER					NULL,
	OBSERVACAO				VARCHAR(4000)			NULL,
	RESUMO_PATENTE			VARCHAR(4000)			NULL,
	QTDEREINVINDICACAO		NUMBER					NULL
);

ALTER TABLE MP_PATENTE
	ADD CONSTRAINT PK_MP_PATENTE
	PRIMARY KEY (IDPATENTE);

ALTER TABLE MP_PATENTE
	ADD CONSTRAINT FK_MP_PATENTE2
	FOREIGN KEY (IDNATUREZAPATENTE) REFERENCES MP_NATUREZA_PATENTE(IDNATUREZA_PATENTE);

CREATE TABLE MP_PATENTEANUIDADE
(
	IDPATENTEANUIDADE NUMBER NOT NULL,
	IDPATENTE NUMBER NOT NULL,
	DESCRICAOANUIDADE VARCHAR(45),
	DATALANCAMENTO NUMBER,
	DATAVENCIMENTO NUMBER,
	DATAPAGAMENTO NUMBER,
	VALORPAGAMENTO FLOAT,
	ANUIDADEPAGA CHAR(1) NOT NULL,
	PEDIDOEXAME CHAR(1),
	DATAVENCTO_SEM_MULTA NUMBER,
	DATAVENCTO_COM_MULTA NUMBER
);

ALTER TABLE MP_PATENTEANUIDADE
	ADD CONSTRAINT PK_MP_PATENTEANUIDADE
	PRIMARY KEY (IDPATENTEANUIDADE);
	
ALTER TABLE MP_PATENTEANUIDADE
	ADD CONSTRAINT FK_MP_PATENTEANUIDADE
	FOREIGN KEY (IDPATENTE) REFERENCES MP_PROCESSOPATENTE(IDPROCESSOPATENTE);	

CREATE TABLE MP_PATENTECLASSIFICACAO
(
	IDPATENTECLASSIFICACAO NUMBER NOT NULL,
	CLASSIFICACAO VARCHAR(20) NOT NULL,
	DESCRICAO_CLASSIFICACAO	VARCHAR(4000),
	IDPATENTE NUMBER NOT NULL,
	TIPO_CLASSIFICACAO NUMBER NOT NULL
);

ALTER TABLE MP_PATENTECLASSIFICACAO
	ADD CONSTRAINT PK_MP_PATENTECLASSIFICACAO
	PRIMARY KEY (IDPATENTECLASSIFICACAO);
	
ALTER TABLE MP_PATENTECLASSIFICACAO
	ADD CONSTRAINT FK_MP_PATENTECLASSIFICACAO
	FOREIGN KEY (IDPATENTE) REFERENCES MP_PROCESSOPATENTE(IDPROCESSOPATENTE);	

CREATE TABLE MP_PATENTEPRIORIDADEUNIONISTA
(
	IDPRIORIDADEUNIONISTA NUMBER NOT NULL,
	DATA_PRIORIDADE	NUMBER,
	NUMERO_PRIORIDADE varchar(20),
	IDPATENTE NUMBER NOT NULL,
	IDPAIS	NUMBER
);

ALTER TABLE MP_PATENTEPRIORIDADEUNIONISTA
	ADD CONSTRAINT PK_MP_PATENTEPRIORIDADEUNIONISTA
	PRIMARY KEY (IDPRIORIDADEUNIONISTA);
	
ALTER TABLE MP_PATENTEPRIORIDADEUNIONISTA
	ADD CONSTRAINT FK_MP_PATENTEPRIORIDADEUNIONISTA
	FOREIGN KEY (IDPATENTE) REFERENCES MP_PROCESSOPATENTE(IDPROCESSOPATENTE);	
	
ALTER TABLE MP_PATENTEPRIORIDADEUNIONISTA
	ADD CONSTRAINT FK_MP_PATENTEPRIORIDADEUNIONISTA2
	FOREIGN KEY (IDPAIS) REFERENCES NCL_PAIS(ID);	

CREATE TABLE MP_PATENTETITULARINVENTOR
(
	IDTITULARINVENTOR				NUMBER				NOT NULL,
	IDPATENTE						NUMBER				NOT NULL
);

ALTER TABLE MP_PATENTETITULARINVENTOR
	ADD CONSTRAINT PK_MP_PATENTETITULARINVENTOR
	PRIMARY KEY (IDTITULARINVENTOR, IDPATENTE);
	
ALTER TABLE MP_PATENTETITULARINVENTOR
	ADD CONSTRAINT FK_MP_PATENTETITULARINVENTOR
	FOREIGN KEY (IDPATENTE) REFERENCES MP_PATENTE(IDPATENTE);

ALTER TABLE MP_PATENTETITULARINVENTOR
	ADD CONSTRAINT FK_MP_PATENTETITULARINVENTOR2
	FOREIGN KEY (IDTITULARINVENTOR) REFERENCES MP_INVENTOR(IDPESSOA);


CREATE TABLE MP_MARCAS(
	IDMARCA NUMBER NOT NULL,
	CODIGONCL INT NOT NULL,
	CODIGOAPRESENTACAO INT NOT NULL,
	IDCLIENTE INT NOT NULL,
	CODIGONATUREZA INT NOT NULL,
	DESCRICAO_MARCA VARCHAR(255) NULL,
	ESPECIFICACAO_PROD_SERV VARCHAR(4000) NULL,
	IMAGEM_MARCA VARCHAR(255) NULL,
	OBSERVACAO_MARCA VARCHAR(4000) NULL,
	CODIGOCLASSE INT NULL,
	CODIGOCLASSE_SUBCLASSE1		INT			NULL,
	CODIGOCLASSE_SUBCLASSE2		INT			NULL,
	CODIGOCLASSE_SUBCLASSE3		INT			NULL,
	PAGAMANUTENCAO				CHAR(1)		NULL,
	PERIODO						VARCHAR(20)	NULL,
	FORMADECOBRANCA				CHAR(1)		NULL,
	VALORDECOBRANCA				FLOAT		NULL
	)	

ALTER TABLE MP_MARCAS
	ADD CONSTRAINT PK_MARCAS
	PRIMARY KEY (IDMARCA);	
	
ALTER TABLE MP_MARCAS
	ADD CONSTRAINT FK_MARCAS_CONTATO
	FOREIGN KEY (IDCLIENTE) REFERENCES MP_CLIENTE(IDCLIENTE);


CREATE TABLE MP_RADICAL_MARCA (
IDRADICAL NUMBER NOT NULL,
DESCRICAORADICAL VARCHAR(50) NOT NULL,
IDMARCA NUMBER NOT NULL,
CODIGONCL INT NULL);

ALTER TABLE MP_RADICAL_MARCA
ADD CONSTRAINT PK_MP_RADICAL_MARCA
	PRIMARY KEY (IDRADICAL);

CREATE TABLE MP_PROCESSOPATENTE
(
	IDPROCESSOPATENTE				BIGINT				NOT NULL,
	NUMEROPROCESSO					VARCHAR(25)				NULL,
	DATAENTRADA						INT						NULL,
	PROCESSODETERCEIRO				CHAR(1)				NOT NULL,
	DATACONCESSAO_REGISTRO			INT						NULL,	
	IDDESPACHO						BIGINT					NULL,
	IDPROCURADOR					BIGINT					NULL,
	ESTRANGEIRO						CHAR(1)					NULL,
	PCT								CHAR(1)					NULL,
	NUMEROPCT						VARCHAR(20)				NULL,
	NUMEROWO						VARCHAR(20)				NULL,
	DATA_DEPOSITOPCT				INT						NULL,
	DATA_PUBLICACAOPCT				INT						NULL,
	ATIVO							CHAR(1)				NOT NULL
);

ALTER TABLE MP_PROCESSOPATENTE
	ADD CONSTRAINT PK_MP_PROCESSOPATENTE
	PRIMARY KEY (IDPROCESSOPATENTE);

ALTER TABLE MP_PROCESSOPATENTE
	ADD CONSTRAINT FK_MP_PROCESSOPATENTE
	FOREIGN KEY (IDPROCURADOR) REFERENCES MP_PROCURADORES(IDPESSOA);
	
ALTER TABLE MP_PROCESSOPATENTE
	ADD CONSTRAINT FK_MP_PROCESSOPATENTE1
	FOREIGN KEY (IDDESPACHO) REFERENCES MP_DESPACHO(IDDESPACHO);

CREATE TABLE MP_DESPACHO_PATENTE(
	IDDESPACHOPATENTE BIGINT NOT NULL,
	CODIGO_SITUACAOPROCESSOPATENTE VARCHAR(50) NOT NULL,
	CODIGO_DESPACHO varchar(50) NOT NULL,
	DESCRICAO varchar(255) NULL,
	DETALHE_DESPACHO varchar(1000) NULL);
	
ALTER TABLE MP_DESPACHO_PATENTE
	ADD CONSTRAINT PK_MP_DESPACHO_PATENTE
	PRIMARY KEY(IDDESPACHOPATENTE);

CREATE TABLE MP_PATENTECLIENTE
(
	IDCLIENTE        NUMBER NOT NULL,
	IDPATENTE        NUMBER NOT NULL
);

ALTER TABLE MP_PATENTECLIENTE
	ADD CONSTRAINT PK_MP_PATENTECLIENTE
	PRIMARY KEY (IDCLIENTE, IDPATENTE);
	
ALTER TABLE MP_PATENTECLIENTE
	ADD CONSTRAINT FK_MP_PATENTECLIENTE
	FOREIGN KEY (IDPATENTE) REFERENCES MP_PATENTE(IDPATENTE);

ALTER TABLE MP_PATENTECLIENTE
	ADD CONSTRAINT FK_MP_PATENTECLIENTE2
	FOREIGN KEY (IDCLIENTE) REFERENCES NCL_CLIENTE(IDPESSOA);


CREATE TABLE MP_REVISTA_MARCAS
(
	IDREVISTAMARCAS			NUMBER			NOT NULL,
	NUMEROREVISTAMARCAS		INT				NOT NULL,
	DATAPUBLICACAO			VARCHAR(20)		NULL,
	DATAPROCESSAMENTO		VARCHAR(20)		NULL,
	NUMEROPROCESSODEMARCA	NUMBER			NULL,
	CODIGODESPACHOANTERIOR	VARCHAR(20)		NULL,
	CODIGODESPACHOATUAL		VARCHAR(20)		NULL,
	APOSTILA				VARCHAR(255)	NULL,
	TEXTODODESPACHO			VARCHAR(4000)	NULL,
	PROCESSADA				CHAR(1)			NOT NULL,
	EXTENSAOARQUIVO			VARCHAR(4)		NULL,
	DATADODEPOSITO			VARCHAR(20)		NULL,
	DATACONCESSAO			VARCHAR(20)		NULL
);

ALTER TABLE MP_REVISTA_MARCAS
	ADD CONSTRAINT PK_MP_REVISTA_MARCAS
	PRIMARY KEY (IDREVISTAMARCAS);

CREATE TABLE MP_REVISTA_PATENTE
(
	IDREVISTAPATENTE        NUMBER			NOT NULL,
	NUMEROREVISTAPATENTE    INT				NOT NULL,
	DATAPUBLICACAO			VARCHAR(20)		NULL,
	DATAPROCESSAMENTO		VARCHAR(20)		NULL,
	PROCESSADA				CHAR(1)			NOT NULL,
	EXTENSAOARQUIVO			VARCHAR(4)		NULL,
	DATADODEPOSITO			VARCHAR(20)		NULL,
	NUMEROPROCESSOPATENTE	VARCHAR(255)	NULL,
	NUMERODOPEDIDO          NUMBER			NULL,
	DATAPUBLICPEDIDO		VARCHAR(20)		NULL,
	DATACONCESSAO			VARCHAR(20)		NULL,
	PRIORIDADEUNIONISTA		VARCHAR(255)	NULL,
	CLASSIFICACAOINTER		VARCHAR(255)	NULL,
	TITULO					VARCHAR(255)	NULL,
	RESUMO					VARCHAR(4000)	NULL,
	DADOSPEDIDOPATENTE		VARCHAR(255)	NULL,
	DADOSPATENTEORIGINAL	VARCHAR(255)	NULL,
	PRIORIDADEINTERNA	    VARCHAR(255)	NULL,
	DEPOSITANTE				VARCHAR(255)	NULL,
	INVENTOR				VARCHAR(255)	NULL,
	TITULAR					VARCHAR(255)	NULL,
	UFTITULAR				VARCHAR(255)	NULL,
	PAISTITULAR				VARCHAR(255)	NULL,
	PROCURADOR				VARCHAR(255)	NULL,
	PAISESDESIGNADOS        VARCHAR(255)	NULL,
	DATAINICIOFASENAC		VARCHAR(20)		NULL,
	DADOSDEPOSINTER         VARCHAR(255)	NULL,
	DADOSPUBLICINTER        VARCHAR(255)	NULL,
	CODIGODESPACHOANTERIOR	VARCHAR(20)		NULL,
	CODIGODESPACHOATUAL		VARCHAR(20)		NULL,	
	RESPPGTOIMPRENDA        VARCHAR(255)	NULL,
	COMPLEMENTO				VARCHAR(4000)	NULL,
	DECISAO					VARCHAR(255)	NULL,
	RECORRENTE				VARCHAR(255)	NULL,
	NUMERODOPROCESSO        NUMBER			NULL,
	CEDENTE					VARCHAR(255)	NULL,
	CESSIONARIA				VARCHAR(255)	NULL,	
	OBSERVACAO				VARCHAR(4000)	NULL,	
	ULTIMAINFORMACAO		VARCHAR(255)	NULL,	
	CERTIFAVERBACAO			VARCHAR(255)	NULL,	
	PAISCEDENTE				VARCHAR(255)	NULL,	
	PAISCESSIONARIA			VARCHAR(255)	NULL,	
	SETOR					VARCHAR(255)	NULL,	
	ENDERECOCESSIONARIA		VARCHAR(255)	NULL,	
	NATUREZADOCUMENTO		VARCHAR(255)	NULL,	
	MOEDADEPAGAMENTO		VARCHAR(255)	NULL,	
	VALOR					VARCHAR(2000)	NULL,	
	PAGAMENTO				VARCHAR(255)	NULL,	
	PRAZO					VARCHAR(1000)	NULL,	
	SERVISENTOSDEAVERBACAO  VARCHAR(255)	NULL,	
	CRIADOR					VARCHAR(255)	NULL,	
	LINGUAGEM				VARCHAR(255)	NULL,	
	CAMPOAPLICACAO			VARCHAR(255)	NULL,	
	TIPODEPROGRAMA			VARCHAR(255)	NULL,	
	DATADACRIACAO			VARCHAR(20)		NULL,	
	REGIMEDEGUARDA			VARCHAR(255)	NULL,
	REQUERENTE				VARCHAR(255)	NULL,
	REDACAO					VARCHAR(255)	NULL,
	DATAPRORROGACAO			VARCHAR(20)		NULL,	
	CLASSIFICACAONACIONAL   VARCHAR(255)	NULL	
);

ALTER TABLE MP_REVISTA_PATENTE
	ADD CONSTRAINT PK_MP_REVISTA_PATENTE
	PRIMARY KEY (IDREVISTAPATENTE);

CREATE TABLE MP_RADICAL_PATENTE (
	IDRADICAL						NUMBER				NOT NULL,
	COLIDENCIA						VARCHAR(50)			NOT NULL,
	IDPATENTE						NUMBER				NOT NULL);

ALTER TABLE MP_RADICAL_PATENTE
	ADD CONSTRAINT PK_MP_RADICAL_PATENTE
	PRIMARY KEY (IDRADICAL);

CREATE TABLE MP_TITULAR (
	IDPESSOA						NUMBER				NOT NULL,
	TIPOPESSOA						SMALLINT			NOT NULL,
	DTCADASTRO						NUMBER					NOT NULL,
	INFOADICIONAL					VARCHAR(4000)		NULL
);

ALTER TABLE MP_TITULAR
	ADD CONSTRAINT PK_MP_TITULAR
	PRIMARY KEY (IDPessoa);

ALTER TABLE MP_TITULAR
	ADD CONSTRAINT FK_MP_TITULAR1
	FOREIGN KEY(IDPESSOA)
	REFERENCES NCL_PESSOA(ID);

CREATE TABLE MP_PATENTETITULAR
(
	IDTITULAR	NUMBER	NOT NULL,
	IDPATENTE	NUMBER	NOT NULL
);

ALTER TABLE MP_PATENTETITULAR
	ADD CONSTRAINT PK_MP_PATENTETITULAR
	PRIMARY KEY (IDTITULAR, IDPATENTE);
	
ALTER TABLE MP_PATENTETITULAR
	ADD CONSTRAINT FK_MP_PATENTETITULAR
	FOREIGN KEY (IDPATENTE) REFERENCES MP_PATENTE(IDPATENTE);

ALTER TABLE MP_PATENTETITULAR
	ADD CONSTRAINT FK_MP_PATENTETITULAR2
	FOREIGN KEY (IDTITULAR) REFERENCES MP_TITULAR(IDPESSOA);