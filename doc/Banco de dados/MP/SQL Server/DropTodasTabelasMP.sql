DECLARE @tableName varchar(250), 
                  @constraintName nvarchar(100)               

DECLARE tables_csr CURSOR FOR  
  select name  
     from SYSOBJECTS 
       where TYPE = 'U' and NAME like 'MP_%'  

Open tables_csr 
FETCH NEXT FROM tables_csr into @tableName 
WHILE @@fetch_status = 0 Begin    
 
     DECLARE  ponteiro CURSOR FOR 
	  SELECT name 
	    FROM sysobjects so JOIN sysconstraints sc  ON so.id = sc.constid 
                      WHERE object_name(so.parent_obj) = @tableName  AND 
                         so.xtype = 'F' 

    OPEN ponteiro 
    FETCH NEXT FROM ponteiro INTO @constraintName 
    WHILE @@FETCH_STATUS = 0    BEGIN 
       EXEC (' alter table ' + @tableName + '  drop constraint  ' + @constraintName) 
       FETCH NEXT FROM ponteiro INTO @constraintName 
    END 
    CLOSE ponteiro 
    DEALLOCATE ponteiro 
    FETCH NEXT FROM tables_csr into @tableName  
END 

CLOSE tables_csr 
Open tables_csr 
FETCH NEXT FROM tables_csr into @tableName 
   WHILE @@fetch_status = 0 Begin 
        EXEC ('drop table  ' + @tableName ) 
   FETCH NEXT FROM tables_csr into @tableName  
END 


CLOSE tables_csr  
DEALLOCATE tables_csr; 