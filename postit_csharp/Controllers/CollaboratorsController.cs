
namespace postit_csharp.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CollaboratorsController : ControllerBase
{
  private readonly CollaboratorsService _collaboratorsService;
  private readonly Auth0Provider _auth0Provider;

  public CollaboratorsController(CollaboratorsService collaboratorsService, Auth0Provider auth0Provider)
  {
    _collaboratorsService = collaboratorsService;
    _auth0Provider = auth0Provider;
  }

  [Authorize]
  [HttpPost]
  public async Task<ActionResult<Collaborator>> CreateCollaborator([FromBody] Collaborator collaboratorData)
  {
    try
    {
      Account userInfo = await _auth0Provider.GetUserInfoAsync<Account>(HttpContext);
      collaboratorData.AccountId = userInfo.Id;
      Collaborator collaborator = _collaboratorsService.CreateCollaborator(collaboratorData);
      return Ok(collaborator);
    }
    catch (Exception e)
    {
      return BadRequest(e.Message);
    }
  }

  [Authorize]
  [HttpDelete("{collaboratorId}")]
  public async Task<ActionResult<string>> RemoveCollaborator(int collaboratorId)
  {
    try
    {
      Account userInfo = await _auth0Provider.GetUserInfoAsync<Account>(HttpContext);
      _collaboratorsService.RemoveCollaborator(collaboratorId, userInfo.Id);
      return Ok("Collaboration has ended");
    }
    catch (Exception e)
    {
      return BadRequest(e.Message);
    }
  }
}
