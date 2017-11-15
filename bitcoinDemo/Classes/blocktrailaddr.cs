using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlockChainDemo.Classes
{
    public class blocktrailaddr
    {
        public Datum[] data { get; set; }
        public int current_page { get; set; }
        public int per_page { get; set; }
        public int total { get; set; }

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
        public static blocktrailaddr FromString(string str)
        {
            return JsonConvert.DeserializeObject<blocktrailaddr>(str);
        }
    }

    public class Datum
    {
        public string hash { get; set; }
        public DateTime time { get; set; }
        public int confirmations { get; set; }
        public int block_height { get; set; }
        public string block_hash { get; set; }
        public bool is_coinbase { get; set; }
        public int estimated_value { get; set; }
        public long total_input_value { get; set; }
        public long total_output_value { get; set; }
        public int total_fee { get; set; }
        public long? estimated_change { get; set; }
        public string estimated_change_address { get; set; }
        public Input[] inputs { get; set; }
        public Output[] outputs { get; set; }

        ///// <summary>
        ///// 將物件轉成 JSON 字串
        ///// </summary>
        ///// <returns></returns>
        public override string ToString()
        {
            return JsonConvert.SerializeObject(this, Formatting.None);
        }

        ///// <summary>
        ///// 將 JSON 字串轉成物件
        ///// </summary>
        ///// <param name="str">JSON 字串</param>
        public static Datum FromString(string str)
        {
            return JsonConvert.DeserializeObject<Datum>(str);
        }
    }

    public class InputBlockAddr
    {
        public int index { get; set; }
        public string output_hash { get; set; }
        public int output_index { get; set; }
        public long value { get; set; }
        public string address { get; set; }
        public string type { get; set; }
        public object multisig { get; set; }
        public string script_signature { get; set; }
        ///// <summary>
        ///// 將物件轉成 JSON 字串
        ///// </summary>
        ///// <returns></returns>
        public override string ToString()
        {
            return JsonConvert.SerializeObject(this, Formatting.None);
        }

        ///// <summary>
        ///// 將 JSON 字串轉成物件
        ///// </summary>
        ///// <param name="str">JSON 字串</param>
        public static InputBlockAddr FromString(string str)
        {
            return JsonConvert.DeserializeObject<InputBlockAddr>(str);
        }
    }

    public class OutputBlockAddr
    {
        public int index { get; set; }
        public long value { get; set; }
        public string address { get; set; }
        public string type { get; set; }
        public object multisig { get; set; }
        public string script { get; set; }
        public string script_hex { get; set; }
        public string spent_hash { get; set; }
        public int spent_index { get; set; }

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
        public static OutputBlockAddr FromString(string str)
        {
            return JsonConvert.DeserializeObject<OutputBlockAddr>(str);
        }
    }




}
