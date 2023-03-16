create procedure DeleteFromLoginUserStats @Id int
as
	delete from LoginUserStats where Id = @Id