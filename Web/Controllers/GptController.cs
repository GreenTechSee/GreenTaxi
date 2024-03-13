using Azure;
using Microsoft.AspNetCore.Mvc;
using Azure.AI.OpenAI;

namespace Web.Controllers
{
	public class GptEntity
	{
		public string? Text { get; set; }
	}

	public class GptController : ControllerBase
	{
		private readonly IConfiguration configuration;

		public GptController(IConfiguration configuration)
		{
			this.configuration = configuration;
		}

		public string GetOrThrow(string key)
		{
			return configuration[key] ?? throw new Exception($"Missing configuration: {key}");
		}

		[HttpPost("GenerateResult")]
		public async Task<string> GenerateResult([FromBody] GptEntity input)
		{
			string openAiKey = GetOrThrow("Secrets:OpenAI:Key");
			string Url = GetOrThrow("Secrets:OpenAI:Endpoint");

			OpenAIClient client = new OpenAIClient(new Uri(Url), new AzureKeyCredential(openAiKey));

			var chatCompletionsOptions = new ChatCompletionsOptions()
			{
				DeploymentName = "gpt-35-turbo-16k",
				Messages =
				{
					new ChatRequestSystemMessage("Du er en digital sykepleier, som prøver så godt du kan å svare på spørsmål på en positiv, men effektiv måte. Du svarer alltid på norsk."),
					new ChatRequestUserMessage(input.Text)
				},
				//MaxTokens = 29999
			};

			var response = await client.GetChatCompletionsAsync(chatCompletionsOptions);
			return response.Value.Choices.First().Message.Content;
		}
	}
}
