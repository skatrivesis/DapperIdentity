--use identityexample1

create table [Tasks](
	Id int identity(1,1) not null PRIMARY KEY,
	UserId int not null FOREIGN KEY references IdentityUser(Id),
	TaskDescription varchar(30) not null, 
	DueDate datetime, 
	Completed int not null
);