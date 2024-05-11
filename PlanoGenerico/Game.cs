using System;
using OpenTK.Graphics;
using OpenTK.Input;
using OpenTK;
using OpenTK.Graphics.OpenGL;
using System.Threading.Tasks;
using System.Threading;




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
                Objeto florero = escenario.ObtenerObjetoPorNombre("florero");
                Partes partePetalos = florero.ObtenerPartePorNombre("floreroBase");


                Thread animacionThread = new Thread(() =>
                {
                    Animacion animacion = new Animacion(florero, 1.0f); // Tiempo entre transformaciones en segundos
                    animacion.AgregarTransformacion(new Transformacion("Traslacion", 10, 0, 0));
                    animacion.AgregarTransformacion(new Transformacion("Escalado", 2, new Punto(0, 0, 0))); // Origen de la escala
                    animacion.AgregarTransformacion(new Transformacion("Rotacion", "y", 90, new Punto(0, 0, 0))); // Origen de la rotación

                    // Ejecutar la animación
                    animacion.EjecutarAnimacion();
                });

                animacionThread.Start();

                base.OnLoad(e);
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




                base.OnUpdateFrame(e);
            }
           
        }
    }

    
}
