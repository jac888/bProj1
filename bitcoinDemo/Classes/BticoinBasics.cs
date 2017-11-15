using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NBitcoin;
using System.Data.SqlClient;
using System.Diagnostics;

namespace BlockChainDemo.Classes
{
    class BticoinBasics
    {
        public void btcBasic()
        {
            Key newkey = new Key();
            var pubkey = newkey.PubKey;
            KeyId keyhash = pubkey.Hash;
            //BitcoinAddress addressnew = pubkey.GetAddress(Network.Main);

            //different menthod to get bitcoin addess
            BitcoinAddress addressnew = pubkey.GetAddress(Network.Main); // get address frim pubkey

            BitcoinAddress addressfromhash = new BitcoinPubKeyAddress(keyhash, Network.Main); // get address from pubkey hash
            Console.WriteLine("Bitcoin Address: {0}", addressfromhash);

            Script scriptPubKey = addressnew.ScriptPubKey;
            BitcoinAddress address = scriptPubKey.GetDestinationAddress(Network.Main); // get address from scriptPubKey
            Console.WriteLine("Bitcoin Address: {0}", address);

            Script hashpubkey_fromhash = keyhash.ScriptPubKey;
            Console.WriteLine("Bitcoin HashScriptKey: {0}", scriptPubKey);

            Console.WriteLine("Bitcoin Address equal : " + (addressfromhash == address && address == addressnew).ToString());
           

            var aStringBuilder = new StringBuilder(scriptPubKey.ToString());
            aStringBuilder.Remove(18, 40); //from position 18 and 40 digits for hash in 2017/nov
            aStringBuilder.Insert(18, keyhash.ToString()); // replace with new hash to compare
            var hashtheString = aStringBuilder.ToString();

            Script scriptPubKeyNew = new Script(hashtheString);
            KeyId hash = (KeyId)scriptPubKey.GetDestination();
            Console.WriteLine("Public Key Hash: {0}", hash);
            Console.WriteLine("Bitcoin HashScriptKey equal : " + (scriptPubKey == scriptPubKeyNew).ToString());

            Console.WriteLine("Bitcoin Hash equal : " + (keyhash == hash).ToString());

            var privatekey = newkey.GetWif(Network.Main).ToString();
            var signature = "";
            var signature_v = "";
            SqlConnection conn = new SqlConnection("Data Source=.\\SQLEXPRESS;Initial Catalog=BlockChain;Integrated Security=False;User ID=sa;Password=q1w2e3r4,k.l;Min Pool Size=0;Max Pool Size=32768");
            conn.Open();
            var sqlStr = @"INSERT INTO dbo.WALLET_DAT ( PRIVATE_KEY ,PUBLIC_KEY ,PUBLIC_HASH ,ADDRESSM ,SIGNATURE_N ,BALANCE ,SIGNATURE_E ,ADDRESSV ) VALUES ( '" + privatekey + "', '" + pubkey + "' ," +
              " '" + keyhash + "', '" + address + "', '" + signature + "', 0, '" + signature_v + "', '" + address + "' )";

            SqlCommand cmd = new SqlCommand(sqlStr, conn);
            try
            {
                cmd.ExecuteNonQuery();
                Console.WriteLine("New Bitcoin Info Recorded to DB! ");
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
                Console.ReadLine();
            }
        }
    }
}
