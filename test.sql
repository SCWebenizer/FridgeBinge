DELETE FROM dbo.MortiiMatii;
DBCC CHECKIDENT ('MortiiMatii', RESEED, 0)

insert into dbo.MortiiMatii (Username, Password) values ('1','1');
insert into dbo.MortiiMatii (Username, Password) values ('2','2');
insert into dbo.MortiiMatii (Username, Password) values ('3','3');
insert into dbo.MortiiMatii (Username, Password) values ('4','4');
insert into dbo.MortiiMatii (Username, Password) values ('5','5');

select * from dbo.MortiiMatii;
