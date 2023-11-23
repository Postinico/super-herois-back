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
    public class HeroiController : Controller
    {
        private readonly Context _dbContext;

        public IValidator<PostHeroiInputModel> _validadorPost;
        public IValidator<PutHeroiInputModel> _validadorPut;

        public HeroiController
        (
            Context context,
            IValidator<PostHeroiInputModel> validadorPost,
            IValidator<PutHeroiInputModel> validadorPut
        )
        {
            _dbContext = context ?? throw new ArgumentNullException(nameof(context));

            _validadorPost = validadorPost ?? throw new ArgumentNullException(nameof(validadorPost));
            _validadorPut = validadorPut ?? throw new ArgumentNullException(nameof(validadorPut));
        }

        /// <summary>
        /// Obter herói específico
        /// </summary>
        /// <returns>Herói específico</returns>
        /// <remarks>Buscar!</remarks>
        /// <param name="id" example="1">Heróis Id</param>
        [HttpGet("{id}")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> ObterIdAsync(int id)
        {
            var heroi = _dbContext.Heroi.SingleOrDefault(c => c.Id == id);
            if (heroi == null)
            {
                var resultViewModel = new ResultViewModel(false, "Herói não localizado.", null);

                await Task.CompletedTask;
                return NotFound(resultViewModel);
            }
            else
            {
                var superpoderes = await ObterHeroiSuperPoderesAsync(id);

                var heroiSuperpoderViewModel = new HeroiSuperpoderViewModel(heroi, superpoderes);

                var resultViewModel = new ResultViewModel(true, "Herói localizado.", heroiSuperpoderViewModel);

                await Task.CompletedTask;
                return Ok(resultViewModel);
            }
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
            if (herois.Count() <= 0)
            {
                var ResultViewModel = new ResultViewModel(false, "Não existe heróis cadastrado.", null);

                await Task.CompletedTask;
                return NotFound(ResultViewModel);
            }
            else
            {
                var heroiViewModel = herois
                .Select(c => new HeroiViewModel(c.Id, c.NomeHeroi))
                .ToList();

                var resultViewModel = new ResultViewModel(true, "Lista de heróis", heroiViewModel);

                await Task.CompletedTask;
                return Ok(resultViewModel);
            }
        }

        /// <summary>
        /// Adicionar.
        /// </summary>
        /// <remarks>Adicionar heróri</remarks>
        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.Created)]
        [ProducesResponseType((int)HttpStatusCode.Conflict)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> AdicionarAsync([FromBody] PostHeroiInputModel request)
        {
            if (_validadorPost.Validate(request).IsValid)
            {
                var heroi = _dbContext.Heroi.SingleOrDefault(c => c.NomeHeroi.ToLower() == request.NomeHeroi.ToLower());
                if (heroi == null)
                {
                    var result = await ValidarSuperPoderesAsync(request.Superpoderes);
                    if (!result.Resultado) return BadRequest(result);

                    var heroiNovo = new Heroi
                    (
                         nome: request.Nome,
                         nomeHeroi: request.NomeHeroi,
                         dataNascimento: DateTime.Parse(request.DataNascimento.ToString("dd/MM/yy")),
                         altura: request.Altura,
                         peso: request.Peso
                    );

                    _dbContext.Heroi.Add(heroiNovo);
                    _dbContext.SaveChanges();

                    var resultAdicionarHeroiSuperPoderesAsync = await AdicionarHeroiSuperPoderesAsync(heroiNovo.Id, request.Superpoderes);
                    if (resultAdicionarHeroiSuperPoderesAsync)
                    {
                        var resultViewModel = new ResultViewModel(resultado: true, menssagem: $"Herói {request.NomeHeroi} cadastrado com sucesso", data: request);

                        await Task.CompletedTask;
                        return Created(nameof(ObterIdAsync), resultViewModel);
                    }
                    else
                    {
                        return BadRequest(new ResultViewModel(resultado: false, menssagem: $"Herói {request.NomeHeroi} erro ao relacionar herói e superpoderes", data: request));
                    }
                }
                else
                {
                    var resultViewModel = new ResultViewModel(resultado: false, menssagem: $"Herói {request.NomeHeroi} existe na base de dados", data: request);

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
        /// <remarks>Alterar herói</remarks>
        /// <param name="id" example="1">Herói Id</param>
        [HttpPut("alterar/{id}")]
        [ProducesResponseType((int)HttpStatusCode.Accepted)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> EditarAsync(int id, [FromBody] PutHeroiInputModel request)
        {
            if (_validadorPut.Validate(request).IsValid)
            {
                var heroi = _dbContext.Heroi.SingleOrDefault(c => c.Id == id);
                if (heroi == null)
                {
                    var resultViewModel = new ResultViewModel(resultado: false, menssagem: $"herói não localizado ID: {id}", data: request);

                    await Task.CompletedTask;
                    return NotFound(resultViewModel);
                }
                else
                {
                    heroi.Update
                        (
                            request.Nome,
                            request.NomeHeroi,
                            request.DataNascimento,
                            request.Altura,
                            request.Peso
                        );

                    await _dbContext.SaveChangesAsync();

                    await DeletarHeroiSuperPoderesAsync(id);

                    await AdicionarHeroiSuperPoderesAsync(id, request.Superpoderes);

                    await Task.CompletedTask;
                    return Accepted(new ResultViewModel(true, "Herói alterado com sucesso!", heroi));
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
        /// <remarks>Deletar herói</remarks>
        /// <param name="id" example="1">Herói Id</param>
        [HttpDelete("deletar/{id}")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> DeletarAsync(int id)
        {
            var heroi = _dbContext.Heroi.SingleOrDefault(c => c.Id == id);
            if (heroi == null)
            {
                var resultViewModel = new ResultViewModel(resultado: false, menssagem: $"Herói não localizado ID: {id}", null);

                await Task.CompletedTask;
                return NotFound(resultViewModel);
            }
            else
            {
                await DeletarHeroiSuperPoderesAsync(id);

                _dbContext.Heroi.Remove(heroi);
                _dbContext.SaveChanges();

                var resultViewModel = new ResultViewModel(resultado: true, menssagem: $"Herói excluído com sucesso ID: {id}", heroi);

                await Task.CompletedTask;
                return Ok(resultViewModel);
            }
        }


        private async Task<ResultViewModel> ValidarSuperPoderesAsync(List<int> superpoderes)
        {
            foreach (var poderId in superpoderes)
            {
                var poder = _dbContext.Superpoder.Find(poderId);
                if (poder == null)
                {
                    await Task.CompletedTask;
                    return new ResultViewModel(resultado: false, menssagem: $"Poder não localizado ID: {poderId}", superpoderes);
                }
            }
            return new ResultViewModel(resultado: true, menssagem: $"", data: null);
        }
        private async Task<bool> AdicionarHeroiSuperPoderesAsync(int heroiId, List<int> superpoderes)
        {
            foreach (var poderId in superpoderes)
            {
                var heroiSuperpoder = new HeroiSuperpoder(heroiId, poderId);

                await _dbContext.HeroiSuperpoder.AddAsync(heroiSuperpoder);
                await _dbContext.SaveChangesAsync();
            }

            return true;
        }
        private async Task<List<SuperpoderViewModel>> ObterHeroiSuperPoderesAsync(int heroiId)
        {
            var superpoderViewModel = new List<SuperpoderViewModel>();

            var heroiSuperpoderes = _dbContext.HeroiSuperpoder.Where(c => c.HeroiId == heroiId).ToList();

            foreach (var heroiSuperpoder in heroiSuperpoderes)
            {
                var Superpoder = _dbContext.Superpoder.FirstOrDefault(c => c.Id == heroiSuperpoder.SuperpoderId);

                superpoderViewModel.Add(new SuperpoderViewModel(Superpoder.Id, Superpoder.SuperPoder));
            }

            await Task.CompletedTask;
            return superpoderViewModel;
        }
        private async Task<bool> DeletarHeroiSuperPoderesAsync(int heroiId)
        {
            var heroiSuperpoderes = _dbContext.HeroiSuperpoder.Where(c => c.HeroiId == heroiId).ToArray();

            _dbContext.HeroiSuperpoder.RemoveRange(heroiSuperpoderes);
            await _dbContext.SaveChangesAsync();

            return true;
        }

        //private async Task<>
    }
}
