using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GenericUtilities.Data;
using GenericUtilities.Services;
using SchoolManager.Site.Domain.Models;

namespace SchoolManager.Site.Business.Services
{
    /// <summary>
    /// Classe responsável por toda regra de negócio da entidade Classroom
    /// </summary>
    public class ClassroomService : CrudService<Classroom>
    {
        public ClassroomService(DbContext context)
            : base(context)
        {
            
        }

        /// <summary>
        /// Pesquisa uma turma pelo seu ID
        /// </summary>
        /// <param name="id">Identificador da turma</param>
        /// <returns></returns>
        public Classroom Sigle(int id)
        {
            var rep = new RepositoryManager<Classroom>(myContext);
            var classroom = new Classroom();

            classroom = rep.Single(x => x.ID == id);

            return classroom;
        }

        /// <summary>
        /// Retorna todas as turmas cadastradas
        /// </summary>
        /// <returns></returns>
        public IList<Classroom> GetAll()
        {
            var rep = new RepositoryManager<Classroom>(myContext);

            return rep.GetAll();
        }
    }
}
