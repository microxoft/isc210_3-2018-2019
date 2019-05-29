using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Ejercicio1_Connect4
{
    class Program
    {
        static Thread hiloCaptura;
        static Juego juegoActual = new Juego();
        static int[] posicionesColumnas = new int[7] { 1, 3, 5, 7, 9, 11, 13 };

        static void InicializarJuego()
        {
            juegoActual.Estado = Juego.eEstadoJuego.MostrandoMenu;
            juegoActual.TurnoPrimerJugador = true;
            juegoActual.SiguienteColumna = -1;
            juegoActual.Tablero = new bool?[6, 7];
            Console.CursorVisible = false;
            hiloCaptura = new Thread(new ThreadStart(CapturarTeclado));
            hiloCaptura.Start();
        }

        static void Main(string[] args)
        {
            InicializarJuego();

            while(juegoActual.Estado != Juego.eEstadoJuego.JuegoFinalizado)
            {
                // Primero: verificamos la entrada del usuario.
                
                if(juegoActual.Estado == Juego.eEstadoJuego.Jugando && juegoActual.SiguienteColumna >= 0)
                {
                    juegoActual.Estado = Juego.eEstadoJuego.Animando;

                    juegoActual.ProximaFila = juegoActual.ProximaFilaDisponible(juegoActual.SiguienteColumna);

                    Thread nuevoHilo = new Thread(new ThreadStart(AnimarNuevaMoneda));
                    nuevoHilo.Start();
                    nuevoHilo.Join();
                    
                    juegoActual.Tablero[juegoActual.ProximaFila, juegoActual.SiguienteColumna] = juegoActual.TurnoPrimerJugador;
                                        
                    if (juegoActual.VerificarFinPartida(juegoActual.ProximaFila, juegoActual.SiguienteColumna))
                    {
                        juegoActual.Estado = Juego.eEstadoJuego.JuegoFinalizado;
                        Console.SetCursorPosition(8, 5);
                        Console.WriteLine("Ha ganado el jugador " + (juegoActual.TurnoPrimerJugador ? Juego.SPRITE1 : Juego.SPRITE2));
                    }
                        

                    juegoActual.SiguienteColumna = -1;
                    juegoActual.TurnoPrimerJugador = !juegoActual.TurnoPrimerJugador;
                }

                // Segundo: actualizamos valores en función del estado actual.

                // Tercero: renderizamos el juego.
                Renderizar();
                Thread.Sleep(50);
            }

            Console.WriteLine("¡Gracias por jugar!");

            Console.ReadKey();
        }

        private static void AnimarNuevaMoneda()
        {
            TimeSpan horaInicio = new TimeSpan(DateTime.Now.Ticks);
            short posAnterior, posInicial = 5, posActual, posFinal;
            posAnterior = posInicial;
            posFinal = (short)(juegoActual.ProximaFila + 1);

            if (posFinal < 1)
                return;

            do
            {
                posActual = Convert.ToInt16(posInicial + Juego.GRAVEDAD * Math.Pow((new TimeSpan(DateTime.Now.Ticks).Subtract(horaInicio).TotalSeconds), 2) / 2);

                Console.SetCursorPosition(posicionesColumnas[juegoActual.SiguienteColumna], posAnterior);
                Console.Write(" "); // Eliminamos la moneda anterior.

                Console.SetCursorPosition(posicionesColumnas[juegoActual.SiguienteColumna], posActual);
                Console.Write(juegoActual.TurnoPrimerJugador ? Juego.SPRITE1 : Juego.SPRITE2);

                posAnterior = posActual;
            } while (posActual <= (posInicial + posFinal));

            juegoActual.Estado = Juego.eEstadoJuego.Jugando;
        }

        static void Renderizar()
        {
            switch(juegoActual.Estado)
            {
                case Juego.eEstadoJuego.MostrandoMenu:
                    Console.SetCursorPosition(0, 0);
                    Console.WriteLine("Seleccione una opción del menú: ");
                    Console.WriteLine("\n\t1: Jugar.");
                    Console.WriteLine("\t2: Salir.");
                    break;
                case Juego.eEstadoJuego.Jugando:
                    ImprimirEscena();
                    ImprimirTablero();
                    break;
            }
        }

        static void CapturarTeclado()
        {
            int _tmp;
            string capturaActual;
            while(true)
            {
                capturaActual = Console.ReadLine();
                switch(juegoActual.Estado)
                {
                    case Juego.eEstadoJuego.MostrandoMenu:
                        if (capturaActual == "2")
                        {
                            juegoActual.Estado = Juego.eEstadoJuego.JuegoFinalizado;
                            Thread.CurrentThread.Abort();
                        }
                        else if (capturaActual == "1")
                        {
                            juegoActual.Estado = Juego.eEstadoJuego.Jugando;
                        }
                        break;
                    case Juego.eEstadoJuego.Jugando:
                        if(Int32.TryParse(capturaActual, out _tmp) && _tmp >= 1 && _tmp <= 7)
                        {
                            // Seleccionó una columna.
                            juegoActual.SiguienteColumna = _tmp - 1;
                        }
                        break;
                }
            }
        }

        static void ImprimirEscena()
        {
            Console.Clear();
            Console.WriteLine("CONNECT-4");
            Console.WriteLine("Turno Actual: " + (juegoActual.TurnoPrimerJugador ? "Primer Jugador" : "Segundo Jugador"));
            Console.WriteLine("\nSeleccione una columna [1-7]");
            Console.WriteLine(" 1 2 3 4 5 6 7");
            Console.WriteLine("--------------");
            Console.WriteLine("| | | | | | | |");
            Console.WriteLine("| | | | | | | |");
            Console.WriteLine("| | | | | | | |");
            Console.WriteLine("| | | | | | | |");
            Console.WriteLine("| | | | | | | |");
            Console.WriteLine("| | | | | | | |");
            Console.WriteLine("--------------");
        }

        static void ImprimirTablero()
        {
            short posInicial = 6;

            for(int i = 0; i < juegoActual.Tablero.GetLength(0); i++)
            {
                for(int j = 0; j < juegoActual.Tablero.GetLength(1); j++)
                {
                    if (juegoActual.Tablero[i, j] == null)
                        continue;

                    Console.SetCursorPosition(posicionesColumnas[j], posInicial + i);
                    Console.Write(juegoActual.Tablero[i,j] == true ? Juego.SPRITE1 : Juego.SPRITE2);
                }
            }
        }
    }
}
