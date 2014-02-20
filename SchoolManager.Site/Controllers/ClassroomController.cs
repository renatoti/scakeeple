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
    public class ClassroomController : SiteBaseController
    {
        private readonly Context ctx;

        /// <summary>
        /// Construtor com a utilização do Simple Injector MVC
        /// </summary>
        /// <param name="context">Contexto configurado no Global.asax</param>
        public ClassroomController(Context context)
        {
            ctx = context;
        }

        /// <summary>
        /// Lista todas as turmas cadastradas
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            var srvClassroom = new ClassroomService(ctx);
            var lstClassrooms = srvClassroom.GetAll();

            return View(lstClassrooms);
        }

        /// <summary>
        /// Apresenta a tela para inclusão de uma nova turma
        /// </summary>
        /// <returns></returns>
        public ActionResult Create()
        {
            using (ctx)
            {
                // Cria uma lista de escolas para preencher o dropdownlist
                var srvCollege = new CollegeService(ctx);
                var lstColleges = srvCollege.GetAll();

                ViewBag.Colleges = new SelectList(lstColleges, "ID", "Name");
            }

            return PartialView("_ModalForm", new Classroom());
        }

        /// <summary>
        /// Apresenta a tela para edição de uma turma
        /// </summary>
        /// <param name="id">Código de identificação da turma</param>
        /// <returns></returns>
        public ActionResult Edit(int id)
        {
            using (ctx)
            {
                var srvClassroom = new ClassroomService(ctx);
                var oClassroom = srvClassroom.Sigle(id);

                // Cria uma lista de escolas para preencher o dropdownlist
                var srvCollege = new CollegeService(ctx);
                var lstColleges = srvCollege.GetAll();

                ViewBag.Colleges = new SelectList(lstColleges, "ID", "Name");

                return PartialView("_ModalForm", oClassroom);
            }
        }

        /// <summary>
        /// Realiza a exclusão de uma turma
        /// </summary>
        /// <param name="id">Código de identificação da turma</param>
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
                    var srvClassroom = new ClassroomService(ctx);
                    var oClassroom = srvClassroom.Sigle(id);

                    vSuccess = srvClassroom.DeleteAndSave(oClassroom);
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
        /// Salva os dados da turma no banco de dados
        /// </summary>
        /// <param name="oClassroom">Objeto com todos os dados da turma</param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Save(Classroom oClassroom)
        {
            using (ctx)
            {
                if (ModelState.IsValid)
                {
                    try
                    {
                        var srvClassroom = new ClassroomService(ctx);

                        if (oClassroom.ID == 0)
                            srvClassroom.InsertAndSave(oClassroom);
                        else
                            srvClassroom.UpdateAndSave(oClassroom);
                    }
                    catch (Exception ex)
                    {
                        ModelState.AddModelError("", "Ocorreu um erro: " + ex.Message);
                    }
                }


                // Cria uma lista de escolas para preencher o dropdownlist
                var srvCollege = new CollegeService(ctx);
                var lstColleges = srvCollege.GetAll();

                ViewBag.Colleges = new SelectList(lstColleges, "ID", "Name");
            }

            return PartialView("_ModalForm", oClassroom);
        }
    }
}
