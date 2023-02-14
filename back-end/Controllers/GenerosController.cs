using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using back_end.Repositorios;
using back_end.Entidades;
using back_end.DTOs;
using back_end.Filtros;
using back_end.Utilidades;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.JwtBearer;

using Microsoft.EntityFrameworkCore;

using AutoMapper;

namespace back_end.Controllers
{
    [Route("api/generos")]
    [ApiController]
    //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class GenerosController : ControllerBase
    {
        //private readonly IRepositorio repositorio;

        private readonly ILogger<GenerosController> logger;
        private readonly ApplicationDBContext context;
        private readonly IMapper mapper;

        public GenerosController(
            //IRepositorio repositorio,
            ILogger<GenerosController> logger,
            ApplicationDBContext context,
            IMapper mapper)
        {
            //this.repositorio = repositorio;
            this.logger = logger;
            this.context = context;
            this.mapper = mapper;
        }

        [HttpGet]               // api/generos
        public async Task<ActionResult<List<GeneroDTO>>> Get([FromQuery] PaginacionDTO paginacionDTO)
        {
            //return new List<Genero>() { new Genero { Id = 1, Nombre = "Comedia" } };
            //return repositorio.ObtenerTodosLosGeneros();
            //var generos =  await context.Generos.ToListAsync();
            //var resultado = new List<GeneroDTO> ();
            //foreach(var genero in generos)
            //{
            //    resultado.Add(new GeneroDTO() { Id = genero.Id, Nombre = genero.Nombre });
            //}
            //return resultado;


            var queryable = context.Generos.AsQueryable();
            await HttpContext.InsertarParametrosPaginacionEnCabecera(queryable);
            var generos = queryable.OrderBy(x => x.Nombre).Paginar(paginacionDTO).ToListAsync();
            //var generos =  await context.Generos.ToListAsync();
            return mapper.Map<List<GeneroDTO>>(generos);

        }

        //[HttpGet("guid")]       //  api/generos/guid
        //public ActionResult<Guid> GetGUID()
        //{
        //    return Ok(new { GUID_GenerosController = repositorio.ObtenerGuid()
        //        //GUID_WeatherForecastController = weatherForecastController.ObtenerGuidWeatherForecastController()
        //    });
        //}

        //[HttpGet("{Id}")]       // api/generos/1
        //public Genero Get(int Id)
        //{
        //    var genero = repositorio.ObtenerPorId(Id);

        //    if (genero == null)
        //    {
        //        //return NotFound();
        //    }

        //    return genero;

        //}

        [HttpGet("{Id:int}")]     
        public async Task<ActionResult<Genero>> Get(int Id)
        {
            //if (!ModelState.IsValid)
            //{
            //    return BadRequest(ModelState);
            //}

            //logger.LogDebug($"Obteniendo un género por el ID {Id} ");

            //var genero = await repositorio.ObtenerPorId(Id);

            //if (genero == null)
            //{
            //    throw new ApplicationException($"El genero de ID {Id} no fue encontrado");
            //    logger.LogWarning($"No pudimos encontrar el género de ID {Id}");
            //    return NotFound();
            //}

            //return genero;
            throw new NotImplementedException();
        }

        [HttpPost]
        public async Task<ActionResult> Post([FromBody] GeneroCreacionDTO generoCreacionDTO)
        {
            //repositorio.CrearGenero(genero);
            //return NoContent();
            //throw new NotImplementedException();
            var genero = mapper.Map<Genero>(generoCreacionDTO);
            context.Add(genero);
            await context.SaveChangesAsync();
            return NoContent();
        }

        [HttpPut]
        public ActionResult Put([FromBody] Genero genero)
        {
            throw new NotImplementedException();
        }

       [HttpDelete]
        public ActionResult Delete()
        {
            throw new NotImplementedException();
        }
    }
}
