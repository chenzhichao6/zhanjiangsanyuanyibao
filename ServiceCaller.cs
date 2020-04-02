using System;
using System.Collections.Generic;
using System.IO;

using System.Net;
using System.Text;

using System.Xml;

namespace WindowsFormsApp1
{
    class ServiceCaller
    {

        public static XmlDocument QueryWebService(string method, string reqArg)
        {

            string RequestParam = @"<REQUEST>
<HEADER>
<APPCODE>3a326dba086047df2090e6161e4949fc</APPCODE>
<SESSIONID>9192a4a09594471da1f235bff165004c</SESSIONID>
<METHOD>{0}</METHOD>
<VERSION>2.0</VERSION>
<REQTIME>{1:yyyy-MM-dd HH:mm:ss}</REQTIME>
<REQTRACENO>201</REQTRACENO>
<ORGCODE>JKZ44081202</ORGCODE>
</HEADER>
<BODY>{2}</BODY>
</REQUEST>";
            DateTime dt = DateTime.Now;
            string fmtParms = String.Format(RequestParam, method, dt, reqArg);

            //ServiceReference1.WebserviceCallEntranceClient webClient 
            //    = new ServiceReference1.WebserviceCallEntranceClient();

            //byte[] byteStr = Encoding.UTF8.GetBytes(fmtParms);
            //string args0 = Convert.ToBase64String(byteStr);
            //string result = webClient.CallFun(args0);
            return null;
            //return ReadXmlResponse(Encoding.UTF8.GetString(Convert.FromBase64String(result)));
        }

        private static XmlDocument ReadXmlResponse(string retXml)
        {
            if (retXml != "")
            {
                XmlDocument doc = new XmlDocument();
                doc.LoadXml(retXml);
                return doc;
            }else
            {
                return null;
            }
        }
    }
}
