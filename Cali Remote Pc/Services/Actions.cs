using System;

namespace Cali_Remote_Pc.Services
{
    public class Actions
    {
        public Actions()
        {

        }

        static Actions()
        {
            Instance = new Actions(0, 0, 0, 0, "", DateTime.Now);
        }

        public int ActionId { get; set; }
        public int Lock { get; set; }
        public int Signout { get; set; }
        public int Restart { get; set; }
        public int Shutdown { get; set; }
        public string Command { get; set; }

        public DateTime RequestTime { get; set; }

        public Actions(int _lock, int _signout, int _restart, int _shutdown, string _command, DateTime _requestTime)
        {
            this.Lock = _lock;
            this.Signout = _signout;
            this.Restart = _restart;
            this.Shutdown = _shutdown;
            this.Command = _command;
            this.RequestTime = _requestTime;
        }

        public static Actions Instance { get; set; }
    }

}
