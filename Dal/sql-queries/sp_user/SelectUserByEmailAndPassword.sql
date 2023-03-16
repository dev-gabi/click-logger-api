create procedure SelectUserByEmailAndPassword @email nvarchar(100), @password nvarchar(100)
as
select * from Users where Email = @email and password = @password