ALTER TABLE DRY_SOLICAUDI ALTER COLUMN LOCAL VARCHAR(1000);
ALTER TABLE DRY_SOLICAUDI ALTER COLUMN ASSUNTO VARCHAR(1000);
ALTER TABLE DRY_SOLICAUDI ALTER COLUMN DESCRICAO VARCHAR(1500);

ALTER TABLE DRY_SOLICCONVT ALTER COLUMN LOCAL VARCHAR(1000);
ALTER TABLE DRY_SOLICCONVT ALTER COLUMN DESCRICAO VARCHAR(1500);
ALTER TABLE DRY_SOLICCONVT ALTER COLUMN OBSERVACAO VARCHAR(2000);

CREATE TABLE DRY_SOLICVISI (
	ID					BIGINT			NOT NULL,
	CODIGO				BIGINT			NOT NULL,
	IDCONTATO			BIGINT			NOT NULL,
	ASSUNTO				VARCHAR(1000)	NOT NULL,
	DESCRICAO			VARCHAR(1500)	NOT NULL,
	DATADECADASTRO		VARCHAR(20)		NOT NULL,
	ESTAATIVA			CHAR(1)			NOT NULL,
	IDUSUARIOCAD		BIGINT			NOT NULL,
	LOCAL				VARCHAR(1000)		NULL
)
;

ALTER TABLE DRY_SOLICVISI
	ADD CONSTRAINT PK_DRY_SOLICVISI
	PRIMARY KEY (ID)
;

ALTER TABLE DRY_SOLICVISI
	ADD CONSTRAINT FK_DRY_SOLICVISI1
	FOREIGN KEY(IDCONTATO)
	REFERENCES DRY_CONTATO(IDPESSOA)
;

CREATE INDEX IDX_DRY_SOLICVISI1
	ON DRY_SOLICVISI(ESTAATIVA)
;

CREATE INDEX IDX_DRY_SOLICVISI2
	ON DRY_SOLICVISI(DATADECADASTRO)
;

CREATE INDEX IDX_DRY_SOLICVISI3
	ON DRY_SOLICVISI(CODIGO)
;

INSERT INTO NCL_FUNCAO (IDMODULO, IDFUNCAO, NOME) VALUES ('MOD.DRY','FUN.DRY.004','Solicitações de visita');
INSERT INTO NCL_OPERACAO (IDMODULO, IDFUNCAO, IDOPERACAO, NOME) VALUES ('MOD.DRY','FUN.DRY.004','OPE.DRY.004.0001','Inserir');
INSERT INTO NCL_OPERACAO (IDMODULO, IDFUNCAO, IDOPERACAO, NOME) VALUES ('MOD.DRY','FUN.DRY.004','OPE.DRY.004.0002','Modificar');
INSERT INTO NCL_OPERACAO (IDMODULO, IDFUNCAO, IDOPERACAO, NOME) VALUES ('MOD.DRY','FUN.DRY.004','OPE.DRY.004.0003','Excluir');
INSERT INTO NCL_OPERACAO (IDMODULO, IDFUNCAO, IDOPERACAO, NOME) VALUES ('MOD.DRY','FUN.DRY.004','OPE.DRY.004.0004','Despachar');
INSERT INTO NCL_OPERACAO (IDMODULO, IDFUNCAO, IDOPERACAO, NOME) VALUES ('MOD.DRY','FUN.DRY.004','OPE.DRY.004.0005','Finalizar');
INSERT INTO NCL_OPERACAO (IDMODULO, IDFUNCAO, IDOPERACAO, NOME) VALUES ('MOD.DRY','FUN.DRY.004','OPE.DRY.004.0006','Imprimir');

INSERT INTO NCL_MENUFUNCAO (IDMODULO, IDFUNCAO, IMAGEM, URL) VALUES ('MOD.DRY','FUN.DRY.004','bogus','Diary/frmSolicitacoesDeVisita.aspx');

BEGIN 
	DECLARE CursorGrupos CURSOR FOR SELECT ID FROM NCL_GRUPO WHERE STATUS = 'A'																
	DECLARE @IdGrupo BIGINT
  
  	BEGIN TRANSACTION
  		OPEN CursorGrupos
	  
    FETCH NEXT FROM CursorGrupos INTO @IdGrupo
	
    WHILE @@FETCH_STATUS = 0
		BEGIN    
		
		INSERT INTO NCL_AUTORIZACAO VALUES (@IdGrupo, 'FUN.DRY.004');
		INSERT INTO NCL_AUTORIZACAO VALUES (@IdGrupo, 'OPE.DRY.004.0001');
		INSERT INTO NCL_AUTORIZACAO VALUES (@IdGrupo, 'OPE.DRY.004.0002');
		INSERT INTO NCL_AUTORIZACAO VALUES (@IdGrupo, 'OPE.DRY.004.0003');
		INSERT INTO NCL_AUTORIZACAO VALUES (@IdGrupo, 'OPE.DRY.004.0004');
		INSERT INTO NCL_AUTORIZACAO VALUES (@IdGrupo, 'OPE.DRY.004.0005');
		INSERT INTO NCL_AUTORIZACAO VALUES (@IdGrupo, 'OPE.DRY.004.0006');

		FETCH NEXT FROM CursorGrupos INTO @IdGrupo
		
		END
		
    CLOSE CursorGrupos
	DEALLOCATE CursorGrupos
	COMMIT TRANSACTION
END;

BEGIN 
	DECLARE CursorOperadores CURSOR FOR SELECT IDPESSOA FROM NCL_OPERADOR WHERE STATUS = 'A'																
	DECLARE @IdPessoa BIGINT
  
  	BEGIN TRANSACTION
  		OPEN CursorOperadores
	  
    FETCH NEXT FROM CursorOperadores INTO @IdPessoa
	
    WHILE @@FETCH_STATUS = 0
		BEGIN  
		  
		INSERT INTO NCL_ATALHO VALUES ('FUN.DRY.004', 'Solicitações de visita', 0, 'Diary/frmSolicitacoesDeVisita.aspx', 'bogus',@IdPessoa)
		
		FETCH NEXT FROM CursorOperadores INTO @IdPessoa
		
		END
		
    CLOSE CursorOperadores
	DEALLOCATE CursorOperadores
	COMMIT TRANSACTION
END;

