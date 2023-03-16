create table Users(
	Id int identity(1000, 1) not null PRIMARY KEY,
	UserName nvarchar(15) not null,
	Email nvarchar(100) not null unique,
	Password nvarchar(100) not null check (Len(Password) > 5),
	JobTitle nvarchar(100) NULL,
	PhoneNumber char(10) null,
	constraint chk_phone check (PhoneNumber not like '%[^0-9]%')
)