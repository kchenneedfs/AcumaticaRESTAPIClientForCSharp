using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

using Newtonsoft.Json;

using Acumatica.RESTClient.Client;
using Acumatica.RESTClient.ContractBasedApi;
using Acumatica.RESTClient.ContractBasedApi.Model;

namespace Acumatica.eCommerce_24_200_001.Model
{
	[DataContract]
	public class PayrollWCCCode : Entity, ITopLevelEntity
	{

		[DataMember(Name="Country", EmitDefaultValue=false)]
		public StringValue? Country { get; set; }

		[DataMember(Name="WCCCodes", EmitDefaultValue=false)]
		public List<WCCCode>? WCCCodes { get; set; }

		public virtual string GetEndpointPath()
		{
			return "entity/eCommerce/24.200.001";
		}
	}
}