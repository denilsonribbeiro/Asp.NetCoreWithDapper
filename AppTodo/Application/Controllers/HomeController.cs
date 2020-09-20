using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Application.Models;
using ORM.Interfaces;
using Entities;

namespace Application.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ITodoRepository _todoRepository;

        public HomeController(ILogger<HomeController> logger, ITodoRepository todoRepository)
        {
            _logger = logger;
            _todoRepository = todoRepository;
        }

        public IActionResult Index()
        {
            return View(_todoRepository.GetAll());
        }

        public IActionResult Cadastrar()
        {
            return View();
        }
        
        [HttpPost]
        public IActionResult Cadastrar(ToDo obj)
        {
            if (ModelState.IsValid)
            {
                _todoRepository.Add(obj);
                Notification.Set(TempData, new Notificacao() { Mensagem = "Tarefa cadastrada com sucesso!", Tipo = TipoNotificacao.success });
                return View("Index", _todoRepository.GetAll());
            }
            else
            {
                Notification.Set(TempData, new Notificacao() { Mensagem = "Não foi possível cadastrar essa tarefa.", Tipo = TipoNotificacao.danger });
                return View();
            }
        }

        public IActionResult Editar(int id)
        {
            return View(_todoRepository.Get(id));
        }

        [HttpPost]
        public IActionResult Editar(ToDo obj)
        {
            if (ModelState.IsValid)
            {
                _todoRepository.Update(obj);
                Notification.Set(TempData, new Notificacao() { Mensagem = "Tarefa editada com sucesso!", Tipo = TipoNotificacao.info });
                return View("Index", _todoRepository.GetAll());
            }
            else
            {
                Notification.Set(TempData, new Notificacao() { Mensagem = "Não foi possível editar a tarefa.", Tipo = TipoNotificacao.warning });
                return View();
            }
            
        }

        public IActionResult Remover(int id)
        {
            return View(_todoRepository.Get(id));
        }

        public IActionResult ConfirmaRemover(int id)
        {
            if (id != null)
            {
                _todoRepository.Remove(_todoRepository.Get(id));
                Notification.Set(TempData, new Notificacao() { Mensagem = "Tarefa removida com sucesso!", Tipo = TipoNotificacao.success});
                return View("Index", _todoRepository.GetAll());
            }
            else
            {
                _todoRepository.Remove(_todoRepository.Get(id));
                Notification.Set(TempData, new Notificacao() { Mensagem = "Não foi possível remover a tarefa.", Tipo = TipoNotificacao.warning });
                return View("Index", _todoRepository.GetAll());
            }
            
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
