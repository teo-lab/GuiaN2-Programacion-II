namespace EjercicioF
{
    // STRATEGY: el contrato que cumplen todas las formas de cobrar.
    public interface ITarifa
    {
        string Nombre { get; }
        decimal Calcular(int minutos);
    }
}
