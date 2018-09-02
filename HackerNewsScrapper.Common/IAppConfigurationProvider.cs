namespace HackerNewsScrapper.Common
{
    public interface IAppConfigurationProvider
    {
        string GetStringValue(string key);
        int GetIntValue(string key);
    }
}
