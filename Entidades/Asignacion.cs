namespace EF_FP_G19.Entidades
{
    public class G19_Asignacion
    {
        public int G19_CodigoProducto { get; set; }
        public int G19_Cantidad { get; set; }
        public double G19_PrecioUnitario { get; set; }

        public G19_Asignacion(int codigoProducto, int cantidad, double precioUnitario)
        {
            G19_CodigoProducto = codigoProducto;
            G19_Cantidad = cantidad;
            G19_PrecioUnitario = precioUnitario;
        }

        public double G19_Importe()
        {
            return G19_Cantidad * G19_PrecioUnitario;
        }

        public override string ToString()
        {
            return $"{G19_CodigoProducto}|{G19_Cantidad}|{G19_PrecioUnitario}";
        }
    }
}