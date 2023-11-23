using API.Persistence;
using API.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace API.Controllers
{
    [Route("api/v1/[controller]")]
    public class SuperpoderController : Controller
    {
        private readonly Context _dbContext;

        public SuperpoderController(Context context)
        {
            _dbContext = context ?? throw new ArgumentNullException(nameof(context));
        }

        /// <summary>
        /// Obter.
        /// </summary>
        /// <remarks>Obter lista de superpoderes</remarks>
        [HttpGet()]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> ObterAsync()
        {
            var herois = _dbContext.Heroi;
            if (herois.Count() <= 0) return NotFound(new ResultViewModel(false, "Não existe heróis cadastrado.", null));

            var resultViewModel = new ResultViewModel(true, "Lista de superpoderes", herois);

            return Ok(resultViewModel);
        }

        /// <summary>
        /// Adicionar.
        /// </summary>
        /// <remarks>Adicionar superpoder</remarks>
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
        /// <remarks>Alterar superpoder</remarks>
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
        /// <remarks>Deletar superpoder</remarks>
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
