namespace Amoeba.Models;

public class Profession
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public List<Client>? Clients { get; set; }
}
