using System;
using System.IO;
using System.Net;
using System.Text;
using OAuth.Net.Common;
using OAuth.Net.Components;

namespace OAuthRequestHelper
{
  public class ServiceProvider
  {
    private const int RequestTimeOut = 1000 * 60 * 10;
    private readonly string _consumerKey;
    private readonly string _consumerSecret;
    private readonly Uri _serviceProviderUri;
    public static readonly ISigningProvider SigningProvider = new HmacSha1SigningProvider();
    public static readonly INonceProvider NonceProvider = new GuidNonceProvider();

    public ServiceProvider(string endpoint, string consumerKey, string consumerSecret)
    {
      _consumerKey = consumerKey;
      _consumerSecret = consumerSecret;
      _serviceProviderUri = new Uri(endpoint);
    }

    private HttpWebRequest GenerateRequest(string contentType, string requestMethod)
    {
      var ts = UnixTime.ToUnixTime(DateTime.Now);
      //Create the needed OAuth Parameters. 
      //Refer - http://oauth.net/core/1.0/#sig_base_example
      var param = new OAuthParameters() {
        ConsumerKey = _consumerKey,
        SignatureMethod = SigningProvider.SignatureMethod,
        Version = Constants.Version1_0,
        Nonce = NonceProvider.GenerateNonce(ts),
        Timestamp = ts.ToString(),
      };

      //Generate Signature Hash
      var signatureBase = SignatureBase.Create(requestMethod.ToUpper(), _serviceProviderUri, param);
      //Set Signature Hash as one of the OAuth Parameter
      param.Signature = SigningProvider.ComputeSignature(signatureBase, _consumerSecret, null);

      var httpWebRequest = (HttpWebRequest)WebRequest.Create(_serviceProviderUri);
      httpWebRequest.Method = requestMethod;
      httpWebRequest.ContentType = contentType;
      httpWebRequest.Timeout = RequestTimeOut;
      //Add the OAuth Parameters to Authorization Header of Request
      httpWebRequest.Headers.Add(Constants.AuthorizationHeaderParameter, param.ToHeaderFormat());
      return httpWebRequest;
    }

    public string GetData()
    {
      var request = GenerateRequest(string.Empty, WebRequestMethods.Http.Get);
      return GetRequestResponse(request);
    }

    public string PostData(string contentType, string data)
    {
      var request = GenerateRequest(contentType, WebRequestMethods.Http.Post);
      var bytes = Encoding.ASCII.GetBytes(data);
      request.ContentLength = bytes.Length;
      if (bytes.Length > 0) {
        using (var requestStream = request.GetRequestStream()) {
          if (!requestStream.CanWrite) throw new Exception("The data cannot be written to request stream");
          try {
            requestStream.Write(bytes, 0, bytes.Length);
          } catch (Exception exception) {
            throw new Exception(string.Format("Error while writing data to request stream - {0}", exception.Message));
          }
        }
      }
      return GetRequestResponse(request);
    }

    private static string GetRequestResponse(HttpWebRequest httpWebRequest)
    {
      if (httpWebRequest == null) throw new ArgumentNullException("httpWebRequest");
      string responseString = null;
      try {
        using (var response = (HttpWebResponse)httpWebRequest.GetResponse()) {
          using (var responseStream = response.GetResponseStream()) {
            if (responseStream != null) {
              var reader = new StreamReader(responseStream);
              responseString = reader.ReadToEnd();
              reader.Close();
              responseStream.Close();
            }
          }
          response.Close();
        }
      } catch (WebException webException) {
        throw new Exception(string.Format("WebException while reading response - {0}", webException.Message));
      } catch (Exception exception) {
        throw new Exception(string.Format("Unhandled exception while reading response - {0}", exception.Message));
      }
      return responseString;
    }

  }
}
