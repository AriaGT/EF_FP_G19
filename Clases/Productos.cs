using EF_FP_G19.Entidades;

namespace EF_FP_G19.Clases
{
    internal class G19_Productos
    {
        public static List<G19_Producto> listaProductos = new List<G19_Producto>();
        private static readonly string G19_RutaDatosTxt = Path.Combine(AppContext.BaseDirectory, "productos.txt");
        public static void G19_añadirProducto(G19_Producto producto)
        {
            listaProductos.Add(producto);
            try
            {
                G19_GuardarEnTxt();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al guardar productos: {ex.Message}");
            }
        }
        public static void G19_GuardarEnTxt()
        {
            using var sw = new StreamWriter(G19_RutaDatosTxt, false);
            sw.WriteLine("PRODUCTOS");
            foreach (var p in listaProductos)
            {
                sw.WriteLine($"{p.G19_CodigoProducto}|{p.G19_NombreProducto}|{p.G19_CategoriaProducto}|{p.G19_PrecioProducto}|{p.G19_StockProducto}");
            }
        }

        public static void G19_CargarDesdeTxt()
        {
            if (!File.Exists(G19_RutaDatosTxt))
                return;

            listaProductos.Clear();

            using var sr = new StreamReader(G19_RutaDatosTxt);
            string? linea;
            bool enProductos = false;

            while ((linea = sr.ReadLine()) != null)
            {
                if (string.IsNullOrWhiteSpace(linea))
                    continue;

                if (linea.Equals("PRODUCTOS", StringComparison.OrdinalIgnoreCase))
                {
                    enProductos = true;
                    continue;
                }

                if (enProductos)
                {
                    var parts = linea.Split('|');
                    if (parts.Length >= 5)
                    {
                        int codigo = int.Parse(parts[0]);
                        string nombre = parts[1];
                        string categoria = parts[2];
                        double precio = double.Parse(parts[3]);
                        int stock = int.Parse(parts[4]);

                        listaProductos.Add(new G19_Producto(codigo, nombre, categoria, precio, stock));
                    }
                }
            }
        }
    }
}
