
namespace postit_csharp.Repositories;

public class AlbumsRepository
{
  private readonly IDbConnection _db;

  public AlbumsRepository(IDbConnection db)
  {
    _db = db;
  }

  internal void ArchiveAlbum(int albumId)
  {
    // NOTE only doing this for soft delete
    string sql = "UPDATE albums SET archived = true WHERE id = @albumId;";

    _db.Execute(sql, new { albumId });
  }

  internal int CreateAlbum(Album albumData)
  {
    string sql = @"
    INSERT INTO albums (title, category, coverImg, creatorId)
    VALUES (@Title, @Category, @CoverImg, @CreatorId);
    SELECT LAST_INSERT_ID()
    ;";

    int albumId = _db.ExecuteScalar<int>(sql, albumData);

    return albumId;

  }

  internal Album GetAlbumById(int albumId)
  {
    string sql = @"
    SELECT 
    alb.*,
    acc.* 
    FROM albums alb
    JOIN accounts acc ON acc.id = alb.creatorId 
    WHERE alb.id = @albumId LIMIT 1
    ;";
    // NOTE query takes in 2 different types representing the tables returned from the sql statement, and a final return type. Order matters here and is dictated by the order in our select
    Album album = _db.Query<Album, Profile, Album>(
      sql,
      // NOTE our map function. The number of arguments it takes in corresponds to how many tables we are bringing in from the sql query.
      // NOTE we set the album creator to the profile we brought in from our sql query, similar to populate from mongoose
      // NOTE map function must return a value, it should match our return type
      (album, profile) =>
      {
        album.Creator = profile;
        return album;
      },
      new { albumId }).FirstOrDefault(); // NOTE we use FirstOrDefault here so our query eventually only returns one row
    return album;
  }

  internal List<Album> GetAlbums()
  {
    string sql = @"
    SELECT
    alb.*,
    acc.* 
    FROM albums alb
    JOIN accounts acc ON acc.id = alb.creatorId
    ;";

    List<Album> albums = _db.Query<Album, Profile, Album>(
      sql,
      (album, profile) =>
      {
        album.Creator = profile;
        return album;
      }
      ).ToList();
    return albums;
  }
}
