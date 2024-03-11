using Core;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers
{
	[ApiController]
	[ResponseCache(NoStore = true, Location = ResponseCacheLocation.None)]
	[Route("/Secure/[controller]/[action]")]
	[AllowAnonymous]
	public class ApiController : ControllerBase
	{
	}
}
