using Api.Models;
using Api.Repositorios;
using Api.Repositorios.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AnimalController : ControllerBase
    {
        private readonly IAnimaisRepositorio _animaisRepositorio;

        public AnimalController(IAnimaisRepositorio animaisRepositorio)
        {
            _animaisRepositorio = animaisRepositorio;
        }

        [HttpGet("GetAllAnimais")]
        public async Task<ActionResult<List<AnimaisModel>>> GetAllAnimais()
        {
            List<AnimaisModel> animais = await _animaisRepositorio.GetAll();
            return Ok(animais);
        }

        [HttpGet("GetAnimaisId/{id}")]
        public async Task<ActionResult<AnimaisModel>> GetAnimaisId(int id)
        {
            AnimaisModel animais = await _animaisRepositorio.GetById(id);
            return Ok(animais);
        }

        [HttpPost("CreateAnimais")]
        public async Task<ActionResult<AnimaisModel>> InsertAnimais([FromBody] AnimaisModel animaisModel)
        {
            AnimaisModel animais = await _animaisRepositorio.InsertAnimais(animaisModel);
            return Ok(animais);
        }

        [HttpPut("UpdateAnimais/{id:int}")]
        public async Task<ActionResult<AnimaisModel>> UpdateAnimais(int id, [FromBody] AnimaisModel animaisModel)
        {
            animaisModel.AnimaisId = id;
            AnimaisModel animais = await _animaisRepositorio.UpdateAnimais(animaisModel, id);
            return Ok(animais);
        }

        [HttpDelete("DeleteAnimais/{id:int}")]
        public async Task<ActionResult<AnimaisModel>> DeleteAnimais(int id)
        {
            bool deleted = await _animaisRepositorio.DeleteAnimais(id);
            return Ok(deleted);
        }
    }
}
