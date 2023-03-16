create procedure DeleteFromLoginPageStats @Id int
as
	delete from LoginPageStats where Id = @Id