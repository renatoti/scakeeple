using GenericUtilities.Data;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenericUtilities.Services
{
    /// <summary>
    /// Classe de serviço base, para construção de regras de negócio baseadas em CRUD
    /// </summary>
    /// <typeparam name="T">Classe que mapeia o a entidade no banco de dados</typeparam>
    public class CrudService<T> : BaseDataService where T : class
    {

        public CrudService(DbContext context)
            : base(context)
        {

        }

        #region Métodos Públicos

        /// <summary>
        /// Validações de regra de negócio que possam ser aplicadas para permissão da alteração ou inserção 
        /// de uma determinada entidade
        /// </summary>
        /// <param name="entity">Entidade a ser presistida</param>
        /// <returns>Se a entidade pode ser persistida</returns>
        public virtual bool CanSave(T entity)
        {
            return true;
        }

        public virtual void Insert(T entity)
        {
            if (!CanSave(entity))
                return;
            var rep = new RepositoryManager<T>(myContext);

            rep.Insert(entity);
        }

        public virtual bool InsertAndSave(T entity)
        {
            if (!CanSave(entity))
                return false;

            Insert(entity);
            return myContext.SaveChanges() > 0;
        }

        public virtual void Update(T entity)
        {
            if (!CanSave(entity))
                return;

            var rep = new RepositoryManager<T>(myContext);

            rep.Update(entity);
        }

        public virtual bool UpdateAndSave(T entity)
        {
            if (!CanSave(entity))
                return false;

            Update(entity);
            return myContext.SaveChanges() > 0;
        }

        public virtual void Delete(T entity)
        {
            var rep = new RepositoryManager<T>(myContext);

            rep.Delete(entity);
        }

        public virtual bool DeleteAndSave(T entity)
        {
            Delete(entity);
            return myContext.SaveChanges() > 0;
        }

        #endregion
    }
}
