using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace postit_csharp.Services;

public class CollaboratorsService
{
  private readonly CollaboratorsRepository _collaboratorsRepository;

  public CollaboratorsService(CollaboratorsRepository collaboratorsRepository)
  {
    _collaboratorsRepository = collaboratorsRepository;
  }

  internal Collaborator CreateCollaborator(Collaborator collaboratorData)
  {
    Collaborator collaborator = _collaboratorsRepository.CreateCollaborator(collaboratorData);
    return collaborator;
  }

  internal List<ProfileCollaboration> GetCollaboratorsByAlbumId(int albumId)
  {
    List<ProfileCollaboration> collaborators = _collaboratorsRepository.GetCollaboratorsByAlbumId(albumId);
    return collaborators;
  }

  internal List<AlbumCollaboration> GetMyAlbumCollaborations(string userId)
  {
    List<AlbumCollaboration> collaborators = _collaboratorsRepository.GetMyAlbumCollaborations(userId);

    return collaborators;
  }

  internal Collaborator GetCollaboratorById(int collaboratorId)
  {
    Collaborator collaborator = _collaboratorsRepository.GetCollaboratorById(collaboratorId);

    if (collaborator == null)
    {
      throw new Exception($"BAD ID: {collaboratorId}");
    }

    return collaborator;

  }

  internal void RemoveCollaborator(int collaboratorId, string userId)
  {
    Collaborator collaborator = GetCollaboratorById(collaboratorId);

    if (collaborator.AccountId != userId)
    {
      throw new Exception("NOT YOUR DATA, BUD");
    }

    _collaboratorsRepository.RemoveCollaborator(collaboratorId);
  }
}
