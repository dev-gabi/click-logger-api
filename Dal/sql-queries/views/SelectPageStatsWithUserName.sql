create view SelectPageStatsWithUserName
as
select u.UserName, ps.Id, ps.ButtonType, ps.ClickedAfterInSeconds
from Users u join LoginUserStats us
on u.Id = us.UserId

join LoginPageStats ps 
on ps.LoginUserStatsId = us.Id

