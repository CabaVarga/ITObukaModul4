using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Web.Http;

namespace Dan01.Controllers
{
    public class UtilController : ApiController
    {
        // GET: util/date
        [Route("util/date")]
        [HttpGet]
        public string CurrentDate()
        {
            return DateTime.Now.ToString(System.Globalization.DateTimeFormatInfo.InvariantInfo);
        }

        // GET: util/family
        [Route("util/family")]
        [HttpGet]
        public List<string> MyFamily()
        {
            return new List<string>() { "Zoltan", "Rozalija", "Denes" };
        }

        // GET: util/myclass
        [Route("util/myclass")]
        [HttpGet]
        public HttpResponseMessage MyClass()
        {
            var response = Request.CreateResponse();

            string[,] grupa = { 
                {"Goran", "Alasov"}, {"Aleksandar", "Đorđević"}, {"Radovan", "Josimović"},
                {"Zoran", "Mandić"}, {"Svetozar", "Milinković"}, {"Marina", "Petrović"},
                {"Dejan", "Pavlović"}, {"Dragana", "Sekulić"}, {"Uroš", "Štrbac"},
                {"Marijana", "Tatalović"}, {"Čaba", "Varga"}, {"Viktor", "Vereš"},
                {"Brane", "Vujković"}, {"Borisav", "Ignjatov"}, {"Boban", "Janković"},
                {"Borka", "Lazarević"}, {"Srđan", "Savić"}, {"Nemanja", "Stojković"},
                {"Danijel", "Petrović"}, {"Maja", "Ristić"}
            };

            StringBuilder sb = new StringBuilder();

            sb.Append("<html>\n\t<head>\n\t\t<title>Moja grupa</title>\n\t</head>\n\t<body>");
            sb.Append("\n\t\t<table>\n\t\t\t<tr><th>Broj</th><th>Ime</th><th>Prezime</th></tr>");
    
            int broj = 1;
            for (int i = 0; i < grupa.GetLength(0); i++)
            {
                sb.AppendFormat("\n\t\t\t<tr><td>{0}</td><td>{1}</td><td>{2}</td></tr>", broj, grupa[i, 0], grupa[i, 1]);
                broj++;
            }

            sb.Append("\n\t\t</table>\n\t</body>\n</html>");

            response.Content = new StringContent(sb.ToString(), new UTF8Encoding());
            response.Content.Headers.ContentType = new MediaTypeHeaderValue("text/html");
            response.Content.Headers.ContentType.CharSet = "utf-8";

            return response;
        }
    }
}
