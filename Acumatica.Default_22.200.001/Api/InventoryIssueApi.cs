using Acumatica.RESTClient.Client;
using Acumatica.Default_22_200_001.Model;

namespace Acumatica.Default_22_200_001.Api
{
	public class InventoryIssueApi : BaseEndpointApi<InventoryIssue>
	{
		public InventoryIssueApi(ApiClient client) : base(client)
		{ }
	}
}