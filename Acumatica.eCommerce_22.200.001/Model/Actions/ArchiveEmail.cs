using System.Collections.Generic;
using System.Runtime.Serialization;

using Newtonsoft.Json;

using Acumatica.RESTClient.Client;
using Acumatica.RESTClient.ContractBasedApi;
using Acumatica.RESTClient.ContractBasedApi.Model;

namespace Acumatica.eCommerce_22_200_001.Model
{
	[DataContract]
	public class ArchiveEmail : EntityAction<Email>
	{
		public ArchiveEmail(Email entity) : base(entity)
		{ }
		public ArchiveEmail() : base()
		{ }
	}
}
