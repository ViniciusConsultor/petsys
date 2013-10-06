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