namespace postit_csharp.Models;

public class Picture
{
  public int Id { get; set; }
  public DateTime CreatedAt { get; set; }
  public DateTime UpdatedAt { get; set; }
  public string ImgUrl { get; set; }
  public int AlbumId { get; set; }
  public string CreatorId { get; set; }
  public Profile Creator { get; set; }
}