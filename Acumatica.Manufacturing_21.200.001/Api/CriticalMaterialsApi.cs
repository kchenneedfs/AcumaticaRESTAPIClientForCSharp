using Acumatica.RESTClient.Client;
using Acumatica.Manufacturing_21_200_001.Model;

namespace Acumatica.Manufacturing_21_200_001.Api
{
	public class CriticalMaterialsApi : BaseEndpointApi<CriticalMaterials>
	{
		public CriticalMaterialsApi(ApiClient client) : base(client)
		{ }
	}
}