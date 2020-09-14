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
        public AppContext _Context { get; }

        public ProfessorController(AppContext context)
        {
            _Context = context;
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok(_Context.Professores.ToList());
        }

        [HttpGet("{id:int}")]
        public IActionResult GetById(int id)        
        {
            var professor = _Context.Professores.FirstOrDefault(a => a.Id == id);
            if (professor == null)
                return BadRequest("Professor não encontrado");
            else
                return Ok(professor);
        }

        [HttpGet("{ByName}")]
        public IActionResult GetByNome(string nome)
        {
            var professor = _Context.Professores.FirstOrDefault(
                a => a.Nome.Contains(nome)          
            );

            if (professor == null)
                return BadRequest("Professor não encontrado");
            else
                return Ok(professor);
        }

        [HttpPost]
        public IActionResult Post(Professor professor)
        {
            _Context.Add(professor);
            _Context.SaveChanges();
            return Ok(professor);
        }

        [HttpPatch]
        public IActionResult Patch(int id, Professor professor)
        {            
            var ProfessorAux = _Context.Professores.FirstOrDefault(a => a.Id == id);
            if(ProfessorAux == null)
                return BadRequest("professor não encontrado!");
            else{
                _Context.Update(professor);
                _Context.SaveChanges();
            }
            return Ok(professor);
        }        

        [HttpPut("{id}")]
        public IActionResult Put(int id, Professor professor)
        {     
            //para não travar o objeto e assim permitir o update .AsNoTracking()  
            var alunoAux = _Context.Alunos.AsNoTracking().FirstOrDefault(a => a.Id == id);
            if(alunoAux == null)
                return BadRequest("Professor não encontrado!");
            else{
                _Context.Update(professor);
                _Context.SaveChanges();
            }

            return Ok(professor);
        }

        [HttpDelete]
        public IActionResult Delete(int id)
        {
            //aqui não é necessario usar AsNoTracking() porque é preciso bloquear o resgistro pra poder excluir
            var professor = _Context.Professores.FirstOrDefault(a => a.Id == id);
            if(professor == null)
                return BadRequest("Professor não encontrado!");
            else{
                _Context.Remove(professor);
                _Context.SaveChanges();
            }

            return Ok("");
        }
    }
}