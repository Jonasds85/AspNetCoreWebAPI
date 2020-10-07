using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SmartSchool.WebAPI.Data;
using SmartSchool.WebAPI.Dtos;
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
        public IMapper _mapper {get;}

        public ProfessorController(IRepositorio repos, IMapper mapper)
        {
            _Repos = repos;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult Get()
        {
            var professores = _Repos.GetAllProfessores(true);
            var result = _mapper.Map<IEnumerable<ProfessorDto>>(professores);
            return Ok(result);
        }

        [HttpGet("[action]")]
        public IActionResult ProfessorRegistrarDto()
        {
            return Ok(new ProfessorRegistrarDto());
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var professor = _Repos.GetByProfessorId(id); 
            if (professor == null)
                return BadRequest("Professor não encontrado");
            else{
                var result = _mapper.Map<ProfessorDto>(professor);
                return Ok(result);
            }                
        }


        [HttpPost]
        public IActionResult Post(ProfessorRegistrarDto model)
        {
            var professor =  _mapper.Map<Professor>(model);
            _Repos.Add(professor);
            _Repos.SaveChanges();
            return Created($"/api/Professor/GetById/{professor.Id}", _mapper.Map<ProfessorDto>(professor));           
        }

        [HttpPatch]
        public IActionResult Patch(int id, ProfessorRegistrarDto model)
        {
            var Professor = _Repos.GetByProfessorId(id);
            if (Professor == null)
                return BadRequest("professor não encontrado!");
            else
            {
                var professor =  _mapper.Map<Professor>(model);
                _Repos.Update(professor);
                _Repos.SaveChanges();
                return Created($"/api/Professor/GetById/{professor.Id}", _mapper.Map<ProfessorDto>(professor)); 
            }
            
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, ProfessorRegistrarDto model)
        {
            //para não travar o objeto e assim permitir o update .AsNoTracking()  
            var professor = _Repos.GetByProfessorId(id);
            if (professor == null)
                return BadRequest("Professor não encontrado!");
            else
            {
                _mapper.Map(model, professor);
                _Repos.Update(professor);
                _Repos.SaveChanges();
                return Created($"/api/Professor/GetById/{professor.Id}", _mapper.Map<ProfessorDto>(professor));
            }

            
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