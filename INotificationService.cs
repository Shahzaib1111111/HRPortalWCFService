using ModelsLib;
using System.Collections.Generic;
using System.ServiceModel;

namespace WCFService
{
    [ServiceContract]
    public interface INotificationService
    {
        [OperationContract]
        string OnSalaryDisbursed(List<Employee> employees);
    }
}
