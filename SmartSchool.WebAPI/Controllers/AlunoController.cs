using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using SmartSchool.WebAPI.Models;

namespace SmartSchool.WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AlunoController : ControllerBase
    {
        public List<Aluno> Alunos = new List<Aluno>(){
            new Aluno(){
                Id = 1, Nome = "Jonas", Telefone = "33481156", SobreNome = "Santos",
            },
            new Aluno(){
                Id = 1, Nome = "Tais", Telefone = "33481156", SobreNome = "Santos"
            },
            new Aluno(){
                Id = 1, Nome = "Jato fan", Telefone = "33481156", SobreNome = "Santos"
            }
        };

        public AlunoController(){}

        [HttpGet]
        public IActionResult Get()
        {
            return Ok(Alunos);
        }

        [HttpGet("{id:int}")]
        public IActionResult GetById(int id)        
        {
            var aluno = Alunos.FirstOrDefault(a => a.Id == id);
            if (aluno == null)
                return BadRequest("Aluno não encontrado");
            else
                return Ok(aluno);
        }

        [HttpGet("{ByName}")]
        public IActionResult GetByNome(string nome, string SobreNome)
        {
            var aluno = Alunos.FirstOrDefault(
                a => a.Nome.Contains(nome) && a.SobreNome.Contains(SobreNome)                
            );

            if (aluno == null)
                return BadRequest("Aluno não encontrado");
            else
                return Ok(aluno);
        }

        [HttpPost]
        public IActionResult Post(Aluno aluno)
        {
            return Ok(aluno);
        }

        [HttpPatch]
        public IActionResult Patch(int id, Aluno aluno)
        {
            return Ok(aluno);
        }        

        [HttpPut("{id}")]
        public IActionResult Put(int id, Aluno aluno)
        {
            return Ok(aluno);
        }

        [HttpDelete]
        public IActionResult Delete(int id)
        {
            return Ok("");
        }


    }
}