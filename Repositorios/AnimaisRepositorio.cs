using Api.Data;
using Api.Models;
using Api.Repositorios.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace Api.Repositorios
{
    public class AnimaisRepositorio : IAnimaisRepositorio
    {
        private readonly Contexto _dbContext;

        public AnimaisRepositorio(Contexto dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<AnimaisModel>> GetAll()
        {
            return await _dbContext.Animal.ToListAsync();
        }

        public async Task<AnimaisModel> GetById(int id)
        {
            return await _dbContext.Animal.FirstOrDefaultAsync(x => x.AnimaisId == id);
        }

        public async Task<AnimaisModel> InsertAnimais(AnimaisModel animais)
        {
            await _dbContext.Animal.AddAsync(animais);
            await _dbContext.SaveChangesAsync();
            return animais;
        }

        public async Task<AnimaisModel> UpdateAnimais(AnimaisModel animais, int id)
        {
            AnimaisModel animal = await GetById(id);
            if (animal == null)
            {
                throw new Exception("Não encontrado.");
            }
            else
            {
                animais.AnimalNome = animais.AnimalNome;
                animais.AnimalRaca = animais.AnimalRaca;
                animais.AnimalTipo = animais.AnimalTipo;
                animais.AnimalCor = animais.AnimalCor;
                animais.AnimalSexo = animais.AnimalSexo;
                animais.AnimalObservacao = animais.AnimalObservacao;
                animais.AnimalFoto = animais.AnimalFoto;
                animais.AnimalDtDesaparecimento = animais.AnimalDtDesaparecimento;
                animais.AnimalDtEncontro = animais.AnimalDtEncontro;
                animais.AnimalStatus = animais.AnimalStatus;
                _dbContext.Animal.Update(animais);
                await _dbContext.SaveChangesAsync();
            }
            return animais;

        }

        public async Task<bool> DeleteAnimais(int id)
        {
            AnimaisModel animais = await GetById(id);
            if (animais == null)
            {
                throw new Exception("Não encontrado.");
            }

            _dbContext.Animal.Remove(animais);
            await _dbContext.SaveChangesAsync();
            return true;
        }

    }
}
