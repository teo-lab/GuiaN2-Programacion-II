// =====================================================================
// SECCIÓN F — Playa de estacionamiento "Plaza Colón"
//
// JUSTIFICACIÓN DE LOS PATRONES
//
// SINGLETON (Bitacora):
//
//   Tiene que haber una sola bitácora para todo el sistema. Si cada parte
//   hiciera su propio new Bitacora(), cada una tendria su propia lista y
//   numeracion, y al mostrarla faltarian movimientos o se repetirian
//   numeros. 
//
// STRATEGY (ITarifa + TarifaHora / TarifaEstadia / TarifaNocturna):
//
//   Hay tres formas distintas de calcular el importe segun los minutos. Sin
//   Strategy, Egresar tendria un switch y cada tarifa nueva.
//   Ademas, cada vehiculo guarda su tarifa al entrar, asi que al salir se cobra 
//   con la correcta sin preguntar nada.
//
// FACTORY (TarifaFactory):
//
//   El operador carga la tarifa como texto (HORA, ESTADIA, NOCHE). La
//   factory convierte ese texto en el objeto correcto, siempre desde un
//   solo lugar. Si el codigo no existe, falla con un mensaje claro en vez
//   de seguir como si nada.
//
// OBSERVER (IPlayaObserver + Playa como notificador):
//
//   Cada ingreso o egreso le interesa a cuatro partes: pantalla, caja,
//   alerta y bitacora. Sin Observer, Playa tendria que llamar a cada una a
//   mano y habria que modificarla para sumar una nueva. Asi, Playa solo
//   avisa, y los suscriptores se pueden agregar o quitar mientras corre el
//   programa.
// =====================================================================

namespace EjercicioF
{
    public static class Demo
    {
        public static void Ejecutar()
        {
            var playa = new Playa(10);
            var pantalla = new PantallaLugares();

            playa.Suscribir(pantalla);
            playa.Suscribir(new CajaObserver());
            playa.Suscribir(new AlertaOcupacionObserver());
            playa.Suscribir(new BitacoraObserver());

            Console.WriteLine("Códigos disponibles: " + string.Join(", ", TarifaFactory.CodigosDisponibles()));

            // Ingresan 8 vehículos con distintos códigos de tarifa
            playa.Ingresar("AB123CD", "HORA");
            playa.Ingresar("AC456EF", "ESTADIA");
            playa.Ingresar("AD789GH", "NOCHE");
            playa.Ingresar("AE111AA", "HORA");
            playa.Ingresar("AF222BB", "HORA");
            playa.Ingresar("AG333CC", "ESTADIA");
            playa.Ingresar("AH444DD", "NOCHE");
            playa.Ingresar("AI555EE", "HORA");

            playa.Egresar("AB123CD", 150);   // HORA, 2 h 30 min -> $ 3.600
            playa.Egresar("AC456EF", 900);   // ESTADIA, 15 h    -> $ 9.600

            playa.Desuscribir(pantalla);     // la pantalla deja de mostrar
            playa.Egresar("AD789GH", 600);   // NOCHE            -> $ 4.500

            // Código de tarifa inválido -> lo frena la factory
            try { playa.Ingresar("AZ999ZZ", "SEMANAL"); }
            catch (ArgumentException ex) { Console.WriteLine("Error: " + ex.Message); }

            // Validaciones de Playa
            try { playa.Ingresar("AE111AA", "HORA"); }
            catch (InvalidOperationException ex) { Console.WriteLine("Error: " + ex.Message); }

            try { playa.Ingresar("   ", "HORA"); }
            catch (InvalidOperationException ex) { Console.WriteLine("Error: " + ex.Message); }

            Bitacora.Instancia.MostrarTodo();
        }
    }
}
