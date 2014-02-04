﻿CREATE TABLE MP_BOLETOS_GERADOS(
ID	BIGINT NOT NULL,
NUMEROBOLETO BIGINT NOT NULL,
NOSSONUMERO BIGINT NOT NULL,
IDCLIENTE BIGINT NOT NULL,
VALOR FLOAT NOT NULL,
DATAGERACAO VARCHAR(20) NOT NULL,
DATAVENCIMENTO VARCHAR(20) NOT NULL,
NUMEROPROCESSO BIGINT NULL,
OBSERVACAO VARCHAR(4000) NULL);

CREATE TABLE MP_BOLETOS_GERADOS_AUX(
ID	BIGINT NOT NULL,
PROXNOSSONUMERO BIGINT NOT NULL,
PROXNUMEROBOLETO BIGINT NOT NULL);

ALTER TABLE MP_PROCESSOPATENTE ADD PAIS BIGINT NULL;

ALTER TABLE MP_PROCESSOPATENTE ADD CONSTRAINT FK_MP_PROCESSOPATENTE3 FOREIGN KEY (PAIS) REFERENCES NCL_PAIS(ID);