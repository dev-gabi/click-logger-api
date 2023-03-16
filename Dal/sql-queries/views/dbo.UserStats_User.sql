create view [UserStats_User]
as
select u.UserName, u.JobTitle, s.SessionInMinutes, s.LoginTime
from Users u left join LoginUserStats s 
on u.Id = s.UserId where s.SessionInMinutes < 5