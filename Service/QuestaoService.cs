using api_questoes.Data;
using api_questoes.Interface;
using api_questoes.Models;
using api_questoes.Models.DTOs;
using Microsoft.EntityFrameworkCore;

public class QuestaoService : IQuestaoService
{
    private readonly AppDbContext _Context;

    public QuestaoService(AppDbContext appDbContext)
    {
        _Context = appDbContext;
    }

    public async Task<Questao> Create(QuestaoDTOCreate dto)
    {
        if (string.IsNullOrWhiteSpace(dto.Enunciado))
            throw new ArgumentException("O enunciado é obrigatório");

        if (dto.Alternativas == null || !dto.Alternativas.Any())
            throw new ArgumentException("A questão deve ter pelo menos uma alternativa");

        if (string.IsNullOrWhiteSpace(dto.RespostaCorreta))
            throw new ArgumentException("A resposta correta é obrigatória");

        if (string.IsNullOrWhiteSpace(dto.Materia))
            throw new ArgumentException("A matéria é obrigatória");

        if (!dto.Alternativas.Contains(dto.RespostaCorreta))
        {
            throw new ArgumentException("A resposta não bate com as alternativas");
        }
        var questao = new Questao
        {
            Enunciado = dto.Enunciado,
            Alternativas = dto.Alternativas,
            RespostaCorreta = dto.RespostaCorreta,
            Materia = dto.Materia,
            NivelDificuldade = dto.NivelDificuldade
        };

        await _Context.Questoes.AddAsync(questao);
        await _Context.SaveChangesAsync();

        return questao;
    }

    public async Task<Questao> GetById(int id)
    {
        var questao = await _Context.Questoes.FindAsync(id);
        if (questao == null)
            throw new KeyNotFoundException("Questão não encontrada");

        return questao;
    }

    public async Task<List<Questao>> GetByMateria(List<string> materias)
    {
        if (materias == null || !materias.Any())
            return new List<Questao>();

        return await _Context.Questoes
            .Where(q => materias.Contains(q.Materia))
            .ToListAsync();
    }

    public async Task Update(int id, QuestaoDTOCreate dto)
    {
        var questao = await _Context.Questoes.FindAsync(id);

        if (questao == null)
            throw new KeyNotFoundException("Questão não encontrada");

        questao.Enunciado = dto.Enunciado;
        questao.Materia = dto.Materia;
        questao.NivelDificuldade = dto.NivelDificuldade;
        questao.RespostaCorreta = dto.RespostaCorreta;
        questao.Alternativas = dto.Alternativas;

        _Context.Questoes.Update(questao);
        await _Context.SaveChangesAsync();
    }

    public async Task Delete(int id)
    {
        var questao = await _Context.Questoes.FindAsync(id);
        if (questao == null)
            throw new KeyNotFoundException("Questão não encontrada");

        _Context.Questoes.Remove(questao);
        await _Context.SaveChangesAsync();
    }
}
