CREATE PROCEDURE [dbo].[PostInsert]
@Title nvarchar(100),
@Text nvarchar(2500),
@Date datetime,
@Id int OUTPUT
AS
BEGIN
  INSERT INTO posts(Title,Text, Date) VALUES(@Title,@Text,@Date)
  SET @Id=@@IDENTITY
END