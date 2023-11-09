using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QHSE.Server.Models;
using QHSE.Server.Repositorio.Contrato;
using QHSE.Shared;

using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using QHSE.Server.Repositorio.Implementacion;
using QHSE.Server.Utilidades;

namespace QHSE.Server.Controllers
{
    [Route("api/[controller]")]
    
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IUsuarioRepositorio _usuarioRepositorio;
        private readonly ITrabajadorRepositorio _trabajadorRepositorio;

        private readonly string secretkey;
        string _rutaUrl;

        private readonly IConfiguration _configuration;
        private readonly IEMailSenderRepositorio _emailSender;

        public UsuarioController(IUsuarioRepositorio usuarioRepositorio, IMapper mapper, IConfiguration config,
                    IEMailSenderRepositorio emailSender, IConfiguration configuration, ITrabajadorRepositorio trabajadorRepositorio)
        {
            _mapper = mapper;
            _usuarioRepositorio = usuarioRepositorio;
            _emailSender = emailSender;
            _configuration = configuration;

            _rutaUrl = _configuration.GetValue<string>("Configuracion:rutaURL");
            secretkey = config.GetSection("settings").GetSection("secretkey").ToString();
            _trabajadorRepositorio = trabajadorRepositorio;
        }

        [HttpGet]
        [Route("Lista")]
        public async Task<IActionResult> Lista()
        {
            ResponseDTO<List<UsuarioDTO>> _response = new ResponseDTO<List<UsuarioDTO>>();

            try
            {
                List<UsuarioDTO> _listaUsuarios = new List<UsuarioDTO>();

                IQueryable<Usuario> query = await _usuarioRepositorio.Consultar();
                query = query.Include(c => c.IdCreateNavigation)
                        .Where(c => c.IdCreateNavigation.Activo == 1)
                        .Include(p => p.IdTrabaNavigation)
                        .Include(t => t.IdTpoUsuaNavigation);

                _listaUsuarios = _mapper.Map<List<UsuarioDTO>>(query.ToList());

                if (_listaUsuarios.Count > 0)
                    _response = new ResponseDTO<List<UsuarioDTO>>() { status = true, msg = "ok", value = _listaUsuarios };
                else
                    _response = new ResponseDTO<List<UsuarioDTO>>() { status = false, msg = "sin resultados", value = null };

                return StatusCode(StatusCodes.Status200OK, _response);
            }
            catch (Exception ex)
            {

                _response = new ResponseDTO<List<UsuarioDTO>>() { status = false, msg = ex.Message, value = null };
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
                request.FechaCrea = DateTime.Now;

                Creacion _usuario = _mapper.Map<Creacion>(request);

                IQueryable<Usuario> query = await _usuarioRepositorio.Consultar(x => x.NomUsua == _usuario.Usuarios.First().NomUsua && x.IdCreateNavigation.Activo==1);

                if (query.Count() > 0)
                {
                    _ResponseDTO = new ResponseDTO<CreacionDTO>() { status = false, msg = "No se pudo crear el usuario" };
                }
                else
                {
                    Creacion _usuarioCreado = await _usuarioRepositorio.Crear(_usuario);


                    if (_usuarioCreado.IdCreate != 0)
                    {
                        _ResponseDTO = new ResponseDTO<CreacionDTO>() { status = true, msg = "ok", value = _mapper.Map<CreacionDTO>(_usuarioCreado) };
                    }
                    else
                    {
                        _ResponseDTO = new ResponseDTO<CreacionDTO>() { status = false, msg = "No se pudo crear el usuario" };
                    }

                    await envioCorreo(_usuarioCreado.Usuarios.First().IdUsua);
                    
                }
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
            int _isUsuario = request.Usuarios.First().IdUsua;
            try
            {
                Creacion _Usuario = _mapper.Map<Creacion>(request);
                Creacion _UsuarioEditar = await _usuarioRepositorio.Obtener(u => u.IdCreate == _Usuario.IdCreate);

                if (_UsuarioEditar.IdCreate != null)
                {
                    
                    _UsuarioEditar.FechaModi = DateTime.Now;
                    _UsuarioEditar.UsuaModi = _Usuario.UsuaModi;
                    _UsuarioEditar.PcModi = _Usuario.PcModi;
                    _UsuarioEditar.Usuarios = _Usuario.Usuarios;

                    bool respuesta = await _usuarioRepositorio.Editar(_UsuarioEditar);

                    if (respuesta)
                        _ResponseDTO = new ResponseDTO<bool>() { status = true, msg = "ok", value = true };
                    else
                        _ResponseDTO = new ResponseDTO<bool>() { status = false, msg = "No se pudo crear el usuario" };
                }
                else
                {
                    _ResponseDTO = new ResponseDTO<bool>() { status = false, msg = "No se encontró el usuario" };
                }

                await envioCorreo(_isUsuario);
                return StatusCode(StatusCodes.Status200OK, _ResponseDTO);
                
            }
            catch (Exception ex)
            {

                _ResponseDTO = new ResponseDTO<bool>() { status = false, msg = ex.Message };
                return StatusCode(StatusCodes.Status500InternalServerError, _ResponseDTO);
            }


        }


        [HttpPost]
        [Route("Login")]
        public async Task<IActionResult> Login([FromBody] UsuarioDTO request)
        {
            ResponseDTO<List<UsuarioDTO>> _ResponseDTO = new ResponseDTO<List<UsuarioDTO>>();
            try
            {

                List<UsuarioDTO> _listaUsuario = new List<UsuarioDTO>();
                
                IQueryable<Usuario> query = await _usuarioRepositorio.Consultar();
                query = query.Include(c => c.IdCreateNavigation)
                        .Where(c => c.IdCreateNavigation.Activo == 1 && c.NomUsua ==request.NomUsua && c.ClaveUsua==request.ClaveUsua)
                        .Include(p => p.IdTrabaNavigation)
                        .Include(t => t.IdTpoUsuaNavigation);


                int idVendedor = query.First().IdTraba ?? default(int);

                _listaUsuario = _mapper.Map<List<UsuarioDTO>>(query.ToList());
                
                
                var _token = GenerarToken(request.NomUsua);
                if (_listaUsuario.Count > 0)
                    _ResponseDTO = new ResponseDTO<List<UsuarioDTO>>() { status = true, msg = "ok", value = _listaUsuario, token = _token };
                else
                    _ResponseDTO = new ResponseDTO<List<UsuarioDTO>>() { status = false, msg = "No se pudo encontrar el usuario", token = "" };

                return StatusCode(StatusCodes.Status200OK, _ResponseDTO);

                
            }
            catch (Exception ex)
            {
                _ResponseDTO = new ResponseDTO<List<UsuarioDTO>>() { status = false, msg = ex.Message, value = null, token = "" };
                return StatusCode(StatusCodes.Status500InternalServerError, _ResponseDTO);
            }
        }

        [HttpPost]
        [Route("Recuperar")]
        public async Task<IActionResult> Recuperar([FromBody] UsuarioDTO request)
        {
            ResponseDTO<List<UsuarioDTO>> _ResponseDTO = new ResponseDTO<List<UsuarioDTO>>();
            try
            {

                List<UsuarioDTO> _listaUsuario = new List<UsuarioDTO>();

                IQueryable<Usuario> query = await _usuarioRepositorio.Consultar();
                query = query.Include(c => c.IdCreateNavigation)
                        .Where(c => c.IdCreateNavigation.Activo == 1 && c.NomUsua == request.NomUsua)
                        .Include(p => p.IdTrabaNavigation)
                        .Include(t => t.IdTpoUsuaNavigation);

                _listaUsuario = _mapper.Map<List<UsuarioDTO>>(query.ToList());
                
                if (_listaUsuario.Count > 0)
                {
                    _ResponseDTO = new ResponseDTO<List<UsuarioDTO>>() { status = true, msg = "ok", value = _listaUsuario };
                    await envioCorreo(query.First().IdUsua);
                }
                    
                else
                    _ResponseDTO = new ResponseDTO<List<UsuarioDTO>>() { status = false, msg = "No se pudo encontrar el usuario", token = "" };

                return StatusCode(StatusCodes.Status200OK, _ResponseDTO);


            }
            catch (Exception ex)
            {
                _ResponseDTO = new ResponseDTO<List<UsuarioDTO>>() { status = false, msg = ex.Message, value = null, token = "" };
                return StatusCode(StatusCodes.Status500InternalServerError, _ResponseDTO);
            }
        }


        [HttpGet]
        [Route("UsuarioDuplicidad")]
        public async Task<IActionResult> ValidarDuplicaid([FromBody] UsuarioDTO request)
        {
            ResponseDTO<List<UsuarioDTO>> _ResponseDTO = new ResponseDTO<List<UsuarioDTO>>();
            try
            {

                List<UsuarioDTO> _listaUsuario = new List<UsuarioDTO>();

                IQueryable<Usuario> query = await _usuarioRepositorio.Consultar(x=>x.NomUsua==request.NomUsua);
                query = query.Include(c => c.IdCreateNavigation)
                        .Where(c => c.IdCreateNavigation.Activo == 1 && c.NomUsua == request.NomUsua)
                        .Include(p => p.IdTrabaNavigation)
                        .Include(t => t.IdTpoUsuaNavigation);



                _listaUsuario = _mapper.Map<List<UsuarioDTO>>(query.ToList());
                

                if (_listaUsuario.Count > 0)
                    _ResponseDTO = new ResponseDTO<List<UsuarioDTO>>() { status = true, msg = "ok", value = _listaUsuario, token = "" };
                else
                    _ResponseDTO = new ResponseDTO<List<UsuarioDTO>>() { status = false, msg = "No se pudo encontrar el usuario", token = "" };

                return StatusCode(StatusCodes.Status200OK, _ResponseDTO);


            }
            catch (Exception ex)
            {
                _ResponseDTO = new ResponseDTO<List<UsuarioDTO>>() { status = false, msg = ex.Message, value = null, token = "" };
                return StatusCode(StatusCodes.Status500InternalServerError, _ResponseDTO);
            }
        }

        public string GenerarToken(string usuario)
        {
            var keyBytes = Encoding.ASCII.GetBytes(secretkey);
            var claims = new ClaimsIdentity();

            claims.AddClaim(new Claim(ClaimTypes.NameIdentifier, usuario));

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = claims,
                Expires = DateTime.UtcNow.AddHours(5),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(keyBytes), SecurityAlgorithms.HmacSha256Signature)
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenConfig = tokenHandler.CreateToken(tokenDescriptor);

            string tokencreado = tokenHandler.WriteToken(tokenConfig);

            return tokencreado;

            //return Ok();
        }

        [HttpGet]
        [Route("correo")]
        public async Task<IActionResult> envioCorreo(int? IdUsua)
        {
            ResponseDTO<List<UsuarioDTO>> _ResponseDTO = new ResponseDTO<List<UsuarioDTO>>();
            try
            {
                List<UsuarioDTO> _listaUsuario = new List<UsuarioDTO>();

                IQueryable<Usuario> query = await _usuarioRepositorio.Consultar();
                query = query.Include(c => c.IdCreateNavigation)
                        .Where(c => c.IdCreateNavigation.Activo == 1 && c.IdUsua==IdUsua)
                        .Include(p => p.IdTrabaNavigation)
                        .Include(t => t.IdTpoUsuaNavigation);


                _listaUsuario = _mapper.Map<List<UsuarioDTO>>(query.ToList());

                if (_listaUsuario.Count()>0)
                {
                    if(!string.IsNullOrEmpty(_listaUsuario.First().Correo))
                    {
                        string link = _rutaUrl.Replace("api/", "");
                        string mensaje = ""; string asunto = ""; string correoUsuario = _listaUsuario.First().Correo;

                        asunto = "Informacion de Usuario:";
                        mensaje += "Por medio de la presente se envia usuario y contraseña:<br/><hr>";
                        mensaje += "<br/> <b>Datos de Usuario: </b> " + _listaUsuario.First().NombresApellidos +
                            "<br/> <b>Usuario: </b> " + _listaUsuario.First().NomUsua +
                            "<br/> <b>Clave: </b> " + _listaUsuario.First().ClaveUsua + "<br/>" +
                            "<a href=" + link + ">" + link + "</a>";

                        var message = new Message(new string[] { correoUsuario }, asunto, mensaje);
                        _emailSender.SendEmail(message);

                        _ResponseDTO.msg = "Correo enviado correctamente...";
                        _ResponseDTO.status = true;
                        return StatusCode(StatusCodes.Status200OK, _ResponseDTO);
                    }
                    else
                    {
                        _ResponseDTO.msg = "Usuario no tiene correo...";
                        _ResponseDTO.status = false;
                        return StatusCode(StatusCodes.Status200OK, _ResponseDTO);
                    }
                }
                else
                {
                    _ResponseDTO.msg = "No existen usuarios registrados...";
                    _ResponseDTO.status = false;
                    return StatusCode(StatusCodes.Status200OK, _ResponseDTO);
                }
                

            }
            catch (Exception ex)
            {
                _ResponseDTO = new ResponseDTO<List<UsuarioDTO>>() { status = false, msg = ex.Message, value = null, token = "" };
                return StatusCode(StatusCodes.Status500InternalServerError, _ResponseDTO);
            }

            
        }


    }

}
