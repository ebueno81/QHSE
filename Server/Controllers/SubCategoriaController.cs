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
    public class SubCategoriaController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly ISubCategoriaRepositorio _subCategoriaRepositorio;

        public SubCategoriaController(ISubCategoriaRepositorio subCategoriaRepositorio, IMapper mapper)
        {
            _mapper = mapper;
            _subCategoriaRepositorio = subCategoriaRepositorio;
        }

        [HttpGet]
        [Route("Lista")]
        public async Task<IActionResult> Lista()
        {
            ResponseDTO<List<SubCategoriaDTO>> _response = new ResponseDTO<List<SubCategoriaDTO>>();

            try
            {
                List<SubCategoriaDTO> _listaSubCategorias = new List<SubCategoriaDTO>();

                IQueryable<SubCategorium> query = await _subCategoriaRepositorio.Consultar();
                query = query.Include(c => c.IdCreateNavigation)
                        .Where(c => c.IdCreateNavigation.Activo == 1)
                        .Include(ca => ca.IdCtgNavigation);

                _listaSubCategorias = _mapper.Map<List<SubCategoriaDTO>>(query.ToList());

                //if (_listaSubCategorias.Count > 0)
                    _response = new ResponseDTO<List<SubCategoriaDTO>>() { status = true, msg = "ok", value = _listaSubCategorias };
                //else
                //    _response = new ResponseDTO<List<SubCategoriaDTO>>() { status = false, msg = "sin resultados", value = null };

                return StatusCode(StatusCodes.Status200OK, _response);
            }
            catch (Exception ex)
            {

                _response = new ResponseDTO<List<SubCategoriaDTO>>() { status = false, msg = ex.Message, value = null };
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
                Creacion SubCategorium = _mapper.Map<Creacion>(request);

                Creacion SubCategoriumCreado = await _subCategoriaRepositorio.Crear(SubCategorium);


                if (SubCategoriumCreado.IdCreate != 0)
                    _ResponseDTO = new ResponseDTO<CreacionDTO>() { status = true, msg = "ok", value = _mapper.Map<CreacionDTO>(SubCategoriumCreado) };
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
                Creacion SubCategorium = _mapper.Map<Creacion>(request);
                Creacion SubCategoriumEditar = await _subCategoriaRepositorio.Obtener(u => u.IdCreate == SubCategorium.IdCreate);

                if (SubCategoriumEditar.IdCreate != null)
                {

                    SubCategoriumEditar.FechaModi = DateTime.Now;
                    SubCategoriumEditar.UsuaModi = SubCategorium.UsuaModi;
                    SubCategoriumEditar.PcModi = SubCategorium.PcModi;
                    SubCategoriumEditar.SubCategoria = SubCategorium.SubCategoria;

                    bool respuesta = await _subCategoriaRepositorio.Editar(SubCategoriumEditar);

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
