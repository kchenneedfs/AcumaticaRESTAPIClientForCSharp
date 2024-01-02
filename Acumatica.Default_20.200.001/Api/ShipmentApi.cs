using Acumatica.RESTClient.Client;
using Acumatica.Default_20_200_001.Model;

namespace Acumatica.Default_20_200_001.Api
{
	public class ShipmentApi : BaseEndpointApi<Shipment>
	{
		public ShipmentApi(ApiClient client) : base(client)
		{ }
	}
}