INSERT INTO NCL_MODULO (IDMODULO, NOME) VALUES ('MOD.LTF','Lotof�cil');
	INSERT INTO NCL_FUNCAO (IDMODULO, IDFUNCAO, NOME) VALUES ('MOD.LTF','FUN.LTF.001','Apostas');
		INSERT INTO NCL_OPERACAO (IDMODULO, IDFUNCAO, IDOPERACAO, NOME) VALUES ('MOD.LTF','FUN.LTF.001','OPE.LTF.001.0001','Inserir');
		INSERT INTO NCL_OPERACAO (IDMODULO, IDFUNCAO, IDOPERACAO, NOME) VALUES ('MOD.LTF','FUN.LTF.001','OPE.LTF.001.0002','Modificar');
		INSERT INTO NCL_OPERACAO (IDMODULO, IDFUNCAO, IDOPERACAO, NOME) VALUES ('MOD.LTF','FUN.LTF.001','OPE.LTF.001.0003','Excluir');
		
INSERT INTO NCL_MENUMODULO (IDMODULO, IMAGEM) VALUES ('MOD.LTF','bogus');
	INSERT INTO NCL_MENUFUNCAO (IDMODULO, IDFUNCAO, IMAGEM, URL) VALUES ('MOD.LTF','FUN.LTF.001','bogus','Lotofacil/frmAposta.aspx');