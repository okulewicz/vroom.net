using System;
using System.Net;
using System.Net.Http;
using System.Text;
using Newtonsoft.Json;
using System.Threading.Tasks;

namespace VROOM.API
{
    public class VroomApiClient : IVroomApiClient
    {
        private readonly string _host;
        private readonly HttpClient _client;
        private readonly JsonSerializerSettings _serializerOptions = new JsonSerializerSettings()
        {
            NullValueHandling = NullValueHandling.Ignore,
        };
        
        public VroomApiClient(string host):
            this(host, 3600)
        {
        }

        public VroomApiClient(string host, int secondsTimeout)
        {
            if (string.IsNullOrEmpty(host))
            {
                throw new ArgumentNullException(nameof(host));
            }

            if (!host.EndsWith("/"))
            {
                host += "/";
            }

            _host = host;

            _client = new HttpClient();
            _client.Timeout = TimeSpan.FromSeconds(secondsTimeout);
        }

        public async Task<VroomOutput> PerformRequest(VroomInput vroomInput)
        {
            string input = JsonConvert.SerializeObject(vroomInput, _serializerOptions);
            var response = await _client.PostAsync(_host,
                new StringContent(input, Encoding.UTF8, "application/json"));

            string content = await response.Content.ReadAsStringAsync();
            var output = JsonConvert.DeserializeObject<VroomOutput>(content, _serializerOptions);
            
            if (response.StatusCode != HttpStatusCode.OK)
            {
                throw new Exception($"Server responded with status code {response.StatusCode}. Content: " +
                                    JsonConvert.SerializeObject(output, _serializerOptions));
            }

            return output;
        }

        public async Task<bool> IsHealthy()
        {
            var result = await _client.GetAsync(_host + "health");
            return result.StatusCode == HttpStatusCode.OK;
        }

        public void Dispose()
        {
            _client.Dispose();
        }
    }
}