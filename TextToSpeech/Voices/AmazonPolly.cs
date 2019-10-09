using Amazon.Polly;
using Amazon.Polly.Model;
using Amazon.Runtime;
using System;
using System.Collections.Generic;
using System.IO;

namespace JocysCom.TextToSpeech.Monitor.Voices
{
	public class AmazonPolly
	{
		//PutLexiconSample.PutLexicon();
		//    GetLexiconSample.GetLexicon();
		//   ListLexiconsSample.ListLexicons();
		//   DeleteLexiconSample.DeleteLexicon();
		//   DescribeVoicesSample.DescribeVoices();
		//   SynthesizeSpeechMarksSample.SynthesizeSpeechMarks();
		//   SynthesizeSpeechSample.SynthesizeSpeech();

		Amazon.RegionEndpoint Region;

		#region Lexicon

		public void GetLexicon()
		{
			var LEXICON_NAME = "SampleLexicon";
			var client = new AmazonPollyClient(Region);
			var getLexiconRequest = new GetLexiconRequest()
			{
				Name = LEXICON_NAME
			};
			try
			{
				var getLexiconResponse = client.GetLexicon(getLexiconRequest);
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
			var client = new AmazonPollyClient(Region);
			var deleteLexiconRequest = new DeleteLexiconRequest()
			{
				Name = LEXICON_NAME
			};
			try
			{
				client.DeleteLexicon(deleteLexiconRequest);
			}
			catch (Exception e)
			{
				Console.WriteLine("Exception caught: " + e.Message);
			}
		}

		public void ListLexicons()
		{
			var client = new AmazonPollyClient(Region);
			var listLexiconsRequest = new ListLexiconsRequest();
			try
			{
				string nextToken;
				do
				{
					var listLexiconsResponse = client.ListLexicons(listLexiconsRequest);
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
			String LEXICON_CONTENT = "<?xml version=\"1.0\" encoding=\"UTF-8\"?>" +
				"<lexicon version=\"1.0\" xmlns=\"http://www.w3.org/2005/01/pronunciation-lexicon\" xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\" " +
				"xsi:schemaLocation=\"http://www.w3.org/2005/01/pronunciation-lexicon http://www.w3.org/TR/2007/CR-pronunciation-lexicon-20071212/pls.xsd\" " +
				"alphabet=\"ipa\" xml:lang=\"en-US\">" +
				"<lexeme><grapheme>test1</grapheme><alias>test2</alias></lexeme>" +
				"</lexicon>";
			String LEXICON_NAME = "SampleLexicon";

			//var credentials = new BasicAWSCredentials(accessKeyID, secretKey);

			var client = new AmazonPollyClient(Region);
			var putLexiconRequest = new PutLexiconRequest()
			{
				Name = LEXICON_NAME,
				Content = LEXICON_CONTENT
			};

			try
			{
				client.PutLexicon(putLexiconRequest);
			}
			catch (Exception e)
			{
				Console.WriteLine("Exception caught: " + e.Message);
			}
		}


		#endregion

		public void DescribeVoices()
		{
			var client = new AmazonPollyClient(Region);

			var allVoicesRequest = new DescribeVoicesRequest();
			var enUsVoicesRequest = new DescribeVoicesRequest()
			{
				LanguageCode = "en-US"
			};

			try
			{
				String nextToken;
				do
				{
					var allVoicesResponse = client.DescribeVoices(allVoicesRequest);
					nextToken = allVoicesResponse.NextToken;
					allVoicesRequest.NextToken = nextToken;

					Console.WriteLine("All voices: ");
					foreach (var voice in allVoicesResponse.Voices)
						Console.WriteLine(" Name: {0}, Gender: {1}, LanguageName: {2}", voice.Name,
							voice.Gender, voice.LanguageName);
				} while (nextToken != null);

				do
				{
					var enUsVoicesResponse = client.DescribeVoices(enUsVoicesRequest);
					nextToken = enUsVoicesResponse.NextToken;
					enUsVoicesRequest.NextToken = nextToken;

					Console.WriteLine("en-US voices: ");
					foreach (var voice in enUsVoicesResponse.Voices)
						Console.WriteLine(" Name: {0}, Gender: {1}, LanguageName: {2}", voice.Name,
							voice.Gender, voice.LanguageName);
				} while (nextToken != null);
			}
			catch (Exception e)
			{
				Console.WriteLine("Exception caught: " + e.Message);
			}
		}

		public void SynthesizeSpeechMarks()
		{
			var client = new AmazonPollyClient(Region);
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
					var synthesizeSpeechResponse = client.SynthesizeSpeech(synthesizeSpeechRequest);
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
			var client = new AmazonPollyClient(Region);
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
					var synthesizeSpeechResponse = client.SynthesizeSpeech(synthesizeSpeechRequest);
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


	}
}
