using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api_questoes.Models;
using api_questoes.Models.DTOs;

namespace api_questoes.Interface
{
    public interface IQuestaoService
    {
        public Task<Questao> Create(QuestaoDTOCreate dto);
        public Task<Questao> GetById(int id);
        public Task<List<Questao>> GetByMateria(List<string> materia, int dificuldade);
        public Task Update(int id, QuestaoDTOCreate dto);
        public Task Delete(int id);
    }
}