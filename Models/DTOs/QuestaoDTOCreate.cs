using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api_questoes.Models.DTOs
{
    public class QuestaoDTOCreate
    {
        public string Enunciado { get; set; } = string.Empty; // Texto da questão

        public List<string> Alternativas { get; set; } = new List<string>(); // Opções de resposta

        public string RespostaCorreta { get; set; } = string.Empty;

        public string Materia { get; set; } = string.Empty; // Ex: "Matemática", "História"

        public int NivelDificuldade { get; set; } // 1=Fácil, 2=Médio, 3=Difícil *Experimental 
    }
}