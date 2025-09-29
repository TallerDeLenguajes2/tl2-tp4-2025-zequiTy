using System.Text.Json;

public class AccesoADatosPedidos
{
    public Cadeteria Cadeteria { get; set; }

    public AccesoADatosPedidos()
    {
        this.Cadeteria = new Cadeteria();
    }


    public List<Pedidos> Obtener(string rutaArchivo)
    {
        if (!File.Exists(rutaArchivo)) return null;

        try
        {
            string json = File.ReadAllText(rutaArchivo);
            var lista = JsonSerializer.Deserialize<List<Pedidos>>(json);

            if (lista != null)
            {
                Cadeteria.ListadoPedidos = lista;
                return lista;
            }else
            {
                return null;
            }
     
        }
        catch
        {
            return null;
        }
    }
}