namespace EjercicioF
{
    // Suscriptor: la pantalla de la entrada muestra cuántos lugares quedan.
    public class PantallaLugares : IPlayaObserver
    {
        public void Actualizar(EventoPlaya evento)
            => Console.WriteLine($"[Pantalla] Lugares libres: {evento.LugaresLibres}");
    }
}
