using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DotNet_API.Context;
using DotNet_API.Entities;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DotNet_API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ContatoController : ControllerBase
    {

        private readonly AgendaContext _context; 
        public ContatoController(AgendaContext context){
            _context = context;
        }

        [HttpPost]
        public IActionResult CreateContato(Contato contato){
            _context.Add(contato);
            _context.SaveChanges();

            return CreatedAtAction(nameof(ObterPorId), new {id = contato.Id}, contato);
        }

        [HttpGet("{id}")]
        public IActionResult ObterPorId(int id){

            var contato = _context.Contatos.Find(id);

            if (contato == null){
                return NotFound();
            }

            return Ok(contato);
        }

        [HttpGet("ObterPorNome/{nome}")]
        public IActionResult ObterPorNome(string nome){

            var contato = _context.Contatos.Where(x => x.Nome.Contains(nome));

            if (contato == null){
                return NotFound();
            }

            return Ok(contato);
        }

        [HttpPut("{id}")]
        public IActionResult AtualizaContato(int id, Contato contato){

            var contato_db = _context.Contatos.Find(id);

            if (contato == null){
                return NotFound();
            }

            contato_db.Nome = contato.Nome;
            contato_db.Telefone = contato.Telefone;
            contato_db.Ativo = contato.Ativo;

            _context.Contatos.Update(contato_db);
            _context.SaveChanges();

            return Ok(contato_db);
        }

        [HttpDelete("{id}")]
        public IActionResult DeletaContato(int id){

            var contato = _context.Contatos.Find(id);

            if (contato == null){
                return NotFound();
            }

            _context.Contatos.Remove(contato);

            return NoContent();
        }
        
    }
}