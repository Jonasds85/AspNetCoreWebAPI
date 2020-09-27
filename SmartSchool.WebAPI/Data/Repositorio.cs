using System.Linq;
using Microsoft.EntityFrameworkCore;
using SmartSchool.WebAPI.Models;

namespace SmartSchool.WebAPI.Data
{
    public class Repositorio : IRepositorio
    {
        public readonly AppContext _Context;

        //em Startup.ConfigureServices, esta configurado a injeção do tipo Repositorio, no qual tem acesso ao AppContext
        public Repositorio(AppContext Context)
        {
            _Context = Context;
        }
        public void Add<T>(T entity) where T : class
        {
            _Context.Add(entity);
        }
        public void Update<T>(T entity) where T : class
        {
            _Context.Update(entity);
        }
        public void Delete<T>(T entity) where T : class
        {
            _Context.Remove(entity);
        }

        public bool SaveChanges()
        {
            var saved = _Context.SaveChanges();
            if (saved > 0)
                return true;
            else
                return false;
        }

        public void SaveChangesAsync()
        {
            _Context.SaveChangesAsync();
        }

        public Aluno[] GetAllAlunos(bool includeProfessor = false)
        {
            IQueryable<Aluno> query = _Context.Alunos;
            if (includeProfessor)
            {
                query = query.Include(a => a.AlunosDisciplinas)//busco objeto alunodisciplina
                             .ThenInclude(ad => ad.Disciplina)//dentro de alunodisciplina busco disciplina
                             .ThenInclude(d => d.Professor);//na disciplina busco o professor
            }

            query = query.AsNoTracking().OrderBy(a => a.Id);
            return query.ToArray();
        }

        public Aluno[] GetAllAlunosByDiciplinaId(int DiciplinaId, bool includeProfessor = false)
        {
            IQueryable<Aluno> query = _Context.Alunos;
            if (includeProfessor)
            {
                query = query.Include(a => a.AlunosDisciplinas)//busco objeto alunodisciplina
                             .ThenInclude(ad => ad.Disciplina)//dentro de alunodisciplina busco disciplina
                             .ThenInclude(d => d.Professor);//na disciplina busco o professor
            }

            //.Any é uma verificação se existe a condição passada como parametro, retorna true ou false 
            query = query.AsNoTracking().Where(a => a.AlunosDisciplinas.Any(d => d.DisciplinaId == DiciplinaId));
            query = query.OrderBy(a => a.Id);

            return query.ToArray();
        }

        public Aluno GetByAlunoId(int AlunoId, bool includeProfessor = false)
        {
            IQueryable<Aluno> query = _Context.Alunos;
            if (includeProfessor)
            {
                query = query.Include(a => a.AlunosDisciplinas)//busco objeto alunodisciplina
                             .ThenInclude(ad => ad.Disciplina)//dentro de alunodisciplina busco disciplina
                             .ThenInclude(d => d.Professor);//na disciplina busco o professor
            }

            query = query.AsNoTracking().Where(a => a.Id == AlunoId);
            return query.FirstOrDefault();
        }

        public Professor[] GetAllProfessores(bool includeAlunos = false)
        {
            IQueryable<Professor> query =  _Context.Professores;
            if (includeAlunos)
            {
                query = query.Include(p => p.Disciplinas)
                             .ThenInclude(d => d.AlunosDisciplinas)
                             .ThenInclude(ad => ad.Aluno);
            }

            query = query.AsNoTracking().OrderBy(p => p.Id);
            return query.ToArray();
        }

        public Professor[] GetAllProfessoresByDiciplinaId(int DisciplinaId, bool includeAlunos = false)
        {
            IQueryable<Professor> query =  _Context.Professores;
            if (includeAlunos)
            {
                query = query.Include(p => p.Disciplinas)
                             .ThenInclude(d => d.AlunosDisciplinas)
                             .ThenInclude(ad => ad.Aluno);
            }

            query = query.AsNoTracking()//não bloquea tabela
                         .Where(prof => prof.Disciplinas.Any(//se existe a condição passada, diciplina dos alunos com o id passado po parametro
                             d => d.AlunosDisciplinas.Any(
                                 ad => ad.DisciplinaId == DisciplinaId
                             )
                         ))
                         .OrderBy(aluno => aluno.Id);            
           
            return query.ToArray();
        }

        public Professor GetByProfessorId(int ProfessorId, bool incluirAluno = false)
        {
            IQueryable<Professor> query =  _Context.Professores;
            if (incluirAluno)
            {
                query = query.Include(p => p.Disciplinas)
                             .ThenInclude(d => d.AlunosDisciplinas)
                             .ThenInclude(ad => ad.Aluno);
            }

            query = query.AsNoTracking()
                         .Where(p => p.Id == ProfessorId);

            return query.FirstOrDefault();
        }
    }
}