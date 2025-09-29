using System.Text.Json;

public class AccesoADatosCadeteria
{
    public Cadeteria Cadeteria { get; set; }

    public AccesoADatosCadeteria()
    {
        this.Cadeteria = new Cadeteria();
    }

    
    public Cadeteria Obtener(string rutaArchivo)
    {
        if (!File.Exists(rutaArchivo)) return null;

        try
        {
            string json = File.ReadAllText(rutaArchivo);
            var datos = JsonSerializer.Deserialize<DatosCadeteria>(json);

            if (datos != null)
            {
                Cadeteria.Nombre = datos.Nombre;
                Cadeteria.Telefono = datos.Telefono;
                return Cadeteria;
            }

            return null;
        }
        catch
        {
            return null;
        }
    }

}