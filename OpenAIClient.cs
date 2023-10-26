using System;
using System.Data;
using System.Net.Http;
using System.Net.Http.Json;
using System.Reflection.Metadata;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Text.Json;
using Newtonsoft.Json;
using static Google.Apis.Requests.BatchRequest;

class OpenAIClient
{
    private readonly HttpClient httpClient;
    private const string apiKey = "";

    public OpenAIClient()
    {
        httpClient = new HttpClient();
        httpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {apiKey}");
    }

    public class Message
    {
        public string ?role { get; set; }
        public string ?content { get; set; }
    }

    private class MessageContainer
    {
        public List<Message> ?messages { get; set; }
    }

    private class Choice
    {
        public Message ?message;
    }

    private class OpenAIResponse
    {
        public List<Choice> ?Choices { get; set; }
    }

    public async Task<string> CallOpenAI(Message message1, Message message2)
    {
        var messageContainer = new MessageContainer
        {
            messages = new List<Message> { message1, message2 }
        };

        var requestContent = new
        {
            messages = messageContainer.messages,
            model = "gpt-3.5-turbo",
            temperature = 0,
            max_tokens = 256,
        };

        try
        {
            var response = await httpClient.PostAsJsonAsync("https://api.openai.com/v1/chat/completions", requestContent);

            if (response.IsSuccessStatusCode)
            {
                var res = await response.Content.ReadAsStringAsync();
                var chatCompletion = JsonConvert.DeserializeObject<OpenAIResponse>(res);
                if (chatCompletion?.Choices?.Count > 0)
                {
                    string? content = chatCompletion.Choices[0].message?.content;
                    Console.WriteLine("traduction is: " + content);
                }
                return res;
            }
            else
            {
                string responseContent = await response.Content.ReadAsStringAsync();
                Console.WriteLine("API Error: " + responseContent);
                return "Error";
            }
        }
        catch (Exception e)
        {
            Console.WriteLine($"Error: {e}");
            return "Error";
        }
    }
}

