create view SelectUserStatsWithUserName 
as

select u.UserName, us.Id, us.LoginTime, us.LogoutTime, us.SessionInMinutes, us.UserId
from LoginUserStats us join Users u
on us.UserId = u.Id