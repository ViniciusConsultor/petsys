﻿
CREATE TABLE PMP_PROCESSOSMARCAREVISTA (
	NUMERODAREVISTA				VARCHAR(50)	NOT NULL,
	DATAPUBLICACAOREVISTA		INTEGER		NOT NULL,
	NUMEROPROCESSODEMARCA		BIGINT		NOT NULL,
	DATADODEPOSITO				INTEGER			NULL,
	DATADACONCESSAO				INTEGER			NULL,
	DATADAVIGENCIA				INTEGER			NULL,
	CODIGODODESPACHO			VARCHAR(255)	NULL,
	NOMEDODESPACHO				VARCHAR(300)	NULL,
	TITULAR						VARCHAR(255)	NULL,
	PAISTITULAR					CHAR(2)			NULL,
	UFTITULAR					CHAR(2)			NULL,
	MARCA						VARCHAR(255)	NULL,
	APRESENTACAO				VARCHAR(50)		NULL,
	NATUREZA					VARCHAR(50)		NULL,
	EDICAOCLASSEVIENA			VARCHAR(50)		NULL,
	CODIGOCLASSEVIENA			VARCHAR(50)		NULL,
	CODIGOCLASSENACIONAL		VARCHAR(50)		NULL,
	CODIGOSUBCLASSENACIONAL		VARCHAR(50)		NULL,
	CODIGOCLASSENICE			VARCHAR(50)		NULL,
	PROCURADOR					VARCHAR(255)	NULL,
)

ALTER TABLE PMP_PROCESSOSMARCAREVISTA
	ADD CONSTRAINT PK_PMP_PROCESSOSMARCAREVISTA
	PRIMARY KEY (NUMERODAREVISTA, NUMEROPROCESSODEMARCA);	

CREATE INDEX IDX_PMP_PROCESSOSMARCAREVISTA1
	ON PMP_PROCESSOSMARCAREVISTA(NUMERODAREVISTA, NUMEROPROCESSODEMARCA, DATAPUBLICACAOREVISTA);
	
CREATE INDEX IDX_PMP_PROCESSOSMARCAREVISTA2
	ON PMP_PROCESSOSMARCAREVISTA(NUMEROPROCESSODEMARCA, DATAPUBLICACAOREVISTA);

CREATE INDEX IDX_PMP_PROCESSOSMARCAREVISTA3
	ON PMP_PROCESSOSMARCAREVISTA(NUMERODAREVISTA, MARCA, DATAPUBLICACAOREVISTA);
	
CREATE INDEX IDX_PMP_PROCESSOSMARCAREVISTA4
	ON PMP_PROCESSOSMARCAREVISTA(MARCA, DATAPUBLICACAOREVISTA);
	
CREATE INDEX IDX_PMP_PROCESSOSMARCAREVISTA5
	ON PMP_PROCESSOSMARCAREVISTA(NUMERODAREVISTA, MARCA, CODIGOCLASSENICE, DATAPUBLICACAOREVISTA);
	
CREATE INDEX IDX_PMP_PROCESSOSMARCAREVISTA6
	ON PMP_PROCESSOSMARCAREVISTA(MARCA, CODIGOCLASSENICE, DATAPUBLICACAOREVISTA);
	
CREATE INDEX IDX_PMP_PROCESSOSMARCAREVISTA7
	ON PMP_PROCESSOSMARCAREVISTA(NUMEROPROCESSODEMARCA);

CREATE INDEX IDX_PMP_PROCESSOSMARCAREVISTA8
	ON PMP_PROCESSOSMARCAREVISTA(TITULAR, NUMERODAREVISTA, DATAPUBLICACAOREVISTA);
	
CREATE INDEX IDX_PMP_PROCESSOSMARCAREVISTA9
	ON PMP_PROCESSOSMARCAREVISTA(TITULAR, DATAPUBLICACAOREVISTA);

CREATE INDEX IDX_PMP_PROCESSOSMARCAREVISTA10
	ON PMP_PROCESSOSMARCAREVISTA(TITULAR, NUMERODAREVISTA, UFTITULAR, PAISTITULAR, DATAPUBLICACAOREVISTA);

CREATE INDEX IDX_PMP_PROCESSOSMARCAREVISTA11
	ON PMP_PROCESSOSMARCAREVISTA(TITULAR, UFTITULAR, PAISTITULAR, DATAPUBLICACAOREVISTA);
	
CREATE INDEX IDX_PMP_PROCESSOSMARCAREVISTA12
	ON PMP_PROCESSOSMARCAREVISTA(TITULAR, NUMERODAREVISTA, UFTITULAR, DATAPUBLICACAOREVISTA);

CREATE INDEX IDX_PMP_PROCESSOSMARCAREVISTA13
	ON PMP_PROCESSOSMARCAREVISTA(TITULAR, UFTITULAR,  DATAPUBLICACAOREVISTA);

CREATE INDEX IDX_PMP_PROCESSOSMARCAREVISTA14
	ON PMP_PROCESSOSMARCAREVISTA(TITULAR, NUMERODAREVISTA, PAISTITULAR, DATAPUBLICACAOREVISTA);

CREATE INDEX IDX_PMP_PROCESSOSMARCAREVISTA15
	ON PMP_PROCESSOSMARCAREVISTA(TITULAR, PAISTITULAR,  DATAPUBLICACAOREVISTA);

CREATE INDEX IDX_PMP_PROCESSOSMARCAREVISTA16
	ON PMP_PROCESSOSMARCAREVISTA(NUMERODAREVISTA, UFTITULAR, PAISTITULAR, DATAPUBLICACAOREVISTA);
	
CREATE INDEX IDX_PMP_PROCESSOSMARCAREVISTA17
	ON PMP_PROCESSOSMARCAREVISTA(UFTITULAR, PAISTITULAR, DATAPUBLICACAOREVISTA);

CREATE INDEX IDX_PMP_PROCESSOSMARCAREVISTA18
	ON PMP_PROCESSOSMARCAREVISTA(NUMERODAREVISTA, UFTITULAR, DATAPUBLICACAOREVISTA);

CREATE INDEX IDX_PMP_PROCESSOSMARCAREVISTA19
	ON PMP_PROCESSOSMARCAREVISTA(UFTITULAR, DATAPUBLICACAOREVISTA);
	
CREATE INDEX IDX_PMP_PROCESSOSMARCAREVISTA20
	ON PMP_PROCESSOSMARCAREVISTA(NUMERODAREVISTA, PAISTITULAR, DATAPUBLICACAOREVISTA);

CREATE INDEX IDX_PMP_PROCESSOSMARCAREVISTA21
	ON PMP_PROCESSOSMARCAREVISTA(PAISTITULAR, DATAPUBLICACAOREVISTA);

CREATE INDEX IDX_PMP_PROCESSOSMARCAREVISTA22
	ON PMP_PROCESSOSMARCAREVISTA(NUMERODAREVISTA, PROCURADOR, DATAPUBLICACAOREVISTA);

CREATE INDEX IDX_PMP_PROCESSOSMARCAREVISTA23
	ON PMP_PROCESSOSMARCAREVISTA(PROCURADOR, DATAPUBLICACAOREVISTA);
