using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using api_questoes.Interface;
using api_questoes.Models;
using api_questoes.Models.DTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace api_questoes.Controllers
{
    [ApiController]
    [Route("api/questao")]
    public class QuestaoController : ControllerBase
    {
        private readonly IQuestaoService _service;

        public QuestaoController(IQuestaoService questaoService)
        {
            _service = questaoService;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] QuestaoDTOCreate dto)
        {
            try
            {
                var novaQuestao = await _service.Create(dto);
                return CreatedAtAction(nameof(GetById), new { id = novaQuestao.Id }, novaQuestao);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { error = ex.Message});
            }
            
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                var questao = await _service.GetById(id);
                return Ok(questao);
            }
            catch (KeyNotFoundException)
            {
                return NotFound($"Nenhuma questão com id {id} encontrada");
            }
            
        }

        [HttpGet("materia")]
        public async Task<IActionResult> GetByMateria([FromQuery] List<string> materias)
        {
            var questoes = await _service.GetByMateria(materias);
            return Ok(questoes);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] QuestaoDTOCreate dto)
        {
            try
            {
                await _service.Update(id, dto);
                return NoContent();
            }
            catch (KeyNotFoundException)
            {
                return NotFound($"Nenhuma questão com id {id} encontrada");
            }
 
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _service.Delete(id);
                return NoContent();
            }
            catch (KeyNotFoundException)
            {
                return NotFound($"Nenhuma questão com id {id} encontrada");
            }
            
        }
    }
}