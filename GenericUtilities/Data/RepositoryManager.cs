using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.Objects;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenericUtilities.Data
{
    /// <summary>
    /// Abstração de Repositório para facilitar o uso dos objetos POCOS do EntityFramework
    /// </summary>
    /// <typeparam name="T">Tipo da classe a ser utilizado</typeparam>
    public class RepositoryManager<T> where T : class
    {
        /// <summary>
        /// Construtor da classe
        /// </summary>
        /// <param name="DbContext">Contexto que está controlando a transação</param>
        public RepositoryManager(DbContext context)
        {
            _context = context;
            _objectSet = _context.Set<T>();
        }

        #region Propriedades  e constantes

        private const string MSG_ENTITY_NULL = "entidade nula.";

        /// <summary>
        /// Representa contexto de dados da aplicação
        /// </summary>
        private DbContext _context;

        /// <summary>
        /// Representa a Entidade corrente
        /// </summary>
        private IDbSet<T> _objectSet;

        #endregion

        #region Métodos

        /// <summary>
        ///Tráz todos os objetos no formato IQueryable
        /// </summary>
        /// <returns>Um objeto IQueryable que contém os resultados da query</returns>
        public IQueryable<T> Fetch()
        {
            return _objectSet;
        }

        /// <summary>
        /// Tráz todos os objetos no formato IList
        /// </summary>
        /// <returns>Um objeto IList que contém os resultados da query</returns>
        public IList<T> GetAll()
        {
            return Fetch().ToList();
        }

        /// <summary>
        /// Procura registro com um critério específico
        /// </summary>
        /// <param name="predicate">Critério de pesquisa</param>
        /// <returns>Um IEnumerable contendo os resultados da query</returns>
        public IEnumerable<T> Find(Func<T, bool> predicate)
        {
            return _objectSet.Where<T>(predicate);
        }

        /// <summary>
        /// Tráz um único registro contendo specificado no critério de pesquisa (normalmente o identificado iunivoquo)
        /// </summary>
        /// <param name="predicate">Critério de pesquisa</param>
        /// <returns>Um objeto contendo o resultado da query</returns>
        public T Single(Func<T, bool> predicate)
        {
            return _objectSet.SingleOrDefault<T>(predicate);
        }

        /// <summary>
        /// Tráz o primeiro registro que atende o critério de pesquisa
        /// </summary>
        /// <param name="predicate">Criterio de pesquisa</param>
        /// <returns>Um objeto contendo o resultado da query</returns>
        public T First(Func<T, bool> predicate)
        {
            return _objectSet.FirstOrDefault<T>(predicate);
        }

        /// <summary>
        /// Deleta uma entidade 
        /// </summary>
        /// <param name="entity">Entidade a ser deletada</param>
        /// <exception cref="ArgumentNullException"> if <paramref name="entity"/> is null</exception>
        public void Delete(T entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(MSG_ENTITY_NULL);
            }

            _objectSet.Remove(entity);
        }

        /// <summary>
        /// Deleta registro que atendam o critério de pesquisa
        /// </summary>
        /// <param name="predicate">Critério de pesquisa</param>
        public void Delete(Func<T, bool> predicate)
        {
            //IEnumerable<T> records = from x in _objectSet.Where<T>(predicate) select x;
            IEnumerable<T> records = _objectSet.Where<T>(predicate);

            foreach (T record in records)
            {
                _objectSet.Remove(record);
            }
        }

        /// <summary>
        ///  Inseri um registro no banco de dados
        /// </summary>
        /// <param name="entity">Entidade a ser inserida</param>
        /// <exception cref="ArgumentNullException"> if <paramref name="entity"/> is null</exception>
        public void Insert(T entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(MSG_ENTITY_NULL);
            }

            _objectSet.Add(entity);
        }

        /// <summary>
        /// Atualiza no banco de dados caso o sem verificar se o registro já existe
        /// </summary>
        /// <param name="entity">Entity a ser salva</param>
        /// <exception cref="ArgumentNullException"> if <paramref name="entity"/> is null</exception>
        public void Update(T entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(MSG_ENTITY_NULL);
            }

            _objectSet.Attach(entity);

            _context.Entry(entity).State = EntityState.Modified;
        }


        /// <summary>
        /// Insere no banco de dados caso o registro já exista atualiza
        /// </summary>
        /// <param name="entity">Entity a ser salva</param>
        /// <exception cref="ArgumentNullException"> if <paramref name="entity"/> is null</exception>
        public void Save(T entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(MSG_ENTITY_NULL);
            }

            _context.Entry(entity).State = _objectSet.Contains<T>(entity) ? EntityState.Modified : EntityState.Added;

            _objectSet.Attach(entity);
        }

        /// <summary>
        /// Atacha o objeto no contexto de banco de dados da aplicação para que mesmo possa saber fazer as
        /// alterações no banco de dados
        /// </summary>
        /// <param name="entity">Entity a ser atachaada</param>        
        /// <exception cref="ArgumentNullException"> if <paramref name="entity"/> is null</exception>
        public void Attach(T entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(MSG_ENTITY_NULL);
            }

            _objectSet.Attach(entity);

            //_context.Entry(entity).State = state;
        }

        #endregion

    }
}
