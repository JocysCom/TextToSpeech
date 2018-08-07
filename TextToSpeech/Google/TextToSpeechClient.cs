using System;
using System.IO;
using System.Net;
using System.Text;
using System.Web;

namespace JocysCom.TextToSpeech.Monitor.Google
{
	public class TextToSpeechClient
	{
		public Uri Url;

		string _Code;
		string _GoogleWebAppClientID;
		string _GoogleWebAppClientSecret;
		public string ApiKey { get; set; }

		// https://developers.google.com/api-client-library/dotnet/get_started
		// https://cloud.google.com/text-to-speech/docs/reference/rest/
		// https://developers.google.com/discovery/v1/reference/apis
		// https://cloud.google.com/text-to-speech/docs/ssml

		// Step 1: Login to Google Account https://console.developers.google.com
		//
		// Step 2: Go to resource manager https://console.developers.google.com/cloud-resource-manager
		// Create API Project:
		//     Project Name: GoogleTTS
		//     Project ID: monitor-tts
		// 
		// Step 3: Go to Project dashboard
		// https://console.developers.google.com/home/dashboard?project=monitor-tts
		// Click on Enable APIs and get credentials such as keys 
		// Find "Cloud Text-to-Speech API" and Enable it.
		// https://console.developers.google.com/apis/library/texttospeech.googleapis.com?q=text&project=monitor-tts
		//
		// Step 4: Create credentials.
		// Go to https://console.cloud.google.com/apis/credentials?project=monitor-tts
		// And follow the wizard.
		// Select "Cloud Text-to-Speech API" as answer to "Which API are you using?"

		//https://aaronparecki.com/oauth-2-simplified/

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
		public string List()
		{
			var response = Call<string>("v1beta1/voices");
			return response;
		}

		T Call<T>(string methodPath, object request = null)
		{
			if (ApiKey == null)
			{
				ApiKey = ReceiveToken(_Code, _GoogleWebAppClientID, _GoogleWebAppClientSecret, "");
			}
			T o = default(T);
			var data = HttpUtility.ParseQueryString("");
			data.Add("key", ApiKey);
			//data.Add("languageCode", "en-GB");
			var client = new WebClient();
			var url = Url.AbsoluteUri + methodPath;
			var webRequest = (HttpWebRequest)WebRequest.Create(url);
			webRequest.ContentType = "application/x-www-form-urlencoded";
			if (request == null)
			{
				webRequest.Method = "GET";
			}
			else
			{
				webRequest.Method = "POST";
				var encoding = Encoding.UTF8;
				var bytes = encoding.GetBytes(data.ToString());
				webRequest.ContentLength = bytes.Length;
				var os = webRequest.GetRequestStream();
				os.Write(bytes, 0, bytes.Length);
			}
			var webResponse = (HttpWebResponse)webRequest.GetResponse();
			var responseStream = webResponse.GetResponseStream();
			var responseStreamReader = new StreamReader(responseStream);
			var result = responseStreamReader.ReadToEnd();
			o = JocysCom.ClassLibrary.Runtime.Serializer.DeserializeFromJson<T>(result);
			return o;
		}

		public string ReceiveClientCredentials(string googleWebAppClientID, string googleWebAppClientSecret)
		{
			var data = HttpUtility.ParseQueryString("");
			//var data = new System.Collections.Specialized.NameValueCollection();
			data.Add("client_id", googleWebAppClientID);
			data.Add("client_secret", googleWebAppClientSecret);
			data.Add("grant_type", "authorization_code");
			//data.Add("grant_type", "client_credentials");
			string url = "https://accounts.google.com/o/oauth2/auth";
			var request = (HttpWebRequest)WebRequest.Create(url);
			request.Method = "POST";
			request.Headers.Add("cache-control", "no-cache");
			request.ContentType = "application/x-www-form-urlencoded"; // "text/html";
			var encoding = Encoding.UTF8;
			var body = data.ToString();
			var bytes = encoding.GetBytes(body);
			request.ContentLength = bytes.Length;
			var os = request.GetRequestStream();
			os.Write(bytes, 0, bytes.Length);
			var webResponse = (HttpWebResponse)request.GetResponse();
			var responseStream = webResponse.GetResponseStream();
			var responseStreamReader = new StreamReader(responseStream);
			var result = responseStreamReader.ReadToEnd();
			return result;
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

		//https://code.google.com/apis/console

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
			byte[] responseBytes = client.UploadValues(url, "GET", data);
			string responseBody = Encoding.UTF8.GetString(responseBytes);
			return responseBody;
		}

	}
}
