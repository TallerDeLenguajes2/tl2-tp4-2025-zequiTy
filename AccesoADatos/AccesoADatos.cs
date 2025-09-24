using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;

public interface AccesosADatos
{
    Cadeteria Cadeteria { get; set; }
    bool CargarCadetes(string rutaArchivo);
    bool CargarCadeteria(string rutaArchivo);
    Cadeteria CargarDesdeArchivos(string rutaInfo, string rutaCadetes);
}

public class AccesosADatosCSV : AccesosADatos
{
    public Cadeteria Cadeteria { get; set; }

    public AccesosADatosCSV()
    {
        this.Cadeteria = new Cadeteria();
    }

    public bool CargarCadetes(string rutaArchivo)
    {
        if (!File.Exists(rutaArchivo)) return false;

        try
        {
            string[] lineas = File.ReadAllLines(rutaArchivo);

            foreach (var linea in lineas)
            {
                var datos = linea.Split(',');
                if (datos.Length == 4 && int.TryParse(datos[0], out int idCadete))
                {
                    string nombre = datos[1];
                    string direccion = datos[2];
                    string telefono = datos[3];

                    var cadete = new Cadete(idCadete, nombre, direccion, telefono);
                    Cadeteria.ListadoCadetes.Add(cadete);
                }
            }

            return true;
        }
        catch
        {
            return false;
        }
    }

    public bool CargarCadeteria(string rutaArchivo)
    {
        if (!File.Exists(rutaArchivo)) return false;

        try
        {
            string[] lineas = File.ReadAllLines(rutaArchivo);
            if (lineas.Length >= 2)
            {
                var datos = lineas[1].Split(',');
                if (datos.Length >= 2)
                {
                    Cadeteria.Nombre = datos[0];
                    Cadeteria.Telefono = datos[1];
                    return true;
                }
            }

            return false;
        }
        catch
        {
            return false;
        }
    }

    public Cadeteria CargarDesdeArchivos(string rutaInfo, string rutaCadetes)
    {
        bool okInfo = CargarCadeteria(rutaInfo);
        bool okCadetes = CargarCadetes(rutaCadetes);

        return (okInfo && okCadetes) ? Cadeteria : null;
    }
}

public class AccesosADatosJSON : AccesosADatos
{
    public Cadeteria Cadeteria { get; set; }

    public AccesosADatosJSON()
    {
        this.Cadeteria = new Cadeteria();
    }

    public bool CargarCadetes(string rutaArchivo)
    {
        if (!File.Exists(rutaArchivo)) return false;

        try
        {
            string json = File.ReadAllText(rutaArchivo);
            var lista = JsonSerializer.Deserialize<List<Cadete>>(json);

            if (lista != null)
            {
                Cadeteria.ListadoCadetes = lista;
                return true;
            }

            return false;
        }
        catch
        {
            return false;
        }
    }

    public bool CargarCadeteria(string rutaArchivo)
    {
        if (!File.Exists(rutaArchivo)) return false;

        try
        {
            string json = File.ReadAllText(rutaArchivo);
            var datos = JsonSerializer.Deserialize<DatosCadeteria>(json);

            if (datos != null)
            {
                Cadeteria.Nombre = datos.Nombre;
                Cadeteria.Telefono = datos.Telefono;
                return true;
            }

            return false;
        }
        catch
        {
            return false;
        }
    }

    public Cadeteria CargarDesdeArchivos(string rutaInfo, string rutaCadetes)
    {
        bool okInfo = CargarCadeteria(rutaInfo);
        bool okCadetes = CargarCadetes(rutaCadetes);

        return (okInfo && okCadetes) ? Cadeteria : null;//operador ternario
    }

}

public class DatosCadeteria
{
    public string Nombre { get; set; }
    public string Telefono { get; set; }
}
