using System;
using System.Collections.Generic;
using System.Text;
using System.Collections.ObjectModel;
using System.IO;
using System.Collections;
using System.Xml;
using System.Text.RegularExpressions;
using System.Net;
using System.Web;

namespace mveEngine
{
    public class HtmlUtil
    {

        public static string Fetch(string url, Encoding code, bool UseProxy, int iteration)
        {
            try
            {
                int attempt = 0;
                while (attempt < iteration)
                {
                    attempt++;
                    try
                    {
                        HttpWebRequest req = (HttpWebRequest)HttpWebRequest.Create(url);
                        req.Timeout = 30000;
                        req.UserAgent = "Mozilla/5.0 (Windows; U; Windows NT 6.1; fr; rv:1.9.0.11) Gecko/2009060215 Firefox/3.0.11 (.NET CLR 3.5.30729)";
                        req.Headers["Accept-Language"] = "fr,fr-fr;q=0.8,en-us;q=0.5,en;q=0.3";

                        using (WebResponse resp = req.GetResponse())
                            try
                            {
                                using (Stream s = resp.GetResponseStream())
                                {
                                    string res = new StreamReader(s, code).ReadToEnd();
                                    return HttpUtility.HtmlDecode(res);
                                }
                            }
                            finally
                            {
                                resp.Close();
                            }
                    }
                    catch (WebException ex)
                    {
                    }
                    catch (IOException ex)
                    {
                    }
                }
            }
            catch (Exception ex)
            {
            }

            return String.Empty;
        }

        public static string Fetch(string url)
        {
            return Fetch(url, Encoding.UTF7, false, 2);
        }

        public static string Fetch(string url, Encoding code)
        {
            return Fetch(url, code, false, 2);
        }


        public static bool Download(string localFile, string remoteFile)
        {
            int bytesdone = 0;

            // Streams to read/write.
            Stream RStream = null;
            Stream LStream = null;

            // The respose of the web request.
            WebResponse response = null;
            try
            {
                // request the URL and get the response.
                WebRequest request = WebRequest.Create(remoteFile);
                if (request != null)
                {
                    response = request.GetResponse();
                    if (response != null)
                    {
                        // If we got a response lets KiB by KiB stream the 
                        // data into the temp file.
                        RStream = response.GetResponseStream();
                        LStream = File.Create(localFile);
                        byte[] buffer = new byte[1024];
                        int bytesRead;
                        do
                        {
                            bytesRead = RStream.Read(buffer, 0, buffer.Length);
                            LStream.Write(buffer, 0, bytesRead);
                            bytesdone += bytesRead;
                        }
                        while (bytesRead > 0);
                    }
                }
            }
            catch (Exception)
            {
                // We don't want error reporting here.
                bytesdone = 0;
            }
            finally
            {
                // Close out all of the streams.
                if (response != null)
                    response.Close();
                if (RStream != null)
                    RStream.Close();
                if (LStream != null)
                    LStream.Close();
            }

            if (bytesdone > 0)
            {
                // If we got it all, lets process the completed download.
                return true;
            }
            else
            {
                return false;

            }
        }

        public static bool RemoteFileExist(string url)
        {
            try
            {
                WebRequest serverRequest = WebRequest.Create(url);
                serverRequest.Timeout = 5000;
                //serverRequest.Method = "HEAD";
                WebResponse serverResponse;
                try //Try to get response from server
                {
                    serverResponse = serverRequest.GetResponse();
                }
                catch //If could not obtain any response
                {
                    return false;
                }
                finally
                {
                    serverRequest.Abort(); //Stop the request
                }
                return true;
            }
            catch { return false; }
        }

        public static string UrlEncode(string url)
        {
            return HttpUtility.UrlEncode(url);
        }

        public static string extractTag(string src, string tagStart, string tagEnd)
        {
            int beginIndex = src.IndexOf(tagStart);
            if (beginIndex < 0)
            {
                // logger.finest("extractTag value= Unknown");
                return string.Empty;
            }
            try
            {
                string subString = src.Substring(beginIndex + tagStart.Length);
                int endIndex = subString.IndexOf(tagEnd);
                if (endIndex < 0)
                {
                    // logger.finest("extractTag value= Unknown");
                    return string.Empty;
                }
                subString = subString.Substring(0, endIndex);


                //String value = HTMLTools.decodeHtml(subString.trim());
                string value = subString.Trim();
                // logger.finest("extractTag value=" + value);
                return value;
            }
            catch (Exception e)
            {
                Console.WriteLine("extractTag an exception occurred during tag extraction : " + e);
                return string.Empty;
            }
        }

        public static string removeOpenedHtmlTags(string src)
        {
            Regex htmlRegex = new Regex("^.*?>", RegexOptions.Compiled);
            Regex htmlRegex2 = new Regex("<.*?$", RegexOptions.Compiled);
            try
            {
                src = htmlRegex.Replace(src, string.Empty);
                //src = htmlRegex2.Replace(src, string.Empty);
            }
            catch { }
            return src;

        }

        public static string removeHtmlTags(string source)
        {
            if (string.IsNullOrEmpty(source)) return string.Empty;
            char[] array = new char[source.Length];
            int arrayIndex = 0;
            bool inside = false;

            for (int i = 0; i < source.Length; i++)
            {
                char let = source[i];
                if (let == '<')
                {
                    inside = true;
                    continue;
                }
                if (let == '>')
                {
                    inside = false;
                    continue;
                }
                if (!inside)
                {
                    array[arrayIndex] = let;
                    arrayIndex++;
                }
            }
            return new string(array, 0, arrayIndex);
        }

        public static string RemoveAddress(string text)
        {
            int index = text.IndexOf("http://");
            if (index == -1) return text;
            if (index == 0) return string.Empty;
            int end = text.IndexOf(" ", index);
            if (end == -1) return text.Substring(0, index - 1);
            string add = text.Substring(index, end - index - 1);
            return text.Replace(add, " ").Trim();
        }

        public static ArrayList extractTags(string src, string sectionStart, string sectionEnd, string startTag, string endTag)
        {
            ArrayList tags = new ArrayList();

            int index = src.IndexOf(sectionStart);
            if (index == -1)
            {
                return tags;
            }
            index += sectionStart.Length;
            int endIndex = src.IndexOf(sectionEnd, index);
            if (endIndex == -1)
            {
                return tags;
            }


            string sectionText = src.Substring(index, endIndex - index);

            int lastIndex = sectionText.Length;
            index = 0;
            int startLen = 0;
            int endLen = endTag.Length;

            if (startTag != null)
            {
                index = sectionText.IndexOf(startTag);
                startLen = startTag.Length;
            }

            while (index != -1)
            {
                index += startLen;
                endIndex = sectionText.IndexOf(endTag, index);

                if (endIndex == -1)
                {
                    endIndex = lastIndex;
                }
                string text = sectionText.Substring(index, endIndex - index);


                // replaceAll used because trim() does not trim unicode space
                //tags.Add(decodeHtml(text.Trim()).Replace("^[\\s\\p{Zs}\\p{Zl}\\p{Zp}]*\\b(.*)\\b[\\s\\p{Zs}\\p{Zl}\\p{Zp}]*$", "$1"));
                tags.Add(text.Trim().Replace("^[\\s\\p{Zs}\\p{Zl}\\p{Zp}]*\\b(.*)\\b[\\s\\p{Zs}\\p{Zl}\\p{Zp}]*$", "$1"));

                endIndex += endLen;
                if (endIndex > lastIndex)
                {
                    break;
                }
                if (startTag != null)
                {
                    index = sectionText.IndexOf(startTag, endIndex);

                }
                else
                {
                    index = endIndex;

                }
            }
            return tags;
        }

        public static List<string> extractAllTags(string src, string startTag, string endTag)
        {
            List<string> tags = new List<string>();

            string sectionText = src;

            int lastIndex = sectionText.Length;
            int index = 0;
            int startLen = 0;
            int endLen = endTag.Length;

            if (startTag != null)
            {
                index = sectionText.IndexOf(startTag);
                startLen = startTag.Length;
            }

            while (index != -1)
            {
                index += startLen;
                int endIndex = sectionText.IndexOf(endTag, index);

                if (endIndex == -1)
                {
                    endIndex = lastIndex;
                }
                string text = sectionText.Substring(index, endIndex - index);


                // replaceAll used because trim() does not trim unicode space
                //tags.Add(decodeHtml(text.Trim()).Replace("^[\\s\\p{Zs}\\p{Zl}\\p{Zp}]*\\b(.*)\\b[\\s\\p{Zs}\\p{Zl}\\p{Zp}]*$", "$1"));
                tags.Add(text.Trim().Replace("^[\\s\\p{Zs}\\p{Zl}\\p{Zp}]*\\b(.*)\\b[\\s\\p{Zs}\\p{Zl}\\p{Zp}]*$", "$1"));

                endIndex += endLen;
                if (endIndex > lastIndex)
                {
                    break;
                }
                if (startTag != null)
                {
                    index = sectionText.IndexOf(startTag, endIndex);

                }
                else
                {
                    index = endIndex;

                }
            }
            return tags;
        }
    }
}