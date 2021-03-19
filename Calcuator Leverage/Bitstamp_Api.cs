using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rechner
{
    class Bitstamp_Api
    {
        public double high { get; set; }
        public double last { get; set; }
        public double timestamp { get; set; }
        public double bid { get; set; }
        public double vwap { get; set; }
        public double volume { get; set; }
        public double low { get; set; }
        public double ask { get; set; }
        public double open { get; set; }
        public double num { get; set; }
        public double leve { get; set; }


        public Bitstamp_Api(double Bid)
        {
            this.bid = Bid;
        }
    }

}
