using System;

namespace Cali_Remote_Pc.Services
{
    public class MyClient
    {
        public MyClient()
        {
                
        }
        static MyClient()
        {
            Instance = new MyClient("Null", 0, DateTime.Now);
        }

        public string Name { get; set; }
        public int Life { get; set; }
        public DateTime RequestTime { get; set; }

        public MyClient(string _name, int _life, DateTime _requestTime)
        {
            this.Name = _name;
            this.Life = _life;
            this.RequestTime = _requestTime;
        }

        public static MyClient Instance { get; set; }
    }
}
