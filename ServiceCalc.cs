namespace WCFSample
{
    public class ServiceCalc : IServiceCalc
    {
        public int Sum(int num1, int num2)
        {
            return num1 + num2;
        }
        public int Subtract(int num1, int num2)
        {
            if (num1 > num2)
            {
                return num1 - num2;
            }
            else
            {
                return 0;
            }
        }
        public int Multiply(int num1, int num2)
        {
            return num1 * num2;
        }
        public int Divide(int num1, int num2)
        {
            if (num2 != 0)
            {
                return (num1 / num2);
            }
            else
            {
                return 1;
            }
        }
    }
}
