using Core;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers;

[ApiController]
[ResponseCache(NoStore = true, Location = ResponseCacheLocation.None)]
[Route("/Secure/[controller]/[action]")]
public class SecureApiController : ControllerBase
{
	public async Task<string> GetLoggedInFnr()
	{
		return User.FindFirst(AppClaims.Fnr)?.Value ?? "";
	}
}
