
using System.ComponentModel.DataAnnotations.Schema;

namespace Amoeba.Models;

public class Client
{
    public int Id { get; set; }
    public string? Image { get; set; }
    [NotMapped]
    public IFormFile? ImageFile { get; set; } 
    public string Name { get; set; }
    public string Description { get; set; }
    public int ProfessionId { get; set; }
    public Profession? profession { get; set; }
} 
