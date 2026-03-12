using Microsoft.AspNetCore.Mvc;

namespace HarranKampusAsistani.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class RingSaatleriController : ControllerBase
{
    [HttpGet]
    public IActionResult Get()
    {
        var data = new[]
        {
            new
            {
                hat = "Ring 1",
                saat = "08:00",
                guzergah = "Ana Kapı → Öğrenci İşleri → Merkezi Kafeterya → Eğitim Fakültesi → Fen-Edebiyat Fakültesi → İİBF → Mühendislik Fakültesi → Ziraat Fakültesi → Ana Kapı"
            },
            new
            {
                hat = "Ring 2",
                saat = "09:00",
                guzergah = "Ana Kapı → Tıp Fakültesi → Harran Üniversitesi Hastanesi → Merkezi Kütüphane → Merkezi Derslik → Kampüs Camii → Ana Kapı"
            },
            new
            {
                hat = "Ring 3",
                saat = "10:00",
                guzergah = "Ana Kapı → Yüzme Havuzu → Merkezi Yemekhane → Öğrenci İşleri → İİBF → Mühendislik Fakültesi → Ana Kapı"
            },
            new
            {
                hat = "Ring 4",
                saat = "12:00",
                guzergah = "Ana Kapı → Eğitim Fakültesi → Fen-Edebiyat Fakültesi → Merkezi Kütüphane → Ziraat Fakültesi → Ana Kapı"
            }
        };

        return Ok(data);
    }
}