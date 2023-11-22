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
    public class PlantillaDetController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IPlantillaDetRepositorio _PlantillaDetDetRepositorio;

        public PlantillaDetController(IPlantillaDetRepositorio plantillaRepositorio, IMapper mapper)
        {
            _mapper = mapper;
            _PlantillaDetDetRepositorio = plantillaRepositorio;
        }

        [HttpGet]
        [Route("Lista")]
        public async Task<IActionResult> Lista()
        {
            ResponseDTO<List<PlantillaDetDTO>> _response = new ResponseDTO<List<PlantillaDetDTO>>();

            try
            {
                List<PlantillaDetDTO> _listaPlantillas = new List<PlantillaDetDTO>();

                IQueryable<PlantillaDet> query = await _PlantillaDetDetRepositorio.Consultar();

                query = query.Include(c => c.IdSubCtgNavigation)
                        .Where(c => c.Activo == 1);

                _listaPlantillas = _mapper.Map<List<PlantillaDetDTO>>(query.ToList());


                _response = new ResponseDTO<List<PlantillaDetDTO>>() { status = true, msg = "ok", value = _listaPlantillas };

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
                Creacion _PlantillaDet = _mapper.Map<Creacion>(request);

                Creacion _PlantillaDetCreado = await _PlantillaDetDetRepositorio.Crear(_PlantillaDet);


                if (_PlantillaDetCreado.IdCreate != 0)
                    _ResponseDTO = new ResponseDTO<CreacionDTO>() { status = true, msg = "ok", value = _mapper.Map<CreacionDTO>(_PlantillaDetCreado) };
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
        public async Task<IActionResult> Editar([FromBody] PlantillaDetDTO request)
        {

            ResponseDTO<bool> _ResponseDTO = new ResponseDTO<bool>();

            try
            {
                PlantillaDet _PlantillaDet = _mapper.Map<PlantillaDet>(request);
                PlantillaDet _PlantillaDetEditar = await _PlantillaDetDetRepositorio.Obtener(u => u.IdPlantillaDet == _PlantillaDet.IdPlantillaDet);

                if (_PlantillaDetEditar.IdPlantillaDet != null)
                {

                    _PlantillaDetEditar.NroOrden = _PlantillaDet.NroOrden;
                    
                    bool respuesta = await _PlantillaDetDetRepositorio.Editar(_PlantillaDetEditar);

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

        [HttpPut]
        [Route("Anular")]
        public async Task<IActionResult> Anular([FromBody] PlantillaDetDTO request)
        {

            ResponseDTO<bool> _ResponseDTO = new ResponseDTO<bool>();

            try
            {
                PlantillaDet _PlantillaDet = _mapper.Map<PlantillaDet>(request);
                PlantillaDet _PlantillaDetEditar = await _PlantillaDetDetRepositorio.Obtener(u => u.IdPlantillaDet == _PlantillaDet.IdPlantillaDet);

                if (_PlantillaDetEditar.IdPlantillaDet != null)
                {

                    _PlantillaDetEditar.Activo = _PlantillaDet.Activo;
                    
                    bool respuesta = await _PlantillaDetDetRepositorio.Editar(_PlantillaDetEditar);

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
