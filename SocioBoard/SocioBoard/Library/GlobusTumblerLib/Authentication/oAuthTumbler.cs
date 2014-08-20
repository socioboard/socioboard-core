using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;
using System.Text;
using System.Net;
using System.Collections.Specialized;
using System.Security.Cryptography;

namespace GlobusTumblrLib.Authentication
{
    public class oAuthTumbler
    {

        public oAuthTumbler()
    {
        //
        // TODO: Add constructor logic here
        //
    }


        #region Variables

        //Appropriate endpoints of Tumblr api. Not to be changed.
        public const string REQUEST_TOKEN = "http://www.tumblr.com/oauth/request_token";
        public const string AUTHORIZE = "http://www.tumblr.com/oauth/authorize";
        public const string ACCESS_TOKEN = "http://www.tumblr.com/oauth/access_token";
        public string CallBackUrl = "";

        private static string _consumerKey = "";
        private static string _consumerSecret = "";
        private string _oauthVerifier = "";
        private static string _token = "";
        private static string _tokenSecret = "";

        protected Random random = new Random();

        protected const string OAuthVersion = "1.0";
        protected const string OAuthParameterPrefix = "oauth_";

        //
        // List of know and used oauth parameters' names
        //        
        protected const string OAuthConsumerKeyKey = "oauth_consumer_key";
        protected const string OAuthCallbackKey = "oauth_callback";
        protected const string OAuthVersionKey = "oauth_version";
        protected const string OAuthSignatureMethodKey = "oauth_signature_method";
        protected const string OAuthSignatureKey = "oauth_signature";
        protected const string OAuthTimestampKey = "oauth_timestamp";
        protected const string OAuthNonceKey = "oauth_nonce";
        protected const string OAuthTokenKey = "oauth_token";
        protected const string OAuthTokenSecretKey = "oauth_token_secret";
        protected const string OAuthVerifierKey = "oauth_verifier";

        protected const string HMACSHA1SignatureType = "HMAC-SHA1";
        protected const string PlainTextSignatureType = "PLAINTEXT";
        protected const string RSASHA1SignatureType = "RSA-SHA1";

        protected string unreservedChars = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-_.~";

        #endregion

        #region Properties

        public static string TumblrConsumerKey
        {
            get
            {
                return _consumerKey;
            }
            set { _consumerKey = value; }
        }

        public static string TumblrConsumerSecret
        {
            get
            {
                return _consumerSecret;
            }
            set { _consumerSecret = value; }
        }

        public static string TumblrToken
        {
            get { return _token; }
            set { _token = value; }
        }

        public static string TumblrTokenSecret
        {
            get { return _tokenSecret; }
            set { _tokenSecret = value; }
        }

        public string TumblrCallBackUrl
        {
            get { return CallBackUrl; }
            set { CallBackUrl = value; }
        }

        public string TumblrOAuthVerifier
        {
            get { return _oauthVerifier; }
            set { _oauthVerifier = value; }
        }

        #endregion

        #region Enum

        public enum Method
        {
            GET,
            POST,
            DELETE
        }

        /// <summary>
        /// Provides a predefined set of algorithms that are supported officially by the protocol
        /// </summary>
        public enum SignatureTypes
        {
            HMACSHA1,
            PLAINTEXT,
            RSASHA1
        }

        #endregion

        #region Nested type: QueryParameter

        /// <summary>
        /// Provides an internal structure to sort the query parameter
        /// </summary>
        protected class QueryParameter
        {
            private readonly string name;
            private readonly string value;

            public QueryParameter(string name, string value)
            {
                this.name = name;
                this.value = value;
            }

            public string Name
            {
                get { return name; }
            }

            public string Value
            {
                get { return value; }
            }
        }

        #endregion

        #region Nested type: QueryParameterComparer

        /// <summary>
        /// Comparer class used to perform the sorting of the query parameters
        /// </summary>
        protected class QueryParameterComparer : IComparer<QueryParameter>
        {
            #region IComparer<QueryParameter> Members

            public int Compare(QueryParameter x, QueryParameter y)
            {
                if (x.Name == y.Name)
                {
                    return string.Compare(x.Value, y.Value);
                }
                else
                {
                    return string.Compare(x.Name, y.Name);
                }
            }

            #endregion
        }

        #endregion

        #region Encode Methods/Variables

        private const string UpperCase = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
        private const string LowerCase = "abcdefghijklmnopqrstuvwxyz";
        private const string Digits = "1234567890";
        private static readonly char[] ValidChars = (UpperCase + LowerCase + Digits + "-._~").ToCharArray();

        private static readonly byte[] percentExtend = System.Text.Encoding.UTF8.GetBytes("25");
        private static readonly byte[] TildeExtend = System.Text.Encoding.UTF8.GetBytes("%7E");

        public static string UrlEncode(string toEncode)
        {
            var osb = new StringBuilder();
            UrlEncode(osb, toEncode);
            return osb.ToString();
        }

        public static string UrlEncode(string toEncode, bool encode)
        {
            var osb = new StringBuilder();
            UrlEncode(osb, toEncode, encode);
            return osb.ToString();
        }

        public static void UrlEncode(Stream osb, String ToEncode)
        {
            for (int Index = 0; Index < ToEncode.Length; Index++)
            {
                char Test = ToEncode[Index];
                if ((Test >= 'A' && Test <= 'Z') ||
                    (Test >= 'a' && Test <= 'z') ||
                    (Test >= '0' && Test <= '9'))
                {
                    osb.WriteByte((byte)Test);
                }
                else if (Test == '-' || Test == '_' || Test == '.' || Test == '~')
                {
                    osb.WriteByte((byte)Test);
                }
                else
                {
                    osb.WriteByte((byte)'%');
                    osb.Write(System.Text.Encoding.UTF8.GetBytes(string.Format("{0:X2}", (int)Test)), 0, 2);
                }
            }
        }

        public static void UrlEncode(StringBuilder osb, String ToEncode)
        {
            UrlEncode(osb, ToEncode, false);
        }

        public static void UrlEncode(StringBuilder osb, String ToEncode, bool encode)
        {
            for (int Index = 0; Index < ToEncode.Length; Index++)
            {
                char Test = ToEncode[Index];
                if ((Test >= 'A' && Test <= 'Z') ||
                    (Test >= 'a' && Test <= 'z') ||
                    (Test >= '0' && Test <= '9'))
                {
                    osb.Append(Test);
                }
                else if (Test == '-' || Test == '_' || Test == '.' || Test == '~')
                {
                    osb.Append(Test);
                }
                else if (Test == '%' && ToEncode[Index + 1] == '2' && ToEncode[Index + 2] == '5')
                {
                    osb.Append("%25");
                    Index += 2;
                    continue;
                }
                else
                {
                    if (encode)
                    {
                        osb.Append("%25");
                    }
                    else
                    {
                        osb.Append("%");
                    }
                    osb.AppendFormat("{0:X2}", (int)Test);
                }
            }
        }

        public static void EncodeSigStream(StringBuilder osb, byte[] buffer)
        {
            for (var x = 0; x < buffer.Length; x++)
            {
                byte b = (byte)buffer[x];
                if (ValidChars.Contains((char)b) && (((char)b) != '%'))
                {
                    osb.Append((char)b);
                }
                else
                {
                    if (osb[osb.Length - 1] == '%' && ((char)b) == '%')
                    {
                        osb.Append("25%");
                    }
                    else
                    {
                        osb.AppendFormat("%25{0:X2}", (int)b);
                    }
                }
            }
        }

        public static void EncodeBodyStream(Stream osb, byte[] buffer)
        {
            int i = -1, last = -1;
            char c;
            byte[] pieces;
            for (var x = 0; x < buffer.Length; x++)
            {
                i = buffer[x];
                c = (char)i;
                pieces = System.Text.Encoding.UTF8.GetBytes(new[] { c });

                if (last == 37)
                {
                    osb.Write(percentExtend, 0, 2);
                }

                if (pieces[0] == 37 && (pieces.Length > 1 && pieces[1] == 37))
                {
                    osb.Write(System.Text.Encoding.UTF8.GetBytes("%25%"), 0, 4);
                }
                else if (!ValidChars.Contains(c) && c != '~' && c != '%')
                {
                    var res = string.Format("%{0:X2}", i);
                    if (res == "%20")
                    {
                        osb.WriteByte((byte)'+');
                    }
                    else
                    {
                        osb.Write(System.Text.Encoding.UTF8.GetBytes(res), 0, 3);
                    }
                }
                else
                {
                    if (c == '~')
                    {
                        osb.Write(TildeExtend, 0, 3);
                    }
                    else
                    {
                        osb.WriteByte((byte)i);
                    }
                }

                last = pieces.Last();
            }
        }

        #endregion

        #region Basic Signature Methods

        private static string GetSignature(string url, string method, string nonce, string timestamp, string token, string tokenSecret, Dictionary<string, object> parameters)
        {
            var dict = new Dictionary<string, object>();
            dict.Add("oauth_consumer_key", TumblrConsumerKey);
            dict.Add("oauth_nonce", nonce.ToString());
            dict.Add("oauth_signature_method", "HMAC-SHA1");
            dict.Add("oauth_timestamp", timestamp);
            dict.Add("oauth_token", token);
            dict.Add("oauth_version", "1.0");
            var sigBase = new StringBuilder();
            var first = true;
            foreach (var d in (parameters == null ? dict : dict.Union(parameters)).OrderBy(p => p.Key))
            {
                if (!first)
                {
                    sigBase.Append("&");
                }
                first = false;
                if (d.Key.StartsWith("data"))
                {
                    sigBase.Append(d.Key);
                }
                else
                {
                    UrlEncode(sigBase, d.Key);
                }
                sigBase.Append("=");
                if (d.Value is byte[])
                {
                    EncodeSigStream(sigBase, (byte[])d.Value);
                }
                else
                {
                    UrlEncode(sigBase, d.Value.ToString());
                }
            }

            String SigBaseString = method.ToUpper() + "&";
            SigBaseString += UrlEncode(url) + "&" + UrlEncode(sigBase.ToString(), false);

            var keyMaterial = Encoding.UTF8.GetBytes(TumblrConsumerSecret + "&" + tokenSecret);
            var HmacSha1Provider = new System.Security.Cryptography.HMACSHA1 { Key = keyMaterial };
            return Convert.ToBase64String(HmacSha1Provider.ComputeHash(Encoding.UTF8.GetBytes(SigBaseString)));
        }

        /// <summary>
        /// Generate the timestamp for the signature        
        /// </summary>
        /// <returns></returns>
        public virtual string GenerateTimeStamp()
        {
            // Default implementation of UNIX time of the current UTC time
            TimeSpan ts = DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0, 0);
            return Convert.ToInt64(ts.TotalSeconds).ToString();
        }

        /// <summary>
        /// Generate a nonce
        /// </summary>
        /// <returns></returns>
        public virtual string GenerateNonce()
        {
            // Just a simple implementation of a random number between 123400 and 9999999
            return random.Next(123400, 9999999).ToString();
        }

        /// <summary>
        /// Generates a signature using the HMAC-SHA1 algorithm
        /// </summary>		
        /// <param name="url">The full url that needs to be signed including its non OAuth url parameters</param>
        /// <param name="consumerKey">The consumer key</param>
        /// <param name="consumerSecret">The consumer seceret</param>
        /// <param name="token">The token, if available. If not available pass null or an empty string</param>
        /// <param name="tokenSecret">The token secret, if available. If not available pass null or an empty string</param>
        /// <param name="callBackUrl">The callback URL (for OAuth 1.0a).If your client cannot accept callbacks, the value MUST be 'oob' </param>
        /// <param name="oauthVerifier">This value MUST be included when exchanging Request Tokens for Access Tokens. Otherwise pass a null or an empty string</param>
        /// <param name="httpMethod">The http method used. Must be a valid HTTP method verb (POST,GET,PUT, etc)</param>
        /// <returns>A base64 string of the hash value</returns>
        public string GenerateSignature(Uri url, string consumerKey, string consumerSecret, string token,
                                        string tokenSecret, string callBackUrl, string oauthVerifier, string httpMethod,
                                        string timeStamp, string nonce, out string normalizedUrl,
                                        out string normalizedRequestParameters)
        {
            return GenerateSignature(url, consumerKey, consumerSecret, token, tokenSecret, callBackUrl, oauthVerifier,
                                     httpMethod, timeStamp, nonce, SignatureTypes.HMACSHA1, out normalizedUrl,
                                     out normalizedRequestParameters);
        }

        /// <summary>
        /// Generates a signature using the specified signatureType 
        /// </summary>		
        /// <param name="url">The full url that needs to be signed including its non OAuth url parameters</param>
        /// <param name="consumerKey">The consumer key</param>
        /// <param name="consumerSecret">The consumer seceret</param>
        /// <param name="token">The token, if available. If not available pass null or an empty string</param>
        /// <param name="tokenSecret">The token secret, if available. If not available pass null or an empty string</param>
        /// <param name="callBackUrl">The callback URL (for OAuth 1.0a).If your client cannot accept callbacks, the value MUST be 'oob' </param>
        /// <param name="oauthVerifier">This value MUST be included when exchanging Request Tokens for Access Tokens. Otherwise pass a null or an empty string</param>
        /// <param name="httpMethod">The http method used. Must be a valid HTTP method verb (POST,GET,PUT, etc)</param>
        /// <param name="signatureType">The type of signature to use</param>
        /// <returns>A base64 string of the hash value</returns>
        public string GenerateSignature(Uri url, string consumerKey, string consumerSecret, string token,
                                        string tokenSecret, string callBackUrl, string oauthVerifier, string httpMethod,
                                        string timeStamp, string nonce, SignatureTypes signatureType,
                                        out string normalizedUrl, out string normalizedRequestParameters)
        {
            normalizedUrl = null;
            normalizedRequestParameters = null;

            switch (signatureType)
            {
                case SignatureTypes.PLAINTEXT:
                    return HttpUtility.UrlEncode(string.Format("{0}&{1}", consumerSecret, tokenSecret));
                case SignatureTypes.HMACSHA1:
                    string signatureBase = GenerateSignatureBase(url, consumerKey, token, tokenSecret, callBackUrl,
                                                                 oauthVerifier, httpMethod, timeStamp, nonce,
                                                                 HMACSHA1SignatureType, out normalizedUrl,
                                                                 out normalizedRequestParameters);

                    var hmacsha1 = new HMACSHA1();
                    hmacsha1.Key =
                        Encoding.ASCII.GetBytes(string.Format("{0}&{1}", BaseUrlEncode(consumerSecret),
                                                              string.IsNullOrEmpty(tokenSecret)
                                                                  ? ""
                                                                  : BaseUrlEncode(tokenSecret)));

                    return GenerateSignatureUsingHash(signatureBase, hmacsha1);
                case SignatureTypes.RSASHA1:
                    throw new NotImplementedException();
                default:
                    throw new ArgumentException("Unknown signature type", "signatureType");
            }
        }


        /// <summary>
        /// Helper function to compute a hash value
        /// </summary>
        /// <param name="hashAlgorithm">The hashing algoirhtm used. If that algorithm needs some initialization, like HMAC and its derivatives, they should be initialized prior to passing it to this function</param>
        /// <param name="data">The data to hash</param>
        /// <returns>a Base64 string of the hash value</returns>
        private string ComputeHash(HashAlgorithm hashAlgorithm, string data)
        {
            if (hashAlgorithm == null)
            {
                throw new ArgumentNullException("hashAlgorithm");
            }

            if (string.IsNullOrEmpty(data))
            {
                throw new ArgumentNullException("data");
            }

            byte[] dataBuffer = Encoding.ASCII.GetBytes(data);
            byte[] hashBytes = hashAlgorithm.ComputeHash(dataBuffer);

            return Convert.ToBase64String(hashBytes);
        }

        /// <summary>
        /// Internal function to cut out all non oauth query string parameters (all parameters not begining with "oauth_")
        /// </summary>
        /// <param name="parameters">The query string part of the Url</param>
        /// <returns>A list of QueryParameter each containing the parameter name and value</returns>
        private List<QueryParameter> GetQueryParameters(string parameters)
        {
            if (parameters.StartsWith("?"))
            {
                parameters = parameters.Remove(0, 1);
            }

            var result = new List<QueryParameter>();

            if (!string.IsNullOrEmpty(parameters))
            {
                string[] p = parameters.Split('&');
                foreach (string s in p)
                {
                    if (!string.IsNullOrEmpty(s) && !s.StartsWith(OAuthParameterPrefix))
                    {
                        if (s.IndexOf('=') > -1)
                        {
                            string[] temp = s.Split('=');
                            result.Add(new QueryParameter(temp[0], temp[1]));
                        }
                        else
                        {
                            result.Add(new QueryParameter(s, string.Empty));
                        }
                    }
                }
            }

            return result;
        }

        /// <summary>
        /// This is a different Url Encode implementation since the default .NET one outputs the percent encoding in lower case.
        /// While this is not a problem with the percent encoding spec, it is used in upper case throughout OAuth
        /// </summary>
        /// <param name="value">The value to Url encode</param>
        /// <returns>Returns a Url encoded string</returns>
        public string BaseUrlEncode(string value)
        {
            var result = new StringBuilder();

            foreach (char symbol in value)
            {
                if (unreservedChars.IndexOf(symbol) != -1)
                {
                    result.Append(symbol);
                }
                else
                {
                    result.Append('%' + String.Format("{0:X2}", (int)symbol));
                }
            }

            return result.ToString();
        }

        /// <summary>
        /// Normalizes the request parameters according to the spec
        /// </summary>
        /// <param name="parameters">The list of parameters already sorted</param>
        /// <returns>a string representing the normalized parameters</returns>
        protected string NormalizeRequestParameters(IList<QueryParameter> parameters)
        {
            var sb = new StringBuilder();
            QueryParameter p = null;
            for (int i = 0; i < parameters.Count; i++)
            {
                p = parameters[i];
                sb.AppendFormat("{0}={1}", p.Name, p.Value);

                if (i < parameters.Count - 1)
                {
                    sb.Append("&");
                }
            }

            return sb.ToString();
        }

        /// <summary>
        /// Generate the signature base that is used to produce the signature
        /// </summary>
        /// <param name="url">The full url that needs to be signed including its non OAuth url parameters</param>
        /// <param name="consumerKey">The consumer key</param>        
        /// <param name="token">The token, if available. If not available pass null or an empty string</param>
        /// <param name="tokenSecret">The token secret, if available. If not available pass null or an empty string</param>
        /// <param name="callBackUrl">The callback URL (for OAuth 1.0a).If your client cannot accept callbacks, the value MUST be 'oob' </param>
        /// <param name="oauthVerifier">This value MUST be included when exchanging Request Tokens for Access Tokens. Otherwise pass a null or an empty string</param>
        /// <param name="httpMethod">The http method used. Must be a valid HTTP method verb (POST,GET,PUT, etc)</param>
        /// <param name="signatureType">The signature type. To use the default values use <see cref="OAuthBase.SignatureTypes">OAuthBase.SignatureTypes</see>.</param>
        /// <returns>The signature base</returns>
        public string GenerateSignatureBase(Uri url, string consumerKey, string token, string tokenSecret,
                                            string callBackUrl, string oauthVerifier, string httpMethod,
                                            string timeStamp, string nonce, string signatureType,
                                            out string normalizedUrl, out string normalizedRequestParameters)
        {
            if (token == null)
            {
                token = string.Empty;
            }

            if (tokenSecret == null)
            {
                tokenSecret = string.Empty;
            }

            if (string.IsNullOrEmpty(consumerKey))
            {
                throw new ArgumentNullException("consumerKey");
            }

            if (string.IsNullOrEmpty(httpMethod))
            {
                throw new ArgumentNullException("httpMethod");
            }

            if (string.IsNullOrEmpty(signatureType))
            {
                throw new ArgumentNullException("signatureType");
            }

            normalizedUrl = null;
            normalizedRequestParameters = null;

            List<QueryParameter> parameters = GetQueryParameters(url.Query);
            parameters.Add(new QueryParameter(OAuthVersionKey, OAuthVersion));
            parameters.Add(new QueryParameter(OAuthNonceKey, nonce));
            parameters.Add(new QueryParameter(OAuthTimestampKey, timeStamp));
            parameters.Add(new QueryParameter(OAuthSignatureMethodKey, signatureType));
            parameters.Add(new QueryParameter(OAuthConsumerKeyKey, consumerKey));

            if (!string.IsNullOrEmpty(callBackUrl))
            {
                parameters.Add(new QueryParameter(OAuthCallbackKey, BaseUrlEncode(callBackUrl)));
            }


            if (!string.IsNullOrEmpty(oauthVerifier))
            {
                parameters.Add(new QueryParameter(OAuthVerifierKey, oauthVerifier));
            }

            if (!string.IsNullOrEmpty(token))
            {
                parameters.Add(new QueryParameter(OAuthTokenKey, token));
            }

            parameters.Sort(new QueryParameterComparer());

            normalizedUrl = string.Format("{0}://{1}", url.Scheme, url.Host);
            if (!((url.Scheme == "http" && url.Port == 80) || (url.Scheme == "https" && url.Port == 443)))
            {
                normalizedUrl += ":" + url.Port;
            }
            normalizedUrl += url.AbsolutePath;
            normalizedRequestParameters = NormalizeRequestParameters(parameters);

            var signatureBase = new StringBuilder();
            signatureBase.AppendFormat("{0}&", httpMethod.ToUpper());
            signatureBase.AppendFormat("{0}&", BaseUrlEncode(normalizedUrl));
            signatureBase.AppendFormat("{0}", BaseUrlEncode(normalizedRequestParameters));

            return signatureBase.ToString();
        }

        /// <summary>
        /// Generate the signature value based on the given signature base and hash algorithm
        /// </summary>
        /// <param name="signatureBase">The signature based as produced by the GenerateSignatureBase method or by any other means</param>
        /// <param name="hash">The hash algorithm used to perform the hashing. If the hashing algorithm requires initialization or a key it should be set prior to calling this method</param>
        /// <returns>A base64 string of the hash value</returns>
        public string GenerateSignatureUsingHash(string signatureBase, HashAlgorithm hash)
        {
            return ComputeHash(hash, signatureBase);
        }

        #endregion

        /// <summary>
        /// Get the link to Twitter's authorization page for this application.
        /// </summary>
        /// <returns>The url with a valid request token, or a null string.</returns>
        public string GetAuthorizationLink()
        {
            string ret = null;
            _token = "";
            _tokenSecret = "";
            string response = TumblrWebRequest(Method.GET, REQUEST_TOKEN, String.Empty);
            if (response.Length > 0)
            {
                //response contains token and token secret.  We only need the token.
                NameValueCollection qs = HttpUtility.ParseQueryString(response);

                if (qs["oauth_callback_confirmed"] != null)
                {
                    if (qs["oauth_callback_confirmed"] != "true")
                    {
                        throw new Exception("OAuth callback not confirmed.");
                    }
                }

                if (qs["oauth_token"] != null)
                {
                    _token = qs["oauth_token"];
                    _tokenSecret = qs["oauth_token_secret"];
                    ret = AUTHORIZE + "?oauth_token=" + qs["oauth_token"];
                }
            }
            return ret;
        }

        /// <summary>
        /// Exchange the request token for an access token.
        /// </summary>
        /// <param name="authToken">The oauth_token is supplied by Twitter's authorization page following the callback.</param>
        /// <param name="oauthVerifier">An oauth_verifier parameter is provided to the client either in the pre-configured callback URL</param>
        public string GetAccessToken(string authToken, string oauthVerifier)
        {
            TumblrToken = authToken;
            TumblrOAuthVerifier = oauthVerifier;

            string response = TumblrWebRequest(Method.GET, ACCESS_TOKEN, String.Empty);

            if (response.Length > 0)
            {
                //Store the Token and Token Secret
                NameValueCollection qs = HttpUtility.ParseQueryString(response);
                if (qs["oauth_token"] != null)
                {
                    TumblrToken = qs["oauth_token"];
                }
                if (qs["oauth_token_secret"] != null)
                {
                    TumblrTokenSecret = qs["oauth_token_secret"];
                }
            }
            return response;
        }

        public static string OAuthData(string url, string method, string token, string secret, Dictionary<string, object> parameters, System.Threading.CancellationToken cancelToken = default(System.Threading.CancellationToken))
        {
            if (parameters == null)
            {
                parameters = new Dictionary<string, object>();
            }
            parameters.Add("d", DateTime.Now.Ticks.ToString());

            var header = OAuthHeader(url, method, token, secret, parameters);

            return PostData(url, method, header, parameters, cancelToken);
        }

        private static string OAuthHeader(string url, string method, string token, string secret, Dictionary<string, object> parameters)
        {
            TimeSpan SinceEpoch = (DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0, 0));
            Random Rand = new Random();
            Int32 Nonce = Rand.Next(1000000000);
            var sig = GetSignature(url, method, Nonce.ToString(), Math.Round(SinceEpoch.TotalSeconds).ToString(), token, secret, parameters);
            return "OAuth oauth_consumer_key=\"" + TumblrConsumerKey + "\", oauth_nonce=\"" + Nonce.ToString() + "\", oauth_signature=\"" + UrlEncode(sig) + "\", oauth_signature_method=\"HMAC-SHA1\", oauth_timestamp=\"" + Math.Round(SinceEpoch.TotalSeconds) + "\", oauth_token=\"" + token + "\", oauth_version=\"1.0\"";
        }

        private static string PostData(String url, string method, string header, Dictionary<string, object> parameters, System.Threading.CancellationToken cancelToken = default(System.Threading.CancellationToken))
        {
            var Response = (HttpWebResponse)PostDataResponse(url, method, header, parameters, cancelToken);
            using (StreamReader ResponseDataStream = new StreamReader(Response.GetResponseStream()))
            {
                return ResponseDataStream.ReadToEnd(); 
            }
        }

        private static WebResponse PostDataResponse(String url, string method, string header, Dictionary<string, object> parameters, System.Threading.CancellationToken cancelToken = default(System.Threading.CancellationToken))
        {
            HttpWebRequest Request;
            if (parameters == null)
            {
                Request = (HttpWebRequest)System.Net.WebRequest.Create(url);
                Request.Method = method.ToUpper();
            }
            else
            {
                Request = (HttpWebRequest)System.Net.WebRequest.Create(url);
                Request.Method = method.ToUpper();
                if (method == "POST")
                {
                    Request.ContentType = "application/x-www-form-urlencoded";
                    using (var str = Request.GetRequestStream())
                    {
                        var first = true;
                        foreach (var p in parameters)
                        {
                            if (cancelToken != null && cancelToken.IsCancellationRequested)
                            {
                                throw new OperationCanceledException();
                            }

                            if (!first)
                            {
                                str.WriteByte((byte)'&');
                            }
                            first = false;
                            UrlEncode(str, p.Key);
                            str.WriteByte((byte)'=');
                            if (p.Value is byte[])
                            {
                                var eam = (byte[])p.Value;
                                EncodeBodyStream(str, eam);
                            }
                            else
                            {
                                UrlEncode(str, p.Value.ToString());
                            }
                        }
                    }
                }
                else
                {
                    var first = true;
                    var str = new StringBuilder("");
                    foreach (var p in parameters)
                    {
                        if (cancelToken != null && cancelToken.IsCancellationRequested)
                        {
                            throw new OperationCanceledException();
                        }

                        if (!first)
                        {
                            str.Append("&");
                        }
                        first = false;
                        UrlEncode(str, p.Key);
                        str.Append("=");
                        UrlEncode(str, p.Value.ToString());
                    }
                    Request = (HttpWebRequest)System.Net.WebRequest.Create(url + "?" + str.ToString());
                    Request.Method = method.ToUpper();
                }
            }

            Request.Headers[HttpRequestHeader.Authorization] = header;
            try
            {
                return Request.GetResponse();
            }
            catch (WebException ex)
            {
                return ex.Response;
            }
        }

        /// <summary>
        /// Submit a web request using oAuth.
        /// </summary>
        /// <param name="method">GET or POST</param>
        /// <param name="url">The full url, including the querystring.</param>
        /// <param name="postData">Data to post (querystring format)</param>
        /// <returns>The web server response.</returns>
        public string TumblrWebRequest(Method method, string url, string postData)
        {
            string outUrl = "";
            string querystring = "";
            string ret = "";


            //Setup postData for signing.
            //Add the postData to the querystring.
            if (method == Method.POST || method == Method.DELETE)
            {
                if (postData.Length > 0)
                {
                    //Decode the parameters and re-encode using the oAuth UrlEncode method.
                    NameValueCollection qs = HttpUtility.ParseQueryString(postData);
                    postData = "";
                    foreach (string key in qs.AllKeys)
                    {
                        if (postData.Length > 0)
                        {
                            postData += "&";
                        }
                        qs[key] = HttpUtility.UrlDecode(qs[key]);
                        qs[key] = UrlEncode(qs[key]);
                        postData += key + "=" + qs[key];
                    }
                    if (url.IndexOf("?") > 0)
                    {
                        url += "&";
                    }
                    else
                    {
                        url += "?text";
                    }
                    url += postData;
                }
            }

            var uri = new Uri(url);

            string nonce = GenerateNonce();
            string timeStamp = GenerateTimeStamp();

            //Generate Signature
            string sig = GenerateSignature(uri,
                                           TumblrConsumerKey,
                                           TumblrConsumerSecret,
                                           TumblrToken,
                                           TumblrTokenSecret,
                                           TumblrCallBackUrl,
                                           TumblrOAuthVerifier,
                                           method.ToString(),
                                           timeStamp,
                                           nonce,
                                           out outUrl,
                                           out querystring);

            querystring += "&oauth_signature=" + UrlEncode(sig);

            //Convert the querystring to postData
            if (method == Method.POST || method == Method.DELETE)
            {
                postData = querystring;
                querystring = "";
            }

            if (querystring.Length > 0)
            {
                outUrl += "?";
            }

            ret = MakeTumblrWebRequest(method, outUrl + querystring, postData);

            return ret;
        }

        /// <summary>
        /// Web Request Wrapper
        /// </summary>
        /// <param name="method">Http Method</param>
        /// <param name="url">Full url to the web resource</param>
        /// <param name="postData">Data to post in querystring format</param>
        /// <returns>The web server response.</returns>
        public string MakeTumblrWebRequest(Method method, string url, string postData)
        {
            try
            {
                HttpWebRequest webRequest = null;
                StreamWriter requestWriter = null;
                string responseData = "";

                webRequest = System.Net.WebRequest.Create(url) as HttpWebRequest;
                webRequest.Method = method.ToString();
                webRequest.ServicePoint.Expect100Continue = false;
                //webRequest.UserAgent  = "Identify your application please.";
                //webRequest.Timeout = 20000;


                if (method == Method.POST || method == Method.DELETE)
                {
                    string Text = HttpUtility.UrlEncode("text", System.Text.UTF8Encoding.UTF8) + "=" + HttpUtility.UrlEncode("Test Post", System.Text.UTF8Encoding.UTF8);
                    webRequest.ContentType = "application/x-www-form-urlencoded";
                    webRequest.Headers.Add("Authorization", postData);
                    //POST the data.
                    requestWriter = new StreamWriter(webRequest.GetRequestStream());
                    try
                    {
                        requestWriter.Write(Text);
                    }
                    catch
                    {
                        throw;
                    }
                    finally
                    {
                        requestWriter.Close();
                        requestWriter = null;
                    }
                }
                WebResponse webResponse = webRequest.GetResponse();
                responseData = TumblrWebResponseGet(webRequest);

                webRequest = null;

                return responseData;
            }
            catch (WebException ex)
            {
                if (ex.Response is HttpWebResponse && ex.Response != null)
                {
                    StreamReader exReader = new StreamReader(ex.Response.GetResponseStream());
                    return exReader.ReadToEnd();
                }
                else
                {
                    return string.Empty;
                }
                // throw ex;
            }
        }

        /// <summary>
        /// Process the web response.
        /// </summary>
        /// <param name="webRequest">The request object.</param>
        /// <returns>The response data.</returns>
        public string TumblrWebResponseGet(HttpWebRequest webRequest)
        {
            StreamReader responseReader = null;
            string responseData = "";

            try
            {
                responseReader = new StreamReader(webRequest.GetResponse().GetResponseStream());
                responseData = responseReader.ReadToEnd();
            }
            catch
            {
                throw;
            }
            finally
            {
                webRequest.GetResponse().GetResponseStream().Close();
                responseReader.Close();
                responseReader = null;
            }

            return responseData;
        }


    }
}
