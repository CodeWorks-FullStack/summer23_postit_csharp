namespace postit_csharp.Controllers;

[ApiController]
[Route("[controller]")]
public class AccountController : ControllerBase
{
  private readonly AccountService _accountService;
  private readonly Auth0Provider _auth0Provider;
  private readonly CollaboratorsService _collaboratorsService;

  public AccountController(AccountService accountService, Auth0Provider auth0Provider, CollaboratorsService collaboratorsService)
  {
    _accountService = accountService;
    _auth0Provider = auth0Provider;
    _collaboratorsService = collaboratorsService;
  }

  [Authorize]
  [HttpGet]
  public async Task<ActionResult<Account>> Get()
  {
    try
    {
      Account userInfo = await _auth0Provider.GetUserInfoAsync<Account>(HttpContext);
      return Ok(_accountService.GetOrCreateProfile(userInfo));
    }
    catch (Exception e)
    {
      return BadRequest(e.Message);
    }
  }

  [Authorize]
  [HttpGet("collaborators")]
  public async Task<ActionResult<List<AlbumCollaboration>>> GetMyAlbumCollaborations()
  {
    try
    {
      Account userInfo = await _auth0Provider.GetUserInfoAsync<Account>(HttpContext);
      List<AlbumCollaboration> collaborators = _collaboratorsService.GetMyAlbumCollaborations(userInfo.Id);
      return Ok(collaborators);
    }
    catch (Exception e)
    {
      return BadRequest(e.Message);
    }
  }
}
