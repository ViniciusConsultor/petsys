﻿
CREATE TABLE NCL_HISTORICOEMAIL (
	ID					BIGINT			NOT NULL,
	DATA				VARCHAR(30)		NOT NULL,
	POSSUIANEXO			CHAR(1)			NOT NULL,
	ASSUNTO				VARCHAR(4000)		NULL,
	REMETENTE			VARCHAR(1000)	NOT NULL,
	MENSAGEM			VARCHAR(4000)		NULL,
	DESTINATARIOSCC		VARCHAR(4000)	NOT NULL,
	DESTINATARIOSCCO	VARCHAR(4000)		NULL,
	CONTEXTO			VARCHAR(1000)		NULL
);


ALTER TABLE NCL_HISTORICOEMAIL
	ADD CONSTRAINT PK_NCL_HISTORICOEMAIL
	PRIMARY KEY(ID)
;

CREATE TABLE NCL_HISTORICOEMAILANEXO (
	IDHISTORICO			BIGINT			NOT NULL,
	NOMEANEXO			VARCHAR(1000)	NOT NULL,
	INDICE				INT				NOT NULL,
	BINARIOANEXO		IMAGE			NOT NULL
);	

ALTER TABLE NCL_HISTORICOEMAILANEXO
	ADD CONSTRAINT PK_NCL_HISTORICOEMAILANEXO
	PRIMARY KEY(IDHISTORICO, INDICE)
;

ALTER TABLE NCL_HISTORICOEMAILANEXO
	ADD CONSTRAINT FK_NCL_HISTORICOEMAILANEXO1
	FOREIGN KEY(IDHISTORICO)
	REFERENCES NCL_HISTORICOEMAIL(ID)
;

INSERT INTO NCL_FUNCAO (IDMODULO, IDFUNCAO, NOME) VALUES ('MOD.NCL','FUN.NCL.020','Histórico de e-mails');
	INSERT INTO NCL_OPERACAO (IDMODULO, IDFUNCAO, IDOPERACAO, NOME) VALUES ('MOD.NCL','FUN.NCL.020','OPE.NCL.020.0001','Detalhar');
	INSERT INTO NCL_OPERACAO (IDMODULO, IDFUNCAO, IDOPERACAO, NOME) VALUES ('MOD.NCL','FUN.NCL.020','OPE.NCL.020.0002','Reenviar');
INSERT INTO NCL_MENUFUNCAO (IDMODULO, IDFUNCAO, IMAGEM, URL, AGRUPADOR) VALUES ('MOD.NCL','FUN.NCL.020','bogus','Nucleo/frmHistoricoDeEmails.aspx', NULL);

INSERT INTO NCL_FUNCAO (IDMODULO, IDFUNCAO, NOME) VALUES ('MOD.MP','FUN.MP.018','Relatório de Manutenções');
	INSERT INTO NCL_MENUFUNCAO (IDMODULO, IDFUNCAO, IMAGEM, URL, AGRUPADOR) VALUES ('MOD.MP','FUN.MP.018','bogus','MP/frmRelatorioDeManutencoes.aspx', 'Relatórios');