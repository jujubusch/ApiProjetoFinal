using Api.Data;
using Api.Models;
using Api.Repositorios.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace Api.Repositorios
{
    public class ObservacoesRepositorio : IObservacoesRepositorio
    {
        private readonly Contexto _dbContext;

        public ObservacoesRepositorio(Contexto dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<ObservacoesModel>> GetAll()
        {
            return await _dbContext.Observacao.ToListAsync();
        }

        public async Task<ObservacoesModel> GetById(int id)
        {
            return await _dbContext.Observacao.FirstOrDefaultAsync(x => x.ObservacoesId == id);
        }

        public async Task<ObservacoesModel> InsertObservacoes(ObservacoesModel observacoes)
        {
            await _dbContext.Observacao.AddAsync(observacoes);
            await _dbContext.SaveChangesAsync();
            return observacoes;
        }

        public async Task<ObservacoesModel> UpdateObservacoes(ObservacoesModel observacoes, int id)
        {
            ObservacoesModel observacao = await GetById(id);
            if (observacao == null)
            {
                throw new Exception("Não encontrado.");
            }
            else
            {
                observacoes.ObservacaoDescricao = observacoes.ObservacaoDescricao;
                observacoes.ObservacaoLocal = observacoes.ObservacaoLocal;
                observacoes.ObservacaoData = observacoes.ObservacaoData;
                _dbContext.Observacao.Update(observacoes);
                await _dbContext.SaveChangesAsync();
            }
            return observacoes;

        }

        public async Task<bool> DeleteObservacoes(int id)
        {
            ObservacoesModel observacoes = await GetById(id);
            if (observacoes == null)
            {
                throw new Exception("Não encontrado.");
            }

            _dbContext.Observacao.Remove(observacoes);
            await _dbContext.SaveChangesAsync();
            return true;
        }

    }
}
