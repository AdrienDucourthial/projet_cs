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

    private class Message
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
        public string ?index;
        public Message ?message;
        public string ?finishReason;
    }

    private class OpenAIResponse
    {
        public List<Choice> ?choices { get; set; }
    }

    public async Task<string> Translate(string text)
    {
        var message1 = new Message
        {
            role = "system",
            content = "You will be provided with a sentence in French, and your task is to translate it into English."
        };
        var message2 = new Message
        {
            role = "user",
            content = text
        };

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

        // Send the request to OpenAI API
        try
        {
            var response = await httpClient.PostAsJsonAsync("https://api.openai.com/v1/chat/completions", requestContent);

            //Console.WriteLine("passed");

            if (response.IsSuccessStatusCode)
            {
                //Console.WriteLine("isSuccessful");
                var res = await response.Content.ReadAsStringAsync();
                //var result = JsonSerializer.Deserialize<OpenAIResponse>(res);
                //var ret = result.choices;
                var chatCompletion = JsonConvert.DeserializeObject<OpenAIResponse>(res);
                if (chatCompletion?.choices?.Count > 0)
                {
                    string? content = chatCompletion.choices[0].message?.content;
                    Console.WriteLine("traduction is: " + content);
                }
                return res;
                // Parse and extract the translation result
                // return the translation
            }
            else
            {
                string responseContent = await response.Content.ReadAsStringAsync();
                // Parse and extract the error message from the responseContent JSON, e.g., using Newtonsoft.Json
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

    public async Task<string> Correct(string text)
    {
        var message1 = new Message
        {
            role = "system",
            content = "You will be provided with statements, and your task is to convert them to standard French."
        };
        var message2 = new Message
        {
            role = "user",
            content = text
        };
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
        // Send the request to OpenAI API
        try
        {
            var response = await httpClient.PostAsJsonAsync("https://api.openai.com/v1/chat/completions", requestContent);

            //Console.WriteLine("passed");

            if (response.IsSuccessStatusCode)
            {
                //Console.WriteLine("isSuccessful");
                var res = await response.Content.ReadAsStringAsync();
                //var result = JsonSerializer.Deserialize<OpenAIResponse>(res);
                //var ret = result.choices;
                var chatCompletion = JsonConvert.DeserializeObject<OpenAIResponse>(res);
                if (chatCompletion?.choices?.Count > 0)
                {
                    string? content = chatCompletion.choices[0].message?.content;
                    Console.WriteLine("correction is: " + content);
                }
                return res;
                // Parse and extract the translation result
                // return the translation
            }
            else
            {
                string responseContent = await response.Content.ReadAsStringAsync();
                // Parse and extract the error message from the responseContent JSON, e.g., using Newtonsoft.Json
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

