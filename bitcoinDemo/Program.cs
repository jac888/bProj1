using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NBitcoin;
using NBitcoin.DataEncoders;
using BlockChainDemo;
using System.Data.SqlClient;
using System.Diagnostics;
using QBitNinja.Client;
using QBitNinja.Client.Models;
using BlockChainDemo.Classes;

namespace BlockChainDemo
{
    class Program
    {
        public static string privatekey = "";
        public static string publicKey = "";
        public static string addressm = "";
        public static string addressv = "";
        public static string publicHash = "";
        public static string signature = "";
        public static string signature_v = "";
        public static BitcoinAddress address;

        static void Main(string[] args)
        {
            //var demo = new BticoinBasics();
            var demo = new BitcoinTrans();
            demo.BtcTx();
            //var demo = new NewKey();
            // demo.newkey();
            #region commented code block #1
            //NBitcoin Hierarchical Deterministic Wallet
            //ExtKey will generate a Key for a corresponding ID,
            //ExtPubKey will generate a PubKey for a corresponding ID.

            //byte[] byteArray = Encoders.Base58.DecodeData("KxPA5YkSzS46PW32cYZLTHBPGxidB3RP8efB4PbaKHojaUjMJrx3");
            //string hex1 = Encoders.Hex.EncodeData(byteArray);

            ExtKey privateKey = new ExtKey("123fff"); //can use random hex seed for generate extkey and store it to db

            ExtPubKey pubKey = privateKey.Neuter(); //generate public key from ExtKey to payment server

            //BitcoinExtPubKey wif = pubKey.GetWif(Network.Main);

            //bytearray to str and str to bytep[]
            //var byteStr = Convert.ToBase64String(pubKey.ChainCode);
            // byte[] bytes = System.Convert.FromBase64String(byteStr);

            //BitcoinSecret mainNetPrivateKey = privateKey.GetBitcoinSecret(Network.Main);  // get our private key for the mainnet

            //Now, give the pubkey to your payment server
            //....

            //The payment server receive an order, note the server does not need the private key to generate the address
            // uint orderID = 1001;
            // address = pubKey.Derive(orderID).PubKey.GetAddress(Network.Main);
            // Console.WriteLine(address);

            //Now on the server that have access to the private key, you get the private key from the orderID
            //Key key = privateKey.Derive(orderID).PrivateKey;
            //BitcoinSecret secret = key.GetBitcoinSecret(Network.Main);
            //BitcoinSecret secret1 = new BitcoinSecret("L1Q3FRh73NffUdahsig3uQTj5raepff664DZtmXQEpzpCANiLiVV");
            //bool matched = (secret == secret1);
            //Console.WriteLine(secret); //Print a nice secret key string
            #endregion

            #region new private key and save all info to database
            /*
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
            var sqlStr = @"INSERT INTO dbo.WALLET_DAT ( PRIVATE_KEY ,PUBLIC_KEY ,PUBLIC_HASH ,ADDRESSM ,SIGNATURE_N ,BALANCE ,SIGNATURE_E ,ADDRESSV ) VALUES ( '" + privatekey + "', '" + publicKey + "' ,"+ 
              " '"+ publicHash + "', '" + addressm + "', '" + signature + "', 0, '" + signature_v + "', '" + addressv + "' )";
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
            */
            #endregion

            #region testing / converting of publickey hash and  we can get address from it or even new a new script(keyID) from ScriptPubKey 
            /*
            var publicKeyHash = new KeyId("6239c056daaa921c156255567106cf7cfb0ab3ca"); //assign a KeyID var from a known pubhash.

            //var testNetAddress = publicKeyHash.GetAddress(Network.TestNet);
            //Console.WriteLine(testNetAddress.ScriptPubKey);
            var mainNetAddress = publicKeyHash.GetAddress(Network.Main); //get bitcoin address from this hash of KeyID
            Console.WriteLine(mainNetAddress.ScriptPubKey); // OP_DUP OP_HASH160 6239c056daaa921c156255567106cf7cfb0ab3ca OP_EQUALVERIFY OP_CHECKSIG  <-- BitcoinAddres' ScriptPubKey

            var paymentScript = publicKeyHash.ScriptPubKey; //assign a new script from pervious keyID script
            var newMainNetAddress = paymentScript.GetDestinationAddress(Network.Main); //get BitcoinAddress from new paymentScript

            if (Debugger.IsAttached)
                Console.WriteLine("two are equal? : " + (mainNetAddress == newMainNetAddress).ToString()); // True?            

            var samePublicKeyHash = (KeyId)paymentScript.GetDestination();
            if (Debugger.IsAttached)
                Console.WriteLine("two are equal? : " + (publicKeyHash == samePublicKeyHash).ToString()); // True?

            var sameMainNetAddress2 = new BitcoinPubKeyAddress(samePublicKeyHash, Network.Main);
            if (Debugger.IsAttached)
                Console.WriteLine("two are equal? : " + (mainNetAddress == sameMainNetAddress2).ToString()); // True?
                */
            #endregion

            //load up serect from a private key
            //BitcoinSecret secret3 = new BitcoinSecret("L4i97fXfcYfkc3B9xEQAYMJvuvvfdS6TzoNuxm4DKsV9Bh7aMhej", Network.Main);

            //signature to the private key and you get encrypted signature to verify later if this address is belong to someone (server side)

            #region transactions details
            // bad example to get treansaction from raw
            //string transactionRaw = "0100000009dd9e20cecf519ce7cdec2c4890e60bbff540b2fafdca2b426304fd8fefc58847000000006b483045022100d08870b424f19d8b3921861bedec81599a9cd5f179e35e80d16709a296d41484022023f1c2a9eab7d5dd8a1043d1d423e185641e79d33f32491638c7caf6029105410121035904165d4ed4aae69b1adef8dd99a21dac2c1ad479188d9d7de3b341aae229deffffffffc5a6457d1532d7cfe5dc6802323806bfd81c365bc4bb9fdadd8cfb2fd39280c3000000006b483045022100f337bc11e419e2317a59a0acd33c2937823aa2b015a1579bd6caab6f55dc828602201cc6985189b2b654ee9b71850697460086429c91f5e90598ca927dfbe315a3940121034e1304481c7403a35e1348289468df9982bbd516a3aedb7f1bc81667f7a09c5dffffffffa94e871f35a616b5a22139cc7dc5a4da35061d6317accb935a4af037573c1dc3000000008a473044022045476041f0d2910269ee4e707dda5678af483339962c0a2d7897c3aa78cb29ea0220476032a6bbe59e67ad5f95cc2f3e5264bb2bca8ea88eb30c96123b9ff7a33a5001410482d593f88a39160eaed14470ee4dad283c29e88d9abb904f953115b1a93d6f3881d6f8c29c53ddb30b2d1c6b657068d60a93ed240d5efca247836f6395807bcdffffffff8d199257330921571d8984bf38c47304b26e3c497a09acc298941e60b998ccfb010000008b483045022100fc8e579f4cabda1e26a294b3f7f227087b64ca2451155b8747bd1f6c96780d6d022041912d38512030e1ec1d3df6b8d91d8b9aa4c564642fd7cafc48f97fd550100101410482d593f88a39160eaed14470ee4dad283c29e88d9abb904f953115b1a93d6f3881d6f8c29c53ddb30b2d1c6b657068d60a93ed240d5efca247836f6395807bcdffffffff0246c072469f817ec87273c0a0d6b30fc840a6aa31f56427cca2ce31163c49fd000000006a473044022034cd8a6ecc391539af0e0af1075cf48599a40fb011936f2397a6b457e5fb60bd02201d059c7cf571d80bb6ead165639334fc6e45985c34a0cf7bc10d9a1817d22d0501210249af09f7a52e81c6de1df85a00792522df76b6a529178673d27754772f5d2758ffffffff2515c8cbc51039bc50bc8b4504617410824d1858dcff721ab3716dae44f350a9000000008b48304502210095a36910e3a466697f0a3a42cd0e487c280b1d48f8a8ea3d2867c1bb6fa6a4cf0220486eb68f95ae081e42dd48cb96d01b9536761e17b4f2ae10935aa406ceed268d01410482d593f88a39160eaed14470ee4dad283c29e88d9abb904f953115b1a93d6f3881d6f8c29c53ddb30b2d1c6b657068d60a93ed240d5efca247836f6395807bcdffffffff36c456759b51e87a75673d8bd8a1d91177164767ea937a4365578930cd8bf855000000006b483045022100eae7bf8ead57bedc858700ff7e8f0f917650a97d905ecc5264c29c6a4e87f7ac022055483fac8618831d163370bb8a083aff5111c795bafdbd48693cf98b5f2e420b012103b1534714e589d87484e2305e32261fc157a7ddd3ca060f5293a3dfbc76b7576bffffffff50a85ca81a0c667b6484ac371493be2a5298fe9e04b095f545cc795ba7dfda19000000006b483045022100dab39ceb5f48718fd3f7f549f5cb28fdd9bca755d031a15f608ebc7902ede62502200fcd17229262dd183fbc134279a9139d9e2a1e1e5723adfec8f3599e3f62b6ed0121025fbd9ac3c3277a06e623dfed29f9d490c643c023987a0412308c4a8e78b12b55ffffffff66fb69807af8e4d8f0cbfece1f02fed8c130c168f3b06d10640d02ffdebf2d90000000008b483045022100df2e15424b9664be46e5eef90030b557655ffd4b9f1dfc4dfd5a0422e8e8d13202204c94a8c9975f914f7926cda55b04193328f612d154fa6c6c908c88b4a4f9729201410482d593f88a39160eaed14470ee4dad283c29e88d9abb904f953115b1a93d6f3881d6f8c29c53ddb30b2d1c6b657068d60a93ed240d5efca247836f6395807bcdffffffff01a4c5a84e000000001976a914b6cefbb855cabf6ee45598f518a98011c22961aa88ac00000000";

            //Transaction tx = new Transaction(transactionRaw);

            //good example by using client :
            // Create a client
            QBitNinjaClient client = new QBitNinjaClient(Network.Main);
            // Parse transaction id  (txid) to NBitcoin.uint256 so the client can eat it
            var transactionId = uint256.Parse("f13dc48fb035bbf0a6e989a26b3ecb57b84f85e0836e777d6edf60d87a4a2d94");
            // Query the transaction
            GetTransactionResponse transactionResponse = client.GetTransaction(transactionId).Result;
            //Console.WriteLine("tx = transactionId is true/false? : " + (tx = transactionId).ToString())
            Transaction transaction = transactionResponse.Transaction;

            Console.WriteLine(transactionResponse.TransactionId); // f13dc48fb035bbf0a6e989a26b3ecb57b84f85e0836e777d6edf60d87a4a2d94
            Console.WriteLine(transaction.GetHash()); // f13dc48fb035bbf0a6e989a26b3ecb57b84f85e0836e777d6edf60d87a4a2d94

            List<ICoin> receivedCoins = transactionResponse.ReceivedCoins;
            foreach (var coin in receivedCoins)
            {
                Money amount = (Money)coin.Amount;

                Console.WriteLine(amount.ToDecimal(MoneyUnit.BTC) + " BTC recivied");
                var paymentScript = coin.TxOut.ScriptPubKey;
                Console.WriteLine(paymentScript);  // It's the ScriptPubKey
                address = paymentScript.GetDestinationAddress(Network.Main);
                Console.WriteLine(address); // 1HfbwN6Lvma9eDsv7mdwp529tgiyfNr7jc
                Console.WriteLine();
            }

            Money TotalamountSpend = Money.Zero;
            List<ICoin> spentCoins = transactionResponse.SpentCoins;
            foreach (var scoin in spentCoins)
            {
                Money amount = (Money)scoin.Amount;

                Console.WriteLine(amount.ToDecimal(MoneyUnit.BTC) + " BTC");
                var paymentScript = scoin.TxOut.ScriptPubKey;
                Console.WriteLine(paymentScript);  // It's the ScriptPubKey
                address = paymentScript.GetDestinationAddress(Network.Main);
                Console.WriteLine(address); // 1HfbwN6Lvma9eDsv7mdwp529tgiyfNr7jc
                Console.WriteLine();
                TotalamountSpend += amount;
            }

            Console.WriteLine("Total Spend = " + TotalamountSpend.ToString() + " BTC");

            var fee = transaction.GetFee(spentCoins.ToArray());
            Console.WriteLine("Transation fee = " + fee.ToString() + " BTC");

            Console.WriteLine("Total recevied = "+ (TotalamountSpend - fee).ToString() + " BTC");

            //var outputs = transaction.Outputs;
            //foreach (var output in outputs)
            //{
            //    Money amt = output.Value;
            //    Console.WriteLine(amt.ToDecimal(MoneyUnit.BTC));
            //    var paymentScript = output.ScriptPubKey;
            //    Console.WriteLine(paymentScript);  // It's the ScriptPubKey
            //    var address = paymentScript.GetDestinationAddress(Network.Main);
            //    Console.WriteLine(address);
            //}

            //var inputs = transaction.Inputs;
            //foreach (TxIn input in inputs)
            //{
            //    OutPoint previousOutpoint = input.PrevOut;
            //    Console.WriteLine(previousOutpoint.Hash); // hash of prev tx
            //    Console.WriteLine(previousOutpoint.N); // idx of out from prev tx, that has been spent in the current tx
            //    Console.WriteLine();
            //}

            //Money spentAmount = Money.Zero;
            //foreach (var spentCoin in spentCoins)
            //{
            //    spentAmount = (Money)spentCoin.Amount.Add(spentAmount);
            //}
            //Console.WriteLine(spentAmount.ToDecimal(MoneyUnit.BTC)); // 13.19703492

            #endregion
            //to verify user use address and signature from client side
            //string messageVerify = "jackson";
            //string signatureVerify = "IGXlagkC3YK4/j0NceYW9rKHsLk1bI+WiTU2QZskuSp+Ju47L6MMw6f72ysvKvJVOsxSqzBdE922I86+P+TsDfc=";

            //var address1 = new BitcoinPubKeyAddress("1BUGQRFBb8cBE6mHxECxNbHoU4deEaCujK");
            //bool verified = address1.VerifyMessage(messageVerify, signatureVerify);
            //Console.WriteLine(verified);


            //string signature = secret.Key.SignMessage("I am Nicolas Dorier, owner of this address 15sYbVpRh6dyWycZMwPdxJWD4xbfxReeHe");

            //main net address : {1BUGQRFBb8cBE6mHxECxNbHoU4deEaCujK}
            //test net address : {mqzDhULAQA3S1DEufoBLCWW8L4EM4gPc8W}

            //BitcoinSecret mainNetPrivateKey = privateKey.GetBitcoinSecret(Network.Main);  // get our private key for the mainnet
            //BitcoinSecret testNetPrivateKey = privateKey.GetBitcoinSecret(Network.TestNet);  // get our private key for the testnet

            //public key : 02d40400fe8097ef9a21f219c15971997cfbc0a58df22d693371a4aa12b55e19f9 

            //note  public key hash = and does not allow you to deduce the PubKey.

            //  var MainNetpubKeyHash = ((BitcoinPubKeyAddress)addressMain).Hash;
            //  var TestNetpubKeyHash = ((BitcoinPubKeyAddress)addressTest).Hash;

            //Console.WriteLine(mainNetPrivateKey); // base58 {L4i97fXfcYfkc3B9xEQAYMJvuvvfdS6TzoNuxm4DKsV9Bh7aMhej}
            //Console.WriteLine(testNetPrivateKey); // base58 {cV58aaXX3cN1mUeRLeDHufozYAE5HtCA4qXP5BWipz99SSE8sFT4}

            //GetWif = 
            //bool WifIsBitcoinSecret = mainNetPrivateKey == privateKey.GetWif(Network.Main);

            //byte[] byteArray = Encoders.Base58.DecodeData("15sYbVpRh6dyWycZMwPdxJWD4xbfxReeHe");
            //Key privateKey123 = new Key(byteArray);
            //  bool WifIsBitcoinSecret = mainNetPrivateKey == privateKey.GetWif(Network.Main);
            //  Console.WriteLine(WifIsBitcoinSecret); // True

            //  Key samePrivateKey = mainNetPrivateKey.PrivateKey;
            // PubKey publicKey = privateKey.PubKey;
            //  BitcoinPubKeyAddress bitcoinPublicKey = publicKey.GetAddress(Network.Main); // 1PUYsjwfNmX64wS368ZR5FMouTtUmvtmTY

            // PubKey samePublicKey = bitcoinPublicKey.ItIsNotPossible;


            //main net address :15sYbVpRh6dyWycZMwPdxJWD4xbfxReeHe
            //byte[] byteArray = Encoders.Base58.DecodeData("15sYbVpRh6dyWycZMwPdxJWD4xbfxReeHe");
            //test net adress : mkPQYcV1UKvveH5nu3FmeigXLB2XAuN4o6

            //byte[] byteArray = Encoders.Base58.DecodeData("mkPQYcV1UKvveH5nu3FmeigXLB2XAuN4o6");

            //string hex = Encoders.Hex.EncodeData(byteArray);
            //string prefix = hex.Substring(0, 2);
            //string pubkeyHash = hex.Substring(2, 40);
            //string checkSum = hex.Substring(42);
            //result hex = 00-356facdac5f5bcae995d13e667bb5864fd1e7d59fb69d021   
            // 00 : Prefix that identify the type of the bitcoin data structure, in this case it is a public key hash, On the Main Network 00 is the prefix for the public key hash. But for Test Network it is 6f.
            // 356facdac5f5bcae995d13e667bb5864fd1e7d59 : actual public key hash (20 bytes)
            // fb69d021 : checksum of all the previous data (detect mistyping)
            //Console.WriteLine(hex);
            //var base58key = Network.CreateFromBase58Data<BitcoinExtKey>(keyStr);
            //     BitcoinAddress address = Network.TestNet.CreateBitcoinAddress("mkPQYcV1UKvveH5nu3FmeigXLB2XAuN4o6");
            //BitcoinAddress address = Network.Main.CreateBitcoinAddress("15sYbVpRh6dyWycZMwPdxJWD4xbfxReeHe");
            //BitcoinAddress address = new BitcoinAddress("15sYbVpRh6dyWycZMwPdxJWD4xbfxReeHe", Network.Main);
            //var test = ((BitcoinPubKeyAddress)address).Hash;
            //var ScriptPubKey = address.ScriptPubKey;
            //var type = ((BitcoinPubKeyAddress)address).Type;
            //var test1 = ScriptPubKey.GetDestinationPublicKeys();
        }
    }
}
