using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenericUtilities.Common
{
    /// <summary>
    /// Classe básica de Herança de todas as outras classes
    /// </summary>
    public class BaseObject
    {
        /// <summary>
        /// Construtor da classe.
        /// </summary>
        public BaseObject()
        {
            Message = string.Empty;
        }

        #region Properties

        /// <summary>
        /// Mensagens utilizadas para diversos fins, especialmente informativas de erros.
        /// </summary>
        public String Message { get; protected set; }

        #endregion
    }
}
