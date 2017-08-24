using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BLL
{
    class AuthInfo
    {
        private const string APIKey = "R_7a153373b10d4d33a8c605a183cbb005";
        private const string BitlyUserName = "gamingneturl";
        
        public string APIKEY
        {
            get
            { return APIKey; }
        }
        public string BITLYUSERNAME
        {
            get
            { return BitlyUserName; }
        }

        public AuthInfo()
        {
            //
            // TODO: Add constructor logic here
            //
        }
    }
}
