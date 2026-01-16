using System.Text.Json.Serialization;

public class Address
{
    public string? Logradouro  {get; set;}
    public string? Bairro  { get; set; }
    public string? Localidade  { get; set; }
    public string? Uf  { get; set; }

    [JsonPropertyName("erro")]
    public string? Erro { get; set; }

}