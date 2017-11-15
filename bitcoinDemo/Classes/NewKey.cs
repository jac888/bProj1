using NBitcoin;
using NBitcoin.DataEncoders;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlockChainDemo.Classes
{
    class NewKey
    {
        #region new private key and save all info to database
        public static string privatekey = "";
        public static string publicKey = "";
        public static string addressm = "";
        public static string addressv = "";
        public static string publicHash = "";
        public static string signature = "";
        public static string signature_v = "";

        public void newkey()
        {
            Key privateKey = new Key(); // generate a random private key
            BitcoinAddress addressMain = privateKey.PubKey.GetAddress(Network.Main); //Get the public key, and derive the address on the Main network
            BitcoinAddress addressTest = privateKey.PubKey.GetAddress(Network.TestNet); //Get the public key, and derive the address on the Test network

            publicKey = privateKey.PubKey.ToString();
            addressm = addressMain.ToString();
            addressv = addressTest.ToString();
            privatekey = privateKey.GetWif(Network.Main).ToString();
            signature = "jackson";
            signature_v = privateKey.SignMessage(signature);

            byte[] byteArray = Encoders.Base58.DecodeData(addressm);

            string hex = Encoders.Hex.EncodeData(byteArray);
            string prefix = hex.Substring(0, 2);
            string pubkeyHash = hex.Substring(2, 40);
            string checkSum = hex.Substring(42);

            publicHash = pubkeyHash;

            SqlConnection conn = new SqlConnection("Data Source=.\\SQLEXPRESS;Initial Catalog=BlockChain;Integrated Security=False;User ID=sa;Password=q1w2e3r4,k.l;Min Pool Size=0;Max Pool Size=32768");
            conn.Open();
            var sqlStr = @"INSERT INTO dbo.WALLET_DAT ( PRIVATE_KEY ,PUBLIC_KEY ,PUBLIC_HASH ,ADDRESSM ,SIGNATURE_N ,BALANCE ,SIGNATURE_E ,ADDRESSV,CR_DAT,CR_USR ) VALUES ( '" + privatekey + "', '" + publicKey + "' ," + " '" + publicHash + "', '" + addressm + "', '" + signature + "', 0, '" + signature_v + "', '" + addressv + "', '"+DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss")+"', 'Jackson Chen' )";
            SqlCommand cmd = new SqlCommand(sqlStr, conn);
            try
            {
                cmd.ExecuteNonQuery();
            }
            catch (SqlException ex)
            {
                Console.WriteLine(ex.StackTrace);
                Console.WriteLine(ex.Message);
                if (Debugger.IsAttached)
                    Console.ReadLine();
            }
            finally
            {
                conn.Close();
            }
        }
        #endregion
    }
}
