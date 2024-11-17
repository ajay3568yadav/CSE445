using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.Serialization;
using System.Text;

namespace WebApplication1.Business.Services
{
    public class Service1
    {

        // download content at URL
        public string DownloadURL(string completeUri)
        {
            WebClient channel = new WebClient();
            return channel.DownloadString(completeUri); 
        }

    }
}
