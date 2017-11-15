using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlockChainDemo.Classes
{
    public class blockchainModel
    {
        public string address { get; set; }
        public int total_received { get; set; }
        public int total_sent { get; set; }
        public int balance { get; set; }
        public int unconfirmed_balance { get; set; }
        public int final_balance { get; set; }
        public int n_tx { get; set; }
        public int unconfirmed_n_tx { get; set; }
        public int final_n_tx { get; set; }
        public Unconfirmed_Txrefs[] unconfirmed_txrefs { get; set; }
        public string tx_url { get; set; }


        /// <summary>
        /// 將物件轉成 JSON 字串
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return JsonConvert.SerializeObject(this, Formatting.None);
        }

        /// <summary>
        /// 將 JSON 字串轉成物件
        /// </summary>
        /// <param name="str">JSON 字串</param>
        public static blockchainModel FromString(string str)
        {
            return JsonConvert.DeserializeObject<blockchainModel>(str);
        }

    }

    public class Unconfirmed_Txrefs
    {
        public string address { get; set; }
        public string tx_hash { get; set; }
        public int tx_input_n { get; set; }
        public int tx_output_n { get; set; }
        public int value { get; set; }
        public bool spent { get; set; }
        public DateTime received { get; set; }
        public int confirmations { get; set; }
        public bool double_spend { get; set; }
        public string preference { get; set; }


        /// <summary>
        /// 將物件轉成 JSON 字串
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return JsonConvert.SerializeObject(this, Formatting.None);
        }

        /// <summary>
        /// 將 JSON 字串轉成物件
        /// </summary>
        /// <param name="str">JSON 字串</param>
        public static Unconfirmed_Txrefs FromString(string str)
        {
            return JsonConvert.DeserializeObject<Unconfirmed_Txrefs>(str);
        }
    }
}
