using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ListaLibrosAppWeb.Models;
using ListaLibrosAppWeb.Utilidades;
using Microsoft.SharePoint.Client;

namespace ListaLibrosAppWeb.Controllers
{
    public class LibrosController : Controller
    {
        // GET: Libros
        public ActionResult Index()
        {
            List<Libro> tareas = new List<Libro>();
            var url = Session["SPAppUrl"].ToString();

            using (ClientContext ctx = new ClientContext(url))
            {
                ctx.Credentials = GestionCuentas.GetCredentials();
                var lista = ctx.Web.Lists.GetByTitle("Libros");
                ctx.Load(lista);
                var query = new CamlQuery();

                var items = lista.GetItems(query);
                ctx.Load(items);

                ctx.ExecuteQuery();

                foreach (ListItem item in items)
                {
                    var t = new Libro()
                    {
                        Id = item.Id,
                        Descripcion = (string)item["Descripcion"],
                        Nombre = (string)item["Titulo"],
                        Disponibilidad = (bool)item["Disponibilidad"],
                        Edicion = Convert.ToInt32(item["Edicion"]),
                        Precio = Convert.ToDouble(item["Precio"]),
                    };

                    tareas.Add(t);
                }

            }


            return View(tareas);
        }
        public ActionResult Alta()
        {
            return View(new Libro());
        }
        [HttpPost]
        public ActionResult Alta(Libro model)
        {

            var url = Session["SPAppUrl"].ToString();

            using (ClientContext ctx = new ClientContext(url))
            {
                ctx.Credentials = GestionCuentas.GetCredentials();
                var lista = ctx.Web.Lists.GetByTitle("Libros");
                ctx.Load(lista);
                var itemC = new ListItemCreationInformation();
                var item = lista.AddItem(itemC);
                item["Titulo"] = model.Nombre;
                item["Descripcion"] = model.Descripcion;
                item["Precio"] = model.Precio;
                item["Edicion"] = model.Edicion;
                item["Disponibilidad"] = model.Disponibilidad;

                item.Update();
                ctx.ExecuteQuery();
            }
            return RedirectToAction("Index");
        }

        public ActionResult Borrar(int id)
        {
            var url = Session["SPAppUrl"].ToString();

            using (ClientContext ctx = new ClientContext(url))
            {
                ctx.Credentials = GestionCuentas.GetCredentials();
                var lista = ctx.Web.Lists.GetByTitle("Libros");
                ctx.Load(lista);
                var item = lista.GetItemById(id);
                item.DeleteObject();
                ctx.ExecuteQuery();


            }

            return RedirectToAction("Index");
        }
        public ActionResult Editar(int id)
        {
            var url = Session["SPAppUrl"].ToString();
            var libro = new Libro();
            using (ClientContext ctx = new ClientContext(url))
            {
                ctx.Credentials = GestionCuentas.GetCredentials();
                var lista = ctx.Web.Lists.GetByTitle("Libros");
                ctx.Load(lista);
                var item = lista.GetItemById(id);
                ctx.Load(item);
               ctx.ExecuteQuery();
               libro = new Libro()
               {
                   Id = item.Id,
                   Descripcion = (string)item["Descripcion"],
                   Nombre = (string)item["Titulo"],
                   Disponibilidad = (bool)item["Disponibilidad"],
                   Edicion = Convert.ToInt32(item["Edicion"]),
                   Precio = Convert.ToDouble(item["Precio"]),
               };



            }

            return View(libro);
        }

        [HttpPost]
        public ActionResult Editar(Libro model)
        {
            var url = Session["SPAppUrl"].ToString();

            using (ClientContext ctx = new ClientContext(url))
            {
                ctx.Credentials = GestionCuentas.GetCredentials();
                var lista = ctx.Web.Lists.GetByTitle("Libros");
                ctx.Load(lista);
                var item = lista.GetItemById(model.Id);
                item["Titulo"] = model.Nombre;
                item["Descripcion"] = model.Descripcion;
                item["Precio"] = model.Precio;
                item["Edicion"] = model.Edicion;
                item["Disponibilidad"] = model.Disponibilidad;



                item.Update();
                ctx.ExecuteQuery();


            }

            return RedirectToAction("Index");
        }

        public ActionResult Disponibilidad(int id)
        {
              var url = Session["SPAppUrl"].ToString();

              using (ClientContext ctx = new ClientContext(url))
              {
                  ctx.Credentials = GestionCuentas.GetCredentials();
                  var lista = ctx.Web.Lists.GetByTitle("Libros");
                  ctx.Load(lista);
                  var item = lista.GetItemById(id);
                  ctx.Load(item);
                  ctx.ExecuteQuery();
                  item["Disponibilidad"] = !((bool)item["Disponibilidad"]);
                  item.Update();
                  ctx.ExecuteQuery();
                  
              }

            return RedirectToAction("Index");
        }
    }
}