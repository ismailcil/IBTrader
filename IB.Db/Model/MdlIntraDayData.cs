using System;

namespace IB.Db.Model
{
    public class MdlIntraDayData
    {
        public long Id { get; set; }
        public int TickerId { get; set; }
        public DateTime Date { get; set; }
        public double Last { get; set; }
        public double Ask { get; set; }
        public double Bid { get; set; }
        public double Vol { get; set; }
        public double Hour { get; set; }
    }
}
