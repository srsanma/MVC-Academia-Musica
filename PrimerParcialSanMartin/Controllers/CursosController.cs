using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PrimerParcialSanMartin.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace PrimerParcialSanMartin.Controllers
{
    public class CursosController : Controller
    {
        private readonly IWebHostEnvironment Hosting;
        private readonly AcademiaDbContext db;
        public CursosController(IWebHostEnvironment hosting, AcademiaDbContext ctx)
        {
            db=ctx;
            Hosting = hosting;
        }
        
        public IActionResult Index(string CampoOrden, string busquedaNombre)
        {

            var listadoOrdenado = db.Cursos.Include(x => x.TipoCursada).ToList();
            if (!(String.IsNullOrEmpty(busquedaNombre)))
            {
                listadoOrdenado = listadoOrdenado.Where(x => x.Nombre.ToUpper().Contains(busquedaNombre.ToUpper()) || x.ProfesorACargo.ToUpper().Contains(busquedaNombre.ToUpper())).ToList();
            }
            

            switch (CampoOrden)
            {
                case "curso":
                    listadoOrdenado = listadoOrdenado.OrderBy(x => x.CodigoCurso).ToList();
                    break;
                case "nombre_curso":
                    listadoOrdenado = listadoOrdenado.OrderBy(x => x.Nombre).ToList();
                    break;
                case "duracion":
                    listadoOrdenado = listadoOrdenado.OrderBy(x => x.Duracion).ToList();
                    break;
                case "profesor":
                    listadoOrdenado = listadoOrdenado.OrderBy(x => x.ProfesorACargo).ToList();
                    break;
                default:
                    listadoOrdenado = listadoOrdenado.OrderBy(x => x.CodigoCurso).ToList();
                    break;
            }
            return View(listadoOrdenado);
        }
        public IActionResult Editar(int codigo)
        {
            var cursoBuscado = db.Cursos.Include(x => x.TipoCursada).ToList().Where(c => c.CodigoCurso == codigo).FirstOrDefault();
            
            ViewBag.Titulo = "Estamos editando a: " + cursoBuscado.Nombre;
            var cursadasDB = db.TipoCursadas.ToList();
            ViewBag.TipoCursada = new Microsoft.AspNetCore.Mvc.Rendering.SelectList(cursadasDB, "Codigo", "Nombre");

            if (!String.IsNullOrEmpty(cursoBuscado.FotoRuta))
            {
                string Carpeta = Path.Combine(Hosting.WebRootPath, "imagenes");
                string rutadestino = Path.Combine(Carpeta, cursoBuscado.FotoRuta);
                var streamArchivo = System.IO.File.OpenRead(rutadestino);
                cursoBuscado.Foto = new FormFile(streamArchivo, 0, streamArchivo.Length, null, Path.GetFileName(streamArchivo.Name));
            }
            return View(cursoBuscado);
        }

        [HttpPost]
        public IActionResult Editar(Curso curform)
        {
            if (ModelState.IsValid)
            {
                Curso cursoAnterior = db.Cursos.Include(x => x.TipoCursada).ToList().Where(c => c.CodigoCurso == curform.CodigoCurso).FirstOrDefault();
                {
                    //si la foto es null no guardar nada sino si
                    if (curform.Foto != null)
                    {
                        curform.FotoRuta = GuardarImagen(curform.Foto).Result;
                        
                    }
                    db.Cursos.Remove(cursoAnterior);
                    db.Cursos.Add(curform);
                    db.SaveChanges();
                    
                }
                
                return RedirectToAction("Index");
            }
            return View(curform);
        }

        public IActionResult Crear()
        {
            ViewBag.TipoCursada = new Microsoft.AspNetCore.Mvc.Rendering.SelectList(db.TipoCursadas, "Codigo", "Nombre");
            return View();
        }

        [HttpPost]
        public IActionResult Crear(Curso curform)
        {
            ViewBag.TipoCursada = new Microsoft.AspNetCore.Mvc.Rendering.SelectList(db.TipoCursadas, "Codigo", "Nombre");

            if (ModelState.IsValid)
            {
                if (ValidarExisteCodigo(curform.CodigoCurso))
                {
                    ModelState.AddModelError("CodigoCurso", "El codigo ya existe");
                }
                else
                {

                    curform.FotoRuta = GuardarImagen(curform.Foto).Result;
                    db.Cursos.Add(curform);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                
            }
            return View(curform);
            
        }

        public IActionResult Ver(int codigo)
        {
            var cursoBuscado = db.Cursos.ToList().Where(c => c.CodigoCurso == codigo).FirstOrDefault();
            ViewBag.Titulo = "Estamos viendo al curso: " + cursoBuscado.Nombre;
            ViewBag.TipoCursada = new Microsoft.AspNetCore.Mvc.Rendering.SelectList(db.TipoCursadas.ToList(), "Nombre", "Nombre");
            return View(cursoBuscado);
        }
        
        public IActionResult Eliminar(int codigo)
        {
            var productoOriginal = db.Cursos.ToList().Where(x => x.CodigoCurso == codigo).FirstOrDefault();
            db.Cursos.Remove(productoOriginal);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public bool ValidarExisteCodigo(int? codigo)
        {
            return db.Cursos.ToList().Exists(x => x.CodigoCurso == codigo);
        }
        private async Task<string> GuardarImagen(IFormFile Foto)
        {
            if (Foto != null)
            {
                string NombreArchivo = Foto.FileName;
                string Carpeta = Path.Combine(Hosting.WebRootPath, "imagenes");
                string rutadestino = Path.Combine(Carpeta, NombreArchivo);
                using(Stream fileStream = new FileStream(rutadestino, FileMode.Create))
                {
                    await Foto.CopyToAsync(fileStream);
                }
                return NombreArchivo;
            }
            return "";
        }
    }

}
