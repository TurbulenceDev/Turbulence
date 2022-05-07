using System.Runtime.Serialization;

namespace Turbulence.ModelGenerator;

[DataContract]
public class TableSource
{
    [DataMember]
    public string DiscordUrl;
    
    [DataMember]
    public string GithubUrl;
    
    [DataMember]
    public string Table;

    public TableSource(string discordUrl, string githubUrl, string table)
    {
        DiscordUrl = discordUrl;
        GithubUrl = githubUrl;
        Table = table;
    }
}