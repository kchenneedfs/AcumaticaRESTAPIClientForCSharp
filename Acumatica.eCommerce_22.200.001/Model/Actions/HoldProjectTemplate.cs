using System.Collections.Generic;
using System.Runtime.Serialization;

using Newtonsoft.Json;

using Acumatica.RESTClient.Client;
using Acumatica.RESTClient.ContractBasedApi;
using Acumatica.RESTClient.ContractBasedApi.Model;

namespace Acumatica.eCommerce_22_200_001.Model
{
	[DataContract]
	public class HoldProjectTemplate : EntityAction<ProjectTemplate>
	{
		public HoldProjectTemplate(ProjectTemplate entity) : base(entity)
		{ }
		public HoldProjectTemplate() : base()
		{ }
	}
}
