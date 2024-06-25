namespace NexaLibery_Backend.API.Shared.Utils;

public static class FormatConnectionStringFromEnv {
    public static string FromEnvToConnectionString(string connectionString)
    {
        string? host = DotNetEnv.Env.GetString("MYSQL_HOST") ?? Environment.GetEnvironmentVariable("MYSQL_HOST");
        if(host == null) throw new Exception("No MYSQL_HOST found");
        string? user = DotNetEnv.Env.GetString("MYSQL_USER") ?? Environment.GetEnvironmentVariable("MYSQL_USER");
        if(user == null) throw new Exception("No MYSQL_USER found");
        string? password = DotNetEnv.Env.GetString("MYSQL_PASSWORD") ?? Environment.GetEnvironmentVariable("MYSQL_PASSWORD");
        if(password == null) throw new Exception("No MYSQL_PASSWORD found");
        string? database = DotNetEnv.Env.GetString("MYSQL_DATABASE") ?? Environment.GetEnvironmentVariable("MYSQL_DATABASE");
        if(database == null) throw new Exception("No MYSQL_DATABASE found");
        string? port = DotNetEnv.Env.GetString("MYSQL_PORT") ?? Environment.GetEnvironmentVariable("MYSQL_PORT");
        if(port == null) throw new Exception("No MYSQL_PORT found");

        return String.Format(connectionString, host, user, password, database, port);
    }
}
