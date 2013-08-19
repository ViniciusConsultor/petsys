﻿INSERT INTO NCL_MODULO (IDMODULO, NOME) VALUES ('MOD.NCL','Núcleo');
	INSERT INTO NCL_FUNCAO (IDMODULO, IDFUNCAO, NOME) VALUES ('MOD.NCL','FUN.NCL.001','Cadastro de municípios');
		INSERT INTO NCL_OPERACAO (IDMODULO, IDFUNCAO, IDOPERACAO, NOME) VALUES ('MOD.NCL','FUN.NCL.001','OPE.NCL.001.0001','Inserir');
		INSERT INTO NCL_OPERACAO (IDMODULO, IDFUNCAO, IDOPERACAO, NOME) VALUES ('MOD.NCL','FUN.NCL.001','OPE.NCL.001.0002','Modificar');
		INSERT INTO NCL_OPERACAO (IDMODULO, IDFUNCAO, IDOPERACAO, NOME) VALUES ('MOD.NCL','FUN.NCL.001','OPE.NCL.001.0003','Excluir');
	INSERT INTO NCL_FUNCAO (IDMODULO, IDFUNCAO, NOME) VALUES ('MOD.NCL','FUN.NCL.002','Cadastro de grupos');
		INSERT INTO NCL_OPERACAO (IDMODULO, IDFUNCAO, IDOPERACAO, NOME) VALUES ('MOD.NCL','FUN.NCL.002','OPE.NCL.002.0001','Inserir');
		INSERT INTO NCL_OPERACAO (IDMODULO, IDFUNCAO, IDOPERACAO, NOME) VALUES ('MOD.NCL','FUN.NCL.002','OPE.NCL.002.0002','Modificar');
		INSERT INTO NCL_OPERACAO (IDMODULO, IDFUNCAO, IDOPERACAO, NOME) VALUES ('MOD.NCL','FUN.NCL.002','OPE.NCL.002.0003','Excluir');
	INSERT INTO NCL_FUNCAO (IDMODULO, IDFUNCAO, NOME) VALUES ('MOD.NCL','FUN.NCL.003','Autorizações');
		INSERT INTO NCL_OPERACAO (IDMODULO, IDFUNCAO, IDOPERACAO, NOME) VALUES ('MOD.NCL','FUN.NCL.003','OPE.NCL.003.0001','Modificar');
	INSERT INTO NCL_FUNCAO (IDMODULO, IDFUNCAO, NOME) VALUES ('MOD.NCL','FUN.NCL.004','Alterar senha de operadores');
	INSERT INTO NCL_FUNCAO (IDMODULO, IDFUNCAO, NOME) VALUES ('MOD.NCL','FUN.NCL.005','Cadastro de operadores');
		INSERT INTO NCL_OPERACAO (IDMODULO, IDFUNCAO, IDOPERACAO, NOME) VALUES ('MOD.NCL','FUN.NCL.005','OPE.NCL.005.0001','Inserir');
		INSERT INTO NCL_OPERACAO (IDMODULO, IDFUNCAO, IDOPERACAO, NOME) VALUES ('MOD.NCL','FUN.NCL.005','OPE.NCL.005.0002','Modificar');
		INSERT INTO NCL_OPERACAO (IDMODULO, IDFUNCAO, IDOPERACAO, NOME) VALUES ('MOD.NCL','FUN.NCL.005','OPE.NCL.005.0003','Excluir');
	INSERT INTO NCL_FUNCAO (IDMODULO, IDFUNCAO, NOME) VALUES ('MOD.NCL','FUN.NCL.006','Cadastro de pessoa');
		INSERT INTO NCL_OPERACAO (IDMODULO, IDFUNCAO, IDOPERACAO, NOME) VALUES ('MOD.NCL','FUN.NCL.006','OPE.NCL.006.0001','Inserir');
		INSERT INTO NCL_OPERACAO (IDMODULO, IDFUNCAO, IDOPERACAO, NOME) VALUES ('MOD.NCL','FUN.NCL.006','OPE.NCL.006.0002','Detalhar');
		INSERT INTO NCL_OPERACAO (IDMODULO, IDFUNCAO, IDOPERACAO, NOME) VALUES ('MOD.NCL','FUN.NCL.006','OPE.NCL.006.0003','Modificar');
		INSERT INTO NCL_OPERACAO (IDMODULO, IDFUNCAO, IDOPERACAO, NOME) VALUES ('MOD.NCL','FUN.NCL.006','OPE.NCL.006.0004','Excluir');	
	INSERT INTO NCL_FUNCAO (IDMODULO, IDFUNCAO, NOME) VALUES ('MOD.NCL','FUN.NCL.007','Alterar senha');
	INSERT INTO NCL_FUNCAO (IDMODULO, IDFUNCAO, NOME) VALUES ('MOD.NCL','FUN.NCL.008','Cadastro de cliente');
		INSERT INTO NCL_OPERACAO (IDMODULO, IDFUNCAO, IDOPERACAO, NOME) VALUES ('MOD.NCL','FUN.NCL.008','OPE.NCL.008.0001','Inserir');
		INSERT INTO NCL_OPERACAO (IDMODULO, IDFUNCAO, IDOPERACAO, NOME) VALUES ('MOD.NCL','FUN.NCL.008','OPE.NCL.008.0002','Modificar');
		INSERT INTO NCL_OPERACAO (IDMODULO, IDFUNCAO, IDOPERACAO, NOME) VALUES ('MOD.NCL','FUN.NCL.008','OPE.NCL.008.0003','Excluir');	
	INSERT INTO NCL_FUNCAO (IDMODULO, IDFUNCAO, NOME) VALUES ('MOD.NCL','FUN.NCL.009','Cadastro de bancos e agências');
		INSERT INTO NCL_OPERACAO (IDMODULO, IDFUNCAO, IDOPERACAO, NOME) VALUES ('MOD.NCL','FUN.NCL.009','OPE.NCL.009.0001','Inserir bancos');
		INSERT INTO NCL_OPERACAO (IDMODULO, IDFUNCAO, IDOPERACAO, NOME) VALUES ('MOD.NCL','FUN.NCL.009','OPE.NCL.009.0002','Modificar bancos');
		INSERT INTO NCL_OPERACAO (IDMODULO, IDFUNCAO, IDOPERACAO, NOME) VALUES ('MOD.NCL','FUN.NCL.009','OPE.NCL.009.0003','Excluir bancos');
		INSERT INTO NCL_OPERACAO (IDMODULO, IDFUNCAO, IDOPERACAO, NOME) VALUES ('MOD.NCL','FUN.NCL.009','OPE.NCL.009.0004','Inserir agências');	
		INSERT INTO NCL_OPERACAO (IDMODULO, IDFUNCAO, IDOPERACAO, NOME) VALUES ('MOD.NCL','FUN.NCL.009','OPE.NCL.009.0005','Modificar agências');	
		INSERT INTO NCL_OPERACAO (IDMODULO, IDFUNCAO, IDOPERACAO, NOME) VALUES ('MOD.NCL','FUN.NCL.009','OPE.NCL.009.0006','Excluir agências');	
	INSERT INTO NCL_FUNCAO (IDMODULO, IDFUNCAO, NOME) VALUES ('MOD.NCL','FUN.NCL.010','Painel de controle');
		INSERT INTO NCL_OPERACAO (IDMODULO, IDFUNCAO, IDOPERACAO, NOME) VALUES ('MOD.NCL','FUN.NCL.010','OPE.NCL.010.0001','Salvar');
	INSERT INTO NCL_FUNCAO (IDMODULO, IDFUNCAO, NOME) VALUES ('MOD.NCL','FUN.NCL.011','Configurações pessoais');
		INSERT INTO NCL_OPERACAO (IDMODULO, IDFUNCAO, IDOPERACAO, NOME) VALUES ('MOD.NCL','FUN.NCL.011','OPE.NCL.011.0001','Atalhos');
		INSERT INTO NCL_OPERACAO (IDMODULO, IDFUNCAO, IDOPERACAO, NOME) VALUES ('MOD.NCL','FUN.NCL.011','OPE.NCL.011.0002','Temas');
		INSERT INTO NCL_OPERACAO (IDMODULO, IDFUNCAO, IDOPERACAO, NOME) VALUES ('MOD.NCL','FUN.NCL.011','OPE.NCL.011.0003','Papel de parede');
		INSERT INTO NCL_OPERACAO (IDMODULO, IDFUNCAO, IDOPERACAO, NOME) VALUES ('MOD.NCL','FUN.NCL.011','OPE.NCL.011.0004','Agenda');			
	INSERT INTO NCL_FUNCAO (IDMODULO, IDFUNCAO, NOME) VALUES ('MOD.NCL','FUN.NCL.012','Agenda');
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
		INSERT INTO NCL_OPERACAO (IDMODULO, IDFUNCAO, IDOPERACAO, NOME) VALUES ('MOD.NCL','FUN.NCL.012','OPE.NCL.012.0011','Imprimir agenda');
	INSERT INTO NCL_FUNCAO (IDMODULO, IDFUNCAO, NOME) VALUES ('MOD.NCL','FUN.NCL.013','Cadastro de fornecedor');
		INSERT INTO NCL_OPERACAO (IDMODULO, IDFUNCAO, IDOPERACAO, NOME) VALUES ('MOD.NCL','FUN.NCL.013','OPE.NCL.013.0001','Inserir');
		INSERT INTO NCL_OPERACAO (IDMODULO, IDFUNCAO, IDOPERACAO, NOME) VALUES ('MOD.NCL','FUN.NCL.013','OPE.NCL.013.0002','Modificar');
		INSERT INTO NCL_OPERACAO (IDMODULO, IDFUNCAO, IDOPERACAO, NOME) VALUES ('MOD.NCL','FUN.NCL.013','OPE.NCL.013.0003','Excluir');	
	INSERT INTO NCL_FUNCAO (IDMODULO, IDFUNCAO, NOME) VALUES ('MOD.NCL','FUN.NCL.014','Cadastro de empresas');
		INSERT INTO NCL_OPERACAO (IDMODULO, IDFUNCAO, IDOPERACAO, NOME) VALUES ('MOD.NCL','FUN.NCL.014','OPE.NCL.014.0001','Inserir');
		INSERT INTO NCL_OPERACAO (IDMODULO, IDFUNCAO, IDOPERACAO, NOME) VALUES ('MOD.NCL','FUN.NCL.014','OPE.NCL.014.0002','Modificar');
		INSERT INTO NCL_OPERACAO (IDMODULO, IDFUNCAO, IDOPERACAO, NOME) VALUES ('MOD.NCL','FUN.NCL.014','OPE.NCL.014.0003','Excluir');	
	INSERT INTO NCL_FUNCAO (IDMODULO, IDFUNCAO, NOME) VALUES ('MOD.NCL','FUN.NCL.015','Visibilidade por empresa');
		INSERT INTO NCL_OPERACAO (IDMODULO, IDFUNCAO, IDOPERACAO, NOME) VALUES ('MOD.NCL','FUN.NCL.015','OPE.NCL.015.0001','Inserir');
		INSERT INTO NCL_OPERACAO (IDMODULO, IDFUNCAO, IDOPERACAO, NOME) VALUES ('MOD.NCL','FUN.NCL.015','OPE.NCL.015.0002','Modificar');
		INSERT INTO NCL_OPERACAO (IDMODULO, IDFUNCAO, IDOPERACAO, NOME) VALUES ('MOD.NCL','FUN.NCL.015','OPE.NCL.015.0003','Excluir');	
														
INSERT INTO NCL_MENUMODULO (IDMODULO, IMAGEM) VALUES ('MOD.NCL','bogus');
	INSERT INTO NCL_MENUFUNCAO (IDMODULO, IDFUNCAO, IMAGEM, URL) VALUES ('MOD.NCL','FUN.NCL.001','bogus','Nucleo/cdMunicipio.aspx');
	INSERT INTO NCL_MENUFUNCAO (IDMODULO, IDFUNCAO, IMAGEM, URL) VALUES ('MOD.NCL','FUN.NCL.002','bogus','Nucleo/cdGrupo.aspx');
	INSERT INTO NCL_MENUFUNCAO (IDMODULO, IDFUNCAO, IMAGEM, URL) VALUES ('MOD.NCL','FUN.NCL.003','bogus','Nucleo/frmAutorizacao.aspx');
	INSERT INTO NCL_MENUFUNCAO (IDMODULO, IDFUNCAO, IMAGEM, URL) VALUES ('MOD.NCL','FUN.NCL.004','bogus','Nucleo/frmAlterarSenhaDoOperador.aspx');
	INSERT INTO NCL_MENUFUNCAO (IDMODULO, IDFUNCAO, IMAGEM, URL) VALUES ('MOD.NCL','FUN.NCL.005','bogus','Nucleo/cdOperador.aspx');
	INSERT INTO NCL_MENUFUNCAO (IDMODULO, IDFUNCAO, IMAGEM, URL) VALUES ('MOD.NCL','FUN.NCL.007','bogus','Nucleo/frmAlterarSenha.aspx');
	INSERT INTO NCL_MENUFUNCAO (IDMODULO, IDFUNCAO, IMAGEM, URL) VALUES ('MOD.NCL','FUN.NCL.008','bogus','Nucleo/cdCliente.aspx');
	INSERT INTO NCL_MENUFUNCAO (IDMODULO, IDFUNCAO, IMAGEM, URL) VALUES ('MOD.NCL','FUN.NCL.009','bogus','Nucleo/cdBancosEAgencias.aspx');
	INSERT INTO NCL_MENUFUNCAO (IDMODULO, IDFUNCAO, IMAGEM, URL) VALUES ('MOD.NCL','FUN.NCL.010','bogus','Nucleo/frmPainelDeControle.aspx');
	INSERT INTO NCL_MENUFUNCAO (IDMODULO, IDFUNCAO, IMAGEM, URL) VALUES ('MOD.NCL','FUN.NCL.011','bogus','Nucleo/frmConfiguracoesPessoais.aspx');
	INSERT INTO NCL_MENUFUNCAO (IDMODULO, IDFUNCAO, IMAGEM, URL) VALUES ('MOD.NCL','FUN.NCL.012','bogus','Nucleo/frmAgenda.aspx');
	INSERT INTO NCL_MENUFUNCAO (IDMODULO, IDFUNCAO, IMAGEM, URL) VALUES ('MOD.NCL','FUN.NCL.013','bogus','Nucleo/cdFornecedor.aspx');
	INSERT INTO NCL_MENUFUNCAO (IDMODULO, IDFUNCAO, IMAGEM, URL) VALUES ('MOD.NCL','FUN.NCL.014','bogus','Nucleo/cdEmpresa.aspx');
	INSERT INTO NCL_MENUFUNCAO (IDMODULO, IDFUNCAO, IMAGEM, URL) VALUES ('MOD.NCL','FUN.NCL.015','bogus','Nucleo/frmVisibilidadePorEmpresa.aspx');