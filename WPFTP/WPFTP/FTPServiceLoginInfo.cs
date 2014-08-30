using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPFTP
{
    public class FTPServiceLoginInfo
    {
        public string IP { get; set; }
        public string Port { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public bool IsAnonymous { get; set; }
    }
}
