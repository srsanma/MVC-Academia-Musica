using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace PrimerParcialSanMartin.Models
{
    public class AcademiaDbContext : DbContext
    {
        public virtual DbSet<Curso> Cursos{get;set;}
        public virtual DbSet<TipoCursada> TipoCursadas{get;set;}
        public AcademiaDbContext(DbContextOptions options) : base(options){

        }
        protected AcademiaDbContext(){

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder){

        }
    }
}