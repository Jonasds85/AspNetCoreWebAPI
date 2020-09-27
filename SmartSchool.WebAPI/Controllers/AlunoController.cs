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
        public readonly IRepositorio _Repos;

        public AlunoController(IRepositorio repos)
        {
            _Repos = repos;
        }

        [HttpGet]
        public IActionResult Get()
        {
            var result = _Repos.GetAllAlunos(true);
            return Ok(result);
        }

        [HttpGet("{id:int}")]
        public IActionResult GetBy(int id)
        {
            var aluno = _Repos.GetByAlunoId(id);
            if (aluno == null)
                return BadRequest("Aluno não encontrado");
            else
                return Ok(aluno);
        }


        [HttpPost]
        public IActionResult Post(Aluno aluno)
        {
            _Repos.Add(aluno);
            _Repos.SaveChanges();
            return Ok(aluno);
        }

        [HttpPatch]
        public IActionResult Patch(int id, Aluno aluno)
        {
            var alunoAux = _Repos.GetByAlunoId(id);
            if (alunoAux == null)
                return BadRequest("Aluno não encontrado!");
            else
            {
                _Repos.Update(aluno);
                _Repos.SaveChanges();
            }
            return Ok(aluno);
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, Aluno aluno)
        {
            //para não travar o objeto e assim permitir o update .AsNoTracking()  
            var alunoAux = _Repos.GetByAlunoId(id);
            if (alunoAux == null)
                return BadRequest("Aluno não encontrado!");
            else
            {
                _Repos.Update(aluno);
                _Repos.SaveChanges();
            }

            return Ok(aluno);
        }

        [HttpDelete]
        public IActionResult Delete(int id)
        {
            //aqui não precisaa isso .AsNoTracking() porque é preciso bloquear o resgistro pra poder excluir
            var aluno = _Repos.GetByAlunoId(id);
            if (aluno == null)
                return BadRequest("Aluno não encontrado!");
            else
            {
                _Repos.Delete(aluno);
                _Repos.SaveChanges();
            }

            return Ok("");
        }


    }
}