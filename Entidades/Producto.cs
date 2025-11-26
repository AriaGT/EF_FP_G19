namespace EF_FP_G19.Entidades
{
    public class G19_Producto
    {
        public int G19_CodigoProducto { get; set; }
        public string G19_NombreProducto { get; set; }
        public string G19_CategoriaProducto { get; set; }
        public double G19_PrecioProducto { get; set; }
        public int G19_StockProducto { get; set; }

        public G19_Producto(int codigo, string nombre, string categoria, double precio, int stock)
        {
            this.G19_CodigoProducto = codigo;
            this.G19_NombreProducto = nombre;
            this.G19_CategoriaProducto = categoria;
            this.G19_PrecioProducto = precio;
            this.G19_StockProducto = stock;
        }

        public override string ToString()
        {
            return $"{G19_CodigoProducto}|{G19_NombreProducto}|{G19_CategoriaProducto}|{G19_PrecioProducto}|{G19_StockProducto}";
        }
    }
}
