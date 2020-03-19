using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Azure.CognitiveService.Controllers
{
    public class TradutorController : Controller
    {
        private readonly string _endPoint = "https://api.cognitive.microsofttranslator.com";

        public async Task<IActionResult> IndexAsync()
        {
            Translator translator = new Translator();
            translator.From = "Quem sou eu? Sou o Fernando aluno da FIAP com o RM 335137!";
            translator.MicrosoftTranslators = await Traduzir(translator.From).ConfigureAwait(true);
            return View(translator);
        }

        public async Task<List<MicrosoftTranslator>> Traduzir(string value)
        {
            try
            {
                object[] body = new object[] { new { Text = value } };
                var requestBody = JsonConvert.SerializeObject(body);

                using (var client = new HttpClient())
                using (var request = new HttpRequestMessage())
                {
                    request.Method = HttpMethod.Post;
                    request.RequestUri = new Uri($"{_endPoint}/translate?api-version=3.0&to=en&to=it&to=ja&to=th");
                    request.Content = new StringContent(requestBody, Encoding.UTF8, "application/json");
                    request.Headers.Add("Ocp-Apim-Subscription-Key", "28043f9b035c4bcfbe3ba401d4125c49");
                    HttpResponseMessage responseApi = await client.SendAsync(request).ConfigureAwait(false);
                    string responseBody = await responseApi.Content.ReadAsStringAsync();

                    List<MicrosoftTranslator> response = JsonConvert.DeserializeObject<List<MicrosoftTranslator>>(responseBody);
                    return response;
                }
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
        }
    }

    public class Translator
    {
        public string From { get; set; }

        public List<MicrosoftTranslator> MicrosoftTranslators { get; set; }
    }

    public class MicrosoftTranslator
    {
        [JsonProperty("detectedLanguage")]
        public DetectedLanguage DetectedLanguage { get; set; }

        [JsonProperty("translations")]
        public List<Translation> Translations { get; set; }
    }

    public class DetectedLanguage
    {
        [JsonProperty("language")]
        public string Language { get; set; }

        [JsonProperty("score")]
        public double Score { get; set; }
    }

    public class Translation
    {
        [JsonProperty("text")]
        public string Text { get; set; }

        [JsonProperty("to")]
        public string To { get; set; }
    }
}