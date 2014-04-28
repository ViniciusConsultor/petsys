﻿ALTER TABLE NCL_CEDENTE ADD TIPODECARTEIRA VARCHAR(50);

ALTER TABLE NCL_CEDENTE ADD INICIONOSSONUMERO BIGINT;

ALTER TABLE FN_ITEMFINANREC ADD IDBOLETO BIGINT;

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

UPDATE NCL_PESSOA SET IDBANCO =NULL, IDAGENCIA = NULL, CNTACORRENTE = NULL, TIPOCNTACORRENTE = NULL;

ALTER TABLE NCL_PESSOA
 DROP CONSTRAINT "FK_NCL_PESSOA2";

ALTER TABLE NCL_AGENCIABANCO
 DROP CONSTRAINT "FK_NCL_AGENCIABANCO2";

ALTER TABLE NCL_AGENCIABANCO
 DROP CONSTRAINT "FK_NCL_AGENCIABANCO1";

DROP TABLE NCL_AGENCIABANCO;

DROP TABLE NCL_BANCO;

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

ALTER TABLE NCL_PESSOA  ALTER COLUMN IDBANCO CHAR(3);

UPDATE NCL_FUNCAO SET NOME = 'Cadastro de Agência Bancária' WHERE IDFUNCAO = 'FUN.NCL.009';
UPDATE NCL_OPERACAO SET NOME = 'Inserir'	WHERE IDOPERACAO = 'OPE.NCL.009.0001';
UPDATE NCL_OPERACAO SET NOME = 'Modificar'	WHERE IDOPERACAO = 'OPE.NCL.009.0002';
UPDATE NCL_OPERACAO SET NOME = 'Excluir'	WHERE IDOPERACAO = 'OPE.NCL.009.0003';
DELETE FROM NCL_AUTORIZACAO WHERE IDDIRETIVA = 'OPE.NCL.009.0004';
DELETE FROM NCL_AUTORIZACAO WHERE IDDIRETIVA = 'OPE.NCL.009.0005';
DELETE FROM NCL_AUTORIZACAO WHERE IDDIRETIVA = 'OPE.NCL.009.0006';
DELETE FROM NCL_OPERACAO WHERE IDOPERACAO = 'OPE.NCL.009.0004';
DELETE FROM NCL_OPERACAO WHERE IDOPERACAO = 'OPE.NCL.009.0005';
DELETE FROM NCL_OPERACAO WHERE IDOPERACAO = 'OPE.NCL.009.0006';

CREATE TABLE FN_CONFIGGERAL (
	INSTRUCOESDOBOLETO VARCHAR(4000)
);