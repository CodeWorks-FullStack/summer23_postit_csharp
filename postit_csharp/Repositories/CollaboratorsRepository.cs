
namespace postit_csharp.Repositories;

public class CollaboratorsRepository
{
  private readonly IDbConnection _db;

  public CollaboratorsRepository(IDbConnection db)
  {
    _db = db;
  }

  internal Collaborator CreateCollaborator(Collaborator collaboratorData)
  {
    string sql = @"
      INSERT INTO collaborators (albumId, accountId)
      VALUES (@AlbumId, @AccountId);
      SELECT LAST_INSERT_ID()
      ;";

    int collaboratorId = _db.ExecuteScalar<int>(sql, collaboratorData);

    collaboratorData.Id = collaboratorId;

    // NOTE not necessary but you can do this if it bugs you
    collaboratorData.CreatedAt = DateTime.Now;
    collaboratorData.UpdatedAt = DateTime.Now;

    return collaboratorData;
  }

  internal Collaborator GetCollaboratorById(int collaboratorId)
  {
    string sql = "SELECT * FROM collaborators WHERE id = @collaboratorId;";

    Collaborator collaborator = _db.QueryFirstOrDefault<Collaborator>(sql, new { collaboratorId });
    return collaborator;
  }

  internal List<ProfileCollaboration> GetCollaboratorsByAlbumId(int albumId)
  {
    string sql = @"
      SELECT
      collab.*,
      acc.*
      FROM collaborators collab
      JOIN accounts acc ON acc.id = collab.accountId
      WHERE collab.albumId = @albumId
      ;";

    List<ProfileCollaboration> collaborators = _db.Query<Collaborator, ProfileCollaboration, ProfileCollaboration>(
      sql,
      (collaborator, profile) =>
      {
        profile.CollaborationId = collaborator.Id;
        return profile;
      },
      new { albumId }).ToList();
    return collaborators;
  }

  internal List<AlbumCollaboration> GetMyAlbumCollaborations(string userId)
  {
    string sql = @"
      SELECT
      collab.*,
      alb.*,
      acc.*
      FROM collaborators collab
      JOIN albums alb ON alb.id = collab.albumId
      JOIN accounts acc ON acc.id = alb.creatorId
      WHERE collab.accountId = @userId
      ;";

    List<AlbumCollaboration> collaborators = _db.Query<Collaborator, AlbumCollaboration, Profile, AlbumCollaboration>(
      sql,
      (collaborator, album, profile) =>
      {
        album.CollaborationId = collaborator.Id;
        album.Creator = profile;
        return album;
      },
      new { userId }
    ).ToList();

    return collaborators;
  }

  internal void RemoveCollaborator(int collaboratorId)
  {
    string sql = "DELETE FROM collaborators WHERE id = @collaboratorId LIMIT 1";

    int rowsAffected = _db.Execute(sql, new { collaboratorId });

    if (rowsAffected > 1)
    {
      throw new Exception("CALL THE POLICE AND FBI");
    }

  }
}
