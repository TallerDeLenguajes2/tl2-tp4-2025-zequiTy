using System;

public class Cadete
{
    private int id;
    private string nombre;
    private string direccion;
    private string telefono;

    public Cadete() { }

    public Cadete(int id, string nombre, string direccion, string telefono)
    {
        this.id = id;
        this.nombre = nombre;
        this.direccion = direccion;
        this.telefono = telefono;
    }

    public int Id { get => id; set => id = value; }
    public string Nombre { get => nombre; set => nombre = value; }
    public string Direccion { get => direccion; set => direccion = value; }
    public string Telefono { get => telefono; set => telefono = value; }

    public string VerDatos()
    {
        return $"ID: {id}, Nombre: {nombre}, Dirección: {direccion}, Teléfono: {telefono}";
    }
}