
﻿using AutoMapper;
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
    public class CategoriaController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly ICategoriaRepositorio _categoriaRepositorio;

        public CategoriaController(ICategoriaRepositorio articuloRepositorio, IMapper mapper)
        {
            _mapper = mapper;
            _categoriaRepositorio = articuloRepositorio;
        }

        [HttpGet]
        [Route("Lista")]
        public async Task<IActionResult> Lista()
        {
            ResponseDTO<List<CategoriaDTO>> _response = new ResponseDTO<List<CategoriaDTO>>();

            try
            {
                List<CategoriaDTO> _listaCategorias = new List<CategoriaDTO>();

                IQueryable<Categorium> query = await _categoriaRepositorio.Consultar();
                query = query.Include(c => c.IdCreateNavigation)
                        .Where(c => c.IdCreateNavigation.Activo == 1);

                _listaCategorias = _mapper.Map<List<CategoriaDTO>>(query.ToList());

                //if (_listaCategorias.Count > 0)
                //    _response = new ResponseDTO<List<CategoriaDTO>>() { status = true, msg = "ok", value = _listaCategorias };
                //else
                    _response = new ResponseDTO<List<CategoriaDTO>>() { status = true, msg = "ok", value = _listaCategorias };

                return StatusCode(StatusCodes.Status200OK, _response);
            }
            catch (Exception ex)
            {

                _response = new ResponseDTO<List<CategoriaDTO>>() { status = false, msg = ex.Message, value = null };
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
                Creacion Categorium = _mapper.Map<Creacion>(request);

                Creacion CategoriumCreado = await _categoriaRepositorio.Crear(Categorium);


                if (CategoriumCreado.IdCreate != 0)
                    _ResponseDTO = new ResponseDTO<CreacionDTO>() { status = true, msg = "ok", value = _mapper.Map<CreacionDTO>(CategoriumCreado) };
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
                Creacion Categorium = _mapper.Map<Creacion>(request);
                Creacion CategoriumEditar = await _categoriaRepositorio.Obtener(u => u.IdCreate == Categorium.IdCreate);

                if (CategoriumEditar.IdCreate != null)
                {

                    CategoriumEditar.FechaModi = DateTime.Now;
                    CategoriumEditar.UsuaModi = Categorium.UsuaModi;
                    CategoriumEditar.PcModi = Categorium.PcModi;
                    CategoriumEditar.Categoria = Categorium.Categoria;

                    bool respuesta = await _categoriaRepositorio.Editar(CategoriumEditar);

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
