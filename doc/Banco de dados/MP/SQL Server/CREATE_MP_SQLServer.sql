﻿CREATE TABLE MP_TIPO_PATENTE (
	IDTIPO_PATENTE 				BIGINT 				NOT NULL,
	DESCRICAO_TIPO_PATENTE 		VARCHAR(255) 		NOT	NULL,
	SIGLA_TIPO 					CHAR(2) 			NOT	NULL,
	TEMPO_INICIO_ANOS 			INT 				NOT	NULL,
	QUANTIDADE_PAGTO 			INT					NOT	NULL,
	TEMPO_ENTRE_PAGTO 			INT	 				NOT	NULL,
	SEQUENCIA_INICIO_PAGTO 		INT 				NOT	NULL,
	TEM_PAGTO_INTERMEDIARIO 	CHAR(1) 				NULL,
	INICIO_INTERMED_SEQUENCIA 	INT 				NOT	NULL,
	QUANTIDADE_PAGTO_INTERMED 	INT 				NOT	NULL,
	TEMPO_ENTRE_PAGTO_INTERMED 	INT					NOT	NULL,
	DESCRICAO_PAGTO 			VARCHAR(255) 		NOT	NULL,
	DESCRICAO_PAGTO_INTERMED 	VARCHAR(255) 		NOT	NULL,
	TEM_PED_EXAME 				CHAR(1) 			    NULL
)
;
ALTER TABLE MP_TIPO_PATENTE
	ADD CONSTRAINT PK_TIPO_PATENTE
	PRIMARY KEY(IDTIPO_PATENTE, DESCRICAO_TIPO_PATENTE, SIGLA_TIPO)
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

CREATE TABLE MP_TIPO_ANDAMENTO_INTERNO(
	IDTIPO_ANDAMENTO_INTERNO bigint NOT NULL,
	DESCRICAO_TIPO varchar(255) NOT NULL);
	
	ALTER TABLE MP_TIPO_ANDAMENTO_INTERNO
	ADD CONSTRAINT PK_TIPO_ANDAMENTO_INTERNO
	PRIMARY KEY(IDTIPO_ANDAMENTO_INTERNO);

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
	
	
CREATE TABLE MP_SITUACAO_PROCESSO(
	IDSITUACAO_PROCESSO bigint NOT NULL,
	DESCRICAO_SITUACAO varchar(50) NULL);
	
	ALTER TABLE MP_SITUACAO_PROCESSO
	ADD CONSTRAINT PK_SITUACAO_PROCESSO
	PRIMARY KEY(IDSITUACAO_PROCESSO);

CREATE TABLE MP_LAYOUT_REVISTA_PATENTE
(
	IDCODIGOIDENTIFICACAOREVISTA INT NOT NULL,
	IDENTIFICADOR VARCHAR(50) NOT NULL,
	DESCRICAO_IDENTIFICADOR VARCHAR(120),
	DESCRICAO_RESUMIDA VARCHAR(50),
	TIPO_IDENTIFICADOR INT NOT NULL,
	TAMANHO_CAMPO INT NOT NULL,
	CAMPO_DELIMITADOR_REGISTRO CHAR(1) NOT NULL,
	CAMPO_IDENTIFICADOR_PROCESSO CHAR(1) NOT NULL,
	CAMPO_IDENTIFICADOR_COLIDENCIA CHAR(1) NOT NULL
)

ALTER TABLE MP_LAYOUT_REVISTA_PATENTE
	ADD CONSTRAINT PK_MP_LAYOUT_REVISTA_PATENTE
	PRIMARY KEY (IDCODIGOIDENTIFICACAOREVISTA);