namespace EjercicioF
{
    // OBSERVER: el contrato de los suscriptores.
    public interface IPlayaObserver
    {
        void Actualizar(EventoPlaya evento);
    }
}
