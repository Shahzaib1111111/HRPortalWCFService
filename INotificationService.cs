using ModelsLib;
using System.Collections.Generic;
using System.ServiceModel;
using System.Threading.Tasks;

namespace WCFService
{
    [ServiceContract]
    public interface INotificationService
    {

        [OperationContract]
        Task OnSalaryDisbursed(List<Employee> employees);
    }
}
