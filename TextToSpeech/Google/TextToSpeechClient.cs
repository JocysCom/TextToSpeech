using System;
using System.IO;
using System.Net;
using System.Text;
using System.Web;

namespace JocysCom.TextToSpeech.Monitor.Google
{
	public class TextToSpeechClient
	{
		public WebClient Client;
		public Uri Url;


		string _Code;
		string _GoogleWebAppClientID;
		string _GoogleWebAppClientSecret;
		string _Token;


		// https://developers.google.com/api-client-library/dotnet/get_started
		// https://cloud.google.com/text-to-speech/docs/reference/rest/
		// https://developers.google.com/discovery/v1/reference/apis
		// https://cloud.google.com/text-to-speech/docs/ssml

		public TextToSpeechClient(string uriString = "https://texttospeech.googleapis.com")
		{
			// Enable TLS 1.1 and 1.2
			var Tls11 = 768;
			var Tls12 = 3072;
			ServicePointManager.SecurityProtocol |= (SecurityProtocolType)Tls11 | (SecurityProtocolType)Tls12;
			// Create Web client.
			Url = new Uri(uriString);
		}

		// https://cloud.google.com/text-to-speech/docs/reference/rest/v1beta1/voices/list
		void List()
		{
			var response = Call<string>("v1beta1/voices");
		}

		T Call<T>(string methodPath, object request = null)
		{
			if (_Token == null)
			{
				_Token = ReceiveToken(_Code, _GoogleWebAppClientID, _GoogleWebAppClientSecret, "");
			}
			T o = default(T);
			var data = new System.Collections.Specialized.NameValueCollection();
			data.Add("key", _Token);
			//data.Add("languageCode", "en-GB");
			var client = new WebClient();
			var url = Url.AbsoluteUri + "/" + methodPath;
			byte[] responseBytes = Client.UploadValues(url, "GET", data);
			string responseBody = Encoding.UTF8.GetString(responseBytes);
			o = JocysCom.ClassLibrary.Runtime.Serializer.DeserializeFromJson<T>(responseBody);
			return o;
		}

		public string ReceiveToken2(string code, string googleWebAppClientID, string googleWebAppClientSecret, string redirectUrl)
		{
			// HttpUtility.ParseQueryString() will return System.Web.HttpValueCollection.
			var data = HttpUtility.ParseQueryString("");
			data.Add("code", code);
			data.Add("client_id", googleWebAppClientID);
			data.Add("client_secret", googleWebAppClientSecret);
			data.Add("redirect_uri", redirectUrl);
			data.Add("grant_type", "authorization_code");
			string url = "https://accounts.google.com/o/oauth2/token";
			var request = (HttpWebRequest)WebRequest.Create(url);
			request.Method = "POST";
			request.ContentType = "application/x-www-form-urlencoded";
			var encoding = Encoding.UTF8;
			var bytes = encoding.GetBytes(data.ToString());
			request.ContentLength = bytes.Length;
			var os = request.GetRequestStream();
			os.Write(bytes, 0, bytes.Length);
			var webResponse = (HttpWebResponse)request.GetResponse();
			var responseStream = webResponse.GetResponseStream();
			var responseStreamReader = new StreamReader(responseStream);
			var result = responseStreamReader.ReadToEnd();
			return result;
		}

		public string ReceiveToken(string code, string googleWebAppClientID, string googleWebAppClientSecret, string redirectUrl)
		{
			var data = new System.Collections.Specialized.NameValueCollection();
			data.Add("code", code);
			data.Add("client_id", googleWebAppClientID);
			data.Add("client_secret", googleWebAppClientSecret);
			//data.Add("redirect_uri", redirectUrl);
			data.Add("grant_type", "authorization_code");
			var client = new WebClient();
			var url = "https://accounts.google.com/o/oauth2/token";
			byte[] responseBytes = Client.UploadValues(url, "GET", data);
			string responseBody = Encoding.UTF8.GetString(responseBytes);
			return responseBody;
		}

	}
}
