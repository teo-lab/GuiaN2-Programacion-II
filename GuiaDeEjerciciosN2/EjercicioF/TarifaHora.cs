namespace EjercicioF
{
    // $ 1.200 por hora o fraccion
    public class TarifaHora : ITarifa
    {
        private const decimal PrecioPorHora = 1200m;

        public string Nombre => "HORA";

        public decimal Calcular(int minutos)
        {
            int horas = (int)Math.Ceiling(minutos / 60.0);
            return horas * PrecioPorHora;
        }
    }
}
