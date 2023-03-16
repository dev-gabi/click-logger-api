create procedure UpdateLoginPageState @Id int, @ButtonType nvarchar(13), @ClickedAfterInseconds int
as
	update LoginPageStats set ButtonType = @ButtonType,
								ClickedAfterInseconds = @ClickedAfterInseconds
								where Id = @Id