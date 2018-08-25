using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TechnicalFounders.MobileAppService.Models;
using TechnicalFounders.Models;

namespace TechnicalFounders.Controllers
{
    [Route("api/[controller]")]
    public class ItemController : Controller
    {
        private readonly ItemContext _context;
        private readonly IItemRepository ItemRepository;

        public ItemController(IItemRepository itemRepository, ItemContext itemContext)
        {
            ItemRepository = itemRepository;
            _context = itemContext;
        }

        [HttpGet]
        public IActionResult List()
        {
            return Ok(ItemRepository.GetAll());
        }

        [HttpGet("{Id}")]
        public Item GetItem(string id)
        {
            Item item = ItemRepository.Get(id);
            return item;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody]Item item)
        {
            try
            {
                if (item == null || !ModelState.IsValid)
                {
                    return BadRequest("Invalid State");
                }

                ItemRepository.Add(item);

                _context.Add(item);
                await _context.SaveChangesAsync();

            }
            catch (Exception)
            {
                return BadRequest("Error while creating");
            }
            return Ok(item);
        }

        [HttpPut]
        public IActionResult Edit([FromBody] Item item)
        {
            try
            {
                if (item == null || !ModelState.IsValid)
                {
                    return BadRequest("Invalid State");
                }
                ItemRepository.Update(item);
            }
            catch (Exception)
            {
                return BadRequest("Error while creating");
            }
            return NoContent();
        }

        [HttpDelete("{Id}")]
        public void Delete(string id)
        {
            ItemRepository.Remove(id);
        }
    }
}
