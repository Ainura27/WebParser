using WebParser.Repository;

namespace WebParser.Extensions
{
    public static class DatabaseExtension
    {
        public static IHost MigrateDatabase(this IHost host)
        {
            using var scope = host.Services.CreateScope();
            var databaseService = scope.ServiceProvider.GetRequiredService<InitializeDatabase>();
            databaseService.CreateDatabase("postdb");
            databaseService.CreateTable("posts", GetQuery("createPostTable"));
            databaseService.CreateProcedure("PostInsert", GetQuery("insertPost"));
            databaseService.CreateProcedure("GetPostByDate", GetQuery("createGetPostByDate"));
            databaseService.CreateProcedure("GetPostsByText", GetQuery("createGetPostsByText"));
            databaseService.CreateProcedure("GetTopTen", GetQuery("createGetTopTen"));
            return host;
        }

        private static string GetQuery(string filename)
        {
            var filePath = @$"{Directory.GetCurrentDirectory()}\Static\DatabaseScripts\{filename}.sql";
            return File.ReadAllText(filePath);
        }

    }
}
