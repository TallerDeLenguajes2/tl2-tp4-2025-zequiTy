using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace cadeteria.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CadeteriaController : ControllerBase
    {
        private AccesosADatosJSON datosAcceso;
        static Cadeteria cadeteria;

        public CadeteriaController()
        {
            datosAcceso = new AccesosADatosJSON();
            cadeteria = datosAcceso.CargarDesdeArchivos("data/Cadeteria.json", "data/Cadetes.json");

        }
        [HttpGet("pedidos")]
        public ActionResult<List<Pedidos>> GetPedidos()
        {

            if (cadeteria == null)

                return NotFound();


            return Ok(cadeteria.ListadoPedidos);
        }

        [HttpGet("cadetes")]
        public ActionResult<List<Cadete>> GetCadetes()
        {


            if (cadeteria == null)
                return NotFound();

            return Ok(cadeteria.ListadoCadetes);
        }

        [HttpGet("informe")]
        public ActionResult<InformeCadeteria> GetInforme()
        {


            if (cadeteria == null)
                return NotFound();

            var informe = cadeteria.GenerarInforme();

            return Ok(informe);
        }

        [HttpPost("pedido")]

        public ActionResult<string> AgregarPedido(Pedidos pedido)
        {


            if (cadeteria == null) return NotFound("No se pudo");

            var resultado = cadeteria.DarDeAltaPedido(
                    pedido.Nro,
                   pedido.Obs,
                    pedido.NombreCliente,
                    pedido.DireccionCliente,
                   pedido.TelefonoCliente,
                    pedido.ReferenciaDireccion,
                    pedido.IdCadete
              );

            return Ok(resultado);
        }
        [HttpPut("asignar")]
        public ActionResult<string> AsignarPedido(int idPedido, int idCadete)
        {

            if (cadeteria == null)
                return NotFound("No se pudo cargar la cadetería.");

            var resultado = cadeteria.AsignarCadeteAPedido(idPedido, idCadete);


            return Ok(resultado);
        }


        [HttpPut("cambiarEstado/{idPedido}/{nuevoEstado}")]
        
        public ActionResult<string> CambiarEstadoPedido(int idPedido, Estado nuevoEstado)
        {
            var cadeteria = datosAcceso.CargarDesdeArchivos("data/Cadeteria.json", "data/Cadetes.json");

            if (cadeteria == null)
                return NotFound("No se pudo cargar la cadetería.");

            var resultado = cadeteria.CambiarEstadoPedido(idPedido, nuevoEstado);

            return Ok(resultado);
        }
[HttpPut("reasignar/{idPedido}/{idNuevoCadete}")]
public ActionResult<string> ReasignarPedido(int idPedido, int idNuevoCadete)
{
    var cadeteria = datosAcceso.CargarDesdeArchivos("data/Cadeteria.json", "data/Cadetes.json");

    if (cadeteria == null)
        return NotFound("No se pudo cargar la cadetería.");

    var resultado = cadeteria.ReasignarPedido(idPedido, idNuevoCadete);

    

    return Ok(resultado);
}
    }
}


/*
ActionResult<List<Pedidos>>

*/