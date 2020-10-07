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
    /// Controlador Aluno
    /// </summary>  
    [ApiController]
    [Route("api/[controller]")]
    public class AlunoController : ControllerBase
    {
        public readonly IRepositorio _Repos;
        private readonly IMapper _mapper;

        public AlunoController(IRepositorio repos, IMapper mapper)
        {
            _mapper = mapper;
            _Repos = repos;
        }

        [HttpGet]
        public IActionResult Get()
        {            
            var alunos = _Repos.GetAllAlunos();
            var result = _mapper.Map<IEnumerable<AlunoDto>>(alunos);
            return Ok(result);
        }

        [HttpGet("{id:int}")]
        public IActionResult GetBy(int id)
        {
            var aluno = _Repos.GetByAlunoId(id);
            if (aluno == null)
                return BadRequest("Aluno não encontrado");
            else{
                var result = _mapper.Map<AlunoDto>(aluno);                
                return Ok(result);
            }                
        }

        [HttpPost]
        public IActionResult Post(AlunoRegistrarDto model)
        {
            var aluno = _mapper.Map<Aluno>(model);
            _Repos.Add(aluno);
            _Repos.SaveChanges();
            //redireciona para GetBy
            return Created($"/api/aluno/{model.Id}", _mapper.Map<AlunoDto>(aluno));
        }

        [HttpPatch]
        public IActionResult Patch(int id, AlunoRegistrarDto model)
        {
            var aluno = _Repos.GetByAlunoId(id);
            if (aluno == null)
                return BadRequest("Aluno não encontrado!");
            else
            {
                _mapper.Map(model, aluno);

                _Repos.Update(aluno);
                _Repos.SaveChanges();

                //redireciona para GetBy
                return Created($"/api/aluno/{model.Id}", _mapper.Map<AlunoDto>(aluno));
            }
          
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, AlunoRegistrarDto model)
        {
            //para não travar o objeto e assim permitir o update .AsNoTracking()  
            var aluno = _Repos.GetByAlunoId(id);
            if (aluno == null)
                return BadRequest("Aluno não encontrado!");
            else
            {
                _mapper.Map(model, aluno);
                _Repos.Update(aluno);
                _Repos.SaveChanges();

                //redireciona para GetBy
                return Created($"/api/aluno/{model.Id}", _mapper.Map<AlunoDto>(aluno));
            }            
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
                return Ok("");
            }
        }


    }
}