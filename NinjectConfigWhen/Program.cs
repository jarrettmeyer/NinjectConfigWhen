using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ninject;
using Ninject.Activation;
using System.Threading;

namespace NinjectConfigWhen
{
    class Program
    {
        static void Main(string[] args)
        {
            var kernel = new StandardKernel();
            kernel.Bind<IMessageService>().ToProvider<MessageServiceProvider>();

            int counter = 0;
            while (counter < 20)
            {
                kernel.Get<IMessageService>().DisplayMessage();
                Thread.Sleep(500); // 500 milliseconds = 0.5 seconds.
                counter += 1;
            }
        }
    }

    public class MessageServiceProvider : IProvider<IMessageService>
    {

        public object Create(IContext context)
        {
            IMessageService messageService;
            if (DateTime.Now.Second % 2 == 0)
                messageService = new EvenMessageService();
            else
                messageService = new OddMessageService();

            return messageService;
        }

        public Type Type
        {
            get { return typeof(IMessageService); }
        }
    }

    public interface IMessageService
    {
        void DisplayMessage();
    }

    public class EvenMessageService : IMessageService
    {
        public void DisplayMessage()
        {
            string message = string.Format("From {0}: {1}", GetType(), DateTime.Now);
            Console.WriteLine(message);
        }
    }

    public class OddMessageService : IMessageService
    {
        public void DisplayMessage()
        {
            string message = string.Format("From {0}: {1}", GetType(), DateTime.Now);
            Console.WriteLine(message);
        }
    }
}
