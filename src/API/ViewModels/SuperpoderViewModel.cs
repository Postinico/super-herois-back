using System.Text.Json.Serialization;

namespace API.ViewModels
{
    public class SuperpoderViewModel
    {
        public SuperpoderViewModel
        (
            int id,
            string superPoder,
            string descricao
        )
        {
            Id = id;
            SuperPoder = superPoder;
            Descricao = descricao;
        }

        [JsonPropertyName("id")]
        public int Id { get; private set; }

        [JsonPropertyName("superPoder")]
        public string SuperPoder { get; private set; }

        [JsonPropertyName("Descricao")]
        public string Descricao { get; private set; }
    }
}
