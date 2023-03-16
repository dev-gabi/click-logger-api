CREATE procedure InsertLoginUserStats @UserId int, @LoginTime DATETIME
as
set nocount on;

declare @id int;

INSERT INTO LoginUserStats  (UserId, LoginTime)
VALUES ( @UserId, @LoginTime);

		
		set @Id =   SCOPE_IDENTITY()
		select * from LoginUserStats where Id = @id
		