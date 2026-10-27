namespace EjercicioF
{
    // Suscriptor: la caja acumula lo cobrado. Solo le interesan los egresos.
    public class CajaObserver : IPlayaObserver
    {
        private decimal _totalRecaudado = 0;

        public void Actualizar(EventoPlaya evento)
        {
            if (evento.Tipo != TipoEventoPlaya.Egreso)
                return;

            _totalRecaudado += evento.Importe;
            Console.WriteLine(
                $"[Caja] Cobrado $ {evento.Importe:N0} a {evento.Patente}. Recaudado: $ {_totalRecaudado:N0}");
        }
    }
}
