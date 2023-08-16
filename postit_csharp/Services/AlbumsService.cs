using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace postit_csharp.Services;

public class AlbumsService
{
  private readonly AlbumsRepository _albumsRepository;

  public AlbumsService(AlbumsRepository albumsRepository)
  {
    _albumsRepository = albumsRepository;
  }

  internal Album ArchiveAlbum(int albumId, string userId)
  {
    Album album = GetAlbumById(albumId);

    if (album.CreatorId != userId)
    {
      throw new Exception("NOT YOUR DATA TO DELETE");
    }

    _albumsRepository.ArchiveAlbum(albumId);

    return album;
  }

  internal Album CreateAlbum(Album albumData)
  {
    int albumId = _albumsRepository.CreateAlbum(albumData);

    Album album = GetAlbumById(albumId);

    return album;
  }

  internal Album GetAlbumById(int albumId)
  {
    Album album = _albumsRepository.GetAlbumById(albumId);


    return album;
  }

  internal List<Album> GetAlbums()
  {
    List<Album> albums = _albumsRepository.GetAlbums();
    return albums;
  }
}
