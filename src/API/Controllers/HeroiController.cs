using API.Persistence;

using System.Net;
using Microsoft.AspNetCore.Mvc;
using API.ViewModels;

namespace API.Controllers
{
    [Route("api/v1/[controller]")]
    public class HeroiController : Controller
    {
        private readonly Context _dbContext;

        public HeroiController(Context context)
        {
            _dbContext = context ?? throw new ArgumentNullException(nameof(context));
        }

        /// <summary>
        /// Obter.
        /// </summary>
        /// <remarks>Obter lista de heróis</remarks>
        [HttpGet()]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> ObterAsync()
        {
            var herois = _dbContext.Heroi;
            if (herois.Count() <= 0) return NotFound(new ResultViewModel(false, "Não existe heróis cadastrado.", null));

            var resultViewModel = new ResultViewModel(true, "Lista de heróis", herois);

            return Ok(resultViewModel);
        }

        /// <summary>
        /// Adicionar.
        /// </summary>
        /// <remarks>Adicionar heróri</remarks>
        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> AdicionarAsync()
        {
            return Ok();
        }

        /// <summary>
        /// Alterar.
        /// </summary>
        /// <remarks>Alterar herói</remarks>
        [HttpPut("alterar/{id}")]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> EditarAsync()
        {
            return Ok();
        }

        /// <summary>
        /// Deletar.
        /// </summary>
        /// <remarks>Deletar herói</remarks>
        [HttpDelete("deletar/{id}")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> DeletarAsync(long id)
        {
            return Ok();
        }
    }
}
