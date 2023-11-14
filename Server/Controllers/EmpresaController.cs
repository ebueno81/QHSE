using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using QHSE.Server.Models;
using QHSE.Server.Repositorio.Contrato;
using QHSE.Shared;

namespace QHSE.Server.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    [ApiController]
    public class EmpresaController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IEmpresaRepositorio _empresaRepositorio;

        public EmpresaController(IEmpresaRepositorio empresaRepositorio, IMapper mapper)
        {
            _mapper = mapper;
            _empresaRepositorio = empresaRepositorio;
        }

        [HttpGet]
        [Route("Lista")]
        public async Task<IActionResult> Lista()
        {
            ResponseDTO<List<EmpresaDTO>> _response = new ResponseDTO<List<EmpresaDTO>>();

            try
            {
                List<EmpresaDTO> _listaEmpresa = new List<EmpresaDTO>();
                IQueryable<Empresa> query = await _empresaRepositorio.Consultar();


                _listaEmpresa = _mapper.Map<List<EmpresaDTO>>(query.ToList());

                if (_listaEmpresa.Count > 0)
                    _response = new ResponseDTO<List<EmpresaDTO>>() { status = true, msg = "ok", value = _listaEmpresa };
                else
                    _response = new ResponseDTO<List<EmpresaDTO>>() { status = false, msg = "sin resultados", value = null };

                return StatusCode(StatusCodes.Status200OK, _response);
            }
            catch (Exception ex)
            {

                _response = new ResponseDTO<List<EmpresaDTO>>() { status = false, msg = ex.Message, value = null };
                return StatusCode(StatusCodes.Status500InternalServerError, _response);
            }

        }

        [HttpPost]
        [Route("Guardar")]
        public async Task<IActionResult> Guardar([FromBody] EmpresaDTO request)
        {

            ResponseDTO<EmpresaDTO> _ResponseDTO = new ResponseDTO<EmpresaDTO>();

            try
            {
                Empresa _empresa = _mapper.Map<Empresa>(request);

                Empresa _empresaCreada = await _empresaRepositorio.Crear(_empresa);


                if (_empresaCreada.IdEmp != 0)
                    _ResponseDTO = new ResponseDTO<EmpresaDTO>() { status = true, msg = "ok", value = _mapper.Map<EmpresaDTO>(_empresaCreada) };
                else
                    _ResponseDTO = new ResponseDTO<EmpresaDTO>() { status = false, msg = "No se pudo crear la empresa" };

                return StatusCode(StatusCodes.Status200OK, _ResponseDTO);

            }
            catch (Exception ex)
            {

                _ResponseDTO = new ResponseDTO<EmpresaDTO>() { status = false, msg = ex.Message };
                return StatusCode(StatusCodes.Status500InternalServerError, _ResponseDTO);
            }


        }

        [HttpPut]
        [Route("Editar")]
        public async Task<IActionResult> Editar([FromBody] EmpresaDTO request)
        {

            ResponseDTO<bool> _ResponseDTO = new ResponseDTO<bool>();

            try
            {
                Empresa _empresa = _mapper.Map<Empresa>(request);
                Empresa _empresaEditar = await _empresaRepositorio.Obtener(u => u.IdEmp == _empresa.IdEmp);

                if (_empresaEditar.IdEmp != null)
                {
                    _empresaEditar.RazEmp = _empresa.RazEmp;
                    _empresaEditar.RucEmp = _empresa.RucEmp;
                    _empresaEditar.NroTelefono = _empresa.NroTelefono;
                    _empresaEditar.DirecEmp = _empresa.DirecEmp;
                    _empresaEditar.DistEmp = _empresa.DistEmp;
                    _empresaEditar.ProvEmp = _empresa.ProvEmp;
                    _empresaEditar.DptEmp = _empresa.DptEmp;
                    
                    _empresaEditar.CorreoEmp = _empresa.CorreoEmp;

                    bool respuesta = await _empresaRepositorio.Editar(_empresaEditar);

                    if (respuesta)
                        _ResponseDTO = new ResponseDTO<bool>() { status = true, msg = "ok", value = true };
                    else
                        _ResponseDTO = new ResponseDTO<bool>() { status = false, msg = "No se pudo crear la empresa" };
                }
                else
                {
                    _ResponseDTO = new ResponseDTO<bool>() { status = false, msg = "No se encontró la empresa" };
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
