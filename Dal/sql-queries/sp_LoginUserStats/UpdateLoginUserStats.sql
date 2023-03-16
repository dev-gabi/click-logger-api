create procedure UpdateLoginUserStats @Id int, @LogoutTime DATETIME, @SessionInMinuts int
as 
update LoginUserStats 
	set LogoutTime = @LogoutTime,
		SessionInMinutes = @SessionInMinuts
		where Id = @Id

			select * from LoginUserStats where Id = @Id