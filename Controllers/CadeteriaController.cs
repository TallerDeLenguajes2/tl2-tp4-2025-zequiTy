using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace cadeteria.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CadeteriaController : ControllerBase
    {
        private AccesosADatosJSON datosAcceso;

        public CadeteriaController()
        {
            datosAcceso = new AccesosADatosJSON();
        }
[HttpGet("pedidos")]
public ActionResult<List<Pedidos>> GetPedidos()
{
    var cadeteria = datosAcceso.CargarDesdeArchivos("data/Cadeteria.json", "data/Cadetes.json");

    if (cadeteria == null)
        return NotFound();

    return Ok(cadeteria.ListadoPedidos);
}

[HttpGet("cadetes")]
public ActionResult<List<Cadete>> GetCadetes()
{
    var cadeteria = datosAcceso.CargarDesdeArchivos("data/Cadeteria.json", "data/Cadetes.json");

    if (cadeteria == null)
        return NotFound();

    return Ok(cadeteria.ListadoCadetes);
}

        [HttpGet("informe")]
        public ActionResult<InformeCadeteria> GetInforme()
        {
            var cadeteria = datosAcceso.CargarDesdeArchivos("data/Cadeteria.json", "data/Cadetes.json");

            if (cadeteria == null)
                return NotFound();

            var informe = cadeteria.GenerarInforme();

            return Ok(informe);
        }
/*
        [HttpPost("pedido")]

public ActionResult<string> AgregarPedido(Pedidos pedido)
{
    var cadeteria = datosAcceso.CargarDesdeArchivos("data/Cadeteria.json", "data/Cadetes.json");

        if (cadeteria == null) return NotFound("No se pudo cargar.");

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
        }*/
    }
}


/*
ActionResult<List<Pedidos>>

*/