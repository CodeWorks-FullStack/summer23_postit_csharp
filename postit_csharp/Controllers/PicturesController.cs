
namespace postit_csharp.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PicturesController : ControllerBase
{
  private readonly PicturesService _picturesService;
  private readonly Auth0Provider _auth0Provider;

  public PicturesController(PicturesService picturesService, Auth0Provider auth0Provider)
  {
    _picturesService = picturesService;
    _auth0Provider = auth0Provider;
  }

  [Authorize]
  [HttpPost]
  public async Task<ActionResult<Picture>> CreatePicture([FromBody] Picture pictureData)
  {
    try
    {
      Account userInfo = await _auth0Provider.GetUserInfoAsync<Account>(HttpContext);
      pictureData.CreatorId = userInfo.Id;
      Picture picture = _picturesService.CreatePicture(pictureData);
      return Ok(picture);
    }
    catch (Exception e)
    {
      return BadRequest(e.Message);
    }
  }

  [Authorize]
  [HttpDelete("{pictureId}")]
  public async Task<ActionResult<string>> RemovePicture(int pictureId)
  {
    try
    {
      Account userInfo = await _auth0Provider.GetUserInfoAsync<Account>(HttpContext);
      _picturesService.RemovePicture(pictureId, userInfo.Id);
      return Ok("Picture was deleted");
    }
    catch (Exception e)
    {
      return BadRequest(e.Message);
    }
  }
}
