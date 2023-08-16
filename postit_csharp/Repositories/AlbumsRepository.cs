using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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

    Album album = _db.Query<Album, Profile, Album>(
      sql,
      (album, profile) =>
      {
        album.Creator = profile;
        return album;
      },
      new { albumId }).FirstOrDefault();
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
