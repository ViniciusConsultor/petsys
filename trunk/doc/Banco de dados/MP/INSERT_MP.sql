﻿INSERT INTO NCL_MODULO (IDMODULO, NOME) VALUES ('MOD.MP','Marcas e Patentes');
	INSERT INTO NCL_FUNCAO (IDMODULO, IDFUNCAO, NOME) VALUES ('MOD.MP','FUN.MP.001','Tipos de Patentes');
		INSERT INTO NCL_OPERACAO (IDMODULO, IDFUNCAO, IDOPERACAO, NOME) VALUES ('MOD.MP','FUN.MP.001','OPE.MP.001.0001','Inserir');
		INSERT INTO NCL_OPERACAO (IDMODULO, IDFUNCAO, IDOPERACAO, NOME) VALUES ('MOD.MP','FUN.MP.001','OPE.MP.001.0002','Modificar');
		INSERT INTO NCL_OPERACAO (IDMODULO, IDFUNCAO, IDOPERACAO, NOME) VALUES ('MOD.MP','FUN.MP.001','OPE.MP.001.0003','Excluir');

	INSERT INTO NCL_FUNCAO (IDMODULO, IDFUNCAO, NOME) VALUES ('MOD.MP','FUN.MP.002','Cadastro de Inventor');
		INSERT INTO NCL_OPERACAO (IDMODULO, IDFUNCAO, IDOPERACAO, NOME) VALUES ('MOD.MP','FUN.MP.002','OPE.MP.002.0001','Inserir');
		INSERT INTO NCL_OPERACAO (IDMODULO, IDFUNCAO, IDOPERACAO, NOME) VALUES ('MOD.MP','FUN.MP.002','OPE.MP.002.0002','Modificar');
		INSERT INTO NCL_OPERACAO (IDMODULO, IDFUNCAO, IDOPERACAO, NOME) VALUES ('MOD.MP','FUN.MP.002','OPE.MP.002.0003','Excluir');

	INSERT INTO NCL_FUNCAO (IDMODULO, IDFUNCAO, NOME) VALUES ('MOD.MP','FUN.MP.003','Cadastro de Tipo de Procedimento Interno');
		INSERT INTO NCL_OPERACAO (IDMODULO, IDFUNCAO, IDOPERACAO, NOME) VALUES ('MOD.MP','FUN.MP.003','OPE.MP.003.0001','Inserir');
		INSERT INTO NCL_OPERACAO (IDMODULO, IDFUNCAO, IDOPERACAO, NOME) VALUES ('MOD.MP','FUN.MP.003','OPE.MP.003.0002','Modificar');
		INSERT INTO NCL_OPERACAO (IDMODULO, IDFUNCAO, IDOPERACAO, NOME) VALUES ('MOD.MP','FUN.MP.003','OPE.MP.003.0003','Excluir');

	INSERT INTO NCL_FUNCAO (IDMODULO, IDFUNCAO, NOME) VALUES ('MOD.MP','FUN.MP.004','Despachos de Marcas');
		INSERT INTO NCL_OPERACAO (IDMODULO, IDFUNCAO, IDOPERACAO, NOME) VALUES ('MOD.MP','FUN.MP.004','OPE.MP.004.0001','Inserir');
		INSERT INTO NCL_OPERACAO (IDMODULO, IDFUNCAO, IDOPERACAO, NOME) VALUES ('MOD.MP','FUN.MP.004','OPE.MP.004.0002','Modificar');
		INSERT INTO NCL_OPERACAO (IDMODULO, IDFUNCAO, IDOPERACAO, NOME) VALUES ('MOD.MP','FUN.MP.004','OPE.MP.004.0003','Excluir');

	INSERT INTO NCL_FUNCAO (IDMODULO, IDFUNCAO, NOME) VALUES ('MOD.MP','FUN.MP.005','Procuradores');
		INSERT INTO NCL_OPERACAO (IDMODULO, IDFUNCAO, IDOPERACAO, NOME) VALUES ('MOD.MP','FUN.MP.005','OPE.MP.005.0001','Inserir');
		INSERT INTO NCL_OPERACAO (IDMODULO, IDFUNCAO, IDOPERACAO, NOME) VALUES ('MOD.MP','FUN.MP.005','OPE.MP.005.0002','Modificar');
		INSERT INTO NCL_OPERACAO (IDMODULO, IDFUNCAO, IDOPERACAO, NOME) VALUES ('MOD.MP','FUN.MP.005','OPE.MP.005.0003','Excluir');	

	INSERT INTO NCL_FUNCAO (IDMODULO, IDFUNCAO, NOME) VALUES ('MOD.MP','FUN.MP.006','Layout Revista Patente');
		INSERT INTO NCL_OPERACAO (IDMODULO, IDFUNCAO, IDOPERACAO, NOME) VALUES ('MOD.MP','FUN.MP.006','OPE.MP.006.0001','Inserir');
		INSERT INTO NCL_OPERACAO (IDMODULO, IDFUNCAO, IDOPERACAO, NOME) VALUES ('MOD.MP','FUN.MP.006','OPE.MP.006.0002','Modificar');
		INSERT INTO NCL_OPERACAO (IDMODULO, IDFUNCAO, IDOPERACAO, NOME) VALUES ('MOD.MP','FUN.MP.006','OPE.MP.006.0003','Excluir');	

	INSERT INTO NCL_FUNCAO (IDMODULO, IDFUNCAO, NOME) VALUES ('MOD.MP','FUN.MP.007','Processo de Marcas');
		INSERT INTO NCL_OPERACAO (IDMODULO, IDFUNCAO, IDOPERACAO, NOME) VALUES ('MOD.MP','FUN.MP.007','OPE.MP.007.0001','Inserir');
		INSERT INTO NCL_OPERACAO (IDMODULO, IDFUNCAO, IDOPERACAO, NOME) VALUES ('MOD.MP','FUN.MP.007','OPE.MP.007.0002','Modificar');
		INSERT INTO NCL_OPERACAO (IDMODULO, IDFUNCAO, IDOPERACAO, NOME) VALUES ('MOD.MP','FUN.MP.007','OPE.MP.007.0003','Excluir');	
		INSERT INTO NCL_OPERACAO (IDMODULO, IDFUNCAO, IDOPERACAO, NOME) VALUES ('MOD.MP','FUN.MP.007','OPE.MP.007.0004','Detalhar');
		
	INSERT INTO NCL_FUNCAO (IDMODULO, IDFUNCAO, NOME) VALUES ('MOD.MP','FUN.MP.008','Cadastro de Marcas');
		INSERT INTO NCL_OPERACAO (IDMODULO, IDFUNCAO, IDOPERACAO, NOME) VALUES ('MOD.MP','FUN.MP.008','OPE.MP.008.0001','Inserir');
		INSERT INTO NCL_OPERACAO (IDMODULO, IDFUNCAO, IDOPERACAO, NOME) VALUES ('MOD.MP','FUN.MP.008','OPE.MP.008.0002','Modificar');
		INSERT INTO NCL_OPERACAO (IDMODULO, IDFUNCAO, IDOPERACAO, NOME) VALUES ('MOD.MP','FUN.MP.008','OPE.MP.008.0003','Excluir');	


INSERT INTO NCL_MENUMODULO (IDMODULO, IMAGEM) VALUES ('MOD.MP','bogus');
	INSERT INTO NCL_MENUFUNCAO (IDMODULO, IDFUNCAO, IMAGEM, URL) VALUES ('MOD.MP','FUN.MP.001','bogus','MP/cdTipoDePatente.aspx');
	INSERT INTO NCL_MENUFUNCAO (IDMODULO, IDFUNCAO, IMAGEM, URL) VALUES ('MOD.MP','FUN.MP.002','bogus','MP/cdInventor.aspx');
	INSERT INTO NCL_MENUFUNCAO (IDMODULO, IDFUNCAO, IMAGEM, URL) VALUES ('MOD.MP','FUN.MP.003','bogus','MP/cdTipoDeProcedimentoInterno.aspx');
	INSERT INTO NCL_MENUFUNCAO (IDMODULO, IDFUNCAO, IMAGEM, URL) VALUES ('MOD.MP','FUN.MP.004','bogus','MP/cdDespachoDeMarcas.aspx');
	INSERT INTO NCL_MENUFUNCAO (IDMODULO, IDFUNCAO, IMAGEM, URL) VALUES ('MOD.MP','FUN.MP.005','bogus','MP/cdCadastroDeProcuradores.aspx');
	INSERT INTO NCL_MENUFUNCAO (IDMODULO, IDFUNCAO, IMAGEM, URL) VALUES ('MOD.MP','FUN.MP.006','bogus','MP/cdLayoutRevistaPatente.aspx');
	INSERT INTO NCL_MENUFUNCAO (IDMODULO, IDFUNCAO, IMAGEM, URL) VALUES ('MOD.MP','FUN.MP.007','bogus','MP/frmProcessosDeMarcas.aspx');
	INSERT INTO NCL_MENUFUNCAO (IDMODULO, IDFUNCAO, IMAGEM, URL) VALUES ('MOD.MP','FUN.MP.008','bogus','MP/cdMarcas.aspx');