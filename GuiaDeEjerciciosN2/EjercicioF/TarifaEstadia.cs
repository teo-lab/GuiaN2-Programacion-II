namespace EjercicioF
{
    // $ 6.000 fijos hasta 12 horas; después, $ 1.200 por cada hora (o fracción) extra.
    public class TarifaEstadia : ITarifa
    {
        private const decimal PrecioFijo = 6000m;
        private const decimal PrecioHoraExtra = 1200m;
        private const int MinutosIncluidos = 12 * 60;

        public string Nombre => "ESTADIA";

        public decimal Calcular(int minutos)
        {
            if (minutos <= MinutosIncluidos)
                return PrecioFijo;

            int horasExtra = (int)Math.Ceiling((minutos - MinutosIncluidos) / 60.0);
            return PrecioFijo + horasExtra * PrecioHoraExtra;
        }
    }
}
