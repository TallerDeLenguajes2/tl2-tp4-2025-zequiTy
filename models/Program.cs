using System;
using System.Linq;

internal class Program
{
    private static void Main(string[] args)
    {
        AccesosADatos datosAcceso;
        string tipoAcceso = "JSON"; // Cambiar a "CSV" si querés usar AccesosADatosCSV

        datosAcceso = tipoAcceso == "JSON"
            ? new AccesosADatosJSON()
            : new AccesosADatosCSV();

        Cadeteria miCadeteria = datosAcceso.CargarDesdeArchivos("Cadeteria.json", "Cadetes.json");

        if (miCadeteria == null)
        {
            Console.WriteLine("Error al cargar los datos");
            return;
        }

        string opcion;
        do
        {
            Console.WriteLine();
            Console.WriteLine("Sistema de gestión de cadetería");
            Console.WriteLine($" Bienvenido a {miCadeteria.Nombre}");
            Console.WriteLine("===========================================");
            Console.WriteLine("1. Dar de alta un pedido");
            Console.WriteLine("2. Asignar pedido a un cadete");
            Console.WriteLine("3. Cambiar estado de un pedido");
            Console.WriteLine("4. Reasignar pedido a otro cadete");
            Console.WriteLine("0. Salir");
            Console.WriteLine("===========================================");
            Console.Write("Seleccione una opción: ");
            opcion = Console.ReadLine();

            switch (opcion)
            {
                case "1":
                    Console.WriteLine("\n--- Alta de Nuevo Pedido ---");
                    int nro = ReadInt("Ingrese Nro de Pedido: ");
                    Console.Write("Observaciones: ");
                    string obs = Console.ReadLine();

                    Console.WriteLine("\nDatos del Cliente:");
                    Console.Write("Nombre: ");
                    string nombreCliente = Console.ReadLine();
                    Console.Write("Dirección: ");
                    string dirCliente = Console.ReadLine();
                    Console.Write("Teléfono: ");
                    string telCliente = Console.ReadLine();
                    Console.Write("Datos de Referencia: ");
                    string refCliente = Console.ReadLine();

                    string resultadoAlta = miCadeteria.DarDeAltaPedido(
                        nro, obs,
                        nombreCliente, dirCliente, telCliente, refCliente,
                        null // sin cadete asignado al inicio
                    );
                    Console.WriteLine(resultadoAlta);
                    break;

                case "2":
                    Console.WriteLine("\n--- Asignar Pedido ---");
                    var pendientes = miCadeteria.ListadoPedidos
                        .Where(p => p.Estadopedido == Estado.Pendiente).ToList();

                    if (!pendientes.Any())
                    {
                        Console.WriteLine("No hay pedidos pendientes.");
                        break;
                    }

                    foreach (var p in pendientes)
                        Console.WriteLine($" - N° {p.Nro} | Cliente: {p.NombreCliente}");

                    int nroPedido = ReadInt("Seleccione el N° de Pedido a asignar: ");

                    Console.WriteLine("\nCadetes Disponibles:");
                    foreach (var c in miCadeteria.ListadoCadetes)
                        Console.WriteLine($" - ID: {c.Id} | Nombre: {c.Nombre}");

                    int idCadete = ReadInt("Seleccione el ID del Cadete: ");
                    string resultadoAsignacion = miCadeteria.AsignarCadeteAPedido(nroPedido, idCadete);
                    Console.WriteLine(resultadoAsignacion);
                    break;

                case "3":
                    Console.WriteLine("\n--- Cambiar Estado de Pedido ---");
                    foreach (var p in miCadeteria.ListadoPedidos)
                        Console.WriteLine($" - N° {p.Nro} | Cliente: {p.NombreCliente} | Estado Actual: {p.Estadopedido}");

                    int nroMod = ReadInt("\nSeleccione el N° de Pedido a modificar: ");
                    Console.WriteLine("Seleccione el nuevo estado:");
                    Console.WriteLine("1. Asignado");
                    Console.WriteLine("2. Entregado");
                    Console.WriteLine("3. Cancelado");
                    Console.Write("Opción: ");
                    string opcionEstado = Console.ReadLine();

                    Estado nuevoEstado = opcionEstado switch
                    {
                        "1" => Estado.Asignado,
                        "2" => Estado.Entregado,
                        "3" => Estado.Cancelado,
                        _ => Estado.Pendiente
                    };

                    string resultadoCambio = miCadeteria.CambiarEstadoPedido(nroMod, nuevoEstado);
                    Console.WriteLine(resultadoCambio);
                    break;

                case "4":
                    Console.WriteLine("\n--- Reasignar Pedido ---");
                    var asignados = miCadeteria.ListadoPedidos
                        .Where(p => p.Estadopedido == Estado.Asignado).ToList();

                    if (!asignados.Any())
                    {
                        Console.WriteLine("No hay pedidos asignados para reasignar.");
                        break;
                    }

                    foreach (var p in asignados)
                        Console.WriteLine($" - N° {p.Nro} | Cliente: {p.NombreCliente}");

                    int nroReasignar = ReadInt("\nSeleccione el N° de Pedido a reasignar: ");

                    Console.WriteLine("\nCadetes Disponibles:");
                    foreach (var c in miCadeteria.ListadoCadetes)
                        Console.WriteLine($" - ID: {c.Id} | Nombre: {c.Nombre}");

                    int idNuevoCadete = ReadInt("Seleccione el ID del NUEVO Cadete: ");
                    string resultadoReasignacion = miCadeteria.ReasignarPedido(nroReasignar, idNuevoCadete);
                    Console.WriteLine(resultadoReasignacion);
                    break;

                case "0":
                    Console.WriteLine("Saliendo del sistema...");
                    var informe = miCadeteria.GenerarInforme();

                    Console.WriteLine("\n=== INFORME CADETERIA ===");
                    foreach (var cadete in informe.ResumenPorCadete)
                    {
                        Console.WriteLine($"Cadete: {cadete.Nombre}");
                        Console.WriteLine($"  Pedidos entregados: {cadete.PedidosEntregados}");
                        Console.WriteLine($"  Monto ganado: ${cadete.Jornal}");
                    }
                    Console.WriteLine($"TOTAL de pedidos entregados: {informe.TotalEntregados}");
                    Console.WriteLine($"TOTAL ganado: ${informe.TotalGanado}");
                    Console.WriteLine($"Promedio de envíos por cadete: {informe.PromedioEnviosPorCadete:F2}");
                    break;

                default:
                    Console.WriteLine("Opción no válida. Intente de nuevo.");
                    break;
            }

        } while (opcion != "0");
    }

    private static int ReadInt(string prompt)
    {
        int value;
        do
        {
            Console.Write(prompt);
            string input = Console.ReadLine();
            if (int.TryParse(input, out value)) return value;
            Console.WriteLine("Entrada inválida. Ingrese un número entero.");
        } while (true);
    }
}