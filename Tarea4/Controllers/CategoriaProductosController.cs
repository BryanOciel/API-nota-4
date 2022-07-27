using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using Tarea4.Models;

namespace Tarea4.Controllers
{
    public class CategoriaProductosController : ApiController
    {
        private Tarea1Entities db = new Tarea1Entities();

        // GET: api/CategoriaProductos
        public IQueryable<CategoriaProductos> GetCategoriaProductos()
        {
            return db.CategoriaProductos;
        }

        // GET: api/CategoriaProductos/5
        [ResponseType(typeof(CategoriaProductos))]
        public IHttpActionResult GetCategoriaProductos(int id)
        {
            CategoriaProductos categoriaProductos = db.CategoriaProductos.Find(id);
            if (categoriaProductos == null)
            {
                return NotFound();
            }

            return Ok(categoriaProductos);
        }

        // PUT: api/CategoriaProductos/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutCategoriaProductos(int id, CategoriaProductos categoriaProductos)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != categoriaProductos.Id)
            {
                return BadRequest();
            }

            db.Entry(categoriaProductos).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CategoriaProductosExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/CategoriaProductos
        [ResponseType(typeof(CategoriaProductos))]
        public IHttpActionResult PostCategoriaProductos(CategoriaProductos categoriaProductos)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.CategoriaProductos.Add(categoriaProductos);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (CategoriaProductosExists(categoriaProductos.Id))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = categoriaProductos.Id }, categoriaProductos);
        }

        // DELETE: api/CategoriaProductos/5
        [ResponseType(typeof(CategoriaProductos))]
        public IHttpActionResult DeleteCategoriaProductos(int id)
        {
            CategoriaProductos categoriaProductos = db.CategoriaProductos.Find(id);
            if (categoriaProductos == null)
            {
                return NotFound();
            }

            db.CategoriaProductos.Remove(categoriaProductos);
            db.SaveChanges();

            return Ok(categoriaProductos);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool CategoriaProductosExists(int id)
        {
            return db.CategoriaProductos.Count(e => e.Id == id) > 0;
        }
    }
}