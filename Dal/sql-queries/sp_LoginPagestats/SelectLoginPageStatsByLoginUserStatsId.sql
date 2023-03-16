create procedure SelectLoginPageStatsByLoginUserStatsId @LoginUserStatsId int
as
select * from LoginPageStats where  LoginUserStatsId = @LoginUserStatsId