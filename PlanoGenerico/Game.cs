using System;
using OpenTK.Graphics;
using OpenTK.Input;
using OpenTK;
using OpenTK.Graphics.OpenGL;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;

namespace PlanoGenerico
{
    internal class Game
    {
        static void Main(string[] args)
        {

            using (Escenarios Grafico = new Escenarios(1080, 720, "Escenario"))
            {
                Grafico.Run(100.0);
            }
        }
        public class Escenarios : GameWindow
        {
            
            Escenario escenario = new Escenario();
            Serializacion serializacion = new Serializacion();           
            Objeto pelota = new Objeto();
            private Animacion animacion  = new Animacion();

            private Vector3 cameraPosition = new Vector3(3, 3, 3);
            private Vector3 cameraFront = Vector3.UnitZ;
            private Vector3 cameraUp = Vector3.UnitY;
            //private float cameraSpeed = 0.1f;
            private float yaw = -90f;
            private float pitch = 0f;

            public Escenarios(int width, int height, string title) : base(width, height, GraphicsMode.Default, title) { }

            protected override void OnLoad(EventArgs e)
            {
                GL.ClearColor(Color4.White);            
                string nombreArchivo = @"D:\UAGRM\Grafica\escenario.json";
                escenario = serializacion.CargarEscenario(nombreArchivo);

                float x1 = 10;
                float x2 = 12;
                float y1 = -1;
                float y2 = 3;
                float z1 = 1;
                float z2 = 3;

                // Crear las caras del primer cubo
                Cara caraFrontalp = new Cara();
                caraFrontalp.addPunto(new Punto(x1, y1, z2));
                caraFrontalp.addPunto(new Punto(x1, y2, z2));
                caraFrontalp.addPunto(new Punto(x2, y2, z2));
                caraFrontalp.addPunto(new Punto(x2, y1, z2));
                caraFrontalp.color = Color4.LightBlue;

                Cara caraTraserat = new Cara();
                caraTraserat.addPunto(new Punto(x1, y1, z1));
                caraTraserat.addPunto(new Punto(x1, y2, z1));
                caraTraserat.addPunto(new Punto(x2, y2, z1));
                caraTraserat.addPunto(new Punto(x2, y1, z1));
                caraTraserat.color = Color4.Gray;

                Cara caraSuperiors = new Cara();
                caraSuperiors.addPunto(new Punto(x1, y2, z1));
                caraSuperiors.addPunto(new Punto(x1, y2, z2));
                caraSuperiors.addPunto(new Punto(x2, y2, z2));
                caraSuperiors.addPunto(new Punto(x2, y2, z1));
                caraSuperiors.color = Color4.Gray;

                Cara caraInferiori = new Cara();
                caraInferiori.addPunto(new Punto(x1, y1, z1));
                caraInferiori.addPunto(new Punto(x1, y1, z2));
                caraInferiori.addPunto(new Punto(x2, y1, z2));
                caraInferiori.addPunto(new Punto(x2, y1, z1));
                caraInferiori.color = Color4.Gray;

                Cara caraLateralIzquierdal = new Cara();
                caraLateralIzquierdal.addPunto(new Punto(x1, y1, z1));
                caraLateralIzquierdal.addPunto(new Punto(x1, y2, z1));
                caraLateralIzquierdal.addPunto(new Punto(x1, y2, z2));
                caraLateralIzquierdal.addPunto(new Punto(x1, y1, z2));
                caraLateralIzquierdal.color = Color4.Gray;

                Cara caraLateralDerechad = new Cara();
                caraLateralDerechad.addPunto(new Punto(x2, y1, z1));
                caraLateralDerechad.addPunto(new Punto(x2, y2, z1));
                caraLateralDerechad.addPunto(new Punto(x2, y2, z2));
                caraLateralDerechad.addPunto(new Punto(x2, y1, z2));
                caraLateralDerechad.color = Color4.Gray;

        
                Partes partesCub = new Partes();
                partesCub.centroDeMasa = new Punto(20, 10, 10);
                partesCub.AgregarCara("Frontal", caraFrontalp);
                partesCub.AgregarCara("Trasera", caraTraserat);
                partesCub.AgregarCara("Superior", caraSuperiors);
                partesCub.AgregarCara("Inferior", caraInferiori);
                partesCub.AgregarCara("LateralIzquierda", caraLateralIzquierdal);
                partesCub.AgregarCara("LateralDerecha", caraLateralDerechad);

                
                pelota.centroDeMasa = new Punto(0, 0, 0);
                pelota.AgregarPartes("pelota", partesCub);

                escenario.AgregarObjeto("pelota", pelota);

                CargarAnimacion();
                

                base.OnLoad(e);
            }
            
            private async void CargarAnimacion()
            {

                Objeto florero = escenario.ObtenerObjetoPorNombre("florero");

                Action adelante = () => pelota.TransladarObjeto(-1, 0, 0);
                Action adelanteLento = () => pelota.TransladarObjeto(-0.5f, 0, 0);
                Action abajo = () => pelota.TransladarObjeto(0, -0.4f, 0);
                Action arriba = () => pelota.TransladarObjeto(0, 1f, 0);
                Action atras = () => pelota.TransladarObjeto(0, 0, -0.1f);
                Action frente = () => pelota.TransladarObjeto(0, 0, 1);
                Action rotar = () => pelota.RotarObjeto ("x", 4);
                Action rotary = () => pelota.RotarObjeto("x", 2);

                Action atrasz = () => florero.TransladarObjeto(0,0,-1);
                Action atrasy = () => florero.TransladarObjeto(0,-1,0);
                Action rotaryf = () => florero.RotarConCentroDeMasa(new Punto(1,2,1), "x",-3);



                Acciones acto1 = new Acciones();
                acto1.objeto.Add(adelanteLento);
                acto1.objeto.Add(arriba);
                acto1.objeto.Add(rotar);


                Acciones acto2 = new Acciones();
                acto2.objeto.Add(adelanteLento);
                acto2.objeto.Add(abajo);
                acto2.objeto.Add(rotar);


                Acciones acto3 = new Acciones();
                acto3.objeto.Add(atras);
                acto3.objeto.Add(arriba);

                Acciones acto4 = new Acciones();
                acto4.objeto.Add(atrasz);
                acto4.objeto.Add(atrasy);
                acto4.objeto.Add(rotaryf);

                Acciones acto5 = new Acciones();
                acto5.objeto.Add(frente);
                acto5.objeto.Add(abajo);
                acto5.objeto.Add(rotar);


                animacion.AgregarAccion(acto1.objeto, 30); //subida
                animacion.AgregarAccion(acto2.objeto, 25); // bajada
                animacion.AgregarAccion(acto3.objeto, 33); // colicion

                Action accionActo4 = () => animacion.AgregarAccion(acto4.objeto, 19);
                Action accionActo5 = () => animacion.AgregarAccion(acto5.objeto, 33);

                // Ejecutar acto4 y acto5 simultáneamente
                Parallel.Invoke(accionActo4, accionActo5);


                animacion.Iniciar();

            }

            /*private void CrearAnimacion()
            {
                float deltaX = -0.1f;
                float initialY = 0.5f;
                float gravity = -0.05f;
                float limiteYRebote = -8f;
                bool llegoLimite = false;

                for (int i = 0; i < 40; i++)
                {
                    float desplazamientoX = deltaX * i;
                    float desplazamientoY;

                    if (!llegoLimite)
                    {
                        desplazamientoY = CalcularDesplazamientoY(initialY, gravity, i);

                        if (desplazamientoY <= limiteYRebote)
                        {
                            // La pelota alcanzó el límite de rebote
                            llegoLimite = true;
                            i = 0; // Reiniciar el contador para la parábola hacia atrás
                        }
                    }
                    else
                    {
                        // Parábola hacia atrás
                        desplazamientoY = CalcularDesplazamientoY(initialY, -gravity, i);

                        // Detener la animación cuando vuelva a la altura inicial
                        if (desplazamientoY >= initialY)
                        {
                            break;
                        }
                    }

                    List<object> accionParabolica = new List<object>
        {
            new Action(() => pelota.TransladarObjeto(desplazamientoX, desplazamientoY, 0)),
            new Action(() => pelota.RotarConCentroDeMasa(new Punto(3, -3, 1), "y", 10))
        };

                    animacion.AgregarAccion(accionParabolica, 1);
                }

                animacion.Iniciar();
            }*/



            private float CalcularDesplazamientoY(float initialY, float gravity, int time)
            {
                return initialY + gravity * time * time;
            }
            protected override void OnRenderFrame(FrameEventArgs e)
            {
                GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);
                GL.Enable(EnableCap.DepthTest);
                GL.LoadIdentity();

                // Actualizar la dirección de la cámara
                cameraFront = new Vector3(
                    (float)(Math.Cos(MathHelper.DegreesToRadians(yaw)) * Math.Cos(MathHelper.DegreesToRadians(pitch))),
                    (float)Math.Sin(MathHelper.DegreesToRadians(pitch)),
                    (float)(Math.Sin(MathHelper.DegreesToRadians(yaw)) * Math.Cos(MathHelper.DegreesToRadians(pitch)))
                );

                // Configurar la matriz de vista para el movimiento de la cámara
                Matrix4 viewMatrix = Matrix4.LookAt(cameraPosition, cameraPosition + cameraFront, cameraUp);
                GL.MatrixMode(MatrixMode.Modelview);
                GL.LoadMatrix(ref viewMatrix);


                
                escenario.DibujarEscenario();
               
                
                Context.SwapBuffers();
                base.OnRenderFrame(e);
            }

            protected override void OnResize(EventArgs e)
            {
                float d = 30;
                GL.Viewport(0, 0, Width, Height);
                GL.MatrixMode(MatrixMode.Projection);
                GL.LoadIdentity();
                GL.Ortho(-d, d, -d, d, -d, d);
                GL.MatrixMode(MatrixMode.Modelview);
                GL.LoadIdentity();
                base.OnResize(e);
            }

            protected override void OnUpdateFrame(FrameEventArgs e)
            {
                Objeto televisor = escenario.ObtenerObjetoPorNombre("Televisor");
                Objeto florero = escenario.ObtenerObjetoPorNombre("florero");
                Partes partePetalos = florero.ObtenerPartePorNombre("floreroBase");
                // Capturar entrada del usuario para rotar la cámara
                if (Keyboard.GetState().IsKeyDown(Key.Left))
                {
                    escenario.ScalarEscenario(0.9f); // Reducir escala del escenario
                }
                if (Keyboard.GetState().IsKeyDown(Key.Right))
                {
                    escenario.ScalarEscenario(1.1f); // Aumentar escala del escenario
                }
                if (Keyboard.GetState().IsKeyDown(Key.Up))
                {
                    escenario.TransladarEscenario(0.0f, 0.1f, 0.0f); // Trasladar el escenario hacia arriba
                }
                if (Keyboard.GetState().IsKeyDown(Key.Down))
                {
                    escenario.TransladarEscenario(0.0f, -0.1f, 0.0f); // Trasladar el escenario hacia abajo
                }
                if (Keyboard.GetState().IsKeyDown(Key.A))
                {
                    televisor.TransladarObjeto(-0.1f, 0.0f, 0.0f); // Traslación hacia la izquierda
                }
                if (Keyboard.GetState().IsKeyDown(Key.D))
                {
                    televisor.TransladarObjeto(0.1f, 0.0f, 0.0f); // Traslación hacia la derecha
                }
                if (Keyboard.GetState().IsKeyDown(Key.W))
                {
                    televisor.TransladarObjeto(0.0f, 0.1f, 0.0f); // Traslación hacia adelante
                }
                if (Keyboard.GetState().IsKeyDown(Key.S))
                if (Keyboard.GetState().IsKeyDown(Key.S))
                {

                    televisor.RotarConCentroDeMasa(new Punto(1, 1, 2f),"x", 0.5f); // Traslación hacia atrás
                }
                if (Keyboard.GetState().IsKeyDown(Key.X))
                {
                    partePetalos.RotarCentroDeMasa(new Punto(1,1,2f), "y", 0.5f); // Traslación hacia adelante
                }
                if (Keyboard.GetState().IsKeyDown(Key.Z))
                {
                    partePetalos.RotarCaras( "y", 0.1f); // Traslación hacia adelante
                }
                if (Keyboard.GetState().IsKeyDown(Key.F))
                {
                    yaw -= 1f; // Girar a la izquierda
                }
                if (Keyboard.GetState().IsKeyDown(Key.H))
                {
                    yaw += 1f; // Girar a la derecha
                }
                if (Keyboard.GetState().IsKeyDown(Key.T))
                {
                    pitch += 1f; // Inclinar hacia arriba
                }
                if (Keyboard.GetState().IsKeyDown(Key.G))
                {
                    pitch -= 1f; // Inclinar hacia abajo
                    
                }

                if (Keyboard.GetState().IsKeyDown(Key.P))
                {
                    animacion.Pausar(); // Pausar la animación
                }

                if (Keyboard.GetState().IsKeyDown(Key.O))
                {
                    animacion.Iniciar();
                }

                if (Keyboard.GetState().IsKeyDown(Key.I))
                {
                    animacion.Detener(); // Detener la animación
                }


                base.OnUpdateFrame(e);
            }
           
        }
    }

    
}
