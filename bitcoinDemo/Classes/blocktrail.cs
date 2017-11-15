using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlockChainDemo.Classes
{
    public class blocktrail
    {
        public string raw { get; set; }
        public string hash { get; set; }
        public DateTime first_seen_at { get; set; }
        public DateTime last_seen_at { get; set; }
        public int block_height { get; set; }
        public DateTime block_time { get; set; }
        public string block_hash { get; set; }
        public int confirmations { get; set; }
        public bool is_coinbase { get; set; }
        public int estimated_value { get; set; }
        public long total_input_value { get; set; }
        public long total_output_value { get; set; }
        public int total_fee { get; set; }
        public long estimated_change { get; set; }
        public string estimated_change_address { get; set; }
        public bool high_priority { get; set; }
        public bool enough_fee { get; set; }
        public bool contains_dust { get; set; }
        public Input[] inputs { get; set; }
        public Output[] outputs { get; set; }
        public bool opt_in_rbf { get; set; }
        public bool unconfirmed_inputs { get; set; }
        public object lock_time_timestamp { get; set; }
        public object lock_time_block_height { get; set; }
        public int size { get; set; }
        public bool is_double_spend { get; set; }
        public object[] double_spend_in { get; set; }

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
        public static blocktrail FromString(string str)
        {
            return JsonConvert.DeserializeObject<blocktrail>(str);
        }
    }

    public class InputBlockTrail
    {
        public int index { get; set; }
        public string output_hash { get; set; }
        public int output_index { get; set; }
        public bool output_confirmed { get; set; }
        public long value { get; set; }
        public long sequence { get; set; }
        public string address { get; set; }
        public string type { get; set; }
        public object multisig { get; set; }
        public object multisig_addresses { get; set; }
        public string script_signature { get; set; }

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
        public static InputBlockTrail FromString(string str)
        {
            return JsonConvert.DeserializeObject<InputBlockTrail>(str);
        }
    }

    public class Output
    {
        public int index { get; set; }
        public long value { get; set; }
        public string address { get; set; }
        public string type { get; set; }
        public object multisig { get; set; }
        public object multisig_addresses { get; set; }
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
        public static Output FromString(string str)
        {
            return JsonConvert.DeserializeObject<Output>(str);
        }
    }

}
