public class Pedidos
{
    private int nro;
    private string obs;
    private Estado estadopedido;
    private string nombreCliente;
    private string direccionCliente;
    private string telefonoCliente;
    private string referenciaDireccion;
    private int? idCadete;

    public Pedidos() { }

    public Pedidos(int nro, string obs, string nombreCliente, string direccionCliente, string telefonoCliente, string referenciaDireccion, int? idCadete)
    {
        this.Nro = nro;
        this.Obs = obs;
        this.Estadopedido = Estado.Pendiente;
        this.NombreCliente = nombreCliente;
        this.DireccionCliente = direccionCliente;
        this.TelefonoCliente = telefonoCliente;
        this.ReferenciaDireccion = referenciaDireccion;
        this.IdCadete = idCadete;
    }

    public int Nro { get => nro; set => nro = value; }
    public string Obs { get => obs; set => obs = value; }
    public Estado Estadopedido { get => estadopedido; set => estadopedido = value; }
    public string NombreCliente { get => nombreCliente; set => nombreCliente = value; }
    public string DireccionCliente { get => direccionCliente; set => direccionCliente = value; }
    public string TelefonoCliente { get => telefonoCliente; set => telefonoCliente = value; }
    public string ReferenciaDireccion { get => referenciaDireccion; set => referenciaDireccion = value; }
    public int? IdCadete { get => idCadete; set => idCadete = value; }

    public string VerDireccionCliente()
    {
        return $"{direccionCliente}, {referenciaDireccion}";
    }

    public string VerDatosCliente()
    {
        return $"{nombreCliente}, {telefonoCliente}";
    }

    public void CambiarEstado(Estado nuevoEstado)
    {
        estadopedido = nuevoEstado;
    }



}

public enum Estado
{
    Pendiente = 0,
    Asignado = 1,
    Entregado = 2,
    Cancelado = 3
}
