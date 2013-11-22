﻿INSERT INTO NCL_MODULO (IDMODULO, NOME) VALUES ('MOD.NCL','Núcleo');
	INSERT INTO NCL_FUNCAO (IDMODULO, IDFUNCAO, NOME) VALUES ('MOD.NCL','FUN.NCL.001','Cadastro de Municípios');
		INSERT INTO NCL_OPERACAO (IDMODULO, IDFUNCAO, IDOPERACAO, NOME) VALUES ('MOD.NCL','FUN.NCL.001','OPE.NCL.001.0001','Inserir');
		INSERT INTO NCL_OPERACAO (IDMODULO, IDFUNCAO, IDOPERACAO, NOME) VALUES ('MOD.NCL','FUN.NCL.001','OPE.NCL.001.0002','Modificar');
		INSERT INTO NCL_OPERACAO (IDMODULO, IDFUNCAO, IDOPERACAO, NOME) VALUES ('MOD.NCL','FUN.NCL.001','OPE.NCL.001.0003','Excluir');
	INSERT INTO NCL_FUNCAO (IDMODULO, IDFUNCAO, NOME) VALUES ('MOD.NCL','FUN.NCL.002','Cadastro de Grupos');
		INSERT INTO NCL_OPERACAO (IDMODULO, IDFUNCAO, IDOPERACAO, NOME) VALUES ('MOD.NCL','FUN.NCL.002','OPE.NCL.002.0001','Inserir');
		INSERT INTO NCL_OPERACAO (IDMODULO, IDFUNCAO, IDOPERACAO, NOME) VALUES ('MOD.NCL','FUN.NCL.002','OPE.NCL.002.0002','Modificar');
		INSERT INTO NCL_OPERACAO (IDMODULO, IDFUNCAO, IDOPERACAO, NOME) VALUES ('MOD.NCL','FUN.NCL.002','OPE.NCL.002.0003','Excluir');
	INSERT INTO NCL_FUNCAO (IDMODULO, IDFUNCAO, NOME) VALUES ('MOD.NCL','FUN.NCL.003','Autorizações');
		INSERT INTO NCL_OPERACAO (IDMODULO, IDFUNCAO, IDOPERACAO, NOME) VALUES ('MOD.NCL','FUN.NCL.003','OPE.NCL.003.0001','Modificar');
	INSERT INTO NCL_FUNCAO (IDMODULO, IDFUNCAO, NOME) VALUES ('MOD.NCL','FUN.NCL.004','Alterar Senha de Operadores');
	INSERT INTO NCL_FUNCAO (IDMODULO, IDFUNCAO, NOME) VALUES ('MOD.NCL','FUN.NCL.005','Cadastro de Operadores');
		INSERT INTO NCL_OPERACAO (IDMODULO, IDFUNCAO, IDOPERACAO, NOME) VALUES ('MOD.NCL','FUN.NCL.005','OPE.NCL.005.0001','Inserir');
		INSERT INTO NCL_OPERACAO (IDMODULO, IDFUNCAO, IDOPERACAO, NOME) VALUES ('MOD.NCL','FUN.NCL.005','OPE.NCL.005.0002','Modificar');
		INSERT INTO NCL_OPERACAO (IDMODULO, IDFUNCAO, IDOPERACAO, NOME) VALUES ('MOD.NCL','FUN.NCL.005','OPE.NCL.005.0003','Excluir');
	INSERT INTO NCL_FUNCAO (IDMODULO, IDFUNCAO, NOME) VALUES ('MOD.NCL','FUN.NCL.006','Cadastro de Pessoa');
		INSERT INTO NCL_OPERACAO (IDMODULO, IDFUNCAO, IDOPERACAO, NOME) VALUES ('MOD.NCL','FUN.NCL.006','OPE.NCL.006.0001','Inserir');
		INSERT INTO NCL_OPERACAO (IDMODULO, IDFUNCAO, IDOPERACAO, NOME) VALUES ('MOD.NCL','FUN.NCL.006','OPE.NCL.006.0002','Detalhar');
		INSERT INTO NCL_OPERACAO (IDMODULO, IDFUNCAO, IDOPERACAO, NOME) VALUES ('MOD.NCL','FUN.NCL.006','OPE.NCL.006.0003','Modificar');
		INSERT INTO NCL_OPERACAO (IDMODULO, IDFUNCAO, IDOPERACAO, NOME) VALUES ('MOD.NCL','FUN.NCL.006','OPE.NCL.006.0004','Excluir');	
	INSERT INTO NCL_FUNCAO (IDMODULO, IDFUNCAO, NOME) VALUES ('MOD.NCL','FUN.NCL.007','Alterar Senha');
	INSERT INTO NCL_FUNCAO (IDMODULO, IDFUNCAO, NOME) VALUES ('MOD.NCL','FUN.NCL.008','Cadastro de Cliente');
		INSERT INTO NCL_OPERACAO (IDMODULO, IDFUNCAO, IDOPERACAO, NOME) VALUES ('MOD.NCL','FUN.NCL.008','OPE.NCL.008.0001','Inserir');
		INSERT INTO NCL_OPERACAO (IDMODULO, IDFUNCAO, IDOPERACAO, NOME) VALUES ('MOD.NCL','FUN.NCL.008','OPE.NCL.008.0002','Modificar');
		INSERT INTO NCL_OPERACAO (IDMODULO, IDFUNCAO, IDOPERACAO, NOME) VALUES ('MOD.NCL','FUN.NCL.008','OPE.NCL.008.0003','Excluir');	
	INSERT INTO NCL_FUNCAO (IDMODULO, IDFUNCAO, NOME) VALUES ('MOD.NCL','FUN.NCL.009','Cadastro de Bancos e Agências');
		INSERT INTO NCL_OPERACAO (IDMODULO, IDFUNCAO, IDOPERACAO, NOME) VALUES ('MOD.NCL','FUN.NCL.009','OPE.NCL.009.0001','Inserir bancos');
		INSERT INTO NCL_OPERACAO (IDMODULO, IDFUNCAO, IDOPERACAO, NOME) VALUES ('MOD.NCL','FUN.NCL.009','OPE.NCL.009.0002','Modificar bancos');
		INSERT INTO NCL_OPERACAO (IDMODULO, IDFUNCAO, IDOPERACAO, NOME) VALUES ('MOD.NCL','FUN.NCL.009','OPE.NCL.009.0003','Excluir bancos');
		INSERT INTO NCL_OPERACAO (IDMODULO, IDFUNCAO, IDOPERACAO, NOME) VALUES ('MOD.NCL','FUN.NCL.009','OPE.NCL.009.0004','Inserir agências');	
		INSERT INTO NCL_OPERACAO (IDMODULO, IDFUNCAO, IDOPERACAO, NOME) VALUES ('MOD.NCL','FUN.NCL.009','OPE.NCL.009.0005','Modificar agências');	
		INSERT INTO NCL_OPERACAO (IDMODULO, IDFUNCAO, IDOPERACAO, NOME) VALUES ('MOD.NCL','FUN.NCL.009','OPE.NCL.009.0006','Excluir agências');	
	INSERT INTO NCL_FUNCAO (IDMODULO, IDFUNCAO, NOME) VALUES ('MOD.NCL','FUN.NCL.010','Painel de Controle');
		INSERT INTO NCL_OPERACAO (IDMODULO, IDFUNCAO, IDOPERACAO, NOME) VALUES ('MOD.NCL','FUN.NCL.010','OPE.NCL.010.0001','Salvar');
	INSERT INTO NCL_FUNCAO (IDMODULO, IDFUNCAO, NOME) VALUES ('MOD.NCL','FUN.NCL.011','Configurações Pessoais');
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
	INSERT INTO NCL_FUNCAO (IDMODULO, IDFUNCAO, NOME) VALUES ('MOD.NCL','FUN.NCL.013','Cadastro de Fornecedor');
		INSERT INTO NCL_OPERACAO (IDMODULO, IDFUNCAO, IDOPERACAO, NOME) VALUES ('MOD.NCL','FUN.NCL.013','OPE.NCL.013.0001','Inserir');
		INSERT INTO NCL_OPERACAO (IDMODULO, IDFUNCAO, IDOPERACAO, NOME) VALUES ('MOD.NCL','FUN.NCL.013','OPE.NCL.013.0002','Modificar');
		INSERT INTO NCL_OPERACAO (IDMODULO, IDFUNCAO, IDOPERACAO, NOME) VALUES ('MOD.NCL','FUN.NCL.013','OPE.NCL.013.0003','Excluir');	
	INSERT INTO NCL_FUNCAO (IDMODULO, IDFUNCAO, NOME) VALUES ('MOD.NCL','FUN.NCL.014','Cadastro de Empresas');
		INSERT INTO NCL_OPERACAO (IDMODULO, IDFUNCAO, IDOPERACAO, NOME) VALUES ('MOD.NCL','FUN.NCL.014','OPE.NCL.014.0001','Inserir');
		INSERT INTO NCL_OPERACAO (IDMODULO, IDFUNCAO, IDOPERACAO, NOME) VALUES ('MOD.NCL','FUN.NCL.014','OPE.NCL.014.0002','Modificar');
		INSERT INTO NCL_OPERACAO (IDMODULO, IDFUNCAO, IDOPERACAO, NOME) VALUES ('MOD.NCL','FUN.NCL.014','OPE.NCL.014.0003','Excluir');	
	INSERT INTO NCL_FUNCAO (IDMODULO, IDFUNCAO, NOME) VALUES ('MOD.NCL','FUN.NCL.015','Visibilidade por Empresa');
		INSERT INTO NCL_OPERACAO (IDMODULO, IDFUNCAO, IDOPERACAO, NOME) VALUES ('MOD.NCL','FUN.NCL.015','OPE.NCL.015.0001','Inserir');
		INSERT INTO NCL_OPERACAO (IDMODULO, IDFUNCAO, IDOPERACAO, NOME) VALUES ('MOD.NCL','FUN.NCL.015','OPE.NCL.015.0002','Modificar');
		INSERT INTO NCL_OPERACAO (IDMODULO, IDFUNCAO, IDOPERACAO, NOME) VALUES ('MOD.NCL','FUN.NCL.015','OPE.NCL.015.0003','Excluir');	
	INSERT INTO NCL_FUNCAO (IDMODULO, IDFUNCAO, NOME) VALUES ('MOD.NCL','FUN.NCL.016','Cadastro de Grupo de Atividade');
		INSERT INTO NCL_OPERACAO (IDMODULO, IDFUNCAO, IDOPERACAO, NOME) VALUES ('MOD.NCL','FUN.NCL.016','OPE.NCL.016.0001','Inserir');
		INSERT INTO NCL_OPERACAO (IDMODULO, IDFUNCAO, IDOPERACAO, NOME) VALUES ('MOD.NCL','FUN.NCL.016','OPE.NCL.016.0002','Modificar');
		INSERT INTO NCL_OPERACAO (IDMODULO, IDFUNCAO, IDOPERACAO, NOME) VALUES ('MOD.NCL','FUN.NCL.016','OPE.NCL.016.0003','Excluir');	
	INSERT INTO NCL_FUNCAO (IDMODULO, IDFUNCAO, NOME) VALUES ('MOD.NCL','FUN.NCL.017','Cadastro de Tipo de Endereço');
		INSERT INTO NCL_OPERACAO (IDMODULO, IDFUNCAO, IDOPERACAO, NOME) VALUES ('MOD.NCL','FUN.NCL.017','OPE.NCL.017.0001','Inserir');
		INSERT INTO NCL_OPERACAO (IDMODULO, IDFUNCAO, IDOPERACAO, NOME) VALUES ('MOD.NCL','FUN.NCL.017','OPE.NCL.017.0002','Modificar');
		INSERT INTO NCL_OPERACAO (IDMODULO, IDFUNCAO, IDOPERACAO, NOME) VALUES ('MOD.NCL','FUN.NCL.017','OPE.NCL.017.0003','Excluir');	
	INSERT INTO NCL_FUNCAO (IDMODULO, IDFUNCAO, NOME) VALUES ('MOD.NCL','FUN.NCL.018','Cadastro de País');
		INSERT INTO NCL_OPERACAO (IDMODULO, IDFUNCAO, IDOPERACAO, NOME) VALUES ('MOD.NCL','FUN.NCL.018','OPE.NCL.018.0001','Inserir');
		INSERT INTO NCL_OPERACAO (IDMODULO, IDFUNCAO, IDOPERACAO, NOME) VALUES ('MOD.NCL','FUN.NCL.018','OPE.NCL.018.0002','Modificar');
		INSERT INTO NCL_OPERACAO (IDMODULO, IDFUNCAO, IDOPERACAO, NOME) VALUES ('MOD.NCL','FUN.NCL.018','OPE.NCL.018.0003','Excluir');	
														
INSERT INTO NCL_MENUMODULO (IDMODULO, IMAGEM) VALUES ('MOD.NCL','bogus');
	
	INSERT INTO NCL_MENUFUNCAO (IDMODULO, IDFUNCAO, IMAGEM, URL, AGRUPADOR) VALUES ('MOD.NCL','FUN.NCL.001','bogus','Nucleo/cdMunicipio.aspx', 'Cadastros');
	INSERT INTO NCL_MENUFUNCAO (IDMODULO, IDFUNCAO, IMAGEM, URL, AGRUPADOR) VALUES ('MOD.NCL','FUN.NCL.002','bogus','Nucleo/cdGrupo.aspx', 'Cadastros');
	INSERT INTO NCL_MENUFUNCAO (IDMODULO, IDFUNCAO, IMAGEM, URL, AGRUPADOR) VALUES ('MOD.NCL','FUN.NCL.003','bogus','Nucleo/frmAutorizacao.aspx', NULL);
	INSERT INTO NCL_MENUFUNCAO (IDMODULO, IDFUNCAO, IMAGEM, URL, AGRUPADOR) VALUES ('MOD.NCL','FUN.NCL.004','bogus','Nucleo/frmAlterarSenhaDoOperador.aspx', NULL);
	INSERT INTO NCL_MENUFUNCAO (IDMODULO, IDFUNCAO, IMAGEM, URL, AGRUPADOR) VALUES ('MOD.NCL','FUN.NCL.005','bogus','Nucleo/cdOperador.aspx', 'Cadastros');
	INSERT INTO NCL_MENUFUNCAO (IDMODULO, IDFUNCAO, IMAGEM, URL, AGRUPADOR) VALUES ('MOD.NCL','FUN.NCL.007','bogus','Nucleo/frmAlterarSenha.aspx', NULL);
	INSERT INTO NCL_MENUFUNCAO (IDMODULO, IDFUNCAO, IMAGEM, URL, AGRUPADOR) VALUES ('MOD.NCL','FUN.NCL.008','bogus','Nucleo/cdCliente.aspx', 'Cadastros');
	INSERT INTO NCL_MENUFUNCAO (IDMODULO, IDFUNCAO, IMAGEM, URL, AGRUPADOR) VALUES ('MOD.NCL','FUN.NCL.009','bogus','Nucleo/cdBancosEAgencias.aspx', 'Cadastros');
	INSERT INTO NCL_MENUFUNCAO (IDMODULO, IDFUNCAO, IMAGEM, URL, AGRUPADOR) VALUES ('MOD.NCL','FUN.NCL.010','bogus','Nucleo/frmPainelDeControle.aspx', 'Configurações');
	INSERT INTO NCL_MENUFUNCAO (IDMODULO, IDFUNCAO, IMAGEM, URL, AGRUPADOR) VALUES ('MOD.NCL','FUN.NCL.011','bogus','Nucleo/frmConfiguracoesPessoais.aspx', 'Configurações');
	INSERT INTO NCL_MENUFUNCAO (IDMODULO, IDFUNCAO, IMAGEM, URL, AGRUPADOR) VALUES ('MOD.NCL','FUN.NCL.012','bogus','Nucleo/frmAgenda.aspx', NULL);
	INSERT INTO NCL_MENUFUNCAO (IDMODULO, IDFUNCAO, IMAGEM, URL, AGRUPADOR) VALUES ('MOD.NCL','FUN.NCL.013','bogus','Nucleo/cdFornecedor.aspx','Cadastros');
	INSERT INTO NCL_MENUFUNCAO (IDMODULO, IDFUNCAO, IMAGEM, URL, AGRUPADOR) VALUES ('MOD.NCL','FUN.NCL.014','bogus','Nucleo/cdEmpresa.aspx', 'Cadastros');
	INSERT INTO NCL_MENUFUNCAO (IDMODULO, IDFUNCAO, IMAGEM, URL, AGRUPADOR) VALUES ('MOD.NCL','FUN.NCL.015','bogus','Nucleo/frmVisibilidadePorEmpresa.aspx', NULL);
	INSERT INTO NCL_MENUFUNCAO (IDMODULO, IDFUNCAO, IMAGEM, URL, AGRUPADOR) VALUES ('MOD.NCL','FUN.NCL.016','bogus','Nucleo/cdGrupoDeAtividade.aspx', 'Cadastros');
	INSERT INTO NCL_MENUFUNCAO (IDMODULO, IDFUNCAO, IMAGEM, URL, AGRUPADOR) VALUES ('MOD.NCL','FUN.NCL.017','bogus','Nucleo/cdTipoDeEndereco.aspx', 'Cadastros');
	INSERT INTO NCL_MENUFUNCAO (IDMODULO, IDFUNCAO, IMAGEM, URL, AGRUPADOR) VALUES ('MOD.NCL','FUN.NCL.018','bogus','Nucleo/cdPaises.aspx', 'Cadastros');