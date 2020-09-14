using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using SmartSchool.WebAPI.Models;

namespace SmartSchool.WebAPI.Data
{
    public class AppContext : DbContext
    {
        public AppContext(DbContextOptions<AppContext> options) : base(options) { }
        
        public DbSet<Aluno> Alunos {get; set;}
        public DbSet<Professor> Professores {get; set;}
        public DbSet<Disciplina> Disciplinas {get; set;}
        public DbSet<AlunoDisciplina> AlunosDisciplinas {get; set;}

        //sobrescrevendo metodo OnModelCreating, para configuração relação muitos pra muitos
        protected override void OnModelCreating(ModelBuilder builder){
            builder.Entity<AlunoDisciplina>()
                .HasKey(ad => new {ad.AlunoId , ad.DisciplinaId});//fala para o entity que se trata de chave composta            
        }
    }
}