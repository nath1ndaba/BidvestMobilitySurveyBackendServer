namespace BidvestMobilitySurveyBackendServer;
public class MobilityConfig
{
    public static string DATABASE_NAME => GetEnv(nameof(DATABASE_NAME));
    public static string DATABASE_HOST => GetEnv(nameof(DATABASE_HOST));
    public static string DATABASE_AUTH_DB => GetEnv(nameof(DATABASE_AUTH_DB));
    public static string DATABASE_USERNAME => GetEnv(nameof(DATABASE_USERNAME));
    public static string DATABASE_USER_PASSWORD => GetEnv(nameof(DATABASE_USER_PASSWORD));


    public static string GetEnv(string key)
    {
        return Environment.GetEnvironmentVariable(key);
    }
}