using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenericUtilities.Services
{
    /// <summary>
    /// Classe base de toda a regra de negócio com acesso a banco de dados
    /// </summary>
    public class BaseDataService : BaseService
    {
        #region Construtor padrão

        public BaseDataService(DbContext context)
        {
            this.myContext = context;
        }
        
        #endregion
        
        #region Propriedades

        public DbContext myContext { get; protected set; }

        #endregion


    }
}
