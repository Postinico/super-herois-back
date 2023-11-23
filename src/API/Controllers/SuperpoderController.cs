using API.InputModels;
using API.Models;
using API.Persistence;
using API.ViewModels;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace API.Controllers
{
    [Route("api/v1/[controller]")]
    public class SuperpoderController : Controller
    {
        private readonly Context _dbContext;

        public IValidator<PostSuperpoderInputModel> _validadorPost;
        public IValidator<PutSuperpoderInputModel> _validadorPut;

        public SuperpoderController
        (
            Context context,
            IValidator<PostSuperpoderInputModel> validadorPost,
            IValidator<PutSuperpoderInputModel> validadorPut
        )
        {
            _dbContext = context ?? throw new ArgumentNullException(nameof(context));

            _validadorPost = validadorPost ?? throw new ArgumentNullException(nameof(validadorPost));
            _validadorPut = validadorPut ?? throw new ArgumentNullException(nameof(validadorPut));
        }

        /// <summary>
        /// Obter Superpoder específico
        /// </summary>
        /// <returns>Superpoder específico</returns>
        /// <remarks>Buscar!</remarks>
        /// <param name="id" example="1">Superpoder Id</param>
        [HttpGet("{id}")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public IActionResult ObterId(int id)
        {
            var superpoder = _dbContext.Superpoder.SingleOrDefault(c => c.Id == id);

            if (superpoder == null)
            {
                var ResultViewModel = new ResultViewModel(false, "Superpoder não localizado.", null);
                return NotFound(ResultViewModel);
            }
            else
            {
                var superpoderViewModel = new SuperpoderViewModel
                    (
                        superpoder.Id,
                        superpoder.SuperPoder,
                        superpoder.Descricao
                    );

                return Ok(superpoderViewModel);
            }
        }

        /// <summary>
        /// Obter.
        /// </summary>
        /// <remarks>Obter lista de Superpoderes</remarks>
        [HttpGet()]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> ObterAsync()
        {
            var herois = _dbContext.Superpoder;
            if (herois.Count() <= 0)
            {
                var ResultViewModel = new ResultViewModel(false, "Não existe Superpoderes cadastrado.", null);

                await Task.CompletedTask;
                return NotFound(ResultViewModel);
            }
            else
            {
                var resultViewModel = new ResultViewModel(true, "Lista de Superpoderes", herois);

                await Task.CompletedTask;
                return Ok(resultViewModel);
            }
        }

        /// <summary>
        /// Adicionar.
        /// </summary>
        /// <remarks>Adicionar Superpoder</remarks>
        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.Created)]
        [ProducesResponseType((int)HttpStatusCode.Conflict)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> AdicionarAsync([FromBody] PostSuperpoderInputModel request)
        {
            if (_validadorPost.Validate(request).IsValid)
            {
                var superpoder = _dbContext.Superpoder.SingleOrDefault(c => c.SuperPoder.ToLower() == request.SuperPoder.ToLower());
                if (superpoder == null)
                {

                    var heroiNovo = new Superpoder
                    (
                         superpoder: request.SuperPoder,
                         descricao: request.Descricao
                    );

                    _dbContext.Superpoder.Add(heroiNovo);
                    _dbContext.SaveChanges();

                    var resultViewModel = new ResultViewModel(resultado: true, menssagem: $"Superpoder {request.SuperPoder} cadastrado com sucesso", data: request);

                    await Task.CompletedTask;
                    return Created(nameof(ObterId), resultViewModel);
                }
                else
                {
                    var resultViewModel = new ResultViewModel(resultado: false, menssagem: $"Superpoder {request.SuperPoder} existe na base de dados", data: request);

                    await Task.CompletedTask;
                    return Conflict(resultViewModel);
                }
            }
            else
            {
                var errorMessage = _validadorPost.Validate(request).Errors.FirstOrDefault().ErrorMessage;

                var resultViewModel = new ResultViewModel(resultado: false, menssagem: errorMessage, data: request);

                await Task.CompletedTask;
                return BadRequest(resultViewModel);
            }
        }

        /// <summary>
        /// Alterar.
        /// </summary>
        /// <remarks>Alterar superporder</remarks>
        /// <param name="id" example="1">Superpoder Id</param>
        [HttpPut("alterar/{id}")]
        [ProducesResponseType((int)HttpStatusCode.Accepted)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> EditarAsync(int id, [FromBody] PutSuperpoderInputModel request)
        {
            if (_validadorPut.Validate(request).IsValid)
            {
                var superpoder = _dbContext.Superpoder.SingleOrDefault(c => c.Id == id);
                if (superpoder == null)
                {
                    var resultViewModel = new ResultViewModel(resultado: false, menssagem: $"Superpoder não localizado ID: {id}", data: request);

                    await Task.CompletedTask;
                    return NotFound(resultViewModel);
                }
                else
                {
                    superpoder.Update(request.SuperPoder, request.Descricao);

                    _dbContext.SaveChanges();

                    await Task.CompletedTask;
                    return Accepted(new ResultViewModel(true, "Superpoder alterado com sucesso!", superpoder));
                }
            }
            else
            {
                var errorMessage = _validadorPut.Validate(request).Errors.FirstOrDefault().ErrorMessage;

                var resultViewModel = new ResultViewModel(resultado: false, menssagem: errorMessage, data: request);

                await Task.CompletedTask;
                return BadRequest(resultViewModel);
            }
        }

        /// <summary>
        /// Deletar.
        /// </summary>
        /// <remarks>Deletar Superpoder</remarks>
        /// <param name="id" example="1">Superpoder Id</param>
        [HttpDelete("deletar/{id}")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> DeletarAsync(int id)
        {
            var Superpoder = _dbContext.Superpoder.SingleOrDefault(c => c.Id == id);
            if (Superpoder == null)
            {
                var resultViewModel = new ResultViewModel(resultado: false, menssagem: $"Superpoder não localizado ID: {id}", null);

                await Task.CompletedTask;
                return NotFound(resultViewModel);
            }
            else
            {
                _dbContext.Superpoder.Remove(Superpoder);
                _dbContext.SaveChanges();

                var resultViewModel = new ResultViewModel(resultado: true, menssagem: $"Superpoder excluído com sucesso ID: {id}", Superpoder);

                await Task.CompletedTask;
                return Ok(resultViewModel);
            }
        }
    }
}
