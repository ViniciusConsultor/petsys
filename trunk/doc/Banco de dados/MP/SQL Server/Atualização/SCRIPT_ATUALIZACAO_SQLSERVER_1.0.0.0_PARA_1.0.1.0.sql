ALTER TABLE MP_DESPACHO_PATENTE ADD TEMPLATEEMAIL VARCHAR(4000);
ALTER TABLE MP_DESPACHO_MARCA ADD TEMPLATEEMAIL VARCHAR(4000);
DELETE FROM NCL_MENUFUNCAO WHERE IDFUNCAO = 'FUN.MP.008';
DELETE FROM NCL_AUTORIZACAO WHERE IDDIRETIVA = 'FUN.MP.008';
DELETE FROM NCL_AUTORIZACAO WHERE IDDIRETIVA = 'OPE.MP.008.0001';
DELETE FROM NCL_AUTORIZACAO WHERE IDDIRETIVA = 'OPE.MP.008.0002';
DELETE FROM NCL_AUTORIZACAO WHERE IDDIRETIVA = 'OPE.MP.008.0003';
DELETE FROM NCL_AUTORIZACAO WHERE IDDIRETIVA = 'OPE.MP.008.0004';
DELETE FROM NCL_ATALHO WHERE ID = 'FUN.MP.008';
DELETE FROM NCL_OPERACAO WHERE IDFUNCAO = 'FUN.MP.008';
DELETE FROM NCL_FUNCAO WHERE IDFUNCAO = 'FUN.MP.008';
DELETE FROM NCL_MENUFUNCAO WHERE IDFUNCAO = 'FUN.MP.006';
DELETE FROM NCL_AUTORIZACAO WHERE IDDIRETIVA = 'FUN.MP.006';
DELETE FROM NCL_AUTORIZACAO WHERE IDDIRETIVA = 'OPE.MP.006.0001';
DELETE FROM NCL_AUTORIZACAO WHERE IDDIRETIVA = 'OPE.MP.006.0002';
DELETE FROM NCL_AUTORIZACAO WHERE IDDIRETIVA = 'OPE.MP.006.0003';
DELETE FROM NCL_AUTORIZACAO WHERE IDDIRETIVA = 'OPE.MP.006.0004';
DELETE FROM NCL_ATALHO WHERE ID = 'FUN.MP.006';
DELETE FROM NCL_OPERACAO WHERE IDFUNCAO = 'FUN.MP.006';
DELETE FROM NCL_FUNCAO WHERE IDFUNCAO = 'FUN.MP.006';
