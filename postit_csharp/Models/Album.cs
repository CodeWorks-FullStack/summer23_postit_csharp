

namespace postit_csharp.Models;

public class Album
{
  public int Id { get; set; }
  public DateTime CreatedAt { get; set; }
  public DateTime UpdatedAt { get; set; }
  public string Title { get; set; }
  public string Category { get; set; }
  public bool Archived { get; set; }
  public string CoverImg { get; set; }
  public string CreatorId { get; set; }

  public Profile Creator { get; set; }
}



