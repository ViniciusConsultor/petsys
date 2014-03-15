﻿CREATE TABLE MP_PASTA (
	ID								BIGINT				NOT NULL,
	CODIGO							VARCHAR(20)			NOT NULL,
	NOME							VARCHAR(100)		NOT NULL
);

ALTER TABLE MP_PASTA
	ADD CONSTRAINT PK_MP_PASTA
	PRIMARY KEY(ID)
;

CREATE INDEX IDX_MP_PASTA
	ON MP_PASTA(NOME)
;

CREATE TABLE MP_NATUREZA_PATENTE (
	IDNATUREZA_PATENTE 				BIGINT 				NOT NULL,
	DESCRICAO_NATUREZA_PATENTE 		VARCHAR(4000) 		NOT	NULL,
	SIGLA_NATUREZA 					CHAR(2) 			NOT	NULL,
	TEMPO_INICIO_ANOS 				INT 				NOT	NULL,
	QUANTIDADE_PAGTO 				INT					NOT	NULL,
	TEMPO_ENTRE_PAGTO 				INT	 				NOT	NULL,
	SEQUENCIA_INICIO_PAGTO 			INT 				NOT	NULL,
	TEM_PAGTO_INTERMEDIARIO 		CHAR(1) 				NULL,
	INICIO_INTERMED_SEQUENCIA 		INT 				NOT	NULL,
	QUANTIDADE_PAGTO_INTERMED 		INT 				NOT	NULL,
	TEMPO_ENTRE_PAGTO_INTERMED 		INT					NOT	NULL,
	DESCRICAO_PAGTO 				VARCHAR(4000) 		NOT	NULL,
	DESCRICAO_PAGTO_INTERMED 		VARCHAR(4000) 		NOT	NULL,
	TEM_PED_EXAME 					CHAR(1) 			    NULL
);

ALTER TABLE MP_NATUREZA_PATENTE
	ADD CONSTRAINT PK_NATUREZA_PATENTE
	PRIMARY KEY(IDNATUREZA_PATENTE);

CREATE TABLE MP_INVENTOR (
	IDPESSOA						BIGINT				NOT NULL,
	TIPOPESSOA						SMALLINT			NOT NULL,
	DTCADASTRO						INT					NOT NULL,
	INFOADICIONAL					VARCHAR(4000)		NULL
);

ALTER TABLE MP_INVENTOR
	ADD CONSTRAINT PK_MP_INVENTOR
	PRIMARY KEY (IDPessoa)
;

ALTER TABLE MP_INVENTOR
	ADD CONSTRAINT FK_MP_INVENTOR1
	FOREIGN KEY(IDPESSOA)
	REFERENCES NCL_PESSOA(ID)
;

CREATE TABLE MP_TIPO_PROCEDIMENTO_INTERNO(
	IDTIPO_PROCEDIMENTO_INTERNO		BIGINT				NOT NULL,
	DESCRICAO_TIPO					VARCHAR(255)		NOT NULL
);
	
ALTER TABLE MP_TIPO_PROCEDIMENTO_INTERNO
	ADD CONSTRAINT PK_MP_TIPO_PROCEDIMENTO_INTERNO
	PRIMARY KEY(IDTIPO_PROCEDIMENTO_INTERNO)
;

CREATE TABLE MP_PROCURADORES
(
	IDPESSOA						BIGINT				NOT NULL,
	TIPOPESSOA						SMALLINT			NOT NULL,
	MATRICULAAPI					VARCHAR(22)				NULL,
	SIGLAORGAO						CHAR(10)				NULL,
	NRREGISTROORGAO					VARCHAR(17)				NULL,
	DATAREGISTROORGAO				INT						NULL,
	OBSCONTATO						VARCHAR(255)			NULL
);

ALTER TABLE MP_PROCURADORES
	ADD CONSTRAINT PK_MP_PROCURADORES
	PRIMARY KEY (IDPessoa);

ALTER TABLE MP_PROCURADORES
	ADD CONSTRAINT FK_MP_PROCURADORES1
	FOREIGN KEY(IDPESSOA)
	REFERENCES NCL_PESSOA(ID);


CREATE TABLE MP_DESPACHO_MARCA(
	IDDESPACHO						BIGINT				NOT NULL,
	CODIGO_DESPACHO					VARCHAR(50)			NOT NULL,
	DESCRICAO_DESPACHO				VARCHAR(4000)		NOT	NULL,
	SITUACAODOPROCESSO				VARCHAR(4000)			NULL,
	PRAZOPROVIDENCIA				INT					NOT NULL,
	PROVIDENCIA						VARCHAR(4000)			NULL,
	DESATIVAPROCESSO				CHAR(1)				NOT NULL,
	DESATIVAPESQCOLIDENCIA			CHAR(1)				NOT NULL,
	TEMPLATEEMAIL					VARCHAR(4000)			NULL
);
	
ALTER TABLE MP_DESPACHO_MARCA
	ADD CONSTRAINT PK_DESPACHO
	PRIMARY KEY(IDDESPACHO)
;

CREATE TABLE MP_PATENTE
(
	IDPATENTE					BIGINT					NOT NULL,
	TITULOPATENTE				VARCHAR(4000)				NULL,
	IDNATUREZAPATENTE			BIGINT					NOT NULL,
	OBRIGACAOGERADA				CHAR(1)					NOT NULL,
	DATACADASTRO				INT							NULL,
	OBSERVACAO					VARCHAR(4000)				NULL,
	RESUMO_PATENTE				VARCHAR(4000)				NULL,
	QTDEREINVINDICACAO			INT							NULL,
	PAGAMANUTENCAO				CHAR(1)						NULL,
	PERIODO						VARCHAR(20)					NULL,
	FORMADECOBRANCA				CHAR(1)						NULL,
	VALORDECOBRANCA				FLOAT						NULL,
	IMAGEM						VARCHAR(255)				NULL,
	DATAPROXIMAMANUTENCAO		INT							NULL
);

ALTER TABLE MP_PATENTE
	ADD CONSTRAINT PK_MP_PATENTE
	PRIMARY KEY (IDPATENTE);

ALTER TABLE MP_PATENTE
	ADD CONSTRAINT FK_MP_PATENTE2
	FOREIGN KEY (IDNATUREZAPATENTE) REFERENCES MP_NATUREZA_PATENTE(IDNATUREZA_PATENTE);

CREATE INDEX IDX_MP_PATENTE1
	ON MP_PATENTE(TITULOPATENTE)
;

CREATE TABLE MP_PATENTEANUIDADE
(
	IDPATENTEANUIDADE				BIGINT				NOT NULL,
	IDPATENTE						BIGINT				NOT NULL,
	DESCRICAOANUIDADE				VARCHAR(45)				NULL,
	DATALANCAMENTO					INT						NULL,
	DATAVENCIMENTO					INT						NULL,
	DATAPAGAMENTO					INT						NULL,
	VALORPAGAMENTO					FLOAT					NULL,
	ANUIDADEPAGA					CHAR(1)				NOT NULL,
	PEDIDOEXAME						CHAR(1)					NULL,	
	DATAVENCTO_SEM_MULTA			INT						NULL,
	DATAVENCTO_COM_MULTA			INT						NULL
);

ALTER TABLE MP_PATENTEANUIDADE
	ADD CONSTRAINT PK_MP_PATENTEANUIDADE
	PRIMARY KEY (IDPATENTEANUIDADE)
;
	
ALTER TABLE MP_PATENTEANUIDADE
	ADD CONSTRAINT FK_MP_PATENTEANUIDADE
	FOREIGN KEY (IDPATENTE) REFERENCES MP_PATENTE(IDPATENTE)
;	

CREATE TABLE MP_PATENTECLASSIFICACAO
(
	IDPATENTECLASSIFICACAO			BIGINT				NOT NULL,
	CLASSIFICACAO					VARCHAR(20)			NOT NULL,
	DESCRICAO_CLASSIFICACAO			VARCHAR(4000)		NULL,
	IDPATENTE						BIGINT				NOT NULL,
	TIPO_CLASSIFICACAO				INT					NOT NULL
);

ALTER TABLE MP_PATENTECLASSIFICACAO
	ADD CONSTRAINT PK_MP_PATENTECLASSIFICACAO
	PRIMARY KEY (IDPATENTECLASSIFICACAO)
;
	
ALTER TABLE MP_PATENTECLASSIFICACAO
	ADD CONSTRAINT FK_MP_PATENTECLASSIFICACAO
	FOREIGN KEY (IDPATENTE) REFERENCES MP_PATENTE(IDPATENTE)
;	

CREATE TABLE MP_PATENTEPRIORIDADEUNIONISTA
(
	IDPRIORIDADEUNIONISTA			BIGINT				NOT NULL,
	DATA_PRIORIDADE					INT						NULL,		
	NUMERO_PRIORIDADE				VARCHAR(20)				NULL,
	IDPATENTE						BIGINT				NOT NULL,
	IDPAIS							BIGINT					NULL
);

ALTER TABLE MP_PATENTEPRIORIDADEUNIONISTA
	ADD CONSTRAINT PK_MP_PATENTEPRIORIDADEUNIONISTA
	PRIMARY KEY (IDPRIORIDADEUNIONISTA)
;
	
ALTER TABLE MP_PATENTEPRIORIDADEUNIONISTA
	ADD CONSTRAINT FK_MP_PATENTEPRIORIDADEUNIONISTA
	FOREIGN KEY (IDPATENTE) REFERENCES MP_PATENTE(IDPATENTE)
;	
	
ALTER TABLE MP_PATENTEPRIORIDADEUNIONISTA
	ADD CONSTRAINT FK_MP_PATENTEPRIORIDADEUNIONISTA2
	FOREIGN KEY (IDPAIS) REFERENCES NCL_PAIS(ID)
;	

CREATE TABLE MP_PATENTEINVENTOR
(
	IDINVENTOR						BIGINT				NOT NULL,
	IDPATENTE						BIGINT				NOT NULL
);

ALTER TABLE MP_PATENTEINVENTOR
	ADD CONSTRAINT PK_MP_PATENTEINVENTOR
	PRIMARY KEY (IDINVENTOR, IDPATENTE);
	
ALTER TABLE MP_PATENTEINVENTOR
	ADD CONSTRAINT FK_MP_PATENTEINVENTOR
	FOREIGN KEY (IDPATENTE) REFERENCES MP_PATENTE(IDPATENTE);

ALTER TABLE MP_PATENTEINVENTOR
	ADD CONSTRAINT FK_MP_MP_PATENTEINVENTOR2
	FOREIGN KEY (IDINVENTOR) REFERENCES MP_INVENTOR(IDPESSOA);

CREATE TABLE MP_MARCAS(
	IDMARCA							BIGINT				NOT NULL,
	CODIGONCL						CHAR(2)				NOT NULL,
	CODIGOAPRESENTACAO				INT					NOT NULL,
	IDCLIENTE						BIGINT				NOT NULL,
	CODIGONATUREZA					INT					NOT NULL,
	DESCRICAO_MARCA					VARCHAR(255)			NULL,
	ESPECIFICACAO_PROD_SERV			VARCHAR(4000)			NULL,
	IMAGEM_MARCA					VARCHAR(255)			NULL,
	OBSERVACAO_MARCA				VARCHAR(4000)			NULL,
	CODIGOCLASSE					INT						NULL,
	CODIGOCLASSE_SUBCLASSE1			INT						NULL,
	CODIGOCLASSE_SUBCLASSE2			INT						NULL,
	CODIGOCLASSE_SUBCLASSE3			INT						NULL,
	PAGAMANUTENCAO					CHAR(1)					NULL,
	PERIODO							VARCHAR(20)				NULL,
	FORMADECOBRANCA					CHAR(1)					NULL,
	VALORDECOBRANCA					FLOAT					NULL,
	DATAPROXIMAMANUTENCAO			INT						NULL
);

ALTER TABLE MP_MARCAS
	ADD CONSTRAINT PK_MARCAS
	PRIMARY KEY (IDMARCA)
;	
	
ALTER TABLE MP_MARCAS
	ADD CONSTRAINT FK_MARCAS_CONTATO
	FOREIGN KEY (IDCLIENTE) REFERENCES NCL_CLIENTE(IDPESSOA)
;

CREATE INDEX IDX_MP_MARCAS1
	ON MP_MARCAS(CODIGONCL)
;

CREATE INDEX IDX_MP_MARCAS2
	ON MP_MARCAS(CODIGOAPRESENTACAO)
;

CREATE INDEX IDX_MP_MARCAS3
	ON MP_MARCAS(CODIGONATUREZA)
;

CREATE INDEX IDX_MP_MARCAS4
	ON MP_MARCAS(DESCRICAO_MARCA)
;

CREATE TABLE MP_PROCESSOMARCA (
	IDPROCESSO						BIGINT				NOT NULL,
	IDMARCA							BIGINT				NOT NULL,
	PROCESSO						BIGINT				NOT NULL,
	DATADECADASTRO					INT					NOT NULL,
	DATADODEPOSITO					INT						NULL,
	DATACONCESSAO					INT						NULL,
	PROCESSOEHTERCEIRO				CHAR(1)				NOT NULL,
	IDDESPACHO						BIGINT					NULL,
	TXTCOMPLDESPACHO				VARCHAR(4000)			NULL,
	IDPROCURADOR					BIGINT					NULL,
	APOSTILA						VARCHAR(4000)			NULL,
	ATIVO							CHAR(1)				NOT NULL
);

ALTER TABLE MP_PROCESSOMARCA
	ADD CONSTRAINT PK_MP_PROCESSOMARCA
	PRIMARY KEY (IDPROCESSO)
;	

ALTER TABLE MP_PROCESSOMARCA
	ADD CONSTRAINT FK_MP_PROCESSOMARCA1
	FOREIGN KEY (IDMARCA) REFERENCES MP_MARCAS(IDMARCA)
;

ALTER TABLE MP_PROCESSOMARCA
	ADD CONSTRAINT FK_MP_PROCESSOMARCA2
	FOREIGN KEY (IDDESPACHO) REFERENCES MP_DESPACHO_MARCA(IDDESPACHO)
;

ALTER TABLE MP_PROCESSOMARCA
	ADD CONSTRAINT FK_MP_PROCESSOMARCA3
	FOREIGN KEY (IDPROCURADOR) REFERENCES MP_PROCURADORES(IDPESSOA)
;

CREATE INDEX IDX_MP_PROCESSOMARCA2
	ON MP_PROCESSOMARCA(PROCESSO)
;

CREATE INDEX IDX_MP_PROCESSOMARCA3
	ON MP_PROCESSOMARCA(DATADECADASTRO)
;

CREATE INDEX IDX_MP_PROCESSOMARCA4
	ON MP_PROCESSOMARCA(ATIVO)
;

CREATE TABLE MP_RADICAL_MARCA (
	IDRADICAL						BIGINT				NOT NULL,
	DESCRICAORADICAL				VARCHAR(50)			NOT NULL,
	IDMARCA							BIGINT				NOT NULL,
	CODIGONCL						CHAR(2)					NULL
);

ALTER TABLE MP_RADICAL_MARCA
	ADD CONSTRAINT PK_MP_RADICAL_MARCA
	PRIMARY KEY (IDRADICAL)
;

CREATE INDEX IDX_MP_RADICAL_MARCA1
	ON MP_RADICAL_MARCA(IDMARCA)
;

CREATE INDEX IDX_MP_RADICAL_MARCA2
	ON MP_RADICAL_MARCA(DESCRICAORADICAL)
;

CREATE TABLE MP_DESPACHO_PATENTE
(
	IDDESPACHOPATENTE				BIGINT				NOT NULL,
	CODIGO							VARCHAR(20)			NOT NULL,
	DESCRICAO						VARCHAR(4000)			NULL,
	TITULO							VARCHAR(255)		NOT NULL,
	SITUACAO						VARCHAR(4000)			NULL,
	PRAZO							INT						NULL,
	PROVIDENCIA						VARCHAR(4000)			NULL,
	DESATIVAPROCESSO				CHAR(1)				NOT NULL,
	AGENDAPAGAMENTO					CHAR(1)				NOT NULL,
	TEMPLATEEMAIL					VARCHAR(4000)			NULL
)
;
	
ALTER TABLE MP_DESPACHO_PATENTE
	ADD CONSTRAINT PK_MP_DESPACHO_PATENTE
	PRIMARY KEY(IDDESPACHOPATENTE)
;

CREATE TABLE MP_PROCESSOPATENTE
(
	IDPROCESSOPATENTE				BIGINT				NOT NULL,
    IDPATENTE						BIGINT				NOT NULL,
	PROCESSO					    VARCHAR(25)			NOT NULL,
	DATADECADASTRO					INT					NOT NULL,
	DATADEPUBLICACAO				INT						NULL,
	DATADEDEPOSITO					INT						NULL,
	DATADECONCESSAO					INT						NULL,
	DATADEEXAME						INT						NULL,
	PROCESSODETERCEIRO				CHAR(1)				NOT NULL,
	IDDESPACHO						BIGINT					NULL,
	IDPROCURADOR					BIGINT					NULL,
	EHESTRANGEIRO					CHAR(1)				NOT NULL,
	NUMEROPCT						VARCHAR(20)				NULL,
	NUMEROWO						VARCHAR(20)				NULL,
	DATADEPOSITOPCT					INT						NULL,
	DATAPUBLICACAOPCT				INT						NULL,
	IDPASTA							BIGINT					NULL,
	ATIVO							CHAR(1)				NOT NULL,
	PAIS							BIGINT					NULL
)
;

ALTER TABLE MP_PROCESSOPATENTE
	ADD CONSTRAINT PK_MP_PROCESSOPATENTE
	PRIMARY KEY (IDPROCESSOPATENTE)
;

ALTER TABLE MP_PROCESSOPATENTE
	ADD CONSTRAINT FK_MP_PROCESSOPATENTE
	FOREIGN KEY (IDPROCURADOR) REFERENCES MP_PROCURADORES(IDPESSOA)
;

ALTER TABLE MP_PROCESSOPATENTE
	ADD CONSTRAINT FK_MP_PROCESSOPATENTE1
	FOREIGN KEY (IDDESPACHO) REFERENCES MP_DESPACHO_PATENTE(IDDESPACHOPATENTE)
;

ALTER TABLE MP_PROCESSOPATENTE
	ADD CONSTRAINT FK_MP_PROCESSOPATENTE2
	FOREIGN KEY (IDPATENTE) REFERENCES MP_PATENTE(IDPATENTE)
;

ALTER TABLE MP_PROCESSOPATENTE
	ADD CONSTRAINT FK_MP_PROCESSOPATENTE3
	FOREIGN KEY (PAIS) REFERENCES NCL_PAIS(ID);

CREATE TABLE MP_PATENTECLIENTE
(
	IDCLIENTE				BIGINT				NOT NULL,
	IDPATENTE				BIGINT				NOT NULL
);

ALTER TABLE MP_PATENTECLIENTE
	ADD CONSTRAINT PK_MP_PATENTECLIENTE
	PRIMARY KEY (IDCLIENTE, IDPATENTE);
	
ALTER TABLE MP_PATENTECLIENTE
	ADD CONSTRAINT FK_MP_PATENTECLIENTE
	FOREIGN KEY (IDPATENTE) REFERENCES MP_PATENTE(IDPATENTE);

ALTER TABLE MP_PATENTECLIENTE
	ADD CONSTRAINT FK_MP_PATENTECLIENTE2
	FOREIGN KEY (IDCLIENTE) REFERENCES NCL_CLIENTE(IDPESSOA);


CREATE TABLE MP_REVISTA_MARCAS
(
	IDREVISTAMARCAS			BIGINT			NOT NULL,
	NUMEROREVISTAMARCAS		INT				NOT NULL,
	DATAPUBLICACAO			VARCHAR(20)		NULL,
	DATAPROCESSAMENTO		VARCHAR(20)		NULL,
	NUMEROPROCESSODEMARCA	BIGINT			NULL,
	CODIGODESPACHOANTERIOR	VARCHAR(20)		NULL,
	CODIGODESPACHOATUAL		VARCHAR(20)		NULL,
	APOSTILA				VARCHAR(255)	NULL,
	TEXTODODESPACHO			VARCHAR(4000)	NULL,
	PROCESSADA				CHAR(1)			NOT NULL,
	EXTENSAOARQUIVO			VARCHAR(4)		NULL,
	DATADODEPOSITO			VARCHAR(20)		NULL,
	DATACONCESSAO			VARCHAR(20)		NULL
);

ALTER TABLE MP_REVISTA_MARCAS
	ADD CONSTRAINT PK_MP_REVISTA_MARCAS
	PRIMARY KEY (IDREVISTAMARCAS);

CREATE TABLE MP_REVISTA_PATENTE
(
	IDREVISTAPATENTE        BIGINT			NOT NULL,
	NUMEROREVISTAPATENTE    INT				NOT NULL,
	DATAPUBLICACAO			VARCHAR(20)		NULL,
	DATAPROCESSAMENTO		VARCHAR(20)		NULL,
	PROCESSADA				CHAR(1)			NOT NULL,
	EXTENSAOARQUIVO			VARCHAR(4)		NULL,
	DATADODEPOSITO			VARCHAR(20)		NULL,
	NUMEROPROCESSOPATENTE	VARCHAR(255)	NULL,
	NUMERODOPEDIDO          BIGINT			NULL,
	DATAPUBLICPEDIDO		VARCHAR(20)		NULL,
	DATACONCESSAO			VARCHAR(20)		NULL,
	PRIORIDADEUNIONISTA		VARCHAR(255)	NULL,
	CLASSIFICACAOINTER		VARCHAR(255)	NULL,
	TITULO					VARCHAR(255)	NULL,
	RESUMO					VARCHAR(4000)	NULL,
	DADOSPEDIDOPATENTE		VARCHAR(255)	NULL,
	DADOSPATENTEORIGINAL	VARCHAR(255)	NULL,
	PRIORIDADEINTERNA	    VARCHAR(255)	NULL,
	DEPOSITANTE				VARCHAR(255)	NULL,
	INVENTOR				VARCHAR(255)	NULL,
	TITULAR					VARCHAR(255)	NULL,
	UFTITULAR				VARCHAR(255)	NULL,
	PAISTITULAR				VARCHAR(255)	NULL,
	PROCURADOR				VARCHAR(255)	NULL,
	PAISESDESIGNADOS        VARCHAR(255)	NULL,
	DATAINICIOFASENAC		VARCHAR(20)		NULL,
	DADOSDEPOSINTER         VARCHAR(255)	NULL,
	DADOSPUBLICINTER        VARCHAR(255)	NULL,
	CODIGODESPACHOANTERIOR	VARCHAR(20)		NULL,
	CODIGODESPACHOATUAL		VARCHAR(20)		NULL,	
	RESPPGTOIMPRENDA        VARCHAR(255)	NULL,
	COMPLEMENTO				VARCHAR(4000)	NULL,
	DECISAO					VARCHAR(255)	NULL,
	RECORRENTE				VARCHAR(255)	NULL,
	NUMERODOPROCESSO        BIGINT			NULL,
	CEDENTE					VARCHAR(255)	NULL,
	CESSIONARIA				VARCHAR(255)	NULL,	
	OBSERVACAO				VARCHAR(4000)	NULL,	
	ULTIMAINFORMACAO		VARCHAR(255)	NULL,	
	CERTIFAVERBACAO			VARCHAR(255)	NULL,	
	PAISCEDENTE				VARCHAR(255)	NULL,	
	PAISCESSIONARIA			VARCHAR(255)	NULL,	
	SETOR					VARCHAR(255)	NULL,	
	ENDERECOCESSIONARIA		VARCHAR(255)	NULL,	
	NATUREZADOCUMENTO		VARCHAR(255)	NULL,	
	MOEDADEPAGAMENTO		VARCHAR(255)	NULL,	
	VALOR					VARCHAR(1000)	NULL,	
	PAGAMENTO				VARCHAR(255)	NULL,	
	PRAZO					VARCHAR(2000)	NULL,	
	SERVISENTOSDEAVERBACAO  VARCHAR(255)	NULL,	
	CRIADOR					VARCHAR(255)	NULL,	
	LINGUAGEM				VARCHAR(255)	NULL,	
	CAMPOAPLICACAO			VARCHAR(255)	NULL,	
	TIPODEPROGRAMA			VARCHAR(255)	NULL,	
	DATADACRIACAO			VARCHAR(20)		NULL,	
	REGIMEDEGUARDA			VARCHAR(255)	NULL,
	REQUERENTE				VARCHAR(255)	NULL,
	REDACAO					VARCHAR(255)	NULL,
	DATAPRORROGACAO			VARCHAR(20)		NULL,	
	CLASSIFICACAONACIONAL   VARCHAR(255)	NULL	
);

ALTER TABLE MP_REVISTA_PATENTE
	ADD CONSTRAINT PK_MP_REVISTA_PATENTE
	PRIMARY KEY (IDREVISTAPATENTE);

CREATE TABLE MP_RADICAL_PATENTE (
	IDRADICAL						BIGINT				NOT NULL,
	COLIDENCIA						VARCHAR(50)			NOT NULL,
	IDPATENTE						BIGINT				NOT NULL);

ALTER TABLE MP_RADICAL_PATENTE
	ADD CONSTRAINT PK_MP_RADICAL_PATENTE
	PRIMARY KEY (IDRADICAL);

CREATE INDEX IDX_MP_RADICAL_PATENTE
	ON MP_RADICAL_PATENTE(IDPATENTE);

CREATE TABLE MP_TITULAR (
	IDPESSOA						BIGINT				NOT NULL,
	TIPOPESSOA						SMALLINT			NOT NULL,
	DTCADASTRO						INT					NOT NULL,
	INFOADICIONAL					VARCHAR(4000)		NULL
);

ALTER TABLE MP_TITULAR
	ADD CONSTRAINT PK_MP_TITULAR
	PRIMARY KEY (IDPessoa);

ALTER TABLE MP_TITULAR
	ADD CONSTRAINT FK_MP_TITULAR1
	FOREIGN KEY(IDPESSOA)
	REFERENCES NCL_PESSOA(ID);

CREATE TABLE MP_PATENTETITULAR
(
	IDTITULAR	BIGINT	NOT NULL,
	IDPATENTE	BIGINT	NOT NULL
);

ALTER TABLE MP_PATENTETITULAR
	ADD CONSTRAINT PK_MP_PATENTETITULAR
	PRIMARY KEY (IDTITULAR, IDPATENTE);
	
ALTER TABLE MP_PATENTETITULAR
	ADD CONSTRAINT FK_MP_PATENTETITULAR
	FOREIGN KEY (IDPATENTE) REFERENCES MP_PATENTE(IDPATENTE);

ALTER TABLE MP_PATENTETITULAR
	ADD CONSTRAINT FK_MP_PATENTETITULAR2
	FOREIGN KEY (IDTITULAR) REFERENCES MP_TITULAR(IDPESSOA);


CREATE TABLE MP_INTERFACEFN (
	IDITEMRECEBIMENTO	BIGINT NOT NULL,
	CONCEITO			VARCHAR(255) NOT NULL,
	IDCONCEITO			BIGINT	NOT NULL,
	DATAVENCIMENTO		INT		NOT NULL
);

ALTER TABLE MP_INTERFACEFN
	ADD CONSTRAINT PK_MP_INTERFACEFN
	PRIMARY KEY(IDITEMRECEBIMENTO, CONCEITO, IDCONCEITO, DATAVENCIMENTO);
;
