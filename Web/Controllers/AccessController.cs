using Core;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using oledid.SyntaxImprovement;
using System.Security.Claims;

namespace Web.Controllers;

[ResponseCache(NoStore = true, Location = ResponseCacheLocation.None)]
public class AccessController : ControllerBase
{
	/// <summary>
	/// Logs the user out of the application and reroutes to returnUrl
	/// </summary>
	/// <param name="returnUrl">
	/// Url to login page
	/// </param>
	/// <returns>
	/// Redirect to Url
	/// </returns>
	[AllowAnonymous]
	public async Task<IActionResult> Logout(string? returnUrl = null)
	{
		var scheme = User.FindFirst(AppClaims.AuthenticationType)?.Value;
		if (scheme is AppAuthenticationSchemes.IdPorten)
		{
			await HttpContext.SignOutAsync(scheme, new AuthenticationProperties
			{
				RedirectUri = Url.Content("~/") + "?ReturnUrl=" + returnUrl
			});
			await HttpContext.SignOutAsync(AppAuthenticationSchemes.AppCookies);
			return Content("");
		}

		await HttpContext.SignOutAsync(AppAuthenticationSchemes.AppCookies);
		return Redirect("~/");
	}

	/// <summary>
	/// Logs the user into the aplication
	/// </summary>
	/// <returns>
	/// Redirect to secure main page
	/// </returns>
	[AllowAnonymous]
	public async Task<IActionResult> Login()
	{
		if (User.Identity is ClaimsIdentity claimsIdentity && claimsIdentity.IsAuthenticated && claimsIdentity.HasClaim(c => c.Type == AppClaims.Fnr && c.Value.HasValue(isWhitespaceValue: false)))
		{
			return Redirect("~/Secure/");
		}

		return await ReAuthenticateAsync();
	}

	private async Task<IActionResult> ReAuthenticateAsync()
	{
		await HttpContext.ChallengeAsync(AppAuthenticationSchemes.IdPorten, new AuthenticationProperties
		{
			IsPersistent = false,
			ExpiresUtc = DateTimeOffset.UtcNow.AddHours(12)
		});

		return Content("Authentication needed.", "text/plain");
	}
}
