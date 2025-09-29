using System.Text.Json;

public class AccesoADatosCadetes
{
    public Cadeteria Cadeteria { get; set; }

    public AccesoADatosCadetes()
    {
        this.Cadeteria = new Cadeteria();
    }


    public List<Cadete> Obtener(string rutaArchivo)
    {
        if (!File.Exists(rutaArchivo)) return null;

        try
        {
            string json = File.ReadAllText(rutaArchivo);
            var lista = JsonSerializer.Deserialize<List<Cadete>>(json);

            if (lista != null)
            {
                Cadeteria.ListadoCadetes = lista;
                return lista;
            }

            return null;
        }
        catch
        {
            return null;
        }
    }

    public void Guardar(List<Pedidos> Pedidos, string rutaArchivo)
    {
        var opcion = new JsonSerializerOptions { WriteIndented = true };
        Cadeteria.ListadoPedidos = Pedidos;
        string JsonText = JsonSerializer.Serialize(Cadeteria, opcion);
        File.WriteAllText(rutaArchivo, JsonText);
     }

}