using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using RpgApi.Models;
using RpgApi.Models.Enuns;

namespace RpgApi.Controllers
{
    [ApiController]
    [Route("[Controller]")]
    public class PersonagensExercicioController : ControllerBase
    {
        private static List<Personagem> personagens = new List<Personagem>()
        {
            new Personagem() { Id = 1, Nome = "Frodo", PontosVida=100, Forca=17, Defesa=23, Inteligencia=33, Classe=ClasseEnum.Cavaleiro},
            new Personagem() { Id = 2, Nome = "Sam", PontosVida=100, Forca=15, Defesa=25, Inteligencia=30, Classe=ClasseEnum.Cavaleiro},
            new Personagem() { Id = 3, Nome = "Galadriel", PontosVida=100, Forca=18, Defesa=21, Inteligencia=35, Classe=ClasseEnum.Clerigo },
            new Personagem() { Id = 4, Nome = "Gandalf", PontosVida=100, Forca=18, Defesa=18, Inteligencia=37, Classe=ClasseEnum.Mago },
            new Personagem() { Id = 5, Nome = "Hobbit", PontosVida=100, Forca=20, Defesa=17, Inteligencia=31, Classe=ClasseEnum.Cavaleiro },
            new Personagem() { Id = 6, Nome = "Celeborn", PontosVida=100, Forca=21, Defesa=13, Inteligencia=34, Classe=ClasseEnum.Clerigo },
            new Personagem() { Id = 7, Nome = "Radagast", PontosVida=100, Forca=25, Defesa=11, Inteligencia=35, Classe=ClasseEnum.Mago }
        };


            [HttpGet("GetByClasse/{classe}")]
            public IActionResult GetByClasse(ClasseEnum classe)
            {
                return Ok(personagens.FindAll(pe => pe.Classe == classe));
            }


        [HttpGet("GetAll")]
        public IActionResult Get()
        {
            return Ok(personagens);
        }

        [HttpGet("{nome}")]
        public IActionResult GetByNome(string nome)
        {
            List<Personagem> listaBuscaPs = personagens.FindAll(p => p.Nome == nome);

            if (listaBuscaPs.Count == 0)
                return BadRequest("Not Found! Por favor digite um nome válido!");
            else
                return Ok(listaBuscaPs);
        }

        [HttpPost ("PostValidacao{addPersonagem}")]
        public IActionResult PostValidacao(Personagem addPersonagem)
        {
            if (addPersonagem.Inteligencia <= 10 /*|| addPersonagem.Defesa > 30*/)
                return BadRequest("Valor de defesa ou inteligência inválido! Insira um valor de Defesa maior do que 10/valor de Inteligência menor do que 30!");
            if (addPersonagem.Defesa > 30)
                return BadRequest("Valor de defesa ou inteligência inválido! Insira um valor de Defesa maior do que 10/valor de Inteligência menor do que 30!");

            else
                personagens.Add(addPersonagem);
                return Ok(personagens);
        }

        [HttpPost ("PostValidacaoMago")]
        public IActionResult PostValidacaoMago(Personagem Mago)
        {
            if(Mago.Inteligencia < 35)
            {
                BadRequest("Inteligência do Mago não pode ter o valor menor que 35" /*+ personagens.FindAll(p => p.Classe == ClasseEnum.Mago)*/);
            }
            personagens.Add(Mago);
            return Ok(personagens);
        }

        [HttpGet("GetClerigoMago")]
        public IActionResult GetClerigoMago()
        {
            List<Personagem> pBuscaPer = personagens.FindAll(ps => ps.Classe != ClasseEnum.Cavaleiro);
            return Ok(pBuscaPer);
        }

        [HttpGet ("GetEstatisticas")]
        public IActionResult GetEstatistica(){
            return Ok("Quantidade de personagens :" + personagens.Count + "A soma das inteligências é: " + personagens.Sum(pe => pe.Inteligencia ));
           
        }
    }
}
 