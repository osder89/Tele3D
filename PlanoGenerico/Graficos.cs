using OpenTK.Graphics;
using OpenTK.Input;
using OpenTK;
using PlanoGenerico.Extras;
using System;
using OpenTK.Graphics.OpenGL;
using System.Drawing;
using System.Runtime.Remoting.Messaging;


using System.Runtime.Remoting.Messaging;

namespace PlanoGenerico
{
    public class Grafico : GameWindow
    {
        private Tele tv;
        private Tele tv1;
        private Tele tvFront1;
        private Tele tvFront2;
        private Tele tvBack1;
        private Tele tvBack2;
        private Vector3 cameraPosition = new Vector3(-5, 5, 20);
        private Vector3 cameraFront = Vector3.UnitZ;
        private Vector3 cameraUp = Vector3.UnitY;
        private float cameraSpeed = 0.1f;
        private float yaw = -90f; // Ángulo de rotación en yaw
        private float pitch = 0f; // Ángulo de rotación en pitch

        public Grafico(int width, int height, string title) : base(width, height, GraphicsMode.Default, title) { }

        protected override void OnLoad(EventArgs e)
        {
            GL.ClearColor(Color4.White);

            // Tele con barra roja entre el soporte y la pantalla
            tv = new Tele(new Punto(-10, 0, 0), 10, 13, 3, Color.Red, Color.LightGray, Color.DarkGray);

            // Tele sin barra
            tv1 = new Tele(new Punto(15, 0, 0), 10, 13, 3, Color.Blue, Color.Red, Color.Green);

            // Telees adelante
            tvFront1 = new Tele(new Punto(-10, 0, 15), 10, 13, 3, Color.Yellow, Color.Orange, Color.Green);
            tvFront2 = new Tele(new Punto(15, 0, 15), 10, 13, 3, Color.LightCoral, Color.Orange, Color.Green);

            // Telees atrás
            tvBack1 = new Tele(new Punto(-10, 0, -15), 10, 13, 3, Color.LightBlue, Color.Orange, Color.Green);
            tvBack2 = new Tele(new Punto(15, 0, -15), 10, 13, 3, Color.Yellow, Color.Orange, Color.Green);

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

            // Dibujar los Telees
            this.tv.Dibujar();
            this.tv1.Dibujar();
            this.tvFront1.Dibujar();
            this.tvFront2.Dibujar();
            this.tvBack1.Dibujar();
            this.tvBack2.Dibujar();

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


