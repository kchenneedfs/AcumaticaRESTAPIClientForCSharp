using System;
using Acumatica.RESTClient.Client;
using Acumatica.Manufacturing_24_200_001.Model;

namespace Acumatica.Manufacturing_24_200_001.Api
{
	[Obsolete("For backward compatibility")]
	public class EngineeringChangeOrderApi : BaseEndpointApi<EngineeringChangeOrder>
	{
		public EngineeringChangeOrderApi(ApiClient client) : base(client)
		{ }
	}
}