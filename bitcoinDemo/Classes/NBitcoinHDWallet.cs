using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NBitcoin;
using NBitcoin.DataEncoders;

namespace BlockChainDemo.Classes
{
    class NBitcoinHDWallet
    {
        BitcoinAddress address;
       public void NBHDWallet()
        {
            //NBitcoin Hierarchical Deterministic Wallet
            //ExtKey will generate a Key for a corresponding ID,
            //ExtPubKey will generate a PubKey for a corresponding ID.

            byte[] byteArray = Encoders.Base58.DecodeData("KxPA5YkSzS46PW32cYZLTHBPGxidB3RP8efB4PbaKHojaUjMJrx3");
            string hex1 = Encoders.Hex.EncodeData(byteArray);

            ExtKey privateKey = new ExtKey("123fff"); //can use random hex seed for generate extkey and store it to db
            ExtPubKey pubKey = privateKey.Neuter(); //generate public key from ExtKey to payment server
            BitcoinExtPubKey wif = pubKey.GetWif(Network.Main);

            //bytearray to str and str to bytep[]
            //var byteStr = Convert.ToBase64String(pubKey.ChainCode);
           //byte[] bytes = System.Convert.FromBase64String(byteStr);

            //BitcoinSecret mainNetPrivateKey = privateKey.GetBitcoinSecret(Network.Main);  // get our private key for the mainnet <-- not working ?

            //Now, give the pubkey to your payment server
            //....
            //The payment server receive an order, note the server does not need the private key to generate the address

            uint orderID = 1001;
            address = pubKey.Derive(orderID).PubKey.GetAddress(Network.Main);
            Console.WriteLine(address);

            //Now on the server that have access to the private key, you get the private key from the orderID
            Key key = privateKey.Derive(orderID).PrivateKey;
            BitcoinSecret secret = key.GetBitcoinSecret(Network.Main);
            BitcoinSecret secret1 = new BitcoinSecret("L1Q3FRh73NffUdahsig3uQTj5raepff664DZtmXQEpzpCANiLiVV");
            bool matched = (secret == secret1);
            Console.WriteLine(secret); //Print a nice secret key string
        }
    }
}
