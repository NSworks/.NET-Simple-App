using System;
using System.Collections.Generic;
using KolProbne.DTOs.Request;
using KolProbne.DTOs.Response;
using KolProbne.Services;
using Microsoft.AspNetCore.Mvc;

namespace KolProbne.Controllers
{
    [ApiController]
        public class CukierniaController : ControllerBase
        {
            private ICukierniaDbService _context;
            public CukierniaController(ICukierniaDbService context)
            {
                _context = context;
            }
            [HttpGet]
            [Route("api/orders/{nazwisko}")]

            public IActionResult GetOrders(string nazwisko)
            {
                List<ZamowResponse> listaZ = _context.GetOrders(nazwisko);
                if (listaZ[0].Zam.Uwagi.Equals("Baza"))
                {
                    return BadRequest("Wystąpił błąd podczas połączenia z bazą");
                }
                if (listaZ[0].Zam.Uwagi.Equals("Error"))
                {
                    return BadRequest("Nie ma takiego klienta");
                }
                return Ok(listaZ);
            }
            [HttpGet]
            [Route("api/orders")]
            public IActionResult GetOrders()
            {
                List<ZamowResponse> listaZ = _context.GetOrders();
                if (listaZ[0].Zam.Uwagi.Equals("Baza"))
                {
                    return BadRequest("Wystąpił błąd podczas połączenia z bazą");
                }
                return Ok(listaZ);
            }
            [HttpGet("{id}")]
            [Route("api/clients/{id}/orders")]
            public IActionResult NewOrder(int id, ZamowRequest zam)
            {
                var zamowienie = _context.NewOrder(id, zam);
                if (zamowienie.Zam.Uwagi.Equals("brak"))
                {
                    return BadRequest("Nie ma takich wyrobow");
                }
                if (zamowienie.Zam.Uwagi.Equals("error"))
                {
                    return BadRequest("Błąz podczas połączenia z bazą");
                }
                if (zamowienie.Zam.Uwagi.Equals("klient brak"))
                {
                    return BadRequest("Nie ma takiego klienta");
                }
                return Ok("Zamowienie przyjęte");
            }

        }
    }