using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NBitcoin;
using QBitNinja.Client;
using QBitNinja.Client.Models;
using System.Data.SqlClient;
using System.Diagnostics;
using BlockChainDemo.Classes;
using System.Net;
using WebLib;
using System.IO;

namespace BlockChainDemo.Classes
{
    class BitcoinTrans
    {
        public string TxidLog = @"D:\\LOG\\TXID\\";
        private static string apikey = "/transactions?api_key=c486fac4c6668db88757d594cc57af1cf2a14510"; //blocktrail.com ap key
        //string mainNet_api = "https://api.blocktrail.com/v1/btc/transaction/";
        string testNet_api = "https://api.blocktrail.com/v1/tbtc/transaction/";
        string json = "";
        string Tx_json = "";
        int T_unconfirmed = 0;
        string url = "https://api.blocktrail.com/v1/tbtc/address/";
        List<int> TbIDList = new List<int>();
        List<string> AddressList = new List<string>();
        List<string> KeyHashList = new List<string>();
        List<int> OrderCountList = new List<int>();
        List<string> TxidList = new List<string>();
        object returnValue = new object();

        //https://blockchain.info/tx/017ee876b7078170066da40894b291e496dc09b7fb3edff4e2e7e8262545c7b1?format=json view rawinfo

        public void BtcTx()
        {
            if (!Directory.Exists(TxidLog))
            {
                Directory.CreateDirectory(TxidLog);
                Logger.Log(TxidLog, LogType.Day, "Log Folder created (" + TxidLog + ")");
            }

            Logger.Log(TxidLog, LogType.Day, "bitcoin balance & incoming transation scan starting...");

            SqlConnection conn = new SqlConnection("Data Source=.\\SQLEXPRESS;Initial Catalog=BlockChain;Integrated Security=False;User ID=sa;Password=q1w2e3r4,k.l;Min Pool Size=0;Max Pool Size=32768");
            conn.Open();
            var sqlStr = @"SELECT ID,ADDRESSV,TRANSTOTAL,PUBLIC_HASH FROM dbo.WALLET_DAT WHERE  SUBSTRING(ADDRESSV,1,1) = 'm'";
            SqlCommand cmd = new SqlCommand(sqlStr, conn);
            Logger.Log(TxidLog, LogType.Day, "prepare list for test net addresses");
            using (SqlDataReader reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    TbIDList.Add(Convert.ToInt32(reader["ID"]));
                    AddressList.Add(Convert.ToString(reader["ADDRESSV"]));
                    OrderCountList.Add(Convert.ToInt32(reader["TRANSTOTAL"]));
                    KeyHashList.Add(Convert.ToString(reader["PUBLIC_HASH"]));
                }
            }
            Logger.Log(TxidLog, LogType.Day, "TestNet bitcoin address Qty : " + AddressList.Count.ToString());

            for (int i = 0; i < AddressList.Count; i++)
            {
                var ApiURL = url + AddressList[i] + apikey;
                Logger.Log(TxidLog, LogType.Day, "ApiURL : " + ApiURL);
                using (WebClient wc = new WebClient())
                    json = wc.DownloadString(ApiURL);

                //Logger.Log(TxidLog, LogType.Day, "json : " + json);

                blocktrailaddr model = blocktrailaddr.FromString(json);
                var transactionCount = model.data.Count();

                if (OrderCountList[i] <= transactionCount)
                {
                    Money T_balance = Money.Zero;
                    Money T_Finalbalance = Money.Zero;

                    for (int j = 0; j < transactionCount; j++)
                    {
                        if (model.data[j].confirmations >= 3)
                            T_balance += model.data[j].estimated_value;

                        T_Finalbalance += model.data[j].estimated_value; //final balance including unconfirmed

                        var txid = model.data[j].hash;
                        var BlocktrailUrl = testNet_api + txid + apikey;
                        using (WebClient wc = new WebClient())
                            Tx_json = wc.DownloadString(ApiURL);

                        blocktrail blocktrial = blocktrail.FromString(json);

                        sqlStr = @"SELECT 1 FROM dbo.TXID WHERE TXID = '" + txid + "' and ID = " + TbIDList[i] + "";
                        cmd = new SqlCommand(sqlStr, conn);
                        try
                        {
                            returnValue = cmd.ExecuteScalar();
                        }
                        catch (SqlException ex) { }

                        if (returnValue == null)
                        {
                            Script scriptPubKey = new Script("OP_DUP OP_HASH160 " + KeyHashList[i] + " OP_EQUALVERIFY OP_CHECKSIG");
                            KeyId hash = (KeyId)scriptPubKey.GetDestination();
                            BitcoinAddress address = new BitcoinPubKeyAddress(hash, Network.TestNet);

                            if (address.ToString() == AddressList[i])
                            {
                                Logger.Log(TxidLog, LogType.Day, "Wallet scriptPubKey match address for: [ " + AddressList[i].ToString() + " ] !");
                                Money amount = model.data[j].estimated_value;
                                sqlStr = @"INSERT INTO dbo.TXID (ID ,TXID,CONFIRMED,AMOUNT,CR_DAT,CR_USR) VALUES (   " + TbIDList[i] + " ,  N'" + txid + "' ,  " + model.data[j].confirmations + ", " + amount.ToString() + " ,'" + DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss") + "', 'Jackson Chen' )";
                                cmd = new SqlCommand(sqlStr, conn);
                                cmd.ExecuteNonQuery();
                                Logger.Log(TxidLog, LogType.Day, "new transaction with address[" + i + "]  :  [ " + AddressList[i].ToString() + " ] was successfully added to Database TXID : [ " + txid + " ]");
                            }
                            //todo send email notification for new transaction....
                        }
                        else
                        {
                            Logger.Log(TxidLog, LogType.Day, "no new transaction with address[" + i + "]  :  [ " + AddressList[i].ToString() + " ]! ");
                        }
                    }
                    sqlStr = @"UPDATE dbo.WALLET_DAT SET T_BALANCE = '" + T_balance.ToString() + "', T_F_BALANCE = '" + T_Finalbalance.ToString() + "', TRANSTOTAL = " + transactionCount + "  , TD_DAT = '" + DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss") + "' ,TD_USR = 'Jackson Chen' WHERE ADDRESSV = '" + AddressList[i] + "'";

                    cmd = new SqlCommand(sqlStr, conn);
                    try
                    {
                        cmd.ExecuteNonQuery();
                        Logger.Log(TxidLog, LogType.Day, "update bitcoin balance / transaction with address : [ " + AddressList[i].ToString() + " ] successfully");
                    }
                    catch (SqlException ex)
                    {
                        Logger.Log(TxidLog, LogType.Day, "update with address : [ " + AddressList[i].ToString() + " ] was failed with error : ");
                        Logger.Log(TxidLog, LogType.Day, ex.StackTrace);
                        Logger.Log(TxidLog, LogType.Day, ex.Message);
                    }

                }
                else
                {
                    Logger.Log(TxidLog, LogType.Day, "no new transaction for address : [ " + AddressList[i].ToString() + " ]! ");
                    continue;
                }
            }
            Logger.Log(TxidLog, LogType.Day, "Update bitcoin balance & incoming transation scan completed! ");

            QBitNinjaClient client = new QBitNinjaClient(Network.TestNet);
            // Parse transaction id  (txid) to NBitcoin.uint256 so the client can eat it

            //Console.WriteLine(transactionId.ToString());

            //GetTransactionResponse transactionResponse = client.GetTransaction(transactionId).Result;
            //Console.WriteLine(transactionResponse.TransactionId); // f13dc48fb035bbf0a6e989a26b3ecb57b84f85e0836e777d6edf60d87a4a2d94

            // Transaction transaction = transactionResponse.Transaction;
            //Console.WriteLine(transaction.GetHash()); // f13dc48fb035bbf0a6e989a26b3ecb57b84f85e0836e777d6edf60d87a4a2d94

            //https://api.blockcypher.com/v1/btc/test3/addrs/mfrrk7xU63UhxzWK2a3ZBdyVCBSiGT4CNc  for testnet api

            //Bitcoin Main : api.blockcypher.com/v1/btc/main
            //Test Net : api.blockcypher.com/v1/btc/test3


            //List<ICoin> receivedCoins = transactionResponse.ReceivedCoins;
            //foreach (var coin in receivedCoins)
            //{   
            //    var paymentScript = coin.TxOut.ScriptPubKey;
            //    Console.WriteLine(paymentScript);  // It's the ScriptPubKey
            //    BitcoinAddress address = paymentScript.GetDestinationAddress(Network.TestNet);
            //    Console.WriteLine(address); // 1HfbwN6Lvma9eDsv7mdwp529tgiyfNr7jc

            //SqlConnection conn = new SqlConnection("Data Source=.\\SQLEXPRESS;Initial Catalog=BlockChain;Integrated Security=False;User ID=sa;Password=q1w2e3r4,k.l;Min Pool Size=0;Max Pool Size=32768");
            //conn.Open();
            //var sqlStr = @"SELECT 1 FROM dbo.WALLET_DAT WHERE ADDRESSM = '"+ address + "' OR ADDRESSV='"+ address + "'";
            //SqlCommand cmd = new SqlCommand(sqlStr, conn);
            //try
            //{
            //    returnValue = cmd.ExecuteScalar();
            //}
            //catch (SqlException ex)
            //{
            //    Console.WriteLine(ex.StackTrace);
            //    Console.WriteLine(ex.Message);
            //    if (Debugger.IsAttached)
            //        Console.ReadLine();
            //}
            //finally
            //{
            //    conn.Close();
            //}

            //if (returnValue != null)
            //{
            //    Money amount = (Money)coin.Amount;
            //    Console.WriteLine(amount.ToDecimal(MoneyUnit.BTC) + " BTC recivied");
            //    Console.WriteLine();
            //}
            //else
            //{
            //    continue;
            //}       
            // }
        }
    }
}
