// ===================================
//               SECCION E
// ===================================

// ============================================
// E1:
// Patron: Strategy.
// Por que: hay varias formulas que se eligen en ejecucion y ya sabemos que
// va a venir otra (el dron). Con una clase por vehiculo detras de una
// interfaz, agregar el dron es crear una clase nueva, sin tocar lo que ya
// funciona.
// ============================================

// ============================================
// E2:
// Patron: Observer.
// Por que: cuando el pedido cambia de estado hay que avisarle a varios, y
// esa lista va a crecer. El pedido avisa a sus suscriptores sin saber
// quienes son, asi que sumar WhatsApp es una clase nueva y un Suscribir,
// sin tocar el pedido.
// ============================================

// ============================================
// E3: 
// Patron: Singleton.
// Por que: tiene que haber una sola configuracion, porque ahora cada pantalla
// hace su propio new. Por eso, si se cambia la sucursal en una, las otras
// siguen con la vieja. Con una unica instancia compartida, todas ven el
// mismo dato.
// ============================================

// ============================================
// E4:
// Patron: Factory + Strategy.
// Por que: el medio de pago llega como texto y hay que convertirlo en el
// objeto correcto. Factory hace esa traduccion en un solo lugar, y cada
// medio de pago es una estrategia que calcula su propio recargo.
// ============================================

// ============================================
// E5:
// Patron: ninguno, alcanza con inyeccion de dependencias.
// Por que: no hay ningun problema que resolver. El repositorio se crea una
// sola vez en Program.cs y no cambia mientras corre el programa.
// ============================================
