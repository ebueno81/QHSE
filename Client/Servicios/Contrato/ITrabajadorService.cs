<<<<<<< HEAD
﻿namespace QHSE.Client.Servicios.Contrato
=======
﻿using QHSE.Shared;
using System.Threading.Tasks;

namespace QHSE.Client.Servicios.Contrato
>>>>>>> 2e36e4e72a9ceccf3d9d1b2d4fac05d43955b108
{
    public interface ITrabajadorService
    {
        Task<ResponseDTO<List<TrabajadorDTO>>> Lista();
<<<<<<< HEAD
        Task<ResponseDTO<CreacionDTO>> Crear(CreacionDTO entidad);
        Task<bool> Editar(CreacionDTO entidad);
        Task<bool> Eliminar(int id);
=======
>>>>>>> 2e36e4e72a9ceccf3d9d1b2d4fac05d43955b108
    }
}
