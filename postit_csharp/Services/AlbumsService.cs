
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

    // NOTE validating creator
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

    if (album == null)
    {
      throw new Exception("Your request was <i>exceptional</i>");
    }

    return album;
  }

  internal List<Album> GetAlbums()
  {
    List<Album> albums = _albumsRepository.GetAlbums();
    return albums;
  }
}
