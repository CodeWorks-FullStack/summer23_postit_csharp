using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace postit_csharp.Repositories;

public class PicturesRepository
{
  private readonly IDbConnection _db;

  public PicturesRepository(IDbConnection db)
  {
    _db = db;
  }

  internal int CreatePicture(Picture pictureData)
  {
    string sql = @"
    INSERT INTO pictures (imgUrl, albumId, creatorId)
    VALUES (@ImgUrl, @AlbumId, @CreatorId);
    SELECT LAST_INSERT_ID()
    ;";

    int pictureId = _db.ExecuteScalar<int>(sql, pictureData);
    // pictureData.Id = pictureId;
    return pictureId;
  }

  internal Picture GetPictureById(int pictureId)
  {
    string sql = @"
        SELECT
        pic.*,
        acc.*
        FROM pictures pic
        JOIN accounts acc ON acc.id = pic.creatorId
        WHERE pic.id = @pictureId
        ;";

    Picture picture = _db.Query<Picture, Profile, Picture>(
      sql,
      (picture, profile) =>
      {
        picture.Creator = profile;
        return picture;
      },
      new { pictureId }
    ).FirstOrDefault();

    return picture;
  }
}
