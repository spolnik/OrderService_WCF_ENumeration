using System;
using System.Collections.ObjectModel;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.ServiceModel.Description;
using System.ServiceModel.Dispatcher;
using System.Text;

namespace Orders.Service
{
    [AttributeUsage(AttributeTargets.Class)]
    public class SanitizingErrorHandlerAttribute : Attribute, IErrorHandler, IServiceBehavior
    {
        private const string FaultAction = "http://www.nprogramming.wordpress.com/ServerFault";

        #region Implementation of IErrorHandler

        public void ProvideFault(Exception error, MessageVersion version, ref Message fault)
        {
            if (!HandleError(error))
                return;

            var id = LogFault(error);

            var message = new StringBuilder();
            message.AppendFormat("*******{0}*******\nServer Error: \n", id);
            message.AppendFormat(error.Message);

            var faultException = new FaultException(message.ToString());
            var msgFault = faultException.CreateMessageFault();
            fault = Message.CreateMessage(version, msgFault, FaultAction);
        }

        public bool HandleError(Exception error)
        {
            return !(error is FaultException);
        }

        #endregion

        #region Implementation of IServiceBehavior

        public void Validate(ServiceDescription serviceDescription, ServiceHostBase serviceHostBase)
        {
        }

        public void AddBindingParameters(ServiceDescription serviceDescription, ServiceHostBase serviceHostBase,
                                         Collection<ServiceEndpoint> endpoints, BindingParameterCollection bindingParameters)
        {
        }

        public void ApplyDispatchBehavior(ServiceDescription serviceDescription, ServiceHostBase serviceHostBase)
        {
            foreach (ChannelDispatcher dispatcher in serviceHostBase.ChannelDispatchers)
                dispatcher.ErrorHandlers.Add(this);
        }

        #endregion

        private static string LogFault(Exception error)
        {
            var id = Guid.NewGuid();
            Console.WriteLine("\n*******{0}*******\nMessage: {1}\nException: {2}\n**************\n", id, error.Message, error);
            return id.ToString();
        }
    }
}