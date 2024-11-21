using System.ServiceModel;

namespace WCFSample
{
    [ServiceContract]
    public interface IServiceCalc
    {

        [OperationContract]
        int Sum(int num1, int num2);

        [OperationContract]
        int Subtract(int num1, int num2);

        [OperationContract]
        int Multiply(int num1, int num2);

        [OperationContract]
        int Divide(int num1, int num2);

    }
}
