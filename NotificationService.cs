using ModelsLib;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace WCFService
{
    public class NotificationService : INotificationService
    {
        private static readonly object m_logLock = new object();
        public string OnSalaryDisbursed(List<Employee> employees)
        {
            try
            {
                int noOfThreads = 4;
                int noOfEmployeesToBeProcessedByEachThread = employees.Count() / noOfThreads;
                List<Task> tasks = new List<Task>();
                for (int i = 0; i < noOfThreads; i++)
                {
                    //DRY RUN
                    //i=0, noOfEmployeesToBeProcessedByEachThread=3
                    //0=> skips 0 employees and take 3 starting from 0
                    //1=> skips 3 employees and take 3 starting from 3
                    //2=> skips 6 employees and take 3 starting from 6
                    List<Employee> batchOfEmployees = employees.Skip(i * noOfEmployeesToBeProcessedByEachThread).Take(noOfEmployeesToBeProcessedByEachThread).ToList();
                    tasks.Add(Task.Run(() => SendEmail(batchOfEmployees)));
                }
                Task.WhenAll(tasks);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return ex.Message;
            }
            return "Email has been sent to all employees";
        }

        private static void SendEmail(object state)
        {
            lock (m_logLock)
            {
                string logFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "logfile.txt");

                using (StreamWriter writer = new StreamWriter(logFilePath, append: true))
                {
                    foreach (var emp in (List<Employee>)state)
                    {
                        DateTime currentDate = DateTime.Now;
                        writer.WriteLine("{0} your salary has been disbursed on {1}", emp.Name, currentDate);
                    }
                }
            }

        }


    }
}
