using Amazon;
using Amazon.Polly;
using Amazon.Polly.Model;
using Amazon.Runtime;
using JocysCom.ClassLibrary;
using JocysCom.ClassLibrary.Controls;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Speech.Synthesis;

namespace JocysCom.TextToSpeech.Monitor.Voices
{
	public class AmazonVoiceClient: IVoiceClient<Voice>
	{

		public AmazonVoiceClient(string accessKey, string secretKey, string regionSystemName)
		{
			var credentials = new BasicAWSCredentials(accessKey, secretKey);
			var region = RegionEndpoint.GetBySystemName(regionSystemName);
			Client = new AmazonPollyClient(credentials, region);
		}

		public AmazonPollyClient Client { get; }

		//PutLexiconSample.PutLexicon();
		//    GetLexiconSample.GetLexicon();
		//   ListLexiconsSample.ListLexicons();
		//   DeleteLexiconSample.DeleteLexicon();
		//   DescribeVoicesSample.DescribeVoices();
		//   SynthesizeSpeechMarksSample.SynthesizeSpeechMarks();
		//   SynthesizeSpeechSample.SynthesizeSpeech();

		#region Lexicon

		public void GetLexicon()
		{
			var LEXICON_NAME = "SampleLexicon";
			var getLexiconRequest = new GetLexiconRequest()
			{
				Name = LEXICON_NAME
			};
			try
			{
				var getLexiconResponse = Client.GetLexicon(getLexiconRequest);
				Console.WriteLine("Lexicon:\n Name: {0}\nContent: {1}", getLexiconResponse.Lexicon.Name,
					getLexiconResponse.Lexicon.Content);
			}
			catch (Exception e)
			{
				Console.WriteLine("Exception caught: " + e.Message);
			}
		}

		public void DeleteLexicon()
		{
			string LEXICON_NAME = "SampleLexicon";
			var deleteLexiconRequest = new DeleteLexiconRequest()
			{
				Name = LEXICON_NAME
			};
			try
			{
				Client.DeleteLexicon(deleteLexiconRequest);
			}
			catch (Exception e)
			{
				Console.WriteLine("Exception caught: " + e.Message);
			}
		}

		public void ListLexicons()
		{
			var listLexiconsRequest = new ListLexiconsRequest();
			try
			{
				string nextToken;
				do
				{
					var listLexiconsResponse = Client.ListLexicons(listLexiconsRequest);
					nextToken = listLexiconsResponse.NextToken;
					listLexiconsResponse.NextToken = nextToken;

					Console.WriteLine("All voices: ");
					foreach (var lexiconDescription in listLexiconsResponse.Lexicons)
					{
						var attributes = lexiconDescription.Attributes;
						Console.WriteLine("Name: " + lexiconDescription.Name
							+ ", Alphabet: " + attributes.Alphabet
							+ ", LanguageCode: " + attributes.LanguageCode
							+ ", LastModified: " + attributes.LastModified
							+ ", LexemesCount: " + attributes.LexemesCount
							+ ", LexiconArn: " + attributes.LexiconArn
							+ ", Size: " + attributes.Size);
					}
				} while (nextToken != null);
			}
			catch (Exception e)
			{
				Console.WriteLine("Exception caught: " + e.Message);
			}
		}

		public void PutLexicon()
		{
			string LEXICON_CONTENT = "<?xml version=\"1.0\" encoding=\"UTF-8\"?>" +
				"<lexicon version=\"1.0\" xmlns=\"http://www.w3.org/2005/01/pronunciation-lexicon\" xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\" " +
				"xsi:schemaLocation=\"http://www.w3.org/2005/01/pronunciation-lexicon http://www.w3.org/TR/2007/CR-pronunciation-lexicon-20071212/pls.xsd\" " +
				"alphabet=\"ipa\" xml:lang=\"en-US\">" +
				"<lexeme><grapheme>test1</grapheme><alias>test2</alias></lexeme>" +
				"</lexicon>";
			string LEXICON_NAME = "SampleLexicon";
			var putLexiconRequest = new PutLexiconRequest()
			{
				Name = LEXICON_NAME,
				Content = LEXICON_CONTENT
			};

			try
			{
				Client.PutLexicon(putLexiconRequest);
			}
			catch (Exception e)
			{
				Console.WriteLine("Exception caught: " + e.Message);
			}
		}

		public static event EventHandler<EventArgs<Exception>> Exception;

		public static void OnEvent<T>(EventHandler<EventArgs<T>> handler, T data)
		{
			if (handler != null)
				ControlsHelper.Invoke(handler, null, new EventArgs<T>(data));
		}

		#endregion

		public Exception LastException { get; set; }

		//InstalledVoiceEx
		public List<Voice> GetVoices(string cultureName = null, bool isNeural = false, int timeout = 20000)
		{
			var request = new DescribeVoicesRequest();
			if (isNeural)
				request.Engine = isNeural ? Engine.Neural : Engine.Standard;
			if (cultureName != null)
				request.LanguageCode = cultureName;
			var voices = GetVoices(request, timeout);
			return voices;
		}

		public InstalledVoiceEx Convert(Voice voice)
		{
			if (voice == null)
				throw new ArgumentNullException(nameof(voice));
			var v = new InstalledVoiceEx();
			v.Voice = voice;
			v.Source = VoiceSource.Amazon;
			// Set culture properties.
			var culture = new CultureInfo(voice.LanguageCode);
			v.SetCulture(culture);
			// Set voice properties.
			v.Name = voice.Name;
			v.Age = VoiceAge.NotSet;
			v.Description = string.Format("{0} {1} - {2} - {3}: {4}", v.Source, v.Name, v.CultureName, v.Gender, string.Join(", ", voice.SupportedEngines));
			v.Version = "";
			if (voice.Gender == Gender.Female)
			{
				v.Gender = VoiceGender.Female;
				v.Female = InstalledVoiceEx.MaxVoice;
			}
			else if (voice.Gender == Gender.Male)
			{
				v.Gender = VoiceGender.Male;
				v.Male = InstalledVoiceEx.MaxVoice;
			}
			return v;
		}

		private List<Voice> GetVoices(DescribeVoicesRequest request, int millisecondsTimeout)
		{
			LastException = null;
			var voices = new List<Voice>();
			var timeout = true;
			var ts = new System.Threading.ThreadStart(delegate ()
			{
				request = request ?? new DescribeVoicesRequest();
				string nextToken;
				try
				{
					do
					{
						var response = Client.DescribeVoices(request);
						nextToken = response.NextToken;
						request.NextToken = nextToken;
						foreach (var voice in response.Voices)
							voices.Add(voice);
					} while (nextToken != null);
				}
				catch (Exception ex)
				{
					LastException = ex;
					Console.WriteLine(ex.ToString());
					//OnEvent(Exception, ex);
				}
				timeout = false;
			});
			var t = new System.Threading.Thread(ts);
			t.Start();
			t.Join(millisecondsTimeout);
			if (timeout)
			{
				var timeoutEx = new Exception("AmmazonPolly.GetVoices timed out (" + millisecondsTimeout.ToString() + "ms)");
				LastException = timeoutEx;
				Console.WriteLine(timeoutEx.ToString());
				//OnEvent(Exception, ex);
			}
			return voices;
		}

		public void SynthesizeSpeechMarks()
		{
			var outputFileName = "speechMarks.json";
			var synthesizeSpeechRequest = new SynthesizeSpeechRequest()
			{
				OutputFormat = OutputFormat.Json,
				SpeechMarkTypes = new List<string>() { SpeechMarkType.Viseme, SpeechMarkType.Word },
				VoiceId = VoiceId.Joanna,
				Text = "This is a sample text to be synthesized."
			};

			try
			{
				using (var outputStream = new FileStream(outputFileName, FileMode.Create, FileAccess.Write))
				{
					var synthesizeSpeechResponse = Client.SynthesizeSpeech(synthesizeSpeechRequest);
					var buffer = new byte[2 * 1024];
					int readBytes;

					var inputStream = synthesizeSpeechResponse.AudioStream;
					while ((readBytes = inputStream.Read(buffer, 0, 2 * 1024)) > 0)
						outputStream.Write(buffer, 0, readBytes);
				}
			}
			catch (Exception e)
			{
				Console.WriteLine("Exception caught: " + e.Message);
			}
		}

		public void SynthesizeSpeech()
		{
			var outputFileName = "speech.mp3";
			var synthesizeSpeechRequest = new SynthesizeSpeechRequest()
			{
				OutputFormat = OutputFormat.Mp3,
				VoiceId = VoiceId.Joanna,
				Text = "This is a sample text to be synthesized."
			};
			try
			{
				using (var outputStream = new FileStream(outputFileName, FileMode.Create, FileAccess.Write))
				{
					var synthesizeSpeechResponse = Client.SynthesizeSpeech(synthesizeSpeechRequest);
					byte[] buffer = new byte[2 * 1024];
					int readBytes;

					var inputStream = synthesizeSpeechResponse.AudioStream;
					while ((readBytes = inputStream.Read(buffer, 0, 2 * 1024)) > 0)
						outputStream.Write(buffer, 0, readBytes);
				}
			}
			catch (Exception e)
			{
				Console.WriteLine("Exception caught: " + e.Message);
			}
		}


		public byte[] SynthesizeSpeech(VoiceId voiceId, string text, OutputFormat format = null, Engine engine = null)
		{
			if (format == null)
				format = OutputFormat.Mp3;
			var request = new SynthesizeSpeechRequest();
			request.OutputFormat = format;
			request.VoiceId = voiceId;
			// Prefer neural voices.
			if (engine != null)
				request.Engine = engine;
			request.Text = text;
			request.TextType = TextType.Ssml;
			var ms = new MemoryStream();
			var response = Client.SynthesizeSpeech(request);
			var bufferSize = 2 * 1024;
			var buffer = new byte[bufferSize];
			int readBytes;
			var inputStream = response.AudioStream;
			while ((readBytes = inputStream.Read(buffer, 0, bufferSize)) > 0)
				ms.Write(buffer, 0, readBytes);
			var bytes = ms.ToArray();
			ms.Dispose();
			return bytes;
		}

	}
}
