namespace EjercicioF
{
    public enum TipoEventoPlaya
    {
        Ingreso,
        Egreso
    }

    // El CONTEXTO que viaja en cada aviso (Observer estilo PUSH: el aviso trae los datos).
    public class EventoPlaya
    {
        public TipoEventoPlaya Tipo { get; }
        public string Patente { get; }
        public decimal Importe { get; }        // 0 en los ingresos
        public int LugaresLibres { get; }

        public EventoPlaya(TipoEventoPlaya tipo, string patente, decimal importe, int lugaresLibres)
        {
            Tipo = tipo;
            Patente = patente;
            Importe = importe;
            LugaresLibres = lugaresLibres;
        }

        public string Descripcion()
        {
            return Tipo == TipoEventoPlaya.Ingreso
                ? $"Ingresó {Patente}"
                : $"Egresó {Patente} - $ {Importe:N0}";
        }
    }
}
