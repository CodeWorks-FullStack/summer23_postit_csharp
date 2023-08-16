namespace postit_csharp.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AlbumsController : ControllerBase
{
  private readonly AlbumsService _albumsService;
  private readonly PicturesService _picturesService;
  private readonly Auth0Provider _auth0Provider;

  public AlbumsController(AlbumsService albumsService, Auth0Provider auth0Provider, PicturesService picturesService)
  {
    _albumsService = albumsService;
    _auth0Provider = auth0Provider;
    _picturesService = picturesService;
  }

  [Authorize]
  [HttpPost]
  public async Task<ActionResult<Album>> CreateAlbum([FromBody] Album albumData)
  {
    try
    {
      Account userInfo = await _auth0Provider.GetUserInfoAsync<Account>(HttpContext);
      // req.body.creatorId = req.userInfo.id
      albumData.CreatorId = userInfo.Id;
      Album album = _albumsService.CreateAlbum(albumData);
      return Ok(album);
    }
    catch (Exception e)
    {
      return BadRequest(e.Message);
    }
  }

  [HttpGet]
  public ActionResult<List<Album>> GetAlbums()
  {
    try
    {
      List<Album> albums = _albumsService.GetAlbums();
      return Ok(albums);
    }
    catch (Exception e)
    {
      return BadRequest(e.Message);
    }
  }

  [HttpGet("{albumId}")]
  public ActionResult<Album> GetAlbumById(int albumId)
  {
    try
    {
      Album album = _albumsService.GetAlbumById(albumId);
      return Ok(album);

    }
    catch (Exception e)
    {
      return BadRequest(e.Message);
    }
  }

  [Authorize]
  [HttpDelete("{albumId}")]
  public async Task<ActionResult<string>> ArchiveAlbum(int albumId)
  {
    try
    {
      Account userInfo = await _auth0Provider.GetUserInfoAsync<Account>(HttpContext);
      Album album = _albumsService.ArchiveAlbum(albumId, userInfo.Id);
      return Ok($"{album.Title} has been archived");
    }
    catch (Exception e)
    {
      return BadRequest(e.Message);
    }
  }

  [HttpGet("{albumId}/pictures")]
  public ActionResult<List<Picture>> GetPicturesByAlbumId(int albumId)
  {
    try
    {
      List<Picture> pictures = _picturesService.GetPicturesByAlbumId(albumId);
      return Ok(pictures);
    }
    catch (Exception e)
    {
      return BadRequest(e.Message);
    }
  }


}
