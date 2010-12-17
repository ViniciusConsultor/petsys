ALTER TABLE NCL_COMPROMISSO ALTER COLUMN ASSUNTO VARCHAR(4000);
ALTER TABLE NCL_COMPROMISSO ALTER COLUMN LOCAL VARCHAR(4000);
ALTER TABLE NCL_COMPROMISSO ALTER COLUMN DESCRICAO VARCHAR(4000);
ALTER TABLE NCL_LEMBRETE ALTER COLUMN ASSUNTO VARCHAR(4000);
ALTER TABLE NCL_LEMBRETE ALTER COLUMN LOCAL VARCHAR(4000);
ALTER TABLE NCL_LEMBRETE ALTER COLUMN DESCRICAO VARCHAR(4000);
ALTER TABLE NCL_TAREFA ALTER COLUMN ASSUNTO VARCHAR(4000);
ALTER TABLE NCL_TAREFA ALTER COLUMN DESCRICAO VARCHAR(4000);
EXEC sp_rename 'NCL_AGENDA', 'NCL_CNFAGENDAUSU';

DELETE FROM NCL_OPERACAO WHERE IDFUNCAO ='FUN.NCL.012';
DELETE FROM NCL_AUTORIZACAO WHERE IDDIRETIVA LIKE 'OPE.NCL.012%'
INSERT INTO NCL_OPERACAO (IDMODULO, IDFUNCAO, IDOPERACAO, NOME) VALUES ('MOD.NCL','FUN.NCL.012','OPE.NCL.012.0001','Inserir compromisso');
INSERT INTO NCL_OPERACAO (IDMODULO, IDFUNCAO, IDOPERACAO, NOME) VALUES ('MOD.NCL','FUN.NCL.012','OPE.NCL.012.0002','Remover compromisso');
INSERT INTO NCL_OPERACAO (IDMODULO, IDFUNCAO, IDOPERACAO, NOME) VALUES ('MOD.NCL','FUN.NCL.012','OPE.NCL.012.0003','Modificar compromisso');
INSERT INTO NCL_OPERACAO (IDMODULO, IDFUNCAO, IDOPERACAO, NOME) VALUES ('MOD.NCL','FUN.NCL.012','OPE.NCL.012.0004','Inserir tarefa');
INSERT INTO NCL_OPERACAO (IDMODULO, IDFUNCAO, IDOPERACAO, NOME) VALUES ('MOD.NCL','FUN.NCL.012','OPE.NCL.012.0005','Remover tarefa');
INSERT INTO NCL_OPERACAO (IDMODULO, IDFUNCAO, IDOPERACAO, NOME) VALUES ('MOD.NCL','FUN.NCL.012','OPE.NCL.012.0006','Modificar tarefa');
INSERT INTO NCL_OPERACAO (IDMODULO, IDFUNCAO, IDOPERACAO, NOME) VALUES ('MOD.NCL','FUN.NCL.012','OPE.NCL.012.0007','Visualizar outras agendas');
INSERT INTO NCL_OPERACAO (IDMODULO, IDFUNCAO, IDOPERACAO, NOME) VALUES ('MOD.NCL','FUN.NCL.012','OPE.NCL.012.0008','Inserir lembrete');
INSERT INTO NCL_OPERACAO (IDMODULO, IDFUNCAO, IDOPERACAO, NOME) VALUES ('MOD.NCL','FUN.NCL.012','OPE.NCL.012.0009','Remover lembrete');
INSERT INTO NCL_OPERACAO (IDMODULO, IDFUNCAO, IDOPERACAO, NOME) VALUES ('MOD.NCL','FUN.NCL.012','OPE.NCL.012.0010','Modificar lembrete');
INSERT INTO NCL_OPERACAO (IDMODULO, IDFUNCAO, IDOPERACAO, NOME) VALUES ('MOD.NCL','FUN.NCL.012','OPE.NCL.012.0011','Imprmir agenda');

BEGIN 
	DECLARE CursorGrupos CURSOR FOR SELECT ID FROM NCL_GRUPO WHERE STATUS = 'A'																
	DECLARE @IdGrupo BIGINT
  
  	BEGIN TRANSACTION
  		OPEN CursorGrupos
	  
    FETCH NEXT FROM CursorGrupos INTO @IdGrupo
	
    WHILE @@FETCH_STATUS = 0
		BEGIN    
		
		INSERT INTO NCL_AUTORIZACAO VALUES (@IdGrupo, 'OPE.NCL.012.0001');
		INSERT INTO NCL_AUTORIZACAO VALUES (@IdGrupo, 'OPE.NCL.012.0002');
		INSERT INTO NCL_AUTORIZACAO VALUES (@IdGrupo, 'OPE.NCL.012.0003');
		INSERT INTO NCL_AUTORIZACAO VALUES (@IdGrupo, 'OPE.NCL.012.0004');
		INSERT INTO NCL_AUTORIZACAO VALUES (@IdGrupo, 'OPE.NCL.012.0005');
		INSERT INTO NCL_AUTORIZACAO VALUES (@IdGrupo, 'OPE.NCL.012.0006');
		INSERT INTO NCL_AUTORIZACAO VALUES (@IdGrupo, 'OPE.NCL.012.0007');
		INSERT INTO NCL_AUTORIZACAO VALUES (@IdGrupo, 'OPE.NCL.012.0008');
		INSERT INTO NCL_AUTORIZACAO VALUES (@IdGrupo, 'OPE.NCL.012.0009');
		INSERT INTO NCL_AUTORIZACAO VALUES (@IdGrupo, 'OPE.NCL.012.0010');
		INSERT INTO NCL_AUTORIZACAO VALUES (@IdGrupo, 'OPE.NCL.012.0011');

		FETCH NEXT FROM CursorGrupos INTO @IdGrupo
		
		END
		
    CLOSE CursorGrupos
	DEALLOCATE CursorGrupos
	COMMIT TRANSACTION
END;

DROP TABLE NCL_CNFGERAL;

CREATE TABLE NCL_CNFGERAL (
	NOTIFERROSREMAIL		CHAR(1)			NOT NULL,
	EMAILREMETNOTIFERROS	VARCHAR(255)		NULL,
	REMETENTEPADRAO			VARCHAR(255)		NULL,
	HABILITARSSL			CHAR(1)				NULL,
	PORTA					SMALLINT			NULL,
	REQUERAUTENTICACAO		CHAR(1)				NULL,
	SHNUSUSERVSAIDA			VARCHAR(255)		NULL,
	USUSERVSAIDA			VARCHAR(255)		NULL,
	SERVSAIDA				VARCHAR(255)		NULL,
	TIPOSERVSAIDA			CHAR(1)				NULL,
	TXTCOMPRO				VARCHAR(50)			NULL,
	TXTCOMPROENTRELNH		CHAR(1)			NOT NULL,
	TXTLEMBRE				VARCHAR(50)			NULL,
	TXTLEMBREENTRELNH		CHAR(1)			NOT NULL,
	TXTTARE					VARCHAR(50)			NULL,
	TXTTAREENTRELNH			CHAR(1)			NOT NULL,
	TXTCABAGEN				VARCHAR(50)		NOT NULL,
	APRELNHCABAGEN			CHAR(1)			NOT NULL,
	APRELNHRODAGEN			CHAR(1)			NOT NULL
)
;

INSERT INTO NCL_CNFGERAL (NOTIFERROSREMAIL, EMAILREMETNOTIFERROS, REMETENTEPADRAO, HABILITARSSL, PORTA, REQUERAUTENTICACAO, SHNUSUSERVSAIDA, USUSERVSAIDA, SERVSAIDA, TIPOSERVSAIDA, TXTCOMPRO, TXTCOMPROENTRELNH, TXTLEMBRE, TXTLEMBREENTRELNH, TXTTARE, TXTTAREENTRELNH, TXTCABAGEN, APRELNHCABAGEN, APRELNHRODAGEN) VALUES('S', 'HERMES@SIMPLETI.COM.BR', 'HERMES@SIMPLETI.COM.BR', 'N', 25, 'S', 'CJORP8Z5JEWM4ECX+ENUAG==', 'HERMES@SIMPLETI.COM.BR', 'MAIL.SIMPLETI.COM.BR', '0', 'Compromissos ', 'N', 'Lembretes ', 'N', 'Tarefas ', 'N', 'Agenda ', 'N', 'N');