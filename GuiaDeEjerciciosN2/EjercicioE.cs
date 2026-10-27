// ===================================
//               SECCIÓN E 
// ===================================

// ============================================
// E1 — Delivery: costo segun moto / bici / auto (+ dron el mes que viene)
// Patron: Strategy (y Factory si el vehículo llega como texto desde la pantalla).
// Por qué: es "¿cómo lo calculo?": misma pregunta (costo del envío), varias
// fórmulas que se eligen en ejecución, y ya sabemos que va a aparecer otra
// (dron). Con una clase por vehículo detrás de una interfaz, el dron es una
// clase nueva y no otro case en un switch que ya funcionaba.
// ============================================

// ============================================
// E2 — Pedido "listo": cocina, mozo, caja (+ WhatsApp más adelante)
// Patrón: Observer.
// Por qué: es "¿a quién le aviso?": un cambio de estado le interesa a varios
// y la lista de interesados va a crecer (WhatsApp). El pedido notifica a una
// lista de suscriptores sin conocerlos; sumar WhatsApp = una clase nueva y
// un Suscribir, sin tocar el código del pedido.
// ============================================

// ============================================
// E3 — Configuración: cada formulario crea la suya y quedan desincronizadas
// Patrón: Singleton.
// Por qué: es "¿cuántos hay?": tiene que haber UNA configuración, pero hoy
// cada pantalla hace su propio new y por eso al cambiar la sucursal en una,
// las otras siguen mostrando la vieja (son objetos distintos). Con una sola
// instancia compartida, todas leen el mismo dato.
// (Alternativa válida: crearla una vez en Program.cs y pasarla por
// constructor con DI; lo importante es que sea un único objeto.)
// ============================================

// ============================================
// E4 — Pagos guardados en JSON con el medio como texto ("TARJETA", "QR"...)
// Patrón: Factory (Simple Factory) + Strategy.
// Por qué: es "¿qué objeto creo?": el tipo llega como TEXTO en ejecución,
// uno por cada pago del archivo, y hay que reconstruir el objeto correcto.
// La factory traduce texto -> objeto en un solo lugar; cada medio de pago es
// una estrategia que sabe calcular su propio recargo.
// ============================================

// ============================================
// E5 — "Agregarle una factory al repositorio para que quede más profesional"
// Patrón: ninguno (alcanza con inyección de dependencias, como ya está).
// Por qué: no hay síntoma que resolver: el repositorio se crea UNA sola vez,
// en un solo lugar (Program.cs), lo decide quien programa y nunca cambia
// mientras corre el programa. Una factory solo agregaría una clase y un
// salto más para leer el código sin ganar nada. Un patrón se justifica por un
// problema, no por "quedar profesional".
// ============================================
