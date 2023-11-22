using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QHSE.Server.Models;
using QHSE.Server.Repositorio.Contrato;
using QHSE.Shared;

namespace QHSE.Server.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    [ApiController]
    public class PlantillaController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IPlantillaRepositorio _plantillaRepositorio;

        public PlantillaController(IPlantillaRepositorio plantillaRepositorio, IMapper mapper)
        {
            _mapper = mapper;
            _plantillaRepositorio = plantillaRepositorio;
        }

        [HttpGet]
        [Route("Lista")]
        public async Task<IActionResult> Lista(int? codigoPlantilla)
        {
            ResponseDTO<List<PlantillaDTO>> _response = new ResponseDTO<List<PlantillaDTO>>();

            try
            {
                List<PlantillaDTO> _listaPlantillas = new List<PlantillaDTO>();


                IQueryable<Plantilla> query = await _plantillaRepositorio.Consultar(codigoPlantilla>0 ? x =>x.IdPlantilla==codigoPlantilla : null);


                query = query.Include(c => c.IdCreateNavigation)
                        .Where(c => c.IdCreateNavigation.Activo == 1)
                        .Include(a => a.IdAreaNavigation);

                _listaPlantillas = _mapper.Map<List<PlantillaDTO>>(query.ToList());

                
                _response = new ResponseDTO<List<PlantillaDTO>>() { status = true, msg = "ok", value = _listaPlantillas };

                return StatusCode(StatusCodes.Status200OK, _response);
            }
            catch (Exception ex)
            {

                _response = new ResponseDTO<List<PlantillaDTO>>() { status = false, msg = ex.Message, value = null };
                return StatusCode(StatusCodes.Status500InternalServerError, _response);
            }

        }

        [Authorize]
        [HttpGet]
        [Route("ListaDetalles")]
        public async Task<IActionResult> ListaDetalles(int codigoPlantilla)
        {
            ResponseDTO<List<PlantillaDetDTO>> _response = new ResponseDTO<List<PlantillaDetDTO>>();

            try
            {
                List<PlantillaDetDTO> _listaPlantilla = new List<PlantillaDetDTO>();

                IQueryable<PlantillaDet> query = await _plantillaRepositorio.ConsultarDetalle(codigoPlantilla);
                query = query.Include(r => r.IdSubCtgNavigation)
                    .Include(x => x.IdPlantillaNavigation)
                    .Include(cr => cr.IdPlantillaNavigation.IdAreaNavigation)
                    .Include(p => p.IdPlantillaNavigation.IdCreateNavigation)
                    .Include(ca => ca.IdSubCtgNavigation.IdCtgNavigation)
                    .Where(p => p.Activo == 1);

                _listaPlantilla = _mapper.Map<List<PlantillaDetDTO>>(query.ToList());

                if (_listaPlantilla.Count > 0)
                    _response = new ResponseDTO<List<PlantillaDetDTO>>() { status = true, msg = "ok", value = _listaPlantilla.ToList() };
                else
                    _response = new ResponseDTO<List<PlantillaDetDTO>>() { status = false, msg = "sin resultados", value = null };

                return StatusCode(StatusCodes.Status200OK, _response);
            }
            catch (Exception ex)
            {

                _response = new ResponseDTO<List<PlantillaDetDTO>>() { status = false, msg = ex.Message, value = null };
                return StatusCode(StatusCodes.Status500InternalServerError, _response);
            }

        }

        [HttpPost]
        [Route("Guardar")]
        public async Task<IActionResult> Guardar([FromBody] CreacionDTO request)
        {

            ResponseDTO<CreacionDTO> _ResponseDTO = new ResponseDTO<CreacionDTO>();

            try
            {
                request.Activo = 1;
                request.FechaCrea = DateTime.Now;

                Creacion _Plantilla = _mapper.Map<Creacion>(request);

                Creacion _PlantillaCreado = await _plantillaRepositorio.Crear(_Plantilla);


                if (_PlantillaCreado.IdCreate != 0)
                    _ResponseDTO = new ResponseDTO<CreacionDTO>() { status = true, msg = "ok", value = _mapper.Map<CreacionDTO>(_PlantillaCreado) };
                else
                    _ResponseDTO = new ResponseDTO<CreacionDTO>() { status = false, msg = "No se pudo crear el registro" };

                return StatusCode(StatusCodes.Status200OK, _ResponseDTO);

            }
            catch (Exception ex)
            {

                _ResponseDTO = new ResponseDTO<CreacionDTO>() { status = false, msg = ex.Message };
                return StatusCode(StatusCodes.Status500InternalServerError, _ResponseDTO);
            }


        }

        [HttpPut]
        [Route("Editar")]
        public async Task<IActionResult> Editar([FromBody] CreacionDTO request)
        {

            ResponseDTO<bool> _ResponseDTO = new ResponseDTO<bool>();

            try
            {
                Creacion _Plantilla = _mapper.Map<Creacion>(request);
                Creacion _PlantillaEditar = await _plantillaRepositorio.Obtener(u => u.IdCreate == _Plantilla.IdCreate);

                if (_PlantillaEditar.IdCreate != null)
                {

                    _PlantillaEditar.FechaModi = DateTime.Now;
                    _PlantillaEditar.UsuaModi = _Plantilla.UsuaModi;
                    _PlantillaEditar.PcModi = _Plantilla.PcModi;
                    _PlantillaEditar.Plantillas = _Plantilla.Plantillas;

                    bool respuesta = await _plantillaRepositorio.Editar(_PlantillaEditar);

                    if (respuesta)
                        _ResponseDTO = new ResponseDTO<bool>() { status = true, msg = "ok", value = true };
                    else
                        _ResponseDTO = new ResponseDTO<bool>() { status = false, msg = "No se pudo crear el registro" };
                }
                else
                {
                    _ResponseDTO = new ResponseDTO<bool>() { status = false, msg = "No se encontró el registro" };
                }

                return StatusCode(StatusCodes.Status200OK, _ResponseDTO);

            }
            catch (Exception ex)
            {

                _ResponseDTO = new ResponseDTO<bool>() { status = false, msg = ex.Message };
                return StatusCode(StatusCodes.Status500InternalServerError, _ResponseDTO);
            }


        }



    }
}
