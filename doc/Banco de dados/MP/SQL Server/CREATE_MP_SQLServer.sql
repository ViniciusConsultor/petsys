﻿CREATE TABLE MP_TIPO_PATENTE (
	IDTIPO_PATENTE 				BIGINT 				NOT NULL,
	DESCRICAO_TIPO_PATENTE 		VARCHAR(4000) 		NOT	NULL,
	SIGLA_TIPO 					CHAR(2) 			NOT	NULL,
	TEMPO_INICIO_ANOS 			INT 				NOT	NULL,
	QUANTIDADE_PAGTO 			INT					NOT	NULL,
	TEMPO_ENTRE_PAGTO 			INT	 				NOT	NULL,
	SEQUENCIA_INICIO_PAGTO 		INT 				NOT	NULL,
	TEM_PAGTO_INTERMEDIARIO 	CHAR(1) 				NULL,
	INICIO_INTERMED_SEQUENCIA 	INT 				NOT	NULL,
	QUANTIDADE_PAGTO_INTERMED 	INT 				NOT	NULL,
	TEMPO_ENTRE_PAGTO_INTERMED 	INT					NOT	NULL,
	DESCRICAO_PAGTO 			VARCHAR(4000) 		NOT	NULL,
	DESCRICAO_PAGTO_INTERMED 	VARCHAR(4000) 		NOT	NULL,
	TEM_PED_EXAME 				CHAR(1) 			    NULL
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
	IDPESSOA					BIGINT				NOT NULL,
	TIPOPESSOA					SMALLINT			NOT NULL,
	DTCADASTRO					INT					NOT NULL,
	INFOADICIONAL				VARCHAR(4000)		NULL
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
	IDTIPO_PROCEDIMENTO_INTERNO BIGINT				NOT NULL,
	DESCRICAO_TIPO				VARCHAR(255)		NOT NULL
);
	
ALTER TABLE MP_TIPO_PROCEDIMENTO_INTERNO
	ADD CONSTRAINT PK_MP_TIPO_PROCEDIMENTO_INTERNO
	PRIMARY KEY(IDTIPO_PROCEDIMENTO_INTERNO);

CREATE TABLE MP_PROCURADORES
(
	IDPESSOA BIGINT NOT NULL,
	TIPOPESSOA	SMALLINT NOT NULL,
	MATRICULAAPI VARCHAR(22),
	SIGLAORGAO CHAR(10),
	NRREGISTROORGAO VARCHAR(17),
	DATAREGISTROORGAO INT,
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
	IDDESPACHO bigint NOT NULL,
	CODIGO_DESPACHO int NOT NULL,
	DETALHE_DESPACHO varchar(4000) NULL,
	IDSITUACAO_PROCESSO bigint NULL,
	REGISTRO char(1) NULL);
	
	ALTER TABLE MP_DESPACHO
	ADD CONSTRAINT PK_DESPACHO
	PRIMARY KEY(IDDESPACHO);
	

CREATE TABLE MP_LAYOUT_REVISTA_PATENTE
(
	IDCODIGOREVISTA BIGINT NOT NULL,
	NOMEDOCAMPO VARCHAR(50) NOT NULL,
	DESCRICAO_CAMPO VARCHAR(120),
	DESCRICAO_RESUMIDA VARCHAR(50),
	TAMANHO_CAMPO INT NOT NULL,
	CAMPO_DELIMITADOR_REGISTRO CHAR(1) NOT NULL,
	CAMPO_IDENTIFICADOR_PROCESSO CHAR(1) NOT NULL,
	CAMPO_IDENTIFICADOR_COLIDENCIA CHAR(1) NOT NULL
);

ALTER TABLE MP_LAYOUT_REVISTA_PATENTE
	ADD CONSTRAINT PK_MP_LAYOUT_REVISTA_PATENTE
	PRIMARY KEY (IDCODIGOREVISTA);

CREATE TABLE MP_PROCESSOPATENTE
(
	IDPROCESSOPATENTE BIGINT NOT NULL,
	NUMEROPROTOCOLO INT,
	NUMEROPROCESSO	VARCHAR(25),
	TITULOPATENTE VARCHAR(500),
	DATAENTRADA INT,
	PROCESSODETERCEIRO CHAR(1) NOT NULL,
	DATACONCESSAO_REGISTRO	INT,
	IDTIPOPATENTE BIGINT NOT NULL,
	IDPROCURADOR BIGINT,
	IDDESPACHO BIGINT,
	LINKINPI VARCHAR(100),
	OBRIGACAOGERADA CHAR(1) NOT NULL,
	DATACADASTRO INT,
	OBSERVACAO VARCHAR(250),
	RESUMO_PATENTE VARCHAR(500),
	ESTRANGEIRO CHAR(1),
	PCT	CHAR(1),
	NUMEROPCT VARCHAR(20),
	NUMEROWO VARCHAR(20),
	DATA_DEPOSITOPCT INT,
	DATA_PUBLICACAOPCT	INT,
	ATIVO CHAR(1) NOT NULL,
	QTDEREINVINDICACAO	INT
);

ALTER TABLE MP_PROCESSOPATENTE
	ADD CONSTRAINT PK_MP_PROCESSOPATENTE
	PRIMARY KEY (IDPROCESSOPATENTE);

ALTER TABLE MP_PROCESSOPATENTE
	ADD CONSTRAINT FK_MP_PROCESSOPATENTE
	FOREIGN KEY (IDPROCURADOR) REFERENCES MP_PROCURADORES(IDPESSOA);

ALTER TABLE MP_PROCESSOPATENTE
	ADD CONSTRAINT FK_MP_PROCESSOPATENTE2
	FOREIGN KEY (IDDESPACHO) REFERENCES MP_DESPACHO(IDDESPACHO);

ALTER TABLE MP_PROCESSOPATENTE
	ADD CONSTRAINT FK_MP_PROCESSOPATENTE3
	FOREIGN KEY (IDTIPOPATENTE) REFERENCES MP_TIPO_PATENTE(IDTIPO_PATENTE);

CREATE TABLE MP_PATENTEANUIDADE
(
	IDPATENTEANUIDADE BIGINT NOT NULL,
	IDPATENTE BIGINT NOT NULL,
	DESCRICAOANUIDADE VARCHAR(45),
	DATALANCAMENTO INT,
	DATAVENCIMENTO INT,
	DATAPAGAMENTO INT,
	VALORPAGAMENTO FLOAT,
	ANUIDADEPAGA CHAR(1) NOT NULL,
	PEDIDOEXAME CHAR(1),
	DATAVENCTO_SEM_MULTA INT,
	DATAVENCTO_COM_MULTA INT
);

ALTER TABLE MP_PATENTEANUIDADE
	ADD CONSTRAINT PK_MP_PATENTEANUIDADE
	PRIMARY KEY (IDPATENTEANUIDADE);
	
ALTER TABLE MP_PATENTEANUIDADE
	ADD CONSTRAINT FK_MP_PATENTEANUIDADE
	FOREIGN KEY (IDPATENTE) REFERENCES MP_PROCESSOPATENTE(IDPROCESSOPATENTE);	

CREATE TABLE MP_PATENTECLASSIFICACAO
(
	IDPATENTECLASSIFICACAO BIGINT NOT NULL,
	CLASSIFICACAO VARCHAR(20) NOT NULL,
	DESCRICAO_CLASSIFICACAO	VARCHAR(100),
	IDPATENTE BIGINT NOT NULL,
	TIPO_CLASSIFICACAO INT NOT NULL
);

ALTER TABLE MP_PATENTECLASSIFICACAO
	ADD CONSTRAINT PK_MP_PATENTECLASSIFICACAO
	PRIMARY KEY (IDPATENTECLASSIFICACAO);
	
ALTER TABLE MP_PATENTECLASSIFICACAO
	ADD CONSTRAINT FK_MP_PATENTECLASSIFICACAO
	FOREIGN KEY (IDPATENTE) REFERENCES MP_PROCESSOPATENTE(IDPROCESSOPATENTE);	

CREATE TABLE MP_PATENTEPRIORIDADEUNIONISTA
(
	IDPRIORIDADEUNIONISTA BIGINT NOT NULL,
	DATA_PRIORIDADE	INT,
	NUMERO_PRIORIDADE varchar(20),
	IDPATENTE BIGINT NOT NULL,
	IDPAIS	BIGINT
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
	IDPATENTETITULARINVENTOR BIGINT NOT NULL,
	IDPATENTE BIGINT NOT NULL,
	IDPROCURADOR BIGINT NOT NULL,
	CONTATO_TITULAR CHAR(10)
);

ALTER TABLE MP_PATENTETITULARINVENTOR
	ADD CONSTRAINT PK_MP_PATENTETITULARINVENTOR
	PRIMARY KEY (IDPATENTETITULARINVENTOR);
	
ALTER TABLE MP_PATENTETITULARINVENTOR
	ADD CONSTRAINT FK_MP_PATENTETITULARINVENTOR
	FOREIGN KEY (IDPATENTE) REFERENCES MP_PROCESSOPATENTE(IDPROCESSOPATENTE);		
	
ALTER TABLE MP_PATENTETITULARINVENTOR
	ADD CONSTRAINT FK_MP_PATENTETITULARINVENTOR2
	FOREIGN KEY (IDPROCURADOR) REFERENCES MP_PROCURADORES(IDPESSOA);


CREATE TABLE MP_MARCAS(
	IDMARCA BIGINT NOT NULL,
	CODIGONCL INT NOT NULL,
	CODIGOAPRESENTACAO INT NOT NULL,
	IDCLIENTE BIGINT NOT NULL,
	CODIGONATUREZA INT NOT NULL,
	DESCRICAO_MARCA VARCHAR(4000) NULL,
	ESPECIFICACAO_PROD_SERV VARCHAR(4000) NULL,
	IMAGEM_MARCA VARCHAR(255) NULL,
	OBSERVACAO_MARCA VARCHAR(4000) NULL,
	CODIGOCLASSE INT NULL,
	CODIGOCLASSE_SUBCLASSE1 INT NULL,
	CODIGOCLASSE_SUBCLASSE2 INT NULL,
	CODIGOCLASSE_SUBCLASSE3 INT NULL)	

ALTER TABLE MP_MARCAS
	ADD CONSTRAINT PK_MARCAS
	PRIMARY KEY (IDMARCA);	
	
ALTER TABLE MP_MARCAS
	ADD CONSTRAINT FK_MARCAS_CONTATO
	FOREIGN KEY (IDCLIENTE) REFERENCES NCL_CLIENTE(IDPESSOA);