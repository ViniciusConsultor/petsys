﻿CREATE TABLE TIPO_PATENTE(
	IDTIPO_PATENTE bigint NOT NULL,
	DESCRICAO_TIPO_PATENTE varchar(255) NULL,
	SIGLA_TIPO char(2) NULL,
	TEMPO_INICIO_ANOS int NULL,
	QUANTIDADE_PAGTO int NULL,
	TEMPO_ENTRE_PAGTO int NULL,
	SEQUENCIA_INICIO_PAGTO int NULL,
	TEM_PAGTO_INTERMEDIARIO char(1) NOT NULL,
	INICIO_INTERMED_SEQUENCIA int NULL,
	QUANTIDADE_PAGTO_INTERMED int NULL,
	TEMPO_ENTRE_PAGTO_INTERMED int NULL,
	DESCRICAO_PAGTO varchar(255) NULL,
	DESCRICAO_PAGTO_INTERMED varchar(255) NULL,
	TEM_PED_EXAME char(1) NOT NULL,
    CONSTRAINT PK_TIPO_PATENTE PRIMARY KEY (IDTIPO_PATENTE))