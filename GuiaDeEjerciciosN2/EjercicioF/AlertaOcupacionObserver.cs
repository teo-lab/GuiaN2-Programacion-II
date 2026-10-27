namespace EjercicioF
{
    // Suscriptor: avisa solo cuando quedan 2 lugares o menos.
    public class AlertaOcupacionObserver : IPlayaObserver
    {
        private const int Limite = 2;

        public void Actualizar(EventoPlaya evento)
        {
            int libres = evento.LugaresLibres;
            if (libres > Limite)
                return;

            if (libres == 0)
                Console.WriteLine("[Alerta] ¡Playa llena!");
            else if (libres == 1)
                Console.WriteLine("[Alerta] ¡Queda 1 lugar!");
            else
                Console.WriteLine($"[Alerta] ¡Quedan {libres} lugares!");
        }
    }
}
