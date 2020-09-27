using SmartSchool.WebAPI.Models;

namespace SmartSchool.WebAPI.Data
{
    public interface IRepositorio
    {
        //T é um tipo generico, ao usar Where eu obrigo T ser do tipo classe
         void Add<T>(T entity) where T : class;
         void Update<T>(T entity) where T : class;
         void Delete<T>(T entity) where T : class;
         bool SaveChanges();
         void SaveChangesAsync();

         //ALUNOS
         Aluno[] GetAllAlunos(bool includeProfessor = false);
         Aluno[] GetAllAlunosByDiciplinaId(int DiciplinaId, bool includeProfessor = false);
         Aluno GetByAlunoId(int AlunoId, bool includeProfessor = false);

         //PROFESSORES
         Professor[] GetAllProfessores(bool includeAlunos = false);
         Professor[] GetAllProfessoresByDiciplinaId(int DisciplinaId, bool includeAlunos = false);
         Professor GetByProfessorId(int ProfessorId, bool incluirProfessor = false);
    }
}