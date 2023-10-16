CREATE PROCEDURE [dbo].[GetPostsByText]
@searchText nvarchar(200)
AS
BEGIN
select p.Title, p.Text, p.Date from posts p 
where UPPER(p.Text) like '%'+ UPPER(@searchText) +'%';
END