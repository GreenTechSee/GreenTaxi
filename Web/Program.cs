using Core;
using Core.Concepts.AppDatabase.Repositories;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.StaticFiles;
using Microsoft.IdentityModel.Tokens;
using oledid.SyntaxImprovement.Extensions;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

var services = builder.Services;
var configuration = builder.Configuration;

// Add services to the container.

services.AddCors();
services.AddControllers();
	//.AddRazorRuntimeCompilation();
services.AddRazorPages();
	//.AddRazorRuntimeCompilation();

services.AddOpenApiDocument(configure => configure.PostProcess = openApiDocument =>
{
	openApiDocument.Info.Title = "GreenTaxi";
});
Dependencies.AddDependencies(services);

services.AddAuthentication(sharedOptions =>
{
	sharedOptions.DefaultAuthenticateScheme = AppAuthenticationSchemes.AppCookies;
	sharedOptions.DefaultScheme = AppAuthenticationSchemes.AppCookies;
	sharedOptions.DefaultChallengeScheme = AppAuthenticationSchemes.AppCookies;
})
	.AddCookie(AppAuthenticationSchemes.AppCookies, options =>
	{
		options.AccessDeniedPath = "/AccessDenied/";
		options.LoginPath = "/Access/Login";
		options.LogoutPath = "/Access/Logout";
	})
	.AddOpenIdConnect(AppAuthenticationSchemes.IdPorten, options =>
	{
		options.MetadataAddress = configuration["Secrets:IdPorten:WellKnown"];
		options.ClientId = configuration["Secrets:IdPorten:ClientId"];
		options.ClientSecret = configuration["Secrets:IdPorten:ClientSecret"];
		options.CallbackPath = "/auth/idPorten/signin-oidc/";
		options.SignedOutCallbackPath = "/auth/idPorten/signout-oidc/";
		options.SignedOutRedirectUri = "/LoggedOut";
		options.RemoteSignOutPath = "/auth/idPorten/frontchannelLogout";
		options.ResponseType = "code";

		options.TokenValidationParameters = new TokenValidationParameters
		{
			ValidateIssuerSigningKey = true
		};

		options.Events.OnTokenValidated = async context =>
		{
			var claimsIdentity = context.Principal?.Identity as ClaimsIdentity;
			if (claimsIdentity == null)
			{
				throw new NullReferenceException(nameof(claimsIdentity));
			}

			var fodselsnummer = claimsIdentity.FindFirst(claim => claim.Type == AppClaims.Fnr)?.Value ?? string.Empty;
			if (fodselsnummer == string.Empty)
			{
				throw new ArgumentException("Invalid fnr: " + fodselsnummer);
			}

			var idportenLevel = claimsIdentity.FindFirst("http://schemas.microsoft.com/claims/authnclassreference")?.Value;
			if (idportenLevel != "idporten-loa-high")
			{
				context.Fail("Must authenticate with idporten-loa-high");
				return;
			}

			var personRepository = context.HttpContext.RequestServices.GetService<IPersonRepository>()!;
			await personRepository.UpsertPerson(fodselsnummer);

			using var sha256 = SHA256.Create();
			var output = Convert.ToBase64String(sha256.ComputeHash(Encoding.UTF8.GetBytes(fodselsnummer + "Jfna7arotieFspNLAZMKmc1JcQTfoZ8k/k7L8PncFng=")));
			if (output == configuration["Secrets:EasterEggHash"])
			{
				claimsIdentity.SetClaim(AppClaims.EasterEgg, true.ToString());
			}

			claimsIdentity.SetClaim(AppClaims.IdTokenHint, context.TokenEndpointResponse?.IdToken ?? throw new NullReferenceException($"{nameof(context)}.{nameof(context.TokenEndpointResponse)}.{nameof(context.TokenEndpointResponse.IdToken)}"));
			claimsIdentity.SetClaim(AppClaims.AuthenticationType, AppAuthenticationSchemes.IdPorten);
		};

		options.Events.OnRedirectToIdentityProvider = async context =>
		{
			context.ProtocolMessage.AcrValues = "idporten-loa-high";
		};

		options.Events.OnRedirectToIdentityProviderForSignOut = async context =>
		{
			var idTokenHint = (context.HttpContext.User.Identity as ClaimsIdentity)?.Claims.FirstOrDefault(c => c.Type == AppClaims.IdTokenHint)?.Value;
			if (idTokenHint != null)
			{
				context.ProtocolMessage.IdTokenHint = idTokenHint;
			}
		};

		options.Events.OnRemoteSignOut = async context =>
		{
			await context.HttpContext.SignOutAsync(AppAuthenticationSchemes.AppCookies);
		};

		options.Events.OnAuthorizationCodeReceived = async context =>
		{
			context.Backchannel.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", Convert.ToBase64String(Encoding.Default.GetBytes($"{context.TokenEndpointRequest!.ClientId}:{context.TokenEndpointRequest.ClientSecret}")));
		};
	});

services.AddAuthorization(options =>
{
	options.FallbackPolicy = new AuthorizationPolicyBuilder()
		.RequireAuthenticatedUser()
		.Build();
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
	app.UseOpenApi();
	app.UseSwaggerUi();
	app.UseReDoc();
}

app.UseHsts();
app.UseHttpsRedirection();

var contentTypeProvider = new FileExtensionContentTypeProvider();
contentTypeProvider.Mappings[".webmanifest"] = "application/manifest+json";
if (app.Environment.IsDevelopment() == false && contentTypeProvider.Mappings.ContainsKey(".map"))
{
	contentTypeProvider.Mappings.Remove(".map");
}

app.UseStaticFiles(new StaticFileOptions
{
	ContentTypeProvider = contentTypeProvider
});

app.UseRouting();
app.UseCors();
app.UseCookiePolicy();
app.UseAuthentication();
app.UseAuthorization();
app.UseStatusCodePages();

app.UseDefaultFiles();

app.UseEndpoints(endpoints =>
{
	endpoints.MapDefaultControllerRoute();
	endpoints.MapRazorPages();
});

app.UseMiniProfiler();

app.Run();
