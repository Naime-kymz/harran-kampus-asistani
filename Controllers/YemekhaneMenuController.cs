using Microsoft.AspNetCore.Mvc;

namespace HarranKampusAsistani.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class YemekhaneMenuController : ControllerBase
{
    [HttpGet]
    public IActionResult Get()
    {
        var menu = new[]
        {
            new
            {
                tarih = "2026-03-05",
                corba = "Mercimek Çorbası",
                anaYemek = "Tavuk Sote",
                yardimciYemek = "Pirinç Pilavı",
                tatli = "Sütlaç"
            },
            new
            {
                tarih = "2026-03-06",
                corba = "Ezogelin Çorbası",
                anaYemek = "Kuru Fasulye",
                yardimciYemek = "Bulgur Pilavı",
                tatli = "Revani"
            }
        };

        return Ok(menu);
    }
}