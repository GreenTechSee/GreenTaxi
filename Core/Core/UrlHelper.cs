using Microsoft.Extensions.Configuration;
using oledid.SyntaxImprovement;

namespace Core
{
	public class UrlHelper
	{
		private readonly IConfiguration configuration;

		public UrlHelper(IConfiguration configuration)
		{
			this.configuration = configuration;
		}

		public string ResolveUrl(string tildeUrl) => configuration["BaseUrl"].RemoveFromEnd("/") + tildeUrl.RemoveFromStart("~");
	}
}
