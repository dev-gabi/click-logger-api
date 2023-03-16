create procedure InsertLoginPagestats @LoginUserStatsId int, @ButtonType nvarchar(13), @ClickedAfterInseconds int
as
insert into LoginPageStats (LoginUserStatsId, ButtonType, ClickedAfterInseconds)
					values( @LoginUserStatsId, @ButtonType, @ClickedAfterInseconds)