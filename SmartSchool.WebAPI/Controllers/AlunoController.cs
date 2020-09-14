using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SmartSchool.WebAPI.Data;
using SmartSchool.WebAPI.Models;

namespace SmartSchool.WebAPI.Controllers
{
    /// <summary>
    /// Controlador Aluno
    /// </summary>  
    [ApiController]
    [Route("api/[controller]")]
    public class AlunoController : ControllerBase
    {
        public AppContext _Context { get; }

        public AlunoController(AppContext context)
        {
            _Context = context;
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok(_Context.Alunos.ToList());
        }

        [HttpGet("{id:int}")]
        public IActionResult GetById(int id)        
        {
            var aluno = _Context.Alunos.FirstOrDefault(a => a.Id == id);
            if (aluno == null)
                return BadRequest("Aluno não encontrado");
            else
                return Ok(aluno);
        }

        [HttpGet("{ByName}")]
        public IActionResult GetByNome(string nome, string SobreNome)
        {
            var aluno = _Context.Alunos.FirstOrDefault(
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
            _Context.Add(aluno);
            _Context.SaveChanges();
            return Ok(aluno);
        }

        [HttpPatch]
        public IActionResult Patch(int id, Aluno aluno)
        {            
            var alunoAux = _Context.Alunos.FirstOrDefault(a => a.Id == id);
            if(alunoAux == null)
                return BadRequest("Aluno não encontrado!");
            else{
                _Context.Update(aluno);
                _Context.SaveChanges();
            }
            return Ok(aluno);
        }        

        [HttpPut("{id}")]
        public IActionResult Put(int id, Aluno aluno)
        {     
            //para não travar o objeto e assim permitir o update .AsNoTracking()  
            var alunoAux = _Context.Alunos.AsNoTracking().FirstOrDefault(a => a.Id == id);
            if(alunoAux == null)
                return BadRequest("Aluno não encontrado!");
            else{
                _Context.Update(aluno);
                _Context.SaveChanges();
            }

            return Ok(aluno);
        }

        [HttpDelete]
        public IActionResult Delete(int id)
        {
            //aqui não precisaa isso .AsNoTracking() porque é preciso bloquear o resgistro pra poder excluir
            var aluno = _Context.Alunos.FirstOrDefault(a => a.Id == id);
            if(aluno == null)
                return BadRequest("Aluno não encontrado!");
            else{
                _Context.Remove(aluno);
                _Context.SaveChanges();
            }

            return Ok("");
        }


    }
}