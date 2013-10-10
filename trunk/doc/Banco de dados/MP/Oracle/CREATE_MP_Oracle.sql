﻿CREATE TABLE MP_TIPO_PATENTE (
	IDTIPO_PATENTE 				NUMBER(11) 				NOT NULL,
	DESCRICAO_TIPO_PATENTE 		VARCHAR2(255) 			NOT	NULL,
	SIGLA_TIPO 					CHAR(2) 				NOT	NULL,
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
)
;
ALTER TABLE MP_TIPO_PATENTE
	ADD CONSTRAINT PK_TIPO_PATENTE
	PRIMARY KEY(IDTIPO_PATENTE)
;

ALTER TABLE MP_TIPO_PATENTE
ADD CONSTRAINT TIPO_PATENTE_UNIQUE UNIQUE (DESCRICAO_TIPO_PATENTE, SIGLA_TIPO);


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
	CODIGO_DESPACHO NUMBER(11) NOT NULL,
	DETALHE_DESPACHO varchar(4000) NULL,
	IDSITUACAO_PROCESSO NUMBER(11) NULL,
	REGISTRO char(1) NULL);
	
	ALTER TABLE MP_DESPACHO
	ADD CONSTRAINT PK_DESPACHO
	PRIMARY KEY(IDDESPACHO);


CREATE TABLE MP_LAYOUT_REVISTA_PATENTE
(
	IDCODIGOREVISTA NUMBER NOT NULL,
	NOMEDOCAMPO VARCHAR(50) NOT NULL,
	DESCRICAO_CAMPO VARCHAR(120),
	DESCRICAO_RESUMIDA VARCHAR(50),
	TAMANHO_CAMPO NUMBER NOT NULL,
	CAMPO_DELIMITADOR_REGISTRO CHAR(1) NOT NULL,
	CAMPO_IDENTIFICADOR_PROCESSO CHAR(1) NOT NULL,
	CAMPO_IDENTIFICADOR_COLIDENCIA CHAR(1) NOT NULL
)

ALTER TABLE MP_LAYOUT_REVISTA_PATENTE
	ADD CONSTRAINT PK_MP_LAYOUT_REVISTA_PATENTE
	PRIMARY KEY (IDCODIGOREVISTA);

CREATE TABLE MP_PROCESSOPATENTE
(
	IDPROCESSOPATENTE NUMBER NOT NULL,
	NUMEROPROTOCOLO NUMBER,
	NUMEROPROCESSO	VARCHAR(25),
	TITULOPATENTE VARCHAR(500),
	DATAENTRADA NUMBER,
	PROCESSODETERCEIRO CHAR(1) NOT NULL,
	DATACONCESSAO_REGISTRO	NUMBER,
	IDTIPOPATENTE NUMBER NOT NULL,
	IDPROCURADOR NUMBER,
	IDDESPACHO NUMBER,
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
	DATA_PUBLICACAOPCT	NUMBER,
	ATIVO CHAR(1) NOT NULL,
	QTDEREINVINDICACAO	NUMBER
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
	DESCRICAO_CLASSIFICACAO	VARCHAR(100),
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
	IDPATENTETITULARINVENTOR NUMBER NOT NULL,
	IDPATENTE NUMBER NOT NULL,
	IDPROCURADOR NUMBER NOT NULL,
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