using FluentValidation;
using System.Text.Json.Serialization;

namespace API.InputModels
{
    public class PostHeroiInputModel
    {
        /// <summary>
        /// "Nome deve ter no mínimo 3 caracteres e no máximo 120 caracteres.
        /// </summary>
        /// <example>Guilherme</example>
        [JsonPropertyName("nome")]
        public string Nome { get; set; }

        /// <summary>
        /// "Nome herói deve ter no mínimo 3 caracteres e no máximo 120 caracteres.
        /// </summary>
        /// <example>Batman</example>
        [JsonPropertyName("nomeHeroi")]
        public string NomeHeroi { get; set; }

        /// <summary>
        /// "Dada de nascimento deve ter 8 caracteres.
        /// </summary>
        /// <example>2023-10-02T00:00:00</example>
        [JsonPropertyName("dataNascimento")]
        public DateTime DataNascimento { get; set; }

        /// <summary>
        /// "Altura.
        /// </summary>
        /// <example>1.73</example>
        [JsonPropertyName("altura")]
        public decimal Altura { get; set; }

        /// <summary>
        /// "Peso.
        /// </summary>
        /// <example>60.5</example>
        [JsonPropertyName("peso")]
        public decimal Peso { get; set; }

        /// <summary>
        /// "Poderes.
        /// </summary>
        /// <list type="bullet">
        /// <item><description>1</description></item>
        /// <item><description>2</description></item>
        /// <item><description>3</description></item>
        /// </list>
        [JsonPropertyName("superpoderes")]
        public List<int> Superpoderes { get; set; }
    }

    public class PostHeroiInputModelValidator : AbstractValidator<PostHeroiInputModel>
    {
        public PostHeroiInputModelValidator()
        {
            RuleFor(x => x.Nome)
                .NotEmpty().WithMessage("Informe nome do herói.")
                .Length(3, 120).WithMessage("Nome deve ter no mínimo 3 caracteres e no máximo 120 caracteres.");

            RuleFor(x => x.NomeHeroi)
                .NotEmpty().WithMessage("Informe nome de herói.")
                .Length(3, 120).WithMessage("Nome herói deve ter no mínimo 3 caracteres e no máximo 120 caracteres.");

            //RuleFor(x => x.DataNascimento)
            //   .(3, 100).WithMessage("Capa url deve ter no mínimo 3 caracteres e no máximo 100 caracteres.");

            RuleFor(x => x.Altura)
                .NotEmpty().WithMessage("Informe altura do herói.");

            RuleFor(x => x.Peso)
                .NotEmpty().WithMessage("Informe peso do herói.");
        }
    }
}
