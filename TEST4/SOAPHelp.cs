using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.Text;
using System.Threading.Tasks;
using ServerApi = TEST4.ServiceReference1;

namespace TEST4
{
    class SOAPHelp
    {
        /// <summary>
        /// Creates custom binding to specified url, detects Transport binding https/http based on url
        /// </summary>
        /// <param name="endpoint">url of the destination endpoint</param>
        /// <param name="version">Soap Messaging version</param>
        /// <returns>CustomBinding</returns>
        private static CustomBinding _createClientBinding(string endpoint, MessageVersion version)
        {

            CustomBinding rRet = new CustomBinding();

            TextMessageEncodingBindingElement rTxtBinding = _createTextMessageEncodingBinding(version);
            rRet.Elements.Add(rTxtBinding);

            if (endpoint.ToLower().StartsWith("https"))
            { // Add HttpsTransportBindingElement for secure transport

                HttpsTransportBindingElement rHttpsTrans = new HttpsTransportBindingElement { MaxBufferSize = int.MaxValue, MaxReceivedMessageSize = int.MaxValue };
                rRet.Elements.Add(rHttpsTrans);

            }
            else
            { // Add HttpTransportBindingElement for insecure transport

                HttpTransportBindingElement rHttpTrans = new HttpTransportBindingElement { MaxBufferSize = int.MaxValue, MaxReceivedMessageSize = int.MaxValue };
                rRet.Elements.Add(rHttpTrans);

            }

            return rRet;

        } // end _createClientBinding

        /// <summary>
        /// Create TextMessageEncodingBindingElement of specified Soap version with lengths set to int.MaxValue
        /// </summary>
        /// <param name="version">Soap Messaging version</param>
        /// <returns>Instance of TextMessageEncodingBindingElement</returns>
        private static TextMessageEncodingBindingElement _createTextMessageEncodingBinding(MessageVersion version)
        {

            TextMessageEncodingBindingElement rRet = new TextMessageEncodingBindingElement(version, Encoding.UTF8);

            rRet.ReaderQuotas.MaxArrayLength = int.MaxValue;
            rRet.ReaderQuotas.MaxStringContentLength = int.MaxValue;
            rRet.ReaderQuotas.MaxBytesPerRead = int.MaxValue;
            rRet.ReaderQuotas.MaxDepth = int.MaxValue;
            rRet.ReaderQuotas.MaxNameTableCharCount = int.MaxValue;

            return rRet;

        } // end _createTextMessageEncodingBinding



        public static ServerApi.StockQuoteSoapClient GetServerApiClient(string endpoint, MessageVersion version = null)
        {

            MessageVersion rVersion = (version != null) ? version : MessageVersion.Soap12; // default to Soap12 if no version info passed
            CustomBinding rBinding = _createClientBinding(endpoint, rVersion);

            rBinding.Name = "EsbIncident";
            EndpointAddress rEndPoint = new EndpointAddress(endpoint);

            ServerApi.StockQuoteSoapClient rClient = new ServerApi.StockQuoteSoapClient(rBinding, rEndPoint);

            return rClient;

        }

    }
}
