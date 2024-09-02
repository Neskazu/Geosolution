using Microsoft.AspNetCore.Mvc;
using CityInfo.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.CodeAnalysis.Options;
using Newtonsoft.Json.Linq;
using CityInfo.Services;
namespace CityInfo.Controllers
{
    public class MapController : Controller
    {
        private readonly ApplicationDbContext context;
        private readonly GeoServerService geoServerService;

        public MapController(ApplicationDbContext context,GeoServerService geoServerService)
        {
            this.context = context;
            this.geoServerService = geoServerService;
        }
        public async Task<IActionResult> Index(string searchQuery)
        {
            //map layers from geoserver
            var layers = new List<string>
            {
                "TestWorkSpace:gis_osm_roads_free_1",
                "TestWorkSpace:gis_osm_buildings_a_free_1"
            };
            ViewBag.Layers = layers;
            var cityObjects = context.CityObjects.AsQueryable();

            if (!string.IsNullOrEmpty(searchQuery))
            {
                cityObjects = cityObjects.Where(o =>
                    o.Name.Contains(searchQuery) ||
                    o.Description.Contains(searchQuery) ||
                    o.CityModelTypeMappings.Any(c=>c.CityObjectType.Name.Contains(searchQuery))
                    );
            }

            return View(await cityObjects.ToListAsync());
        }
        [HttpGet]
        public IActionResult Create()
        {
            // Загружаем типы объектов из базы данных
            var cityObjectTypes = context.CityObjectTypes.ToList();

            // Создаем MultiSelectList на основе типов объектов
            ViewBag.CityObjectTypes = new MultiSelectList(cityObjectTypes, "Id", "Name");

            // Проверка содержимого списка
            foreach (var type in cityObjectTypes)
            {
                Console.WriteLine("TEST  " + type.Name);
            }

            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CityObjectModel cityObjectModel, List<int> selectedObjectTypes)
        {
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values.SelectMany(v => v.Errors);
                foreach (var error in errors)
                {
                    Console.WriteLine($"Validation Error: {error.ErrorMessage}");
                }
            }
            if (ModelState.IsValid)
            {
                context.Add(cityObjectModel);
                await context.SaveChangesAsync();

                foreach (var typeId in selectedObjectTypes)
                {
                    var cityModelTypeMapping = new CityModelTypeMapping
                    {
                        CityObjectModelId = cityObjectModel.Id,
                        CityObjectTypeId = typeId
                    };
                    context.Add(cityModelTypeMapping);
                }

                await context.SaveChangesAsync();
                await geoServerService.AddCityObjectAsync(cityObjectModel);
                return RedirectToAction(nameof(Index));
            }
            ViewBag.CityObjectTypes = new MultiSelectList(context.CityObjects, "Id", "Name");
            return View(cityObjectModel);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cityObject = await context.CityObjects.FindAsync(id);
            if (cityObject == null)
            {
                return NotFound();
            }
            return View(cityObject);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Description,Latitude,Longitude,ObjectType")] CityObjectModel cityObjectModel)
        {
            if (id != cityObjectModel.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    context.Update(cityObjectModel);
                    await context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CityObjectExists(cityObjectModel.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(List));
            }
            return View(cityObjectModel);
        }
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cityObject = await context.CityObjects
                .FirstOrDefaultAsync(m => m.Id == id);
            if (cityObject == null)
            {
                return NotFound();
            }

            return View(cityObject);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var cityObject = await context.CityObjects.FindAsync(id);
            context.CityObjects.Remove(cityObject);
            await context.SaveChangesAsync();
            return RedirectToAction(nameof(List));
        }
        public async Task<IActionResult> List(string searchString)
        {
            var cityObjects = context.CityObjects.AsQueryable();

            if (!String.IsNullOrEmpty(searchString))
            {
                cityObjects = cityObjects.Where(s => s.Name.Contains(searchString));
            }

            return View(await cityObjects.ToListAsync());
        }

        private bool CityObjectExists(int id)
        {
            return context.CityObjects.Any(e => e.Id == id);
        }
    }
}
