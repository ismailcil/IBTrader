using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Xml;
using System.Xml.Serialization;
using Newtonsoft.Json;
using IB.Core.Model;

namespace IB.Core.Extensions
{
    /// <summary>
    /// Summary description for GeneralExtensions
    /// </summary>
    public static class GeneralExtensions
    {
        private static readonly IFormatProvider FormatProviderEn = CultureInfo.CreateSpecificCulture("en-US");
        private static readonly IFormatProvider FormatProviderTr = CultureInfo.CreateSpecificCulture("tr-TR");
       
        public static string SerializeObject<T>(this object cls)
        {
            return JsonConvert.SerializeObject((T)cls);
        }
        
        public static T DeserializeObject<T>(this string jsonString)
        {
            return JsonConvert.DeserializeObject<T>(jsonString);
        }
        
        public static string ConvertFilename(object unconvertedOb)
        {
            if (unconvertedOb == null)
                return "";

            var unconverted = unconvertedOb.ToString();
            var chars = new string[13];
            var replaceChars = new string[13];

            chars[0] = "ş";
            replaceChars[0] = "s";

            chars[1] = "ğ";
            replaceChars[1] = "g";

            chars[2] = "ı";
            replaceChars[2] = "i";

            chars[3] = "ö";
            replaceChars[3] = "o";

            chars[4] = "ü";
            replaceChars[4] = "u";

            chars[5] = "ç";
            replaceChars[5] = "c";

            chars[6] = "Ş";
            replaceChars[6] = "S";

            chars[7] = "Ğ";
            replaceChars[7] = "G";

            chars[8] = "İ";
            replaceChars[8] = "i";

            chars[9] = "Ö";
            replaceChars[9] = "O";

            chars[10] = "Ü";
            replaceChars[10] = "U";

            chars[11] = "Ç";
            replaceChars[11] = "C";

            chars[12] = "I";
            replaceChars[12] = "i";


            for (var i = 0; i < chars.Length; i++)
            {
                unconverted = unconverted.Replace(chars[i], replaceChars[i]);
            }

            unconverted = unconverted.ToLower();

            unconverted = Regex.Replace(unconverted, "[^A-Za-z0-9]", " ");
            unconverted = Regex.Replace(unconverted, " +", " ");
            unconverted = unconverted.Trim();
            unconverted = Regex.Replace(unconverted, " ", "-");

            return unconverted;
        }
        
        private static string GetStringFormat(int f)
        {
            return "{0:N" + f + "}";
        }
        
        public static string ToStringWithFormatNumber(this decimal value, int format)
        {
            return string.Format(GetStringFormat(format), value);
        }

        private static string ToStringWithFormatNumber(this decimal value, int format, string culture)
        {
            var formatProvider = FormatProviderEn;
            if (culture == "TR")
                formatProvider = FormatProviderTr;
            return string.Format(formatProvider, GetStringFormat(format), value);
        }
        
        public static string ToStringWithFormatNumber(this double value, int format, string culture)
        {
            var formatProvider = FormatProviderEn;
            if (culture == "TR")
                formatProvider = FormatProviderTr;
            return string.Format(formatProvider, GetStringFormat(format), value);
        }
        
        public static string ToStringWithFormatNumber(this double value, int format)
        {
            var formatProvider = FormatProviderEn;
            return string.Format(formatProvider, GetStringFormat(format), value);
        }
        
        public static int ToInt(this string value)
        {
            var result = 0;

            if (!string.IsNullOrEmpty(value))
                int.TryParse(value, out result);

            return result;
        }
        
        public static int ToInt(this object value)
        {
            var result = 0;

            if (value != null)
                int.TryParse(Convert.ToString(value), out result);

            return result;
        }
        
        public static int ToInt(this object value, IFormatProvider format)
        {
            var result = 0;

            if (value != null)
                int.TryParse(Convert.ToString(value, format), out result);

            return result;
        }
        
        public static DateTime? GetDateTimeFromString(this string datetime, string format)
        {
            try
            {
                return DateTime.ParseExact(datetime, format, new CultureInfo("tr-TR", true));
            }
            catch
            {
                return null;
            }
        }
        
        public static string GetStringFromDateTime(this DateTime datetime, string format)
        {
            return datetime.ToString(format, new CultureInfo("tr-TR", true));
        }
        
        public static decimal ToDecimal(this string value, IFormatProvider fProvider = null)
        {
            decimal result = 0;
            if (fProvider == null)
                fProvider = FormatProviderEn;
            if (!string.IsNullOrEmpty(value))
                decimal.TryParse(value, NumberStyles.AllowDecimalPoint, fProvider, out result);

            return result;
        }
        
        public static decimal ToDecimalReplaceComma(this string value)
        {
            decimal result = 0;

            value = value.Replace(",", ".");

            if (!string.IsNullOrEmpty(value))
                decimal.TryParse(value, NumberStyles.Any, FormatProviderEn, out result);

            return result;
        }
        
        public static string ConvertCharTrToEn(this string body)
        {
            var sNews = body;

            sNews = sNews.Replace("ı", "I");
            sNews = sNews.Replace("i", "I");
            sNews = sNews.Replace("İ", "I");
            sNews = sNews.Replace("ğ", "g");
            sNews = sNews.Replace("Ğ", "G");
            sNews = sNews.Replace("ş", "S");
            sNews = sNews.Replace("Ş", "S");
            sNews = sNews.Replace("ü", "U");
            sNews = sNews.Replace("Ü", "U");
            sNews = sNews.Replace("ö", "O");
            sNews = sNews.Replace("Ö", "O");
            sNews = sNews.Replace("ç", "C");
            sNews = sNews.Replace("Ç", "C");

            return sNews;
        }
        
        public static string ToStringWithIcon(this double value, int format, string culture)
        {
            var sNews = Convert.ToDecimal(value).ToStringWithFormatNumber(format, culture);

            if (value > 0)
                sNews = "+" + Convert.ToDecimal(value).ToStringWithFormatNumber(format, culture);

            return sNews;
        }
        
        public static decimal ToDecimal(this object value)
        {
            decimal result = 0;
            if (value == null) return result;
            if (value != DBNull.Value)
            {
                result = Convert.ToDecimal(value);
            }

            return result;
        }
        
        public static double ToDouble(this object value)
        {
            try
            {
                double result = 0;
                if (value != null)
                {
                    if (value != DBNull.Value)
                    {
                        if (double.IsNaN(Convert.ToDouble(value)))
                            return 0;

                        result = Convert.ToDouble(value);
                    }
                }

                return result;
            }
            catch (Exception)
            {
                return 0;
            }
        }
        
        public static DateTime ToDateTime(this string value)
        {
            var result = new DateTime();
            if (!string.IsNullOrEmpty(value))
            {
                result = DateTime.Parse(value);
            }

            return result;
        }
        
        public static DateTime ToDateTime(this object value)
        {
            var result = new DateTime();
            if (value != null)
            {
                result = DateTime.Parse(Convert.ToString(value));
            }

            return result;
        }
        
        public static bool ToBool(this string value)
        {
            var result = false;

            if (!string.IsNullOrEmpty(value))
                bool.TryParse(value, out result);

            return result;
        }
        
        public static bool ToBool(this object value)
        {
            var result = false;

            if (value != null)
                bool.TryParse(Convert.ToString(value), out result);

            return result;
        }
        
        public static int ToSecond(this string text)
        {
            if (!string.IsNullOrEmpty(text))
            {
                try
                {
                    var dt = Convert.ToDateTime(text);
                    return new TimeSpan(dt.Hour, dt.Minute, dt.Second).TotalSeconds.ToInt();
                }
                catch (Exception)
                {
                    return 0;
                }            
            }
            else return 0;
        }

        //data table -> <T>
        public static IList<T> ConvertToList<T>(this DataTable dt) where T : class, new()
        {
            if (dt == null || dt.Rows.Count == 0) return null;
            return (from DataRow row in dt.Rows select ConvertDataRowToEntity<T>(row)).ToList();
        }
        
        //datarow -> <T>
        public static T ConvertDataRowToEntity<T>(this DataRow row) where T : class, new()
        {
            var objType = typeof(T);
            var obj = Activator.CreateInstance<T>();
            foreach (DataColumn column in row.Table.Columns)
            {
                var property =
                    objType.GetProperty(column.ColumnName,
                        BindingFlags.Public | BindingFlags.Instance | BindingFlags.IgnoreCase);
                if (property == null || !property.CanWrite)
                {
                    continue;
                }
                var value = row[column.ColumnName];
                if (value == DBNull.Value) value = null;
                try
                {
                    property.SetValue(obj, value, null);
                }
                catch (Exception ex)
                {
                    // ignored
                }
            }
            return obj;
        }
        
        public static string Right(this string str, int length)
        {
            return str.Substring(str.Length - length, length);
        }
        
        public static string InnerSerialize<T>(this T req)
        {
            var xs = new XmlSerializer(typeof(T));
            var ms = new MemoryStream();
            var ns = new XmlSerializerNamespaces();
            ns.Add("", "");
            xs.Serialize(ms, req,ns);
            var data = Encoding.UTF8.GetString(ms.ToArray());

            return data;
        }
        
        public static string DeSerializeXml(this object results)
        {
            if (results == null)
                return "";
            var memoryStream = new MemoryStream();
            var xs = new XmlSerializer(results.GetType());
            var xmlTextWriter = new XmlTextWriter(memoryStream, Encoding.UTF8);
            xs.Serialize(xmlTextWriter, results);
            var result = Encoding.UTF8.GetString(memoryStream.ToArray());
            return result;

        }
        
        //XML raw responsu , Response objesine deserialize eder.
        public static T InnerDeserialize<T>(this string raw, Encoding enc = null)
        {
            var xs = new XmlSerializer(typeof(T));
            var ms = new MemoryStream(enc != null ? enc.GetBytes(raw) : Encoding.UTF8.GetBytes(raw));
            var ret = xs.Deserialize(ms);
            return (T)ret;
        }
        
        public static DataTable AsDataTable<T>(this IEnumerable<T> data)
        {
            var properties = TypeDescriptor.GetProperties(typeof(T));
            var table = new DataTable();
            foreach (PropertyDescriptor prop in properties)
                table.Columns.Add(prop.Name, Nullable.GetUnderlyingType(prop.PropertyType) ?? prop.PropertyType);
            foreach (var item in data)
            {
                var row = table.NewRow();
                foreach (PropertyDescriptor prop in properties)
                    row[prop.Name] = prop.GetValue(item) ?? DBNull.Value;
                table.Rows.Add(row);
            }
            return table;
        }

        public static string ToSha256(this string sha256Data)
        {
            var sha = new SHA256CryptoServiceProvider();
            var hashedPassword = sha256Data;
            var hashbytes = Encoding.GetEncoding("ISO-8859-9").GetBytes(hashedPassword);
            var inputbytes = sha.ComputeHash(hashbytes);
            return GetHexaDecimal(inputbytes);
        }
        
        public static string ToMd5(this string input)
        {
            var enc = Encoding.Unicode.GetEncoder();

            var rawBytes = new byte[input.Length * 2];
            enc.GetBytes(input.ToCharArray(), 0, input.Length, rawBytes, 0, true);

            var md5 = new MD5CryptoServiceProvider();
            var result = md5.ComputeHash(rawBytes);

            var sb = new StringBuilder();
            foreach (var t in result)
            {
                sb.Append(t.ToString("X2"));
            }

            return sb.ToString();
        }
        
        private static string GetHexaDecimal(byte[] bytes)
        {
            var s = new StringBuilder();
            var length = bytes.Length;
            for (var n = 0; n <= length - 1; n++)
            {
                s.Append(String.Format("{0,2:x}", bytes[n]).Replace(" ", "0"));
            }
            return s.ToString();
        }
        
        public static string ReadTextFromFile(this string filePath)
        {
            try
            {
                var text = File.ReadAllText(filePath, Encoding.Default);// Encoding.GetEncoding("ISO-8859-9"));
                return text;
            }
            catch
            {
                return null;
            }
        }
        
        public static string ReturnExtension(this string fileExtension)
        {
            switch (fileExtension)
            {
                case "txt":
                    return "word";
                case "doc":
                    return "word";
                case "docx":
                    return "word";
                case "xls":
                    return "excel";
                case "xlsx":
                    return "excel";
                case "pdf":
                    return "pdf";
                default:
                    return null;
            }
        }
        
        public static string ConvertFilename(this string unconvertedOb)
        {
            if (unconvertedOb == null)
                return "";

            var unconverted = unconvertedOb;
            var chars = new string[13];
            var replaceChars = new string[13];

            chars[0] = "ş";
            replaceChars[0] = "s";

            chars[1] = "ğ";
            replaceChars[1] = "g";

            chars[2] = "ı";
            replaceChars[2] = "i";

            chars[3] = "ö";
            replaceChars[3] = "o";

            chars[4] = "ü";
            replaceChars[4] = "u";

            chars[5] = "ç";
            replaceChars[5] = "c";

            chars[6] = "Ş";
            replaceChars[6] = "S";

            chars[7] = "Ğ";
            replaceChars[7] = "G";

            chars[8] = "İ";
            replaceChars[8] = "i";

            chars[9] = "Ö";
            replaceChars[9] = "O";

            chars[10] = "Ü";
            replaceChars[10] = "U";

            chars[11] = "Ç";
            replaceChars[11] = "C";

            chars[12] = "I";
            replaceChars[12] = "i";

            for (var i = 0; i < chars.Length; i++)
            {
                unconverted = unconverted.Replace(chars[i], replaceChars[i]);
            }

            unconverted = unconverted.ToLower();

            unconverted = Regex.Replace(unconverted, "[^A-Za-z0-9]", " ");
            unconverted = Regex.Replace(unconverted, " +", " ");
            unconverted = unconverted.Trim();
            unconverted = Regex.Replace(unconverted, " ", "-");

            return unconverted;
        }
        
        public static string ReplaceMail(this List<MailBodyReplaceClass> Listitems, string mailBody)
        {
            return Listitems.Aggregate(mailBody, (current, item) => current.Replace("##" + item.FieldName + "##", item.FieldValue));
        }
        
        public static string SerializeData<T>(this T results)
        {
            if (results == null)
                return "";

            var objectXml = "";

            var serializer = new DataContractSerializer(typeof(T));
            using (var backing = new StringWriter())
            using (var writer = new XmlTextWriter(backing))
            {
                serializer.WriteObject(writer, results);
                objectXml = backing.ToString();
            }

            return objectXml;
        }
        
        //XML raw responsu , Response objesine deserialize eder.
        public static T DeserializeData<T>(this string data)
        {
            var serializer = new DataContractSerializer(typeof(T));
            using (var backing = new StringReader(data))
            using (var reader = new XmlTextReader(backing))
            {
                return (T)serializer.ReadObject(reader);
            }
        }
        
        public static DateTime ToDateTime(this long unixTimeStamp)
        {
            // Unix timestamp is seconds past epoch
            var dtDateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
            dtDateTime = dtDateTime.AddSeconds(unixTimeStamp).ToLocalTime();
            return dtDateTime;
        }
        
        public static long ToUnixTime(this DateTime dt)
        {
            return (long)(dt.ToUniversalTime().Subtract(new DateTime(1970, 1, 1))).TotalSeconds;
        }
        
        public static long ToJavaTime(this DateTime dt)
        {
            return (long)(dt.ToUniversalTime().Subtract(new DateTime(1970, 1, 1))).TotalSeconds * 1000;
        }
        
        public static Stream ToStream(this string str)
        {
            try
            {
                var stream = new MemoryStream();
                var writer = new StreamWriter(stream);
                writer.Write(str);
                writer.Flush();
                stream.Position = 0;
                return stream;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        
        public static void Merge<TKey, TValue>(this IDictionary<TKey, TValue> first, IDictionary<TKey, TValue> second)
        {
            if (second == null || first == null) return;
            foreach (var item in second.Where(item => !first.ContainsKey(item.Key)))
                first.Add(item.Key, item.Value);
        }

        private static byte[] GetCryData(byte[] originalData, byte[] key, byte[] iv)
        {
            var ms = new MemoryStream();
            var alg = Rijndael.Create();
            alg.Key = key;
            alg.IV = iv;
            var cs = new CryptoStream(ms,
                alg.CreateEncryptor(), CryptoStreamMode.Write);
            cs.Write(originalData, 0, originalData.Length);
            cs.Close();
            var crypData = ms.ToArray();
            return crypData;
        }
        
        private static byte[] GetEncData(byte[] cryData, byte[] key, byte[] iv)
        {
            var ms = new MemoryStream();
            var alg = Rijndael.Create();
            alg.Key = key;
            alg.IV = iv;
            var cs = new CryptoStream(ms, alg.CreateDecryptor(), CryptoStreamMode.Write);
            cs.Write(cryData, 0, cryData.Length);
            cs.Close();
            var encyData = ms.ToArray();
            return encyData;
        }
        
        public static string CrypData(this string forCrypData,string password)
        {
            var wCryptDataArray = System.Text.Encoding.Unicode.GetBytes(forCrypData);

            var pdb = new PasswordDeriveBytes(password, new byte[] {0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d,
                0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76});

            var lastCrypData = GetCryData(wCryptDataArray,
                pdb.GetBytes(32), pdb.GetBytes(16));

            return Convert.ToBase64String(lastCrypData);
        }
        
        public static string EncrData(this string text, string password)
        {
            var cryptDataArray = Convert.FromBase64String(text);

            var pdb = new PasswordDeriveBytes(password,
                new byte[] {0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65,
                    0x64, 0x76, 0x65, 0x64, 0x65, 0x76});

            var encrDataArray = GetEncData(cryptDataArray,
                pdb.GetBytes(32), pdb.GetBytes(16));

            return System.Text.Encoding.Unicode.GetString(encrDataArray);
        }

        public static string GetRandomPassWord(this string text, byte lenght)
        {
            var chars = new char[] {
                'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j', 'k', 'l', 'm', 'n', 'o', 'p', 'q', 'r', 's', 't', 'u', 'v', 'w', 'x', 'y', 'z',
                'A', 'B', 'C', 'D', 'E', 'F', 'F', 'H', 'I', 'J', 'K', 'L', 'M', 'N', 'O', 'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z',
                '0', '1', '2', '3', '4', '5', '6', '7', '8', '9'
            };
            var String = string.Empty;
            var random = new Random();

            for (byte a = 0; a < lenght; a++)
            {
                String += chars[random.Next(0, 62)];
            };

            return (String);
        }

        public static string ToXmlInnerText(this IEnumerable<XmlNode> xNode, string title)
        {
            var firstOrDefault = xNode.FirstOrDefault(n => n.Name == title);
            return firstOrDefault != null ? firstOrDefault.InnerText : "";
        }
    }
}


