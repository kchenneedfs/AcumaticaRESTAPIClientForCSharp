using Acumatica.RESTClient.Client;
using Acumatica.Default_20_200_001.Model;

namespace Acumatica.Default_20_200_001.Api
{
	public class FinancialYearApi : BaseEndpointApi<FinancialYear>
	{
		public FinancialYearApi(ApiClient client) : base(client)
		{ }
	}
}