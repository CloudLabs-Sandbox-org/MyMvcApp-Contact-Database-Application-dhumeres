using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using MyMvcApp.Models;

namespace MyMvcApp.Controllers;

public class UserController : Controller
{
    public static System.Collections.Generic.List<User> userlist = new System.Collections.Generic.List<User>();

    // GET: User
    public ActionResult Index()
    {
        // Muestra la lista de usuarios
        return View(userlist);
    }

    // GET: User/Search?query=texto
    public ActionResult Search(string query)
    {
        if (string.IsNullOrWhiteSpace(query))
            return View("Index", userlist);

        var results = userlist
            .Where(u =>
                (!string.IsNullOrEmpty(u.Name) && u.Name.Contains(query, StringComparison.OrdinalIgnoreCase)) ||
                (!string.IsNullOrEmpty(u.Email) && u.Email.Contains(query, StringComparison.OrdinalIgnoreCase)) ||
                (!string.IsNullOrEmpty(u.Phone) && u.Phone.Contains(query, StringComparison.OrdinalIgnoreCase))
            )
            .ToList();

        return View("Index", results);
    }

    // GET: User/Details/5
    public ActionResult Details(int id)
    {
        // Busca el usuario por ID
        var user = userlist.FirstOrDefault(u => u.Id == id);
        if (user == null)
            return NotFound();
        return View(user);
    }

    // GET: User/Create
    public ActionResult Create()
    {
        // Muestra el formulario de creación
        return View();
    }

    // POST: User/Create
    [HttpPost]
    [ValidateAntiForgeryToken]
    public ActionResult Create(User user)
    {
        if (ModelState.IsValid)
        {
            // Asigna un ID único
            user.Id = userlist.Count > 0 ? userlist.Max(u => u.Id) + 1 : 1;
            userlist.Add(user);
            return RedirectToAction(nameof(Index));
        }
        return View(user);
    }

    // GET: User/Edit/5
    public ActionResult Edit(int id)
    {
        var user = userlist.FirstOrDefault(u => u.Id == id);
        if (user == null)
            return NotFound();
        return View(user);
    }

    // POST: User/Edit/5
    [HttpPost]
    [ValidateAntiForgeryToken]
    public ActionResult Edit(int id, User user)
    {
        var existingUser = userlist.FirstOrDefault(u => u.Id == id);
        if (existingUser == null)
            return NotFound();

        if (ModelState.IsValid)
        {
            // Actualiza los datos del usuario
            existingUser.Name = user.Name;
            existingUser.Email = user.Email;
            existingUser.Phone = user.Phone;
            // Agrega aquí otros campos si existen
            return RedirectToAction(nameof(Index));
        }
        return View(user);
    }

    // GET: User/Delete/5
    public ActionResult Delete(int id)
    {
        var user = userlist.FirstOrDefault(u => u.Id == id);
        if (user == null)
            return NotFound();
        return View(user);
    }

    // POST: User/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public ActionResult DeleteConfirmed(int id)
    {
        var user = userlist.FirstOrDefault(u => u.Id == id);
        if (user != null)
        {
            userlist.Remove(user);
        }
        return RedirectToAction(nameof(Index));
    }
}