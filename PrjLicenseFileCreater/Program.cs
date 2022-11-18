using License;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Xml;

namespace PrjLicenseFileCreater
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Enter Computer Code:");
            Console.WriteLine();

            var linestring_=Console.ReadLine();  //For when the user types something and presses enter.n

            var encrypted = Encrypt1("CHMA-1252-FSDFDSF","CHAMD");
            Console.WriteLine(encrypted);

            string readableString_=EncodeServerName(encrypted.ToString());
            Console.WriteLine(readableString_);


            Console.WriteLine(DecodeServerName(readableString_));

            Console.ReadLine();

            //Console.ReadLine();
            //for (int i = 0; i < 1000; i++)
            //{
            //    Thread.Sleep(2000);
            //    String ans = Console.ReadLine().ToString();
            //    if (ans == "exit")
            //    {
            //        break;
            //    }
            //    Console.WriteLine("Index is "+i.ToString()+DateTime.Now.ToString("MMM/dd/yyyy"));
            //}

            

            //Console.ReadKey(); //For it to close when someone presses any key, or:

            //------- generate xml documents

            XmlDocument doc = new XmlDocument();
            XmlNode docNode = doc.CreateXmlDeclaration("1.0", "UTF-8", null);
            doc.AppendChild(docNode);

            XmlNode productsNode = doc.CreateElement("products");
            doc.AppendChild(productsNode);

            XmlNode productNode = doc.CreateElement("product");
            XmlAttribute productAttribute = doc.CreateAttribute("id");
            productAttribute.Value = "01";
            productNode.Attributes.Append(productAttribute);
            productsNode.AppendChild(productNode);

            XmlNode nameNode = doc.CreateElement("Name");
            nameNode.AppendChild(doc.CreateTextNode("Java"));
            productNode.AppendChild(nameNode);
            XmlNode priceNode = doc.CreateElement("Price");
            priceNode.AppendChild(doc.CreateTextNode("Free"));
            productNode.AppendChild(priceNode);

            // Create and add another product node.
            productNode = doc.CreateElement("product");
            productAttribute = doc.CreateAttribute("id");
            productAttribute.Value = "02";
            productNode.Attributes.Append(productAttribute);
            productsNode.AppendChild(productNode);
            nameNode = doc.CreateElement("Name");
            nameNode.AppendChild(doc.CreateTextNode("C#"));
            productNode.AppendChild(nameNode);
            priceNode = doc.CreateElement("Price");
            priceNode.AppendChild(doc.CreateTextNode("Free"));
            productNode.AppendChild(priceNode);

            //doc.Save(Console.Out);
            //Console.ReadKey(); //For it to close when someone presses any key, or:
            doc.Save(@"c:\MyXL.slf");
            
            //----- end xml creater
        }



        static string convertToReadableText(string sb)
        {
            byte[] bytes;
            byte[] hasedbytes;
            //Task.WaitAll(task);
            bytes = System.Text.Encoding.UTF8.GetBytes(sb.ToString());
            hasedbytes = System.Security.Cryptography.SHA256.Create().ComputeHash(bytes);
            var key = Convert.ToBase64String(hasedbytes).Substring(25);
            var formatKey_ = System.Text.RegularExpressions.Regex.Replace(key, "[^a-zA-Z^0-9]", "0");
            var finalformatKey_ = System.Text.RegularExpressions.Regex.Replace(formatKey_, @".{5}(?!$)", "$0-");
            return (finalformatKey_).ToString();
        }

        public static string Encrypt1(string Text, string Key)
        {
            string initVector = "tu89geji340t89u2";
            int keysize = 256;

            byte[] initVectorBytes = Encoding.UTF8.GetBytes(initVector);
            byte[] plainTextBytes = Encoding.UTF8.GetBytes(Text);
            PasswordDeriveBytes password = new PasswordDeriveBytes(Key, null);
            byte[] keyBytes = password.GetBytes(keysize / 8);
            RijndaelManaged symmetricKey = new RijndaelManaged();
            symmetricKey.Mode = CipherMode.CBC;
            ICryptoTransform encryptor = symmetricKey.CreateEncryptor(keyBytes, initVectorBytes);
            MemoryStream memoryStream = new MemoryStream();
            CryptoStream cryptoStream = new CryptoStream(memoryStream, encryptor, CryptoStreamMode.Write);
            cryptoStream.Write(plainTextBytes, 0, plainTextBytes.Length);
            cryptoStream.FlushFinalBlock();
            byte[] Encrypted = memoryStream.ToArray();
            memoryStream.Close();
            cryptoStream.Close();
            return Convert.ToBase64String(Encrypted);
        }

       


        public static string EncodeServerName(string serverName)
        {
            //return Convert.ToBase64String(Encoding.UTF8.GetBytes(serverName));

            byte[] NameEncodein = new byte[serverName.Length];
            NameEncodein = System.Text.Encoding.UTF8.GetBytes(serverName);
            string EcodedName = Convert.ToBase64String(NameEncodein);
            return EcodedName;

        }

        public static string DecodeServerName(string encodedServername)
        {
            //return Encoding.UTF8.GetString(Convert.FromBase64String(encodedServername));
            System.Text.UTF8Encoding encoder = new System.Text.UTF8Encoding();
            System.Text.Decoder strDecoder = encoder.GetDecoder();
            byte[] to_DecodeByte = Convert.FromBase64String(encodedServername);
            int charCount = strDecoder.GetCharCount(to_DecodeByte, 0, to_DecodeByte.Length);
            char[] decoded_char = new char[charCount];
            strDecoder.GetChars(to_DecodeByte, 0, to_DecodeByte.Length, decoded_char, 0);
            string Name = new string(decoded_char);

            return Name;
        }

    }
}
