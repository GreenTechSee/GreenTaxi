using Core;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers;

[ApiController]
[Authorize(AppPolicies.LoggedIn)]
[ResponseCache(NoStore = true, Location = ResponseCacheLocation.None)]
[Route("/Secure/[controller]/[action]")]
public class SecureApiController
{
}
