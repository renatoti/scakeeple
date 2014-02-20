using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SchoolManager.Site.Business.Services;
using SchoolManager.Site.Data;
using SchoolManager.Site.Domain.Models;

namespace SchoolManager.Site.Controllers
{
    public class CollegeController : SiteBaseController
    {
        private readonly Context ctx;

        /// <summary>
        /// Construtor com a utilização do Simple Injector MVC
        /// </summary>
        /// <param name="context">Contexto configurado no Global.asax</param>
        public CollegeController(Context context)
        {
            ctx = context;
        }

        /// <summary>
        /// Lista todas as escolas cadastradas
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            using (ctx)
            {
                var srvCollege = new CollegeService(ctx);
                var lstColleges = srvCollege.GetAll();

                return View(lstColleges);
            }
        }

        /// <summary>
        /// Apresenta a tela para inclusão de uma nova escola
        /// </summary>
        /// <returns></returns>
        public ActionResult Create()
        {
            return PartialView("_ModalForm", new College());
        }

        /// <summary>
        /// Apresenta a tela para edição de uma escola
        /// </summary>
        /// <param name="id">Código de identificação da escola</param>
        /// <returns></returns>
        public ActionResult Edit(int id)
        {
            using (ctx)
            {
                var srvCollege = new CollegeService(ctx);
                var oCollege = srvCollege.Sigle(id);

                return PartialView("_ModalForm", oCollege);
            }
        }

        /// <summary>
        /// Realiza a exclusão de uma escola
        /// </summary>
        /// <param name="id">Código de identificação da escola</param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Delete(int id)
        {
            var vSuccess = true;
            var vErro = string.Empty;

            try
            {
                using (ctx)
                {
                    var srvCollege = new CollegeService(ctx);
                    var oCollege = srvCollege.Sigle(id);

                    vSuccess = srvCollege.DeleteAndSave(oCollege);
                }
            }
            catch (Exception ex)
            {
                vSuccess = false;
                vErro = "Ocorreu um erro durante a exclusão! " + ex.Message;
            }

            return Json(new { success = vSuccess, message = vErro });
        }

        /// <summary>
        /// Salva os dados da escola no banco de dados
        /// </summary>
        /// <param name="oCollege">Objeto com todos os dados da escola</param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Save(College oCollege)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    using (ctx)
                    {
                        var srvCollege = new CollegeService(ctx);

                        if (oCollege.ID == 0)
                            srvCollege.InsertAndSave(oCollege);
                        else
                            srvCollege.UpdateAndSave(oCollege);
                    }
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", "Ocorreu um erro: " + ex.Message);
                }
            }

            return PartialView("_ModalForm", oCollege);
        }

    }
}
