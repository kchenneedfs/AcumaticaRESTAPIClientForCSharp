using Acumatica.RESTClient.Client;
using Acumatica.eCommerce_22_200_001.Model;

namespace Acumatica.eCommerce_22_200_001.Api
{
	public class ProjectTaskApi : BaseEndpointApi<ProjectTask>
	{
		public ProjectTaskApi(ApiClient client) : base(client)
		{ }
	}
}