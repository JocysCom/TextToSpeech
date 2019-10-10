using Amazon;
using Amazon.Polly;
using Amazon.Polly.Model;
using Amazon.Runtime;
using JocysCom.ClassLibrary;
using JocysCom.ClassLibrary.Controls;
using System;
using System.Collections.Generic;
using System.IO;

namespace JocysCom.TextToSpeech.Monitor.Voices
{
	public class AmazonPolly
	{

		public AmazonPolly(string accessKey, string secretKey, string regionSystemName)
		{
			_credentials = new BasicAWSCredentials(accessKey, secretKey);
			_Region = RegionEndpoint.GetBySystemName(regionSystemName);
			Client = new AmazonPollyClient(_credentials, _Region);
		}

		public AmazonPollyClient Client { get; }
		BasicAWSCredentials _credentials;
		RegionEndpoint _Region;

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
				ControlsHelper.Invoke(() => { handler(null, new EventArgs<T>(data)); });
		}

		#endregion

		public List<Voice> GetVoices(DescribeVoicesRequest request = null)
		{
			request = request ?? new DescribeVoicesRequest();
			var voices = new List<Voice>();
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
				OnEvent(Exception, ex);
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


		public byte[] SynthesizeSpeech(Voice voice, string text, OutputFormat format = null)
		{
			if (format == null)
				format = OutputFormat.Mp3;
			var request = new SynthesizeSpeechRequest();
			request.OutputFormat = format;
			request.VoiceId = voice.Id;
			// Prefer neural voices.
			if (voice.SupportedEngines.Contains(Engine.Neural))
				request.Engine = Engine.Neural;
			request.Text = text;
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
