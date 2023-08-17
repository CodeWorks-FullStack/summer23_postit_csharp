namespace postit_csharp.Models;

public class Collaborator
{
  public int Id { get; set; }
  public DateTime CreatedAt { get; set; }
  public DateTime UpdatedAt { get; set; }
  public int AlbumId { get; set; }
  public string AccountId { get; set; }
}

public class ProfileCollaboration : Profile
{
  public int CollaborationId { get; set; }

  // NOTE all of this is now brought in through inheritance
  // public string Id { get; set; }
  // public DateTime CreatedAt { get; set; }
  // public DateTime UpdatedAt { get; set; }
  // public string Name { get; set; }
  // public string Picture { get; set; }
}

public class AlbumCollaboration : Album
{
  public int CollaborationId { get; set; }

}