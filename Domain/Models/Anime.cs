namespace Hachiko.Domain.Models;

public class Anime
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public List<string> Genres { get; set; } = new();
    public int Episodes { get; set; }
    public DateTime? ReleaseDate { get; set; }
    public double? Rating { get; set; }
}