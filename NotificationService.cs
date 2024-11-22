using ModelsLib;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace WCFService
{
    public class NotificationService : INotificationService
    {
        public async Task OnSalaryDisbursed(List<Employee> employees)
        {
            int noOfThreads = 4;
            int noOfEmployeesToBeProcessedByEachThread = employees.Count() / noOfThreads;
            List<Task> tasks = new List<Task>();
            for (int i = 0; i < noOfThreads; i++)
            {
                //DRY RUN
                //i=0 noOfEmployeesToBeProcessedByEachThread=3
                //0=> skips 0 employees and take 3 st from 0
                //1=> skips 3 employees and take 3 st from 3
                //2=> skips 6 employees and take 3 st from 6
                //3=> skips 9 employees and take 3 st from 9
                List<Employee> batchOfEmployees = employees.Skip(i * noOfEmployeesToBeProcessedByEachThread).Take(noOfEmployeesToBeProcessedByEachThread).ToList();
                tasks.Add(Task.Run(() => SendEmail(batchOfEmployees)));
            }
            await Task.WhenAll(tasks);
            Debug.WriteLine("Email has been sent to all employees");
        }

        private static void SendEmail(object state)
        {
            foreach (var emp in (List<Employee>)state)
            {
                DateTime currentDate = DateTime.Now;
                Debug.WriteLine("{0} your salary has been disbursed on {1}", emp.Name, currentDate);
            }
        }
    }
}
