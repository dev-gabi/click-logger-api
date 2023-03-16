create procedure SelectLoginUserStatsById @Id int
as
select * from LoginUserStats where Id = @Id