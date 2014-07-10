UPDATE MP_PROCESSOPATENTE
SET PROCESSO = 
	CASE LEN(PROCESSO)
		WHEN 0 THEN '00000000'
		WHEN 1 THEN '0000000' + PROCESSO
		WHEN 2 THEN '000000' + PROCESSO
		WHEN 3 THEN '00000' + PROCESSO
		WHEN 4 THEN '0000' + PROCESSO
		WHEN 5 THEN '000' + PROCESSO
		WHEN 6 THEN '00' + PROCESSO
		ELSE '0' + PROCESSO
	END
WHERE 
	PROCESSO IN(SELECT PROCESSO FROM MP_PROCESSOPATENTE WHERE LEN(PROCESSO) < 8);

alter table FN_ITEMFINANREC add NUMEROBOLETOGERADO varchar(20) null;

ALTER TABLE MP_REVISTA_PATENTE ALTER COLUMN TITULO VARCHAR(1000);
ALTER TABLE MP_REVISTA_PATENTE ALTER COLUMN DADOSPEDIDOPATENTE VARCHAR(1000);
ALTER TABLE MP_REVISTA_PATENTE ALTER COLUMN DADOSPATENTEORIGINAL VARCHAR(1000);
ALTER TABLE MP_REVISTA_PATENTE ALTER COLUMN PAISESDESIGNADOS VARCHAR(1000);
ALTER TABLE MP_REVISTA_PATENTE ALTER COLUMN DADOSDEPOSINTER VARCHAR(1000);
ALTER TABLE MP_REVISTA_PATENTE ALTER COLUMN DADOSPUBLICINTER VARCHAR(1000);
ALTER TABLE MP_REVISTA_PATENTE ALTER COLUMN DECISAO VARCHAR(1000);
ALTER TABLE MP_REVISTA_PATENTE ALTER COLUMN RECORRENTE VARCHAR(1000);
ALTER TABLE MP_REVISTA_PATENTE ALTER COLUMN CEDENTE VARCHAR(1000);
ALTER TABLE MP_REVISTA_PATENTE ALTER COLUMN CESSIONARIA VARCHAR(1000);
ALTER TABLE MP_REVISTA_PATENTE ALTER COLUMN ULTIMAINFORMACAO VARCHAR(1000);
ALTER TABLE MP_REVISTA_PATENTE ALTER COLUMN CERTIFAVERBACAO VARCHAR(1000);
ALTER TABLE MP_REVISTA_PATENTE ALTER COLUMN CAMPOAPLICACAO VARCHAR(1000);