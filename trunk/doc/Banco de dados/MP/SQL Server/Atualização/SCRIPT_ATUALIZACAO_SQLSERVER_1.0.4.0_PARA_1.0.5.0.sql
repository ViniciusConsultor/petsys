﻿UPDATE NCL_FUNCAO SET NOME ='Contas a Receber' WHERE IDFUNCAO='FUN.FN.001';
UPDATE NCL_MENUFUNCAO SET URL='FN/frmContasAReceber.aspx' WHERE IDFUNCAO = 'FUN.FN.001';

CREATE TABLE FN_ITEMFINANREC (
	ID				BIGINT			NOT NULL,
	IDCLIENTE		BIGINT			NOT NULL,
	VALOR			FLOAT			NOT NULL,
	OBSERVACAO		VARCHAR(4000)		NULL,
	DATALACAMENTO	INT				NOT NULL,
	SITUACAO		SMALLINT		NOT NULL,
	DATARECEBIMENTO	INT					NULL,
	TIPOLANCAMENTO	SMALLINT		NOT NULL,
	DATAVENCIMENTO	INT				NOT	NULL
);

ALTER TABLE FN_ITEMFINANREC
	ADD CONSTRAINT PK_FN_ITEMFINANREC
	PRIMARY KEY (ID);	

ALTER TABLE FN_ITEMFINANREC
	ADD CONSTRAINT FK_FN_ITEMFINANREC_NCL_CLIENTE
	FOREIGN KEY (IDCLIENTE) REFERENCES NCL_CLIENTE(IDPESSOA);

CREATE TABLE MP_INTERFACEFN (
	IDITEMRECEBIMENTO	BIGINT NOT NULL,
	CONCEITO			VARCHAR(255) NOT NULL,
	IDCONCEITO			BIGINT	NOT NULL,
	DATAVENCIMENTO		INT		NOT NULL
);

ALTER TABLE MP_INTERFACEFN
	ADD CONSTRAINT PK_MP_INTERFACEFN
	PRIMARY KEY(IDITEMRECEBIMENTO, CONCEITO, IDCONCEITO, DATAVENCIMENTO);
;
