using System.Collections.Generic;
using System.Runtime.Serialization;

using Newtonsoft.Json;

using Acumatica.RESTClient.Client;
using Acumatica.RESTClient.ContractBasedApi;
using Acumatica.RESTClient.ContractBasedApi.Model;

namespace Acumatica.eCommerce_22_200_001.Model
{
	[DataContract]
	public class CloseOrder : EntityAction<ServiceOrder>
	{
		public CloseOrder(ServiceOrder entity) : base(entity)
		{ }
		public CloseOrder() : base()
		{ }
	}
}
