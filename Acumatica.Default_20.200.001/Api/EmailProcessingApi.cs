using Acumatica.RESTClient.Client;
using Acumatica.Default_20_200_001.Model;

namespace Acumatica.Default_20_200_001.Api
{
	public class EmailProcessingApi : BaseEndpointApi<EmailProcessing>
	{
		public EmailProcessingApi(ApiClient client) : base(client)
		{ }
	}
}