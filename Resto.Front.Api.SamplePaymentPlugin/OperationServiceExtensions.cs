using System;
using Resto.Front.Api.V6;
using Resto.Front.Api.V6.Attributes.JetBrains;
using Resto.Front.Api.V6.Data.Security;
using Resto.Front.Api.V6.Exceptions;

namespace Resto.Front.Api.SamplePaymentPlugin
{
    internal static class OperationServiceExtensions
    {
        private const string Pin = "12344321";

        [NotNull]
        public static ICredentials GetCredentials([NotNull] this IOperationService operationService)
        {
            if (operationService == null)
                throw new ArgumentNullException("operationService");

            try
            {
                return operationService.AuthenticateByPin(Pin);
            }
            catch (AuthenticationException)
            {
                PluginContext.Log.Warn("Cannot authenticate. Check pin for plugin user.");
                throw;
            }
        }
    }
}
