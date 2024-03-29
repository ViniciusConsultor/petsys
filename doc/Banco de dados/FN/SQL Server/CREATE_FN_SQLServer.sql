﻿CREATE TABLE FN_BOLETOS_GERADOS (
	ID					BIGINT					NOT NULL,
	NUMEROBOLETO		BIGINT					NOT NULL,
	NOSSONUMERO			BIGINT						NULL,
	IDCLIENTE			BIGINT					NOT NULL,
	VALOR				FLOAT					NOT NULL,
	DATAGERACAO			VARCHAR(20)				NOT NULL,
	DATAVENCIMENTO		VARCHAR(20)				NOT NULL,
	OBSERVACAO			VARCHAR(4000)				NULL,
	STATUSBOLETO		VARCHAR(20)					NULL,
	EHBOLETOAVULSO		CHAR(1)						NULL,
	IDCEDENTE			BIGINT						NULL,
	INSTRUCOES			VARCHAR(4000)				NULL
);

ALTER TABLE FN_BOLETOS_GERADOS
	ADD CONSTRAINT PK_FN_BOLETOS_GERADOS
	PRIMARY KEY (ID);	
	
ALTER TABLE FN_BOLETOS_GERADOS
	ADD CONSTRAINT FK_FN_BOLETOS_GERADOS_NCL_CLIENTE
	FOREIGN KEY (IDCLIENTE) REFERENCES NCL_CLIENTE(IDPESSOA);

CREATE INDEX IDX_FN_BOLETOS_GERADOS1
	ON FN_BOLETOS_GERADOS(NUMEROBOLETO);
	
CREATE INDEX IDX_FN_BOLETOS_GERADOS2
	ON FN_BOLETOS_GERADOS(IDCLIENTE);

CREATE TABLE FN_BOLETOS_GERADOS_AUX(
	ID					BIGINT					NOT NULL,
	PROXNOSSONUMERO		BIGINT					NOT NULL,
	IDCEDENTE			BIGINT					NOT NULL
);

ALTER TABLE FN_BOLETOS_GERADOS_AUX
	ADD CONSTRAINT PK_FN_BOLETOS_GERADOS_AUX
	PRIMARY KEY (ID);	

CREATE TABLE FN_CNFBOLETO (
	IMAGEMBOLETO		VARCHAR(255)				NULL,
	IDCEDENTE			BIGINT						NULL,
	TIPOPESSOA			SMALLINT					NULL
);

ALTER TABLE FN_CNFBOLETO
	ADD CONSTRAINT FK_FN_CNFMODGRL1
	FOREIGN KEY(IDCEDENTE)
	REFERENCES NCL_CEDENTE(IDPESSOA)
;

CREATE TABLE FN_CNFINDFINAN (
	VALORSALMIN			FLOAT						NULL
);

CREATE TABLE FN_ITEMFINANREC	 (
	ID							BIGINT			NOT NULL,
	IDCLIENTE					BIGINT			NOT NULL,
	VALOR						FLOAT			NOT NULL,
	OBSERVACAO					VARCHAR(4000)		NULL,
	DATALACAMENTO				INT				NOT NULL,
	SITUACAO					SMALLINT		NOT NULL,
	DATARECEBIMENTO				INT					NULL,
	TIPOLANCAMENTO				SMALLINT		NOT NULL,
	DATAVENCIMENTO				INT				NOT	NULL,
	DESCRICAO					VARCHAR(255)		NULL,
	FORMARECEBIMENTO			SMALLINT			NULL,
	NUMEROBOLETOGERADO			VARCHAR(20)			NULL,
	BOLETOGERADOCOLETIVAMENTE	CHAR(1)			NOT NULL
);

ALTER TABLE FN_ITEMFINANREC
	ADD CONSTRAINT PK_FN_ITEMFINANREC
	PRIMARY KEY (ID);	

ALTER TABLE FN_ITEMFINANREC
	ADD CONSTRAINT FK_FN_ITEMFINANREC_NCL_CLIENTE
	FOREIGN KEY (IDCLIENTE) REFERENCES NCL_CLIENTE(IDPESSOA);

CREATE INDEX IDX_FN_ITEMFINANREC1
	ON FN_ITEMFINANREC(SITUACAO, DATALACAMENTO);
	
CREATE INDEX IDX_FN_ITEMFINANREC2
	ON FN_ITEMFINANREC(SITUACAO, IDCLIENTE, DATALACAMENTO);

CREATE INDEX IDX_FN_ITEMFINANREC3
	ON FN_ITEMFINANREC(SITUACAO, DESCRICAO, DATALACAMENTO);

CREATE INDEX IDX_FN_ITEMFINANREC4
	ON FN_ITEMFINANREC(SITUACAO, DATAVENCIMENTO, DATALACAMENTO);
	
CREATE INDEX IDX_FN_ITEMFINANREC5
	ON FN_ITEMFINANREC(SITUACAO, FORMARECEBIMENTO, DATALACAMENTO);

CREATE INDEX IDX_FN_ITEMFINANREC6
	ON FN_ITEMFINANREC(SITUACAO, FORMARECEBIMENTO, DATALACAMENTO);

CREATE TABLE FN_CONFIGGERAL (
	INSTRUCOESDOBOLETO				VARCHAR(4000)
);

CREATE TABLE FN_ITEMFINANRECBOLETO (
	IDITEMFINANREC		BIGINT					NOT NULL,
	IDBOLETO			BIGINT						NULL
);


ALTER TABLE FN_ITEMFINANRECBOLETO
	ADD CONSTRAINT PK_FN_ITEMFINANRECBOLETO
	PRIMARY KEY (IDITEMFINANREC);	
