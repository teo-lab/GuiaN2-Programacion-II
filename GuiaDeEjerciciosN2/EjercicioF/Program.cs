// =====================================================================
// SECCIÓN F — Playa de estacionamiento "Plaza Colón"
//
// JUSTIFICACIÓN DE LOS PATRONES
//
// SINGLETON (Bitacora):
//   La bitácora tiene que ser UNA para todo el sistema. Si cada parte hiciera
//   su propio new Bitacora(), cada una tendría su lista y su numeración: al
//   mostrarla faltarían movimientos y los números se repetirían (el mismo bug
//   del historial de Faro Envíos). Con el constructor privado ni siquiera se
//   puede cometer ese error: no compila.
//
// STRATEGY (ITarifa + TarifaHora / TarifaEstadia / TarifaNocturna):
//   Hay tres formas de cobrar lo mismo (minutos -> importe). Sin Strategy,
//   Egresar tendría un switch por código y cada tarifa nueva (MOTO, por ej.)
//   obligaría a abrirlo. Además cada vehículo guarda SU tarifa al entrar, así
//   al salir se cobra con la correcta sin preguntar qué tipo era.
//
// FACTORY (TarifaFactory):
//   El operador carga el código como TEXTO (HORA, ESTADIA, NOCHE) en cada
//   ingreso. Alguien tiene que traducir ese texto al objeto correcto, y tiene
//   que ser un solo lugar: si no, el switch de "texto -> new Tarifa..." se
//   repetiría donde haga falta y un código inválido pasaría sin aviso. Acá un
//   código desconocido falla con un mensaje claro.
//
// OBSERVER (IPlayaObserver + Playa como notificador):
//   Cada ingreso/egreso le interesa a cuatro partes (pantalla, caja, alerta,
//   bitácora). Sin Observer, Playa llamaría a las cuatro a mano y cada aviso
//   nuevo obligaría a modificarla. Así Playa solo cambia su estado y avisa;
//   las reacciones se suman o se quitan en ejecución (como la pantalla que
//   se desuscribe a mitad de camino) sin tocar Playa.
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
