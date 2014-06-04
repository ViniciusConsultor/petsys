﻿INSERT INTO NCL_MODULO (IDMODULO, NOME) VALUES ('MOD.FN','Financeiro');
	INSERT INTO NCL_FUNCAO (IDMODULO, IDFUNCAO, NOME) VALUES ('MOD.FN','FUN.FN.001','Contas a Receber');
		INSERT INTO NCL_OPERACAO (IDMODULO, IDFUNCAO, IDOPERACAO, NOME) VALUES ('MOD.FN','FUN.FN.001','OPE.FN.001.0001','Inserir');
		INSERT INTO NCL_OPERACAO (IDMODULO, IDFUNCAO, IDOPERACAO, NOME) VALUES ('MOD.FN','FUN.FN.001','OPE.FN.001.0002','Modificar');
		INSERT INTO NCL_OPERACAO (IDMODULO, IDFUNCAO, IDOPERACAO, NOME) VALUES ('MOD.FN','FUN.FN.001','OPE.FN.001.0003','Cancelar');

	INSERT INTO NCL_FUNCAO (IDMODULO, IDFUNCAO, NOME) VALUES ('MOD.FN','FUN.FN.002','Gerar Boleto avulso');

	INSERT INTO NCL_FUNCAO (IDMODULO, IDFUNCAO, NOME) VALUES ('MOD.FN','FUN.FN.004','Configurações de índices financeiros');
		INSERT INTO NCL_OPERACAO (IDMODULO, IDFUNCAO, IDOPERACAO, NOME) VALUES ('MOD.FN','FUN.FN.004','OPE.FN.004.0001','Salvar');

	INSERT INTO NCL_FUNCAO (IDMODULO, IDFUNCAO, NOME) VALUES ('MOD.FN','FUN.FN.005','Histórico de boletos gerados');

	INSERT INTO NCL_FUNCAO (IDMODULO, IDFUNCAO, NOME) VALUES ('MOD.FN','FUN.FN.006','Configuração Geral');
		INSERT INTO NCL_OPERACAO (IDMODULO, IDFUNCAO, IDOPERACAO, NOME) VALUES ('MOD.FN','FUN.FN.006','OPE.FN.006.0001','Salvar');

	INSERT INTO NCL_FUNCAO (IDMODULO, IDFUNCAO, NOME) VALUES ('MOD.FN','FUN.FN.007','Lançamentos financeiros de recebimento');
		INSERT INTO NCL_OPERACAO (IDMODULO, IDFUNCAO, IDOPERACAO, NOME) VALUES ('MOD.FN','FUN.FN.007','OPE.FN.007.0001','Inserir');
		INSERT INTO NCL_OPERACAO (IDMODULO, IDFUNCAO, IDOPERACAO, NOME) VALUES ('MOD.FN','FUN.FN.007','OPE.FN.007.0002','Modificar');
		INSERT INTO NCL_OPERACAO (IDMODULO, IDFUNCAO, IDOPERACAO, NOME) VALUES ('MOD.FN','FUN.FN.007','OPE.FN.007.0003','Cancelar');
		INSERT INTO NCL_OPERACAO (IDMODULO, IDFUNCAO, IDOPERACAO, NOME) VALUES ('MOD.FN','FUN.FN.007','OPE.FN.007.0004','Efetivar cobrança');

INSERT INTO NCL_MENUMODULO (IDMODULO, IMAGEM) VALUES ('MOD.FN','bogus');
	INSERT INTO NCL_MENUFUNCAO (IDMODULO, IDFUNCAO, IMAGEM, URL, AGRUPADOR) VALUES ('MOD.FN','FUN.FN.001','bogus','FN/frmContasAReceber.aspx', NULL);
	INSERT INTO NCL_MENUFUNCAO (IDMODULO, IDFUNCAO, IMAGEM, URL, AGRUPADOR) VALUES ('MOD.FN','FUN.FN.002','bogus','FN/frmBoletoAvulso.aspx',NULL);
	INSERT INTO NCL_MENUFUNCAO (IDMODULO, IDFUNCAO, IMAGEM, URL, AGRUPADOR) VALUES ('MOD.FN','FUN.FN.004','bogus','FN/frmConfiguracaoDeIndicesFinaceiros.aspx', 'Configurações');
	INSERT INTO NCL_MENUFUNCAO (IDMODULO, IDFUNCAO, IMAGEM, URL, AGRUPADOR) VALUES ('MOD.FN','FUN.FN.005','bogus','FN/frmBoletosGerados.aspx', NULL);
	INSERT INTO NCL_MENUFUNCAO (IDMODULO, IDFUNCAO, IMAGEM, URL, AGRUPADOR) VALUES ('MOD.FN','FUN.FN.006','bogus','FN/frmConfiguracaoGeralFinaceiro.aspx', 'Configurações');
	INSERT INTO NCL_MENUFUNCAO (IDMODULO, IDFUNCAO, IMAGEM, URL, AGRUPADOR) VALUES ('MOD.FN','FUN.FN.007','bogus','FN/frmGerenciamentoDeItensFinanceiros.aspx', NULL);