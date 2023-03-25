--CREATE procedure [SelectLoginUserStatsByUserName] @UserName nvarchar(15)
--as
--select u.UserName, ls.Id, ls.LoginTime, ls.LogoutTime, ls.SessionInMinutes, ls.UserId
--from Users u join LoginUserStats ls
--on u.Id = ls.UserId
--where u.UserName = @UserName

CREATE procedure [SelectLoginUserStatsByUserName] @UserName nvarchar(15)
as
begin with StatsWithUserName as (
		select u.UserName, ls.Id, ls.LoginTime, ls.LogoutTime, ls.SessionInMinutes, ls.UserId
		from Users u join LoginUserStats ls
		on u.Id = ls.UserId
)

select * from StatsWithUserName
where UserName = @UserName

end