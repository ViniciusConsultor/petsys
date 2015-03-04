﻿CREATE TABLE PMP_PROCESSOSMARCAREVISTA (
	NUMERODAREVISTA				INTEGER		NOT NULL,
	DATAPUBLICACAOREVISTA		INTEGER		NOT NULL,
	NUMEROPROCESSODEMARCA		BIGINT		NOT NULL,
	DATADODEPOSITO				INTEGER			NULL,
	DATADACONCESSAO				INTEGER			NULL,
	DATADAVIGENCIA				INTEGER			NULL,
	CODIGODODESPACHO			VARCHAR(255)	NULL,
	NOMEDODESPACHO				VARCHAR(1000)	NULL,
	TEXTOCOMPLEMENTARDESPACHO	VARCHAR(8000)	NULL,
	TITULAR						VARCHAR(500)	NULL,
	PAISTITULAR					CHAR(2)			NULL,
	UFTITULAR					CHAR(2)			NULL,
	MARCA						VARCHAR(1000)	NULL,
	APRESENTACAO				VARCHAR(50)		NULL,
	NATUREZA					VARCHAR(50)		NULL,
	EDICAOCLASSEVIENA			VARCHAR(50)		NULL,
	CODIGOCLASSEVIENA			VARCHAR(50)		NULL,
	CODIGOCLASSENACIONAL		VARCHAR(50)		NULL,
	CODIGOSUBCLASSENACIONAL		VARCHAR(50)		NULL,
	CODIGOCLASSENICE			VARCHAR(50)		NULL,
	ESPECIFICACAOCLASSENICE		VARCHAR(4000)	NULL,
	PROCURADOR					VARCHAR(500)	NULL,
	APOSTILA					VARCHAR(4000)	NULL
)

ALTER TABLE PMP_PROCESSOSMARCAREVISTA
	ADD CONSTRAINT PK_PMP_PROCESSOSMARCAREVISTA
	PRIMARY KEY (NUMERODAREVISTA, NUMEROPROCESSODEMARCA);	


