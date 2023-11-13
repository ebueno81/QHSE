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
    public class CreacionController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly ICreacionRepositorio _creacionRepositorio;
        

        public CreacionController(ICreacionRepositorio personaRepositorio, IMapper mapper)
        {
            _mapper = mapper;
            _creacionRepositorio = personaRepositorio;
        }


        [HttpPut]
        [Route("Editar")]
        public async Task<IActionResult> Editar([FromBody] CreacionDTO request)
        {

            ResponseDTO<bool> _ResponseDTO = new ResponseDTO<bool>();

            try
            {
                Creacion _creacion = _mapper.Map<Creacion>(request);
                Creacion _creacionEditar = await _creacionRepositorio.Obtener(u => u.IdCreate == _creacion.IdCreate);

                if (_creacionEditar.IdCreate != null)
                {
                    _creacionEditar.UsuaModi = _creacion.UsuaModi;
                    _creacionEditar.FechaModi = _creacion.FechaModi;
                    _creacionEditar.PcModi = _creacion.PcModi;

                    bool respuesta = await _creacionRepositorio.Editar(_creacionEditar);

                    if (respuesta)
                        _ResponseDTO = new ResponseDTO<bool>() { status = true, msg = "ok", value = true };
                    else
                        _ResponseDTO = new ResponseDTO<bool>() { status = false, msg = "No se pudo crear el creacion" };
                }
                else
                {
                    _ResponseDTO = new ResponseDTO<bool>() { status = false, msg = "No se encontró el creacion" };
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
        public async Task<IActionResult> Anular([FromBody] CreacionDTO request)
        {

            ResponseDTO<bool> _ResponseDTO = new ResponseDTO<bool>();

            try
            {
                request.FechaAnula = DateTime.Now;
                Creacion _creacion = _mapper.Map<Creacion>(request);
                Creacion _creacionEditar = await _creacionRepositorio.Obtener(u => u.IdCreate == _creacion.IdCreate);

                if (_creacionEditar.IdCreate != null)
                {
                    _creacionEditar.Activo = 0;
                    _creacionEditar.UsuaAnula = _creacion.UsuaAnula;
                    _creacionEditar.FechaModi = DateTime.Now;
                    _creacionEditar.PcAnula = _creacion.PcAnula;
                    
                    bool respuesta = await _creacionRepositorio.Editar(_creacionEditar);

                    

                    if (respuesta)
                        _ResponseDTO = new ResponseDTO<bool>() { status = true, msg = "ok", value = true };
                    else
                        _ResponseDTO = new ResponseDTO<bool>() { status = false, msg = "No se pudo crear el creacion" };
                }
                else
                {
                    _ResponseDTO = new ResponseDTO<bool>() { status = false, msg = "No se encontró el creacion" };
                }

                return StatusCode(StatusCodes.Status200OK, _ResponseDTO);

            }
            catch (Exception ex)
            {

                _ResponseDTO = new ResponseDTO<bool>() { status = false, msg = ex.Message };
                return StatusCode(StatusCodes.Status500InternalServerError, _ResponseDTO);
            }


        }

        [HttpDelete]
        [Route("Eliminar/{id:int}")]
        public async Task<IActionResult> Eliminar(int id)
        {
            ResponseDTO<string> _ResponseDTO = new ResponseDTO<string>();
            try
            {
                Creacion _personaEliminar = await _creacionRepositorio.Obtener(u => u.IdCreate == id);

                if (_personaEliminar != null)
                {

                    bool respuesta = await _creacionRepositorio.Eliminar(_personaEliminar);

                    if (respuesta)
                        _ResponseDTO = new ResponseDTO<string>() { status = true, msg = "ok", value = "" };
                    else
                        _ResponseDTO = new ResponseDTO<string>() { status = false, msg = "No se pudo eliminar el creacion", value = "" };
                }

                return StatusCode(StatusCodes.Status200OK, _ResponseDTO);
            }
            catch (Exception ex)
            {
                _ResponseDTO = new ResponseDTO<string>() { status = false, msg = ex.Message };
                return StatusCode(StatusCodes.Status500InternalServerError, _ResponseDTO);
            }
        }

    }
}
