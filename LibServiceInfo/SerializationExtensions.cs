#region

using System;
using System.IO;
using System.IO.Compression;
using System.Runtime.Serialization;
using System.Xml;

#endregion

namespace LibServiceInfo
{
    public static class SerializationExtensions
    {
        public static string EncodeToBase64(string theString)
        {
            return Convert.ToBase64String(
                   System.Text.Encoding.UTF8.GetBytes(theString));
        }

        public static string DecodeBase64(string theString)
        {
            return System.Text.Encoding.UTF8.GetString(
                   Convert.FromBase64String(theString));
        }

        public static string GetZipBase64(string str2Zip)
        {
            byte[] aryBase64Zip = null;
            try
            {
                using (MemoryStream ms = new MemoryStream())
                {
                    using (GZipStream gzs = new GZipStream(ms, CompressionMode.Compress))
                    {
                        using (StreamWriter sw = new StreamWriter(gzs))
                        {
                            sw.Write(EncodeToBase64(str2Zip));
                        }
                    }
                    aryBase64Zip = ms.ToArray();
                }
            }
            catch (Exception ex)
            {
                return "Error Zipping and Encoding";
            }
            return Convert.ToBase64String(aryBase64Zip);
        }



        public static string GetUnZipUnBase64(string input)
        {
            string original_string = "";
            try
            {
                using (MemoryStream ms = new MemoryStream(Convert.FromBase64String(input)))
                {
                    using (GZipStream gzs = new GZipStream(ms, CompressionMode.Decompress))
                    {
                        using (StreamReader sr = new StreamReader(gzs))
                        {
                            original_string = DecodeBase64(sr.ReadToEnd());
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                return "Error Unziping and decoding";
            }
            return original_string;
        }

        public static string Serialize<T>(this T obj)
        {
            var serializer = new DataContractSerializer(obj.GetType());
            using (var writer = new StringWriter())
            using (var stm = new XmlTextWriter(writer))
            {
                serializer.WriteObject(stm, obj);
                return writer.ToString();
            }
        }

        public static string SerializeAndZip<T>(this T obj)
        {
            var serializer = new DataContractSerializer(obj.GetType());
            using (var writer = new StringWriter())
            using (var stm = new XmlTextWriter(writer))
            {
                serializer.WriteObject(stm, obj);
                return GetZipBase64(writer.ToString());
            }
        }

        public static T Deserialize<T>(this string serialized)
        {
            var serializer = new DataContractSerializer(typeof (T));
            using (var reader = new StringReader(serialized))
            using (var stm = new XmlTextReader(reader))
            {
                return (T) serializer.ReadObject(stm);
            }
        }

        public static T UnzipAndDeserialize<T>(this string serialized)
        {
            if (!String.IsNullOrEmpty(serialized))
            {
                
            
            var serializer = new DataContractSerializer(typeof(T));
            using (var reader = new StringReader(GetUnZipUnBase64(serialized)))
            using (var stm = new XmlTextReader(reader))
            {
                return (T)serializer.ReadObject(stm);
            }
            }
            else
            {
                return default(T);
            }
        }
    }
}