namespace postit_csharp.Models;

public class Account : Profile
{
  public string Email { get; set; }

  // public string Id { get; set; }
  // public DateTime CreatedAt { get; set; }
  // public DateTime UpdatedAt { get; set; }
  // public string Name { get; set; }
  // public string Picture { get; set; }
}

public class Profile
{
  public string Id { get; set; }
  public DateTime CreatedAt { get; set; }
  public DateTime UpdatedAt { get; set; }
  public string Name { get; set; }
  public string Picture { get; set; }

}