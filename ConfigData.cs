using System;
using System.Collections.Generic;

namespace SpecflowProject
{
    public class ConfigData
    {
        public string enviroment { get; set; }
        public string deviceName { get; set; }
        public string country { get; set; }
        public string language { get; set; }
        public string website_name { get; set; }
        public string browser { get; set; }
        public string platform { get; set; }
        public string version { get; set; }
        public string driver_mode { get; set; }
        public int implicit_wait { get; set; }
        public int page_load_timeout { get; set; }
        public bool incognitoMode { get; set; }
        public string url { get; set; }

    }
}
