using System;
using System.IO;
using System.Net;
using System.Text;
using System.Windows.Forms;
using System.Xml;

namespace TrainingLoad.Util
{
    static class ConnectionUtils
    {
        public static WebRequest CreateHttpWebRequest(string url, int timeout)
        {
            WebRequest req = WebRequest.Create(url);
            req.Timeout = timeout * 1000;

            return req;
        }
    }
}
