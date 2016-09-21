using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Web;
using System.Web.Mvc;

namespace GoldenPalm.DAL
{
    public static class UtilWeb
    {
        //private static byte[] key = { };
        //private static byte[] IV = { 0x12, 0x34, 0x56, 0x78, 0x90, 0xab, 0xcd, 0xef };
        //private static byte[] _salt = Encoding.ASCII.GetBytes("o6806642kbM7c5");
        private static string sEncryptionKey;

        private static byte[] Key = { };
        private static byte[] Vector = { 146, 64, 191, 111, 23, 3, 113, 119, 231, 121, 112, 79, 32, 114, 156, 9 };


        private static ICryptoTransform EncryptorTransform, DecryptorTransform;
        private static System.Text.UTF8Encoding UTFEncoder;



        /// -------------- Two Utility Methods (not used but may be useful) -----------
        /// Generates an encryption key.
        static public byte[] GenerateEncryptionKey()
        {
            //Generate a Key.
            RijndaelManaged rm = new RijndaelManaged();
            rm.GenerateKey();
            return rm.Key;
        }

        /// Generates a unique encryption vector
        static public byte[] GenerateEncryptionVector()
        {
            //Generate a Vector
            RijndaelManaged rm = new RijndaelManaged();
            rm.GenerateIV();
            return rm.IV;
        }


        /// ----------- The commonly used methods ------------------------------    
        /// Encrypt some text and return a string suitable for passing in a URL.
        /// 

        public static string Encrypt(int? intValue, bool UseSession = true)
        {
            return Encrypt(Convert.ToString(intValue), UseSession);
        }

        public static string Encrypt(string TextValue, bool UseSession = true)
        {
            if (string.IsNullOrEmpty(TextValue))
                return "";
            else
                return ByteArrToString(EncryptByte(TextValue, UseSession));
        }

        /// Encrypt some text and return an encrypted byte array.
        public static byte[] EncryptByte(string TextValue, bool UseSession = true)
        {
            RijndaelManaged rm = new RijndaelManaged();
            sEncryptionKey = "abcdefgh";
            if (UseSession)
                sEncryptionKey = HttpContext.Current.Session.SessionID.Substring(0, 8);
            Key = System.Text.Encoding.UTF8.GetBytes(sEncryptionKey);
            EncryptorTransform = rm.CreateEncryptor(Key, Vector);
            DecryptorTransform = rm.CreateDecryptor(Key, Vector);

            //Used to translate bytes to text and vice versa
            UTFEncoder = new System.Text.UTF8Encoding();
            //Used to stream the data in and out of the CryptoStream.
            MemoryStream memoryStream = new MemoryStream();

            //Translates our text value into a byte array.
            Byte[] bytes = UTFEncoder.GetBytes(TextValue);


            //Create an encryptor and a decryptor using our encryption method, key, and vector.

            /*
             * We will have to write the unencrypted bytes to the stream,
             * then read the encrypted result back from the stream.
             */
            #region Write the decrypted value to the encryption stream
            CryptoStream cs = new CryptoStream(memoryStream, EncryptorTransform, CryptoStreamMode.Write);
            cs.Write(bytes, 0, bytes.Length);
            cs.FlushFinalBlock();
            #endregion

            #region Read encrypted value back out of the stream
            memoryStream.Position = 0;
            byte[] encrypted = new byte[memoryStream.Length];
            memoryStream.Read(encrypted, 0, encrypted.Length);
            #endregion

            //Clean up.
            cs.Close();
            memoryStream.Close();

            return encrypted;
        }

        /// The other side: Decryption methods

        public static string Decrypt(int intValue, bool UseSession = true)
        {
            return Decrypt(Convert.ToString(intValue), UseSession);
        }

        public static string Decrypt(string EncryptedString, bool UseSession = true)
        {
            if (string.IsNullOrEmpty(EncryptedString))
                return "";
            else
                return DecryptByte(StrToByteArray(EncryptedString), UseSession);
        }

        /// Decryption when working with byte arrays.    
        public static string DecryptByte(byte[] EncryptedValue, bool UseSession = true)
        {
            #region Write the encrypted value to the decryption stream
            RijndaelManaged rm = new RijndaelManaged();
            sEncryptionKey = "abcdefgh";
            if (UseSession)
                sEncryptionKey = HttpContext.Current.Session.SessionID.Substring(0, 8);
            Key = System.Text.Encoding.UTF8.GetBytes(sEncryptionKey);
            EncryptorTransform = rm.CreateEncryptor(Key, Vector);
            DecryptorTransform = rm.CreateDecryptor(Key, Vector);

            //Used to translate bytes to text and vice versa
            UTFEncoder = new System.Text.UTF8Encoding();
            MemoryStream encryptedStream = new MemoryStream();


            //Create an encryptor and a decryptor using our encryption method, key, and vector.

            CryptoStream decryptStream = new CryptoStream(encryptedStream, DecryptorTransform, CryptoStreamMode.Write);
            decryptStream.Write(EncryptedValue, 0, EncryptedValue.Length);
            decryptStream.FlushFinalBlock();
            #endregion

            #region Read the decrypted value from the stream.
            encryptedStream.Position = 0;
            Byte[] decryptedBytes = new Byte[encryptedStream.Length];
            encryptedStream.Read(decryptedBytes, 0, decryptedBytes.Length);
            encryptedStream.Close();
            #endregion
            return UTFEncoder.GetString(decryptedBytes);
        }

        /// Convert a string to a byte array.  NOTE: Normally we'd create a Byte Array from a string using an ASCII encoding (like so).
        //      System.Text.ASCIIEncoding encoding = new System.Text.ASCIIEncoding();
        //      return encoding.GetBytes(str);
        // However, this results in character values that cannot be passed in a URL.  So, instead, I just
        // lay out all of the byte values in a long string of numbers (three per - must pad numbers less than 100).
        public static byte[] StrToByteArray(string str)
        {
            if (str.Length == 0)
                throw new Exception("Invalid string value in StrToByteArray");

            byte val;
            byte[] byteArr = new byte[str.Length / 3];
            int i = 0;
            int j = 0;
            do
            {
                val = byte.Parse(str.Substring(i, 3));
                byteArr[j++] = val;
                i += 3;
            }
            while (i < str.Length);
            return byteArr;
        }

        // Same comment as above.  Normally the conversion would use an ASCII encoding in the other direction:
        //      System.Text.ASCIIEncoding enc = new System.Text.ASCIIEncoding();
        //      return enc.GetString(byteArr);    
        public static string ByteArrToString(byte[] byteArr)
        {
            byte val;
            string tempStr = "";
            for (int i = 0; i <= byteArr.GetUpperBound(0); i++)
            {
                val = byteArr[i];
                if (val < (byte)10)
                    tempStr += "00" + val.ToString();
                else if (val < (byte)100)
                    tempStr += "0" + val.ToString();
                else
                    tempStr += val.ToString();
            }
            return tempStr;
        }

        public static SelectList getSelectedValue(List<ListColumn> list)
        {

            IEnumerable<SelectListItem> typeItems = list.Select(x => new SelectListItem
            {
                Value = x.value.ToString(),

            });

            return new SelectList(typeItems, "Value");

        }



        public static SelectList getSelectList(List<ListColumn> list)
        {

            IEnumerable<SelectListItem> typeItems = list.Select(x => new SelectListItem
            {
                Value = x.value.ToString(),
                Text = x.text,
            });


            return new SelectList(typeItems, "Value", "Text");

        }

        public static IEnumerable<SelectListItem> ToSelectList(List<ListColumn> list)
        {
            bool isSelected = list.Count == 1 ? true : false;


            IEnumerable<SelectListItem> typeItems = list.Select(x => new SelectListItem
            {
                Value = x.value.ToString(),
                Text = x.text,
                Selected = isSelected
            });

            return typeItems;


        }

        public static SelectList getSelectListEncrypt(List<ListColumn> list)
        {

            IEnumerable<SelectListItem> typeItems = list.Select(x => new SelectListItem
            {
                Value = Encrypt(x.value.ToString()),
                Text = x.text
            });

            return new SelectList(typeItems, "Value", "Text");


        }
        public static SelectList getSelectListEncrypt(SelectList list)
        {

            IEnumerable<SelectListItem> typeItems = list.Select(x => new SelectListItem
            {
                Value = Encrypt(x.Value.ToString()),
                Text = x.Text
            });

            return new SelectList(typeItems, "Value", "Text");


        }

        public static SelectList getSelectList(string MasterCode, bool SeqAsValue = true)
        {
            string value = "CodeDetailSeq";

            if (!SeqAsValue)
                value = "CodeDetail";

            return new SelectList(Codes.GetAllCode(MasterCode), value, "CodeDetailText");

        }

        public static SelectList getSelectList(string MasterCode, string Level1Seq, bool SeqAsValue = true)
        {
            string value = "CodeDetailSeq";

            if (!SeqAsValue)
                value = "CodeDetail";

            return new SelectList(Codes.GetAllCode(MasterCode, Convert.ToInt32(Level1Seq)), value, "CodeDetailText");

        }


        public static T postWebApi<T>(object data, Uri webApiUrl)
        {
            // Create a WebClient to POST the request
            WebClient client = new WebClient();

            // Set the header so it knows we are sending JSON
            client.Headers[HttpRequestHeader.ContentType] = "application/json";

            // Serialise the data we are sending in to JSON
            string serialisedData = JsonConvert.SerializeObject(data);

            // Make the request
            var response = client.UploadString(webApiUrl, serialisedData);

            // Deserialise the response into a GUID
            return JsonConvert.DeserializeObject<T>(response);
        }




    }







}



