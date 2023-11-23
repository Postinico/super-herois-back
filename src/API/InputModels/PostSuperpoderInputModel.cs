using FluentValidation;
using System.Text.Json.Serialization;

namespace API.InputModels
{
    public class PostSuperpoderInputModel
    {
        /// <summary>
        /// "Nome deve ter no mínimo 3 caracteres e no máximo 50 caracteres.
        /// </summary>
        /// <example>Soco</example>
        [JsonPropertyName("nome")]
        public string SuperPoder { get; set; }

        /// <summary>
        /// "Descrição deve ter no mínimo 3 caracteres e no máximo 250 caracteres.
        /// </summary>
        /// <example>soco azul</example>
        [JsonPropertyName("Descricao")]
        public string Descricao { get; set; }
    }

    public class PostSuperpoderInputModelValidator : AbstractValidator<PostSuperpoderInputModel>
    {
        public PostSuperpoderInputModelValidator()
        {
            RuleFor(x => x.SuperPoder)
                .NotEmpty().WithMessage("Informe superpoder .")
                .Length(3, 50).WithMessage("Superpoder deve ter no mínimo 3 caracteres e no máximo 50 caracteres.");

            RuleFor(x => x.Descricao)
                .Length(3, 120).WithMessage("Descrição deve ter no mínimo 3 caracteres e no máximo 250 caracteres.");
        }
    }
}
