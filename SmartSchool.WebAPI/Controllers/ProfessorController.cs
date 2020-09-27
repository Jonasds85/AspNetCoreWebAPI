using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SmartSchool.WebAPI.Data;
using SmartSchool.WebAPI.Models;

namespace SmartSchool.WebAPI.Controllers
{
    /// <summary>
    /// Controlador Professor
    /// </summary>  
    [ApiController]
    [Route("api/[controller]")]
    public class ProfessorController : ControllerBase
    {
        public IRepositorio _Repos { get; }

        public ProfessorController(IRepositorio repos)
        {
            _Repos = repos;
        }

        [HttpGet]
        public IActionResult Get()
        {
            var result = _Repos.GetAllProfessores(true);
            return Ok(result);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var professor = _Repos.GetByProfessorId(id); 
            if (professor == null)
                return BadRequest("Professor não encontrado");
            else
                return Ok(professor);
        }


        [HttpPost]
        public IActionResult Post(Professor professor)
        {
            _Repos.Add(professor);
            _Repos.SaveChanges();
            return Ok(professor);
        }

        [HttpPatch]
        public IActionResult Patch(int id, Professor professor)
        {
            var ProfessorAux = _Repos.GetByProfessorId(id);
            if (ProfessorAux == null)
                return BadRequest("professor não encontrado!");
            else
            {
                _Repos.Update(professor);
                _Repos.SaveChanges();
            }
            return Ok(professor);
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, Professor professor)
        {
            //para não travar o objeto e assim permitir o update .AsNoTracking()  
            var alunoAux = _Repos.GetByProfessorId(id);
            if (alunoAux == null)
                return BadRequest("Professor não encontrado!");
            else
            {
                _Repos.Update(professor);
                _Repos.SaveChanges();
            }

            return Ok(professor);
        }

        [HttpDelete]
        public IActionResult Delete(int id)
        {
            //aqui não é necessario usar AsNoTracking() porque é preciso bloquear o resgistro pra poder excluir
            var professor = _Repos.GetByProfessorId(id);
            if (professor == null)
                return BadRequest("Professor não encontrado!");
            else
            {
                _Repos.Delete(professor);
                _Repos.SaveChanges();
            }

            return Ok("");
        }
    }
}