using EFCore.Domain;
using EFCore.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace EFCore.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BatalhaController : ControllerBase
    {
        private readonly HeroiContext _context;

        public BatalhaController(HeroiContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        // GET: api/<BatalhaController>
        [HttpGet]
        public ActionResult Get()
        {
            try
            {
                var batalhas = _context.Batalhas;
                return Ok(batalhas);
            }
            catch (Exception ex)
            {
                return BadRequest($"Erro: {ex}");
            }
        }

        // GET api/<BatalhaController>/5
        [HttpGet("{id}")]
        public ActionResult Get([FromRoute]int id)
        {
            try
            {
                var batalha = _context.Batalhas
                                        .AsNoTracking()
                                        .Where(b => b.Id == id);
                return Ok(batalha);
            }
            catch (Exception ex)
            {
                return BadRequest($"Erro: {ex}");
            }
        }

        // POST api/<BatalhaController>
        [HttpPost]
        public ActionResult Post([FromBody] Batalha input)
        {
            try
            {
                _context.Add(input);
                _context.SaveChanges();

                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest($"Erro: {ex}");
            }
            
        }

        // PUT api/<BatalhaController>/5
        [HttpPut("{id}")]
        public ActionResult Put(int id, [FromBody] Batalha input)
        {
            try
            {
                if (_context.Batalhas
                    .AsNoTracking()
                    .FirstOrDefault(h => h.Id == id) != null)
                {
                    _context.Update(input);
                    _context.SaveChanges();

                    return Ok();
                }
                else
                {
                    return NoContent();
                }
                
            }
            catch (Exception ex)
            {
                return BadRequest($"Erro: {ex}");
            }
        }

        // DELETE api/<BatalhaController>/5
        [HttpDelete("{id}")]
        public ActionResult Delete([FromRoute]int id)
        {
            try
            {
                if (_context.Batalhas
                    .AsNoTracking()
                    .FirstOrDefault(
                        h => h.Id == id
                    ) != null)
                {
                    var batalha = _context.Batalhas
                                        .AsNoTracking()
                                        .Where(b => b.Id == id)
                                        .Single();
                    _context.Batalhas.Remove(batalha);
                    _context.SaveChanges();

                    return Ok();
                }
                else
                {
                    return NoContent();
                }

            }
            catch (Exception ex)
            {
                return BadRequest($"Erro: {ex}");
            }
        }
    }
}
