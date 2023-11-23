using System.Text.Json.Serialization;

namespace API.ViewModels
{
    public class SuperpoderViewModel
    {
        public SuperpoderViewModel
        (
            int id,
            string superPoder
        )
        {
            Id = id;
            SuperPoder = superPoder;
        }

        [JsonPropertyName("id")]
        public int Id { get; private set; }

        [JsonPropertyName("superPoder")]
        public string SuperPoder { get; private set; }
    }
}
