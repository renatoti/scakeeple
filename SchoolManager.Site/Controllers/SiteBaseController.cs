using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GenericUtilities.Controllers;
using SchoolManager.Site.Data;
using SchoolManager.Site.Models;

namespace SchoolManager.Site.Controllers
{
    /// <summary>
    /// Controller base específico do projeto
    /// </summary>
    [HandleError]
    public class SiteBaseController : BaseController
    {
        /// <summary>
        /// Apresenta o modal para confirmar uma ação
        /// </summary>
        /// <param name="oConfirm">Modelo com os dados para apresentação do modal</param>
        /// <returns></returns>
        public ActionResult ConfirmModal(ConfirmVM oConfirm)
        {
            return PartialView("_ConfirmModal", oConfirm);
        }

    }
}
