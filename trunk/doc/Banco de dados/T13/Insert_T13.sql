INSERT INTO NCL_MODULO (IDMODULO, NOME) VALUES ('MOD.T13','T13');
	INSERT INTO NCL_FUNCAO (IDMODULO, IDFUNCAO, NOME) VALUES ('MOD.T13','FUN.T13.001','Cadastro de servi�os prestados');
		INSERT INTO NCL_OPERACAO (IDMODULO, IDFUNCAO, IDOPERACAO, NOME) VALUES ('MOD.T13','FUN.T13.001','OPE.T13.001.0001','Inserir');
		INSERT INTO NCL_OPERACAO (IDMODULO, IDFUNCAO, IDOPERACAO, NOME) VALUES ('MOD.T13','FUN.T13.001','OPE.T13.001.0002','Modificar');
		INSERT INTO NCL_OPERACAO (IDMODULO, IDFUNCAO, IDOPERACAO, NOME) VALUES ('MOD.T13','FUN.T13.001','OPE.T13.001.0003','Excluir');
	INSERT INTO NCL_FUNCAO (IDMODULO, IDFUNCAO, NOME) VALUES ('MOD.T13','FUN.T13.002','Lan�amento de servi�os prestados');
		INSERT INTO NCL_OPERACAO (IDMODULO, IDFUNCAO, IDOPERACAO, NOME) VALUES ('MOD.T13','FUN.T13.002','OPE.T13.002.0001','Inserir');
		INSERT INTO NCL_OPERACAO (IDMODULO, IDFUNCAO, IDOPERACAO, NOME) VALUES ('MOD.T13','FUN.T13.002','OPE.T13.002.0002','Modificar');
		INSERT INTO NCL_OPERACAO (IDMODULO, IDFUNCAO, IDOPERACAO, NOME) VALUES ('MOD.T13','FUN.T13.002','OPE.T13.002.0003','Excluir');
		INSERT INTO NCL_OPERACAO (IDMODULO, IDFUNCAO, IDOPERACAO, NOME) VALUES ('MOD.T13','FUN.T13.002','OPE.T13.002.0004','Imprimir'); 
		INSERT INTO NCL_OPERACAO (IDMODULO, IDFUNCAO, IDOPERACAO, NOME) VALUES ('MOD.T13','FUN.T13.002','OPE.T13.002.0005','Reaproveitar'); 
	INSERT INTO NCL_FUNCAO (IDMODULO, IDFUNCAO, NOME) VALUES ('MOD.T13','FUN.T13.003','Imprimir lan�amento de servi�os prestados');
		INSERT INTO NCL_OPERACAO (IDMODULO, IDFUNCAO, IDOPERACAO, NOME) VALUES ('MOD.T13','FUN.T13.003','OPE.T13.003.0001','Imprimir');
INSERT INTO NCL_MENUMODULO (IDMODULO, IMAGEM) VALUES ('MOD.T13','bogus');
	INSERT INTO NCL_MENUFUNCAO (IDMODULO, IDFUNCAO, IMAGEM, URL) VALUES ('MOD.T13','FUN.T13.001','bogus','T13/cdServicos.aspx');
	INSERT INTO NCL_MENUFUNCAO (IDMODULO, IDFUNCAO, IMAGEM, URL) VALUES ('MOD.T13','FUN.T13.002','bogus','T13/frmLancamentoDeServicosPrestados.aspx');
	INSERT INTO NCL_MENUFUNCAO (IDMODULO, IDFUNCAO, IMAGEM, URL) VALUES ('MOD.T13','FUN.T13.003','bogus','T13/frmImpressaoLancamentos.aspx');