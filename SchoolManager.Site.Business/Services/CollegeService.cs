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
    /// Classe responsável por toda a regra de negócio da entidade College
    /// </summary>
    public class CollegeService : CrudService<College>
    {
        public CollegeService(DbContext context) 
            : base (context)
        {
            
        }

        /// <summary>
        /// Pesquisa uma escola pelo seu ID
        /// </summary>
        /// <param name="id">Identificador da escola</param>
        /// <returns></returns>
        public College Sigle(int id)
        {
            var rep = new RepositoryManager<College>(myContext);
            var college = new College();

            college = rep.Single(x => x.ID == id);

            return college;
        }

        /// <summary>
        /// Retorna todas as escolas cadastradas
        /// </summary>
        /// <returns></returns>
        public IList<College> GetAll()
        {
            var rep = new RepositoryManager<College>(myContext);

            return rep.GetAll();
        }
    }
}
