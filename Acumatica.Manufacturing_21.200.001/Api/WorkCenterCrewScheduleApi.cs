using Acumatica.RESTClient.Client;
using Acumatica.Manufacturing_21_200_001.Model;

namespace Acumatica.Manufacturing_21_200_001.Api
{
	public class WorkCenterCrewScheduleApi : BaseEndpointApi<WorkCenterCrewSchedule>
	{
		public WorkCenterCrewScheduleApi(ApiClient client) : base(client)
		{ }
	}
}