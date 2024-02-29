namespace StoreCone.Api.Configuration;

public class DatabaseSettings
{
    public string ConnectionString {get;set;} = string.Empty;
    public string DatabaseName {get;set;} = string.Empty;
    public string CollectionName {get;set;} = string.Empty;
}