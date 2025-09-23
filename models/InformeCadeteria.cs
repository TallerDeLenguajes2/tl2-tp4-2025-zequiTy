public class InformeCadeteria
{
    public List<ResumenCadete> ResumenPorCadete { get; set; } = new List<ResumenCadete>();
    public int TotalEntregados { get; set; }
    public double TotalGanado { get; set; }
    public double PromedioEnviosPorCadete { get; set; }
}

public class ResumenCadete
{
    public string Nombre { get; set; }
    public int PedidosEntregados { get; set; }
    public double Jornal { get; set; }
}