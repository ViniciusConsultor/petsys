﻿CREATE TABLE MP_BOLETOS_GERADOS(
ID	BIGINT NOT NULL,
NUMEROBOLETO BIGINT NOT NULL,
NOSSONUMERO BIGINT NOT NULL,
IDCLIENTE BIGINT NOT NULL,
VALOR FLOAT NOT NULL,
DATAGERACAO VARCHAR(20) NOT NULL,
DATAVENCIMENTO VARCHAR(20) NOT NULL,
NUMEROPROCESSO BIGINT NULL,
OBSERVACAO VARCHAR(4000) NULL);

ALTER TABLE MP_BOLETOS_GERADOS
	ADD CONSTRAINT PK_BOLETOS_GERADOS
	PRIMARY KEY (ID);	
	
ALTER TABLE MP_BOLETOS_GERADOS
	ADD CONSTRAINT FK_BOLETOS_GERADOS_NCL_CLIENTE
	FOREIGN KEY (IDCLIENTE) REFERENCES NCL_CLIENTE(IDPESSOA);

CREATE INDEX IDX_MP_BOLETOS_GERADOS1
	ON MP_BOLETOS_GERADOS(NUMEROBOLETO);
	
CREATE INDEX IDX_MP_BOLETOS_GERADOS2
	ON MP_BOLETOS_GERADOS(IDCLIENTE);

CREATE TABLE MP_BOLETOS_GERADOS_AUX(
ID	BIGINT NOT NULL,
PROXNOSSONUMERO BIGINT NOT NULL,
PROXNUMEROBOLETO BIGINT NOT NULL);

ALTER TABLE MP_BOLETOS_GERADOS_AUX
	ADD CONSTRAINT PK_BOLETOS_GERADOS_AUX
	PRIMARY KEY (ID);	

ALTER TABLE MP_PROCESSOPATENTE ADD PAIS BIGINT NULL;

ALTER TABLE MP_PROCESSOPATENTE ADD CONSTRAINT FK_MP_PROCESSOPATENTE3 FOREIGN KEY (PAIS) REFERENCES NCL_PAIS(ID);

UPDATE MP_PROCESSOPATENTE SET PAIS = 10030;
ALTER TABLE MP_PROCESSOPATENTE ADD CONSTRAINT FK_MP_PROCESSOPATENTE3 FOREIGN KEY (PAIS) REFERENCES NCL_PAIS(ID);

INSERT INTO NCL_FUNCAO (IDMODULO, IDFUNCAO, NOME) VALUES ('MOD.NCL','FUN.NCL.019','Cadastro de Cedentes');
		INSERT INTO NCL_OPERACAO (IDMODULO, IDFUNCAO, IDOPERACAO, NOME) VALUES ('MOD.NCL','FUN.NCL.019','OPE.NCL.019.0001','Inserir');
		INSERT INTO NCL_OPERACAO (IDMODULO, IDFUNCAO, IDOPERACAO, NOME) VALUES ('MOD.NCL','FUN.NCL.019','OPE.NCL.019.0002','Modificar');
		INSERT INTO NCL_OPERACAO (IDMODULO, IDFUNCAO, IDOPERACAO, NOME) VALUES ('MOD.NCL','FUN.NCL.019','OPE.NCL.019.0003','Excluir');	
INSERT INTO NCL_MENUFUNCAO (IDMODULO, IDFUNCAO, IMAGEM, URL, AGRUPADOR) VALUES ('MOD.NCL','FUN.NCL.019','bogus','Nucleo/cdCedente.aspx', 'Cadastros');

CREATE TABLE NCL_CEDENTE (
	IDPESSOA			BIGINT			NOT NULL,
	TIPOPESSOA			SMALLINT		NOT NULL,
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

INSERT INTO NCL_FUNCAO (IDMODULO, IDFUNCAO, NOME) VALUES ('MOD.MP','FUN.MP.018','Configurações do módulo');
		INSERT INTO NCL_OPERACAO (IDMODULO, IDFUNCAO, IDOPERACAO, NOME) VALUES ('MOD.MP','FUN.MP.018','OPE.MP.018.0001','Salvar');

INSERT INTO NCL_MENUFUNCAO (IDMODULO, IDFUNCAO, IMAGEM, URL, AGRUPADOR) VALUES ('MOD.MP','FUN.MP.018','bogus','MP/frmConfiguracaoDoModulo.aspx', 'Configurações');

INSERT INTO NCL_OPERACAO (IDMODULO, IDFUNCAO, IDOPERACAO, NOME) VALUES ('MOD.MP','FUN.MP.007','OPE.MP.007.0005','Enviar e-mail');	

INSERT INTO NCL_OPERACAO (IDMODULO, IDFUNCAO, IDOPERACAO, NOME) VALUES ('MOD.MP','FUN.MP.009','OPE.MP.009.0005','Enviar e-mail');	

CREATE TABLE MP_CNFMODGRL (
	VALORSALARIOMINIO	FLOAT				NULL,
	IMAGEMBOLETO		VARCHAR(255)		NULL,
	IDCEDENTE			BIGINT				NULL,
	TIPOPESSOA			SMALLINT			NULL
)
;

ALTER TABLE MP_CNFMODGRL
	ADD CONSTRAINT FK_MP_CNFMODGRL1
	FOREIGN KEY(IDCEDENTE)
	REFERENCES NCL_CEDENTE(IDPESSOA)
;


ALTER TABLE MP_PATENTE ADD IMAGEM VARCHAR(255) NULL;
