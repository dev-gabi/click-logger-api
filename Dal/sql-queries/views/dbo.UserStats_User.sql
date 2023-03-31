create view [UserStats_User]
as
select u.UserName, u.JobTitle, s.SessionInMinutes, s.LoginTime, s.LogoutTime, s.UserId, s.Id
from Users u left join LoginUserStats s 
on u.Id = s.UserId where s.SessionInMinutes < 5