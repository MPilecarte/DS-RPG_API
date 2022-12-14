using Microsoft.AspNetCore.Mvc;
using RpgApi.Data;
using System.Threading.Tasks;
using RpgApi.Models;
using System;
using Microsoft.EntityFrameworkCore;

namespace RpgApi.Controllers
{

    [ApiController]
    [Route("[Controller]")]
    public class PersonagensController : ControllerBase
    {
        private readonly DataContext _context;
        public PersonagensController(DataContext context)
        {
            _context = context;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetSingle(int id)
        {
            try
            {
                Personagem p = await _context.Personagens
                    .FirstOrDefaultAsync(pBusca => pBusca.Id == id);

                return Ok(p);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet ("GetAll")]
        public async Task<IActionResult> Get()
        {
            try
            {
                List<Personagem> lista = await _context.Personagens.ToListAsync();
                return Ok(lista);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
