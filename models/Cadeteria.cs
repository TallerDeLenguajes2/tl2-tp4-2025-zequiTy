using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;

public class Cadeteria
{
    private string nombre;
    private string telefono;
    private List<Cadete> listadoCadetes;
    private List<Pedidos> listadoPedidos;

    public Cadeteria()
    {
        listadoCadetes = new List<Cadete>();
        listadoPedidos = new List<Pedidos>();
    }

    public Cadeteria(string nombre, string telefono)
    {
        this.nombre = nombre;
        this.telefono = telefono;
        listadoCadetes = new List<Cadete>();
        listadoPedidos = new List<Pedidos>();
    }

    public string Nombre { get => nombre; set => nombre = value; }
    public string Telefono { get => telefono; set => telefono = value; }
    public List<Cadete> ListadoCadetes { get => listadoCadetes; set => listadoCadetes = value; }
    public List<Pedidos> ListadoPedidos { get => listadoPedidos; set => listadoPedidos = value; }


    /*
    Se realizo el cambio de AsignarPedidoACadete hacia AsignarCadeteAPedido
    La cual se encarga de recibir el numero del pedido y el id del cadete
    Encuentra a ambos en sus respectivas listas y asigna dicho cadete encontrado a el pedido encontrado
    */
    public string AsignarCadeteAPedido(int nroPedido, int idCadete)
    {
        var pedido = listadoPedidos.FirstOrDefault(p => p.Nro == nroPedido);
        var cadete = listadoCadetes.FirstOrDefault(c => c.Id == idCadete);

        if (pedido != null && cadete != null)
        {
            //var cadeteAnt = listadoPedidos.FirstOrDefault(l => l.Cadete == cadete);
            var cadeteAnt = pedido.IdCadete;
            pedido.IdCadete = cadete.Id;
            pedido.CambiarEstado(Estado.Asignado);
            return $"Pedido N°{pedido.Nro} asignado a cadete {cadete.Nombre}.";
        }
        else
        {
            return "No se encontro el cadete o pedido";
        }
    }


  public string DarDeAltaPedido(int nro, string obs, string nombreCliente, string direccionCliente, string telefonoCliente, string referenciaDireccion, int? idCadete)
{
    if (listadoPedidos.Any(p => p.Nro == nro))
    {
        return $"Ya existe un pedido con número {nro}. No se agregó.";
    }

    var cliente = new Cliente(nombreCliente, direccionCliente, telefonoCliente, referenciaDireccion);
    Cadete cadete = null;

    if (idCadete.HasValue)
        cadete = listadoCadetes.FirstOrDefault(c => c.Id == idCadete.Value);

    var pedido = new Pedidos(nro, obs, nombreCliente, direccionCliente, telefonoCliente, referenciaDireccion, idCadete);
    listadoPedidos.Add(pedido);

    return $"Pedido N°{nro} creado exitosamente.";
}

    public string ReasignarPedido(int nroPedido, int idNuevoCadete)
    {
        Cadete cadeteNuevo = listadoCadetes.FirstOrDefault(l => l.Id == idNuevoCadete);
        Pedidos pedido = listadoPedidos.FirstOrDefault(l => l.Nro == nroPedido);
        if (pedido != null && cadeteNuevo != null)
        {
            var cadeteAnt = pedido.IdCadete;
            if (cadeteAnt != null && pedido.Estadopedido != Estado.Pendiente && pedido.Estadopedido != Estado.Entregado)
            {
                pedido.IdCadete = cadeteNuevo.Id;
                pedido.Estadopedido = Estado.Asignado;
                return $"Pedido N°{pedido.Nro} reasignado exitosamente.";

            }
            else
            {
                return "El pedido no tiene un cadete asignado actualmente.";
            }
            //cadeteNuevo.AsignarPedido(pedido);
        }
        else
        {
            return "No se encontró pedido o nuevo cadete.";

        }
    }


    public string CambiarEstadoPedido(int nroPedido, Estado nuevoEstado)
    {
        var pedido = listadoPedidos.FirstOrDefault(p => p.Nro == nroPedido);
        if (pedido != null)
        {
            pedido.CambiarEstado(nuevoEstado);
            return $"Pedido N°{pedido.Nro} cambiado a {nuevoEstado}.";
        }

        return "No se encontró el pedido solicitado.";
    }


public int PedidosEntregadosPorCadete(int idCadete)
{
    return listadoPedidos.Count(p => p.Estadopedido == Estado.Entregado && p.IdCadete != null && p.IdCadete == idCadete);
}

    public double JornalACobrarPorCadete(int idCadete)
    {
        int entregados = PedidosEntregadosPorCadete(idCadete);
        return entregados * 500;
    }
 public InformeCadeteria GenerarInforme()
{
    var informe = new InformeCadeteria();

    foreach (var cadete in listadoCadetes)
    {
        int entregados = PedidosEntregadosPorCadete(cadete.Id);
        double jornal = JornalACobrarPorCadete(cadete.Id);

        informe.ResumenPorCadete.Add(new ResumenCadete
        {
            Nombre = cadete.Nombre,
            PedidosEntregados = entregados,
            Jornal = jornal
        });

        informe.TotalEntregados += entregados;
        informe.TotalGanado += jornal;
    }

    if (listadoCadetes.Count > 0)
        informe.PromedioEnviosPorCadete = (double)informe.TotalEntregados / listadoCadetes.Count;

    return informe;
}

}
