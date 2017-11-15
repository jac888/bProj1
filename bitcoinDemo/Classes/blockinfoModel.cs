using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlockChainDemo.Classes
{
    public class blockchainInfo
    {
        public int ver { get; set; }
        public Input[] inputs { get; set; }
        public int weight { get; set; }
        public int block_height { get; set; }
        public string relayed_by { get; set; }
        public Out[] _out { get; set; }
        public int lock_time { get; set; }
        public int size { get; set; }
        public bool double_spend { get; set; }
        public int time { get; set; }
        public int tx_index { get; set; }
        public int vin_sz { get; set; }
        public string hash { get; set; }
        public int vout_sz { get; set; }
    }

    public class Input
    {
        public long sequence { get; set; }
        public string witness { get; set; }
        public Prev_Out prev_out { get; set; }
        public string script { get; set; }
    }

    public class Prev_Out
    {
        public bool spent { get; set; }
        public int tx_index { get; set; }
        public int type { get; set; }
        public string addr { get; set; }
        public int value { get; set; }
        public int n { get; set; }
        public string script { get; set; }
    }

    public class Out
    {
        public bool spent { get; set; }
        public int tx_index { get; set; }
        public int type { get; set; }
        public string addr { get; set; }
        public int value { get; set; }
        public int n { get; set; }
        public string script { get; set; }
    }
}
