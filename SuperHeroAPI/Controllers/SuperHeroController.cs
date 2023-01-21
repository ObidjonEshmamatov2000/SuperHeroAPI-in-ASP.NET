using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace SuperHeroAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SuperHeroController : ControllerBase
    {
        private readonly DataContext _context;

        public SuperHeroController(DataContext dataContext)
        {
            this._context = dataContext;
        }

        [HttpGet]
        public async Task<ActionResult<List<SuperHero>>> Get()
        {
            return Ok(await _context.SuperHeroes.ToListAsync());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<List<SuperHero>>> Get(int id)
        {
            var hero = await this._context.SuperHeroes.FindAsync(id);
            if (hero == null) return BadRequest($"hero with id={id} not found");
            return Ok(hero);
        }

        [HttpPost]
        public async Task<ActionResult<List<SuperHero>>> Post(SuperHero superHero)
        {
            _context.SuperHeroes.Add(superHero);
            await _context.SaveChangesAsync();
            return Created("created", await _context.SuperHeroes.ToListAsync());
        }

        [HttpPut]
        public async Task<ActionResult<List<SuperHero>>> Edit(SuperHero request)
        {
            var hero = await this._context.SuperHeroes.FindAsync(request.ID);
            if (hero == null) return BadRequest($"hero with id={request.ID} not found");

            hero.Name = request.Name;
            hero.FirstName = request.FirstName;
            hero.LastName = request.LastName;
            hero.Place = request.Place;

            await _context.SaveChangesAsync();
            return Ok(await _context.SuperHeroes.ToListAsync());
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<List<SuperHero>>> Delete(int id)
        {
            var hero = await this._context.SuperHeroes.FindAsync(id);
            if (hero == null) return BadRequest($"hero with id={id} not found");

            _context.SuperHeroes.Remove(hero);
            await _context.SaveChangesAsync();
            return Ok(_context.SuperHeroes.ToListAsync());
        }
    }
}
