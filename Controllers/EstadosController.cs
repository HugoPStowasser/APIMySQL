﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using APIMySQL.Data;
using APIMySQL.Models;

namespace APIMySQL.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EstadosController : ControllerBase
    {
        private readonly APIDbContext _context;

        public EstadosController(APIDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Estado>>> GetEstado()
        {
          if (_context.Estado == null)
          {
              return NotFound();
          }
            return await _context.Estado.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Estado>> GetEstado(string id)
        {
          if (_context.Estado == null)
          {
              return NotFound();
          }
            var estado = await _context.Estado.FindAsync(id);

            if (estado == null)
            {
                return NotFound();
            }

            return estado;
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutEstado(string id, Estado estado)
        {
            if (id != estado.Sigla)
            {
                return BadRequest();
            }

            _context.Entry(estado).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EstadoExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        [HttpPost]
        public async Task<ActionResult<Estado>> PostEstado(Estado estado)
        {
          if (_context.Estado == null)
          {
              return Problem("Entity set 'APIDbContext.Estado'  is null.");
          }
            _context.Estado.Add(estado);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (EstadoExists(estado.Sigla))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetEstado", new { id = estado.Sigla }, estado);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEstado(string id)
        {
            if (_context.Estado == null)
            {
                return NotFound();
            }
            var estado = await _context.Estado.FindAsync(id);
            if (estado == null)
            {
                return NotFound();
            }

            _context.Estado.Remove(estado);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool EstadoExists(string id)
        {
            return (_context.Estado?.Any(e => e.Sigla == id)).GetValueOrDefault();
        }
    }
}
