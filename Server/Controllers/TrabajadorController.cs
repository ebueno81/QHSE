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
    public class TrabajadorController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly ITrabajadorRepositorio _trabajadorRepositorio;

        public TrabajadorController(ITrabajadorRepositorio articuloRepositorio, IMapper mapper)
        {
            _mapper = mapper;
            _trabajadorRepositorio = articuloRepositorio;
        }

        [HttpGet]
        [Route("Lista")]
        public async Task<IActionResult> Lista()
        {
            ResponseDTO<List<TrabajadorDTO>> _response = new ResponseDTO<List<TrabajadorDTO>>();

            try
            {
                List<TrabajadorDTO> _listaTrabajadors = new List<TrabajadorDTO>();

                IQueryable<Trabajador> query = await _trabajadorRepositorio.Consultar();
                query = query.Include(c => c.IdCreateNavigation)
                        .Where(c => c.IdCreateNavigation.Activo == 1);

                _listaTrabajadors = _mapper.Map<List<TrabajadorDTO>>(query.ToList());

                if (_listaTrabajadors.Count > 0)
                    _response = new ResponseDTO<List<TrabajadorDTO>>() { status = true, msg = "ok", value = _listaTrabajadors };
                else
                    _response = new ResponseDTO<List<TrabajadorDTO>>() { status = false, msg = "sin resultados", value = null };

                return StatusCode(StatusCodes.Status200OK, _response);
            }
            catch (Exception ex)
            {

                _response = new ResponseDTO<List<TrabajadorDTO>>() { status = false, msg = ex.Message, value = null };
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
                Creacion _Trabajador = _mapper.Map<Creacion>(request);

                Creacion _TrabajadorCreado = await _trabajadorRepositorio.Crear(_Trabajador);


                if (_TrabajadorCreado.IdCreate != 0)
                    _ResponseDTO = new ResponseDTO<CreacionDTO>() { status = true, msg = "ok", value = _mapper.Map<CreacionDTO>(_TrabajadorCreado) };
                else
                    _ResponseDTO = new ResponseDTO<CreacionDTO>() { status = false, msg = "No se pudo crear el vendedor" };

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
                Creacion _Trabajador = _mapper.Map<Creacion>(request);
                Creacion _TrabajadorEditar = await _trabajadorRepositorio.Obtener(u => u.IdCreate == _Trabajador.IdCreate);

                if (_TrabajadorEditar.IdCreate != null)
                {

                    _TrabajadorEditar.FechaModi = DateTime.Now;
                    _TrabajadorEditar.UsuaModi = _Trabajador.UsuaModi;
                    _TrabajadorEditar.PcModi = _Trabajador.PcModi;
                    _TrabajadorEditar.Trabajadors = _Trabajador.Trabajadors;

                    bool respuesta = await _trabajadorRepositorio.Editar(_TrabajadorEditar);

                    if (respuesta)
                        _ResponseDTO = new ResponseDTO<bool>() { status = true, msg = "ok", value = true };
                    else
                        _ResponseDTO = new ResponseDTO<bool>() { status = false, msg = "No se pudo crear el vendedor" };
                }
                else
                {
                    _ResponseDTO = new ResponseDTO<bool>() { status = false, msg = "No se encontró el vendedor" };
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
