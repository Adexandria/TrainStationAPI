using RestSharp;
using RestSharp.Authenticators;
using TrainStationAPI.Model;

namespace TrainStationAPI.Services
{
    public class EmailService
    {
		private IConfiguration config;
		public EmailService(IConfiguration config)
		{
			this.config = config;
		}
		public async Task<bool> SendSimpleMessage(Mail newMail)
		{
			try
			{
				RestClient client = new RestClient("https://api.mailgun.net/v3")
				{
					Authenticator = new HttpBasicAuthenticator("api", config["APIKey"])
				};
				RestRequest request = new RestRequest();
				request.AddParameter("domain", "paprika.software", ParameterType.UrlSegment);
				request.Resource = "paprika.software/messages";
				request.AddParameter("from", "Automated Online BookStore System <mailgun@paprika.software>");
				request.AddParameter("to", newMail.To);
				request.AddParameter("subject", newMail.Subject);
				request.AddParameter("text", newMail.Text);
				request.Method = Method.Post;
				var response = await client.ExecuteAsync(request);
				if (response.IsSuccessful)
				{
					return true;
				}
				return false;
			}
			catch (Exception e)
			{

				throw e;
			}

		}
	}
}
