declare @currentDate datetime
set @currentDate = GetDate()
 
declare @fileName varchar(255)
set @fileName = 'C:\backup\MyDatabase'
    + cast(Year(@currentDate) as varchar(4))
    + Replicate('0', 2 - Len(cast(Month(@currentDate) as varchar(2))))
            + cast(Month(@currentDate) as varchar(2))
    + Replicate('0', 2 - Len(cast(Day(@currentDate) as varchar(2))))
            + cast(Day(@currentDate) as varchar(2))
    + '_' +
    + Replicate('0', 2 - Len(cast(DatePart(hour, @currentDate) as varchar(2))))
            + cast(DatePart(hour, @currentDate) as varchar(2))
    + Replicate('0', 2 - Len(cast(DatePart(minute, @currentDate) as varchar(2))))
            + cast(DatePart(minute, @currentDate) as varchar(2)) + '.bak'
 
backup database [MP] to disk = @fileName 