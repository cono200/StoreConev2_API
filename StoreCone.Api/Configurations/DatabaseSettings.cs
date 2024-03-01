namespace StoreCone.Api.Configuration;

public class DatabaseSettings
{
    public string ConnectionString {get;set;} = string.Empty;
    public string DatabaseName {get;set;} = string.Empty;
    public string CollectionName {get;set;} = string.Empty;
    public string ProdCollectionName { get;set;} = string.Empty;
    public string MermaCollectioName { get; set; } = string.Empty; //ESTO TIENE QUE SER IGUAL AL QUE PONEMOS EN EL JSON
    //EL CUAL SE TIENE QUE USAR EN EL CORRESPONDIENTE SERVICIO
    public string UsuarioCollectionName { get; set; } = string.Empty;  
}
