using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK.Graphics;
using OpenTK.Input;
using OpenTK;
using System;
using OpenTK.Graphics.OpenGL;
using Newtonsoft.Json;
using System.IO;
using OpenTK.Graphics;



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

                // Coordenadas del primer cubo
                float x1 = -12f;
                float x2 = 12f;
                float y1 = -8f;
                float y2 = 8f;
                float z1 = 0f;
                float z2 = 10f;

                // Crear las caras del primer cubo
                Cara caraFrontal1 = new Cara();
                caraFrontal1.addPunto(new Punto(x1, y1, z2));
                caraFrontal1.addPunto(new Punto(x1, y2, z2));
                caraFrontal1.addPunto(new Punto(x2, y2, z2));
                caraFrontal1.addPunto(new Punto(x2, y1, z2));
                caraFrontal1.color = Color4.Gray;

                Cara caraTrasera1 = new Cara();
                caraTrasera1.addPunto(new Punto(x1, y1, z1));
                caraTrasera1.addPunto(new Punto(x1, y2, z1));
                caraTrasera1.addPunto(new Punto(x2, y2, z1));
                caraTrasera1.addPunto(new Punto(x2, y1, z1));
                caraTrasera1.color = Color4.LightBlue;

                Cara caraSuperior1 = new Cara();
                caraSuperior1.addPunto(new Punto(x1, y2, z1));
                caraSuperior1.addPunto(new Punto(x1, y2, z2));
                caraSuperior1.addPunto(new Punto(x2, y2, z2));
                caraSuperior1.addPunto(new Punto(x2, y2, z1));
                caraSuperior1.color = Color4.Gray;

                Cara caraInferior1 = new Cara();
                caraInferior1.addPunto(new Punto(x1, y1, z1));
                caraInferior1.addPunto(new Punto(x1, y1, z2));
                caraInferior1.addPunto(new Punto(x2, y1, z2));
                caraInferior1.addPunto(new Punto(x2, y1, z1));
                caraInferior1.color = Color4.Gray;

                Cara caraLateralIzquierda1 = new Cara();
                caraLateralIzquierda1.addPunto(new Punto(x1, y1, z1));
                caraLateralIzquierda1.addPunto(new Punto(x1, y2, z1));
                caraLateralIzquierda1.addPunto(new Punto(x1, y2, z2));
                caraLateralIzquierda1.addPunto(new Punto(x1, y1, z2));
                caraLateralIzquierda1.color = Color4.Gray;

                Cara caraLateralDerecha1 = new Cara();
                caraLateralDerecha1.addPunto(new Punto(x2, y1, z1));
                caraLateralDerecha1.addPunto(new Punto(x2, y2, z1));
                caraLateralDerecha1.addPunto(new Punto(x2, y2, z2));
                caraLateralDerecha1.addPunto(new Punto(x2, y1, z2));
                caraLateralDerecha1.color = Color4.Gray;

                // Crear las partes del primer cubo con el centro de masa ajustado
                Partes partesCubo1 = new Partes();
                partesCubo1.centroDeMasa = new Punto(2,2,2); // Centro de masa del primer cubo
                partesCubo1.AgregarCara("Frontal", caraFrontal1);
                partesCubo1.AgregarCara("Trasera", caraTrasera1);
                partesCubo1.AgregarCara("Superior", caraSuperior1);
                partesCubo1.AgregarCara("Inferior", caraInferior1);
                partesCubo1.AgregarCara("LateralIzquierda", caraLateralIzquierda1);
                partesCubo1.AgregarCara("LateralDerecha", caraLateralDerecha1);

                Objeto televisor = new Objeto();
                televisor.AgregarPartes("pantalla", partesCubo1);
                televisor.centroDeMasa = new Punto(01f, 1f, 1f);

                // Crear el escenario y agregar los objetos

                escenario.centroDeMasa = new Punto(0f, 0f, 0f);
                escenario.AgregarObjeto("Televisor", televisor);
               


                //string json = JsonConvert.SerializeObject(escenario);
                //GuardarEscenario(@"D:\UAGRM\Grafica\escenario.json", escenario);
                //Console.WriteLine(json);

                //string nombreArchivo = @"D:\UAGRM\Grafica\escenario.json";
                //Console.WriteLine(nombreArchivo);// Ruta y nombre del archivo
                //escenario = CargarEscenario(nombreArchivo);



                base.OnLoad(e);
            }
            public void GuardarEscenario(string nombreArchivo, Escenario escenario)
            {
                string json = JsonConvert.SerializeObject(escenario);
                File.WriteAllText(nombreArchivo, json);
                Console.WriteLine("Escenario guardado correctamente en " + nombreArchivo);
            }

            public static Escenario CargarEscenario(string nombreArchivo)
            {
                string json = File.ReadAllText(nombreArchivo);
                Escenario escenario = JsonConvert.DeserializeObject<Escenario>(json);
                return escenario;
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


                //tele.DibujarObjeto();
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
                // Capturar entrada del usuario para rotar la cámara
                if (Keyboard.GetState().IsKeyDown(Key.Left))
                {
                    yaw -= 1f; // Girar a la izquierda
                }
                if (Keyboard.GetState().IsKeyDown(Key.Right))
                {
                    yaw += 1f; // Girar a la derecha
                }
                if (Keyboard.GetState().IsKeyDown(Key.Up))
                {
                    pitch += 1f; // Inclinar hacia arriba
                }
                if (Keyboard.GetState().IsKeyDown(Key.Down))
                {
                    pitch -= 1f; // Inclinar hacia abajo
                }

                base.OnUpdateFrame(e);
            }
           
        }
    }

    
}
