create table LoginUserStats (
	Id int identity not null primary key,
	UserId int not null,
	LoginTime DateTime not null,
	LogoutTime DateTime null,
	SessionInMinutes int null

	constraint FK_UserLoginUserStats foreign key (UserId) references Users(Id)
	)