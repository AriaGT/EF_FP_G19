using EF_FP_G19.Entidades;

namespace EF_FP_G19.Clases
{
    internal class G19_Clientes
    {
        public static List<G19_Cliente> listaClientes = new List<G19_Cliente>();
        private static readonly string G19_RutaDatosTxt = Path.Combine(AppContext.BaseDirectory, "clientes.txt");
        public static void G19_añadirCliente(G19_Cliente cliente)
        {
            listaClientes.Add(cliente);
            try
            {
                G19_GuardarEnTxt();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al guardar clientes: {ex.Message}");
            }
        }

        public static void G19_GuardarEnTxt()
        {
            using var sw = new StreamWriter(G19_RutaDatosTxt, false);
            sw.WriteLine("CLIENTES");
            foreach (var c in listaClientes)
            {
                string asignaciones = c.G19_ProductosAsignados.Count == 0
                    ? string.Empty
                    : string.Join(";", c.G19_ProductosAsignados.Select(a => $"{a.G19_CodigoProducto},{a.G19_Cantidad},{a.G19_PrecioUnitario}"));
                sw.WriteLine($"{c.G19_NombreCliente}|{c.G19_ApellidosCliente}|{c.G19_DniCliente}|{c.G19_CelularCliente}|{c.G19_TotalGastadoDineroCliente}|{asignaciones}");
            }
        }
        public static void G19_CargarDesdeTxt()
        {
            if (!File.Exists(G19_RutaDatosTxt))
                return;

            listaClientes.Clear();

            using var sr = new StreamReader(G19_RutaDatosTxt);
            string? linea;
            bool enClientes = false;

            while ((linea = sr.ReadLine()) != null)
            {
                if (string.IsNullOrWhiteSpace(linea))
                    continue;

                if (linea.Equals("CLIENTES", StringComparison.OrdinalIgnoreCase))
                {
                    enClientes = true;
                    continue;
                }

                if (enClientes)
                {
                    var parts = linea.Split('|');
                    if (parts.Length >= 6)
                    {
                        string nombre = parts[0];
                        string apellidos = parts[1];
                        int dni = int.Parse(parts[2]);
                        int celular = int.Parse(parts[3]);
                        string asignacionesStr = parts[5];

                        var cliente = new G19_Cliente(nombre, apellidos, dni, celular);

                        if (!string.IsNullOrWhiteSpace(asignacionesStr))
                        {
                            var asigns = asignacionesStr.Split(';', StringSplitOptions.RemoveEmptyEntries);
                            foreach (var a in asigns)
                            {
                                var campos = a.Split(',', StringSplitOptions.RemoveEmptyEntries);
                                if (campos.Length == 3 &&
                                    int.TryParse(campos[0], out int codProd) &&
                                    int.TryParse(campos[1], out int cant) &&
                                    double.TryParse(campos[2], out double precioUnit))
                                {
                                    cliente.G19_AñadirAsignacion(new G19_Asignacion(codProd, cant, precioUnit));
                                }
                            }
                        }

                        cliente.G19_RecalcularTotalGastado();
                        listaClientes.Add(cliente);
                    }
                }
            }
        }
    }
}
