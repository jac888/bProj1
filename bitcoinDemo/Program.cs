using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NBitcoin;
using NBitcoin.DataEncoders;
using bitcoinDemo;
using System.Data.SqlClient;
using System.Diagnostics;

namespace bitcoinDemo
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

        static void Main(string[] args)
        {

            //NBitcoin Hierarchical Deterministic Wallet
            //ExtKey will generate a Key for a corresponding ID,
            //ExtPubKey will generate a PubKey for a corresponding ID.

            //byte[] byteArray = Encoders.Base58.DecodeData("KxPA5YkSzS46PW32cYZLTHBPGxidB3RP8efB4PbaKHojaUjMJrx3");
            //string hex1 = Encoders.Hex.EncodeData(byteArray);

            //ExtKey privateKey = new ExtKey("123fff"); //can use random hex seed for extkey and store it to db

            //ExtPubKey pubKey = privateKey.Neuter(); //generate public key to payment server

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
