CREATE PROCEDURE [dbo].[GetPostByDate]
@from nvarchar(25),
@to nvarchar(25)
AS
BEGIN
	select p.Title, p.Text, p.Date from posts p 
	where p.Date between @from and @to;
END