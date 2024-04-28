using System;
using OpenTK.Graphics;
using OpenTK.Input;
using OpenTK;
using OpenTK.Graphics.OpenGL;
using Newtonsoft.Json;
using System.IO;
using System.Collections.Generic;



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
            Escenario escenario2 = new Escenario();
            


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
                float x1 = -12;
                float x2 = 12;
                float y1 = -8;
                float y2 = 8;
                float z1 = 1;
                float z2 = 3;

                // Crear las caras del primer cubo
                Cara caraFrontal1 = new Cara();
                caraFrontal1.addPunto(new Punto(x1, y1, z2));
                caraFrontal1.addPunto(new Punto(x1, y2, z2));
                caraFrontal1.addPunto(new Punto(x2, y2, z2));
                caraFrontal1.addPunto(new Punto(x2, y1, z2));
                caraFrontal1.color = Color4.LightBlue;

                Cara caraTrasera1 = new Cara();
                caraTrasera1.addPunto(new Punto(x1, y1, z1));
                caraTrasera1.addPunto(new Punto(x1, y2, z1));
                caraTrasera1.addPunto(new Punto(x2, y2, z1));
                caraTrasera1.addPunto(new Punto(x2, y1, z1));
                caraTrasera1.color = Color4.Gray;

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
                partesCubo1.centroDeMasa = new Punto(0,0,0); // Centro de masa del primer cubo
                partesCubo1.AgregarCara("Frontal", caraFrontal1);
                partesCubo1.AgregarCara("Trasera", caraTrasera1);
                partesCubo1.AgregarCara("Superior", caraSuperior1);
                partesCubo1.AgregarCara("Inferior", caraInferior1);
                partesCubo1.AgregarCara("LateralIzquierda", caraLateralIzquierda1);
                partesCubo1.AgregarCara("LateralDerecha", caraLateralDerecha1);

                // Coordenadas de la barra
                float barraX1 = x1; // Misma posición X que el cubo
                float barraX2 = x2;
                float barraY1 = y1 ; // Posición Y por debajo del cubo
                float barraY2 = y1 - 1f;
                float barraZ1 = (z1 + z2) / 2f - 1f; // Posición Z en el centro entre las caras frontal y trasera del cubo
                float barraZ2 = 3; // Longitud de la barra

                // Crear las caras de la barra
                Cara caraFrontalBarra = new Cara();
                caraFrontalBarra.addPunto(new Punto(barraX1, barraY1, barraZ2));
                caraFrontalBarra.addPunto(new Punto(barraX1, barraY2, barraZ2));
                caraFrontalBarra.addPunto(new Punto(barraX2, barraY2, barraZ2));
                caraFrontalBarra.addPunto(new Punto(barraX2, barraY1, barraZ2));
                

                Cara caraTraseraBarra = new Cara();
                caraTraseraBarra.addPunto(new Punto(barraX1, barraY1, barraZ1));
                caraTraseraBarra.addPunto(new Punto(barraX1, barraY2, barraZ1));
                caraTraseraBarra.addPunto(new Punto(barraX2, barraY2, barraZ1));
                caraTraseraBarra.addPunto(new Punto(barraX2, barraY1, barraZ1));
               

                Cara caraSuperiorBarra = new Cara();
                caraSuperiorBarra.addPunto(new Punto(barraX1, barraY2, barraZ1));
                caraSuperiorBarra.addPunto(new Punto(barraX1, barraY2, barraZ2));
                caraSuperiorBarra.addPunto(new Punto(barraX2, barraY2, barraZ2));
                caraSuperiorBarra.addPunto(new Punto(barraX2, barraY2, barraZ1));
               

                Cara caraInferiorBarra = new Cara();
                caraInferiorBarra.addPunto(new Punto(barraX1, barraY1, barraZ1));
                caraInferiorBarra.addPunto(new Punto(barraX1, barraY1, barraZ2));
                caraInferiorBarra.addPunto(new Punto(barraX2, barraY1, barraZ2));
                caraInferiorBarra.addPunto(new Punto(barraX2, barraY1, barraZ1));
                

                Cara caraLateralIzquierdaBarra = new Cara();
                caraLateralIzquierdaBarra.addPunto(new Punto(barraX1, barraY1, barraZ1));
                caraLateralIzquierdaBarra.addPunto(new Punto(barraX1, barraY2, barraZ1));
                caraLateralIzquierdaBarra.addPunto(new Punto(barraX1, barraY2, barraZ2));
                caraLateralIzquierdaBarra.addPunto(new Punto(barraX1, barraY1, barraZ2));
               

                Cara caraLateralDerechaBarra = new Cara();
                caraLateralDerechaBarra.addPunto(new Punto(barraX2, barraY1, barraZ1));
                caraLateralDerechaBarra.addPunto(new Punto(barraX2, barraY2, barraZ1));
                caraLateralDerechaBarra.addPunto(new Punto(barraX2, barraY2, barraZ2));
                caraLateralDerechaBarra.addPunto(new Punto(barraX2, barraY1, barraZ2));
               
                // Crear las partes de la barra con el centro de masa ajustado
                Partes partesBarra = new Partes();
                partesBarra.centroDeMasa = new Punto(0,0,0); // Centro de masa de la barra
                partesBarra.AgregarCara("Frontal", caraFrontalBarra);
                partesBarra.AgregarCara("Trasera", caraTraseraBarra);
                partesBarra.AgregarCara("Superior", caraSuperiorBarra);
                partesBarra.AgregarCara("Inferior", caraInferiorBarra);
                partesBarra.AgregarCara("LateralIzquierda", caraLateralIzquierdaBarra);
                partesBarra.AgregarCara("LateralDerecha", caraLateralDerechaBarra);


                // Coordenadas del soporte
                float soporteX1 = x1 /7; // Posición X a la izquierda de la pantalla
                float soporteX2 = x2 /7; // Posición X a la derecha de la pantalla
                float soporteY1 = y1  - 10; // Posición Y arriba de la pantalla
                float soporteY2 = y1 -1 ; // Altura del soporte
                float soporteZ1 = (z1 + z2 ) / 2f - 1f; // Posición Z adelante de la pantalla
                float soporteZ2 = 3; // Profundidad del soporte

                // Crear las caras del soporte
                Cara caraFrontalSoporte = new Cara();
                caraFrontalSoporte.addPunto(new Punto(soporteX1, soporteY1, soporteZ2));
                caraFrontalSoporte.addPunto(new Punto(soporteX1, soporteY2, soporteZ2));
                caraFrontalSoporte.addPunto(new Punto(soporteX2, soporteY2, soporteZ2));
                caraFrontalSoporte.addPunto(new Punto(soporteX2, soporteY1, soporteZ2));
                caraFrontalSoporte.color = Color4.Gray; // Por ejemplo, puedes asignarle un color gris

                Cara caraTraseraSoporte = new Cara();
                caraTraseraSoporte.addPunto(new Punto(soporteX1, soporteY1, soporteZ1));
                caraTraseraSoporte.addPunto(new Punto(soporteX1, soporteY2, soporteZ1));
                caraTraseraSoporte.addPunto(new Punto(soporteX2, soporteY2, soporteZ1));
                caraTraseraSoporte.addPunto(new Punto(soporteX2, soporteY1, soporteZ1));
                caraTraseraSoporte.color = Color4.Gray; // Por ejemplo, puedes asignarle un color gris

                Cara caraSuperiorSoporte = new Cara();
                caraSuperiorSoporte.addPunto(new Punto(soporteX1, soporteY2, soporteZ1));
                caraSuperiorSoporte.addPunto(new Punto(soporteX1, soporteY2, soporteZ2));
                caraSuperiorSoporte.addPunto(new Punto(soporteX2, soporteY2, soporteZ2));
                caraSuperiorSoporte.addPunto(new Punto(soporteX2, soporteY2, soporteZ1));
                caraSuperiorSoporte.color = Color4.Gray; // Por ejemplo, puedes asignarle un color gris

                Cara caraInferiorSoporte = new Cara();
                caraInferiorSoporte.addPunto(new Punto(soporteX1, soporteY1, soporteZ1));
                caraInferiorSoporte.addPunto(new Punto(soporteX1, soporteY1, soporteZ2));
                caraInferiorSoporte.addPunto(new Punto(soporteX2, soporteY1, soporteZ2));
                caraInferiorSoporte.addPunto(new Punto(soporteX2, soporteY1, soporteZ1));
                caraInferiorSoporte.color = Color4.Gray; // Por ejemplo, puedes asignarle un color gris

                Cara caraLateralIzquierdaSoporte = new Cara();
                caraLateralIzquierdaSoporte.addPunto(new Punto(soporteX1, soporteY1, soporteZ1));
                caraLateralIzquierdaSoporte.addPunto(new Punto(soporteX1, soporteY2, soporteZ1));
                caraLateralIzquierdaSoporte.addPunto(new Punto(soporteX1, soporteY2, soporteZ2));
                caraLateralIzquierdaSoporte.addPunto(new Punto(soporteX1, soporteY1, soporteZ2));
                caraLateralIzquierdaSoporte.color = Color4.Gray; // Por ejemplo, puedes asignarle un color gris

                Cara caraLateralDerechaSoporte = new Cara();
                caraLateralDerechaSoporte.addPunto(new Punto(soporteX2, soporteY1, soporteZ1));
                caraLateralDerechaSoporte.addPunto(new Punto(soporteX2, soporteY2, soporteZ1));
                caraLateralDerechaSoporte.addPunto(new Punto(soporteX2, soporteY2, soporteZ2));
                caraLateralDerechaSoporte.addPunto(new Punto(soporteX2, soporteY1, soporteZ2));
                caraLateralDerechaSoporte.color = Color4.Gray; // Por ejemplo, puedes asignarle un color gris

                
                Partes partesSoporte = new Partes();
                partesSoporte.centroDeMasa = new Punto(0,0,0); 
                partesSoporte.AgregarCara("Frontal", caraFrontalSoporte);
                partesSoporte.AgregarCara("Trasera", caraTraseraSoporte);
                partesSoporte.AgregarCara("Superior", caraSuperiorSoporte);
                partesSoporte.AgregarCara("Inferior", caraInferiorSoporte);
                partesSoporte.AgregarCara("LateralIzquierda", caraLateralIzquierdaSoporte);
                partesSoporte.AgregarCara("LateralDerecha", caraLateralDerechaSoporte);



                // Coordenadas de la base del soporte
                float baseX1 = soporteX1 *3 ; // Misma posición X que el soporte
                float baseX2 = soporteX2 *3 ;
                float baseY1 = soporteY1 - 2 ; // Misma posición Y que el soporte
                float baseY2 = soporteY1 ;
                float baseZ1 = soporteZ1  -  4 ; // Misma posición Z que el soporte
                float baseZ2 = 6 ; // Grosor de la base

                // Crear las caras de la base del soporte
                Cara caraBaseFrontal = new Cara();
                caraBaseFrontal.addPunto(new Punto(baseX1, baseY1, baseZ2));
                caraBaseFrontal.addPunto(new Punto(baseX1, baseY2, baseZ2));
                caraBaseFrontal.addPunto(new Punto(baseX2, baseY2, baseZ2));
                caraBaseFrontal.addPunto(new Punto(baseX2, baseY1, baseZ2));
                caraBaseFrontal.color = Color4.DarkGray; // Por ejemplo, puedes asignarle un color gris oscuro

                Cara caraBaseTrasera = new Cara();
                caraBaseTrasera.addPunto(new Punto(baseX1, baseY1, baseZ1));
                caraBaseTrasera.addPunto(new Punto(baseX1, baseY2, baseZ1));
                caraBaseTrasera.addPunto(new Punto(baseX2, baseY2, baseZ1));
                caraBaseTrasera.addPunto(new Punto(baseX2, baseY1, baseZ1));
                caraBaseTrasera.color = Color4.DarkGray; // Por ejemplo, puedes asignarle un color gris oscuro

                Cara caraBaseSuperior = new Cara();
                caraBaseSuperior.addPunto(new Punto(baseX1, baseY2, baseZ1));
                caraBaseSuperior.addPunto(new Punto(baseX1, baseY2, baseZ2));
                caraBaseSuperior.addPunto(new Punto(baseX2, baseY2, baseZ2));
                caraBaseSuperior.addPunto(new Punto(baseX2, baseY2, baseZ1));
                caraBaseSuperior.color = Color4.DarkGray; // Por ejemplo, puedes asignarle un color gris oscuro

                Cara caraBaseInferior = new Cara();
                caraBaseInferior.addPunto(new Punto(baseX1, baseY1, baseZ1));
                caraBaseInferior.addPunto(new Punto(baseX1, baseY1, baseZ2));
                caraBaseInferior.addPunto(new Punto(baseX2, baseY1, baseZ2));
                caraBaseInferior.addPunto(new Punto(baseX2, baseY1, baseZ1));
                caraBaseInferior.color = Color4.DarkGray; // Por ejemplo, puedes asignarle un color gris oscuro

                Cara caraBaseLateralIzquierda = new Cara();
                caraBaseLateralIzquierda.addPunto(new Punto(baseX1, baseY1, baseZ1));
                caraBaseLateralIzquierda.addPunto(new Punto(baseX1, baseY2, baseZ1));
                caraBaseLateralIzquierda.addPunto(new Punto(baseX1, baseY2, baseZ2));
                caraBaseLateralIzquierda.addPunto(new Punto(baseX1, baseY1, baseZ2));
                caraBaseLateralIzquierda.color = Color4.DarkGray; // Por ejemplo, puedes asignarle un color gris oscuro

                Cara caraBaseLateralDerecha = new Cara();
                caraBaseLateralDerecha.addPunto(new Punto(baseX2, baseY1, baseZ1));
                caraBaseLateralDerecha.addPunto(new Punto(baseX2, baseY2, baseZ1));
                caraBaseLateralDerecha.addPunto(new Punto(baseX2, baseY2, baseZ2));
                caraBaseLateralDerecha.addPunto(new Punto(baseX2, baseY1, baseZ2));
                caraBaseLateralDerecha.color = Color4.DarkGray; // Por ejemplo, puedes asignarle un color gris oscuro

                // Crear las partes de la base del soporte con el centro de masa ajustado
                Partes partesBase = new Partes();
                partesBase.centroDeMasa = new Punto(0,0,0); // Centro de masa de la base del soporte
                partesBase.AgregarCara("Frontal", caraBaseFrontal);
                partesBase.AgregarCara("Trasera", caraBaseTrasera);
                partesBase.AgregarCara("Superior", caraBaseSuperior);
                partesBase.AgregarCara("Inferior", caraBaseInferior);
                partesBase.AgregarCara("LateralIzquierda", caraBaseLateralIzquierda);
                partesBase.AgregarCara("LateralDerecha", caraBaseLateralDerecha);

               
                Objeto televisor = new Objeto();
                televisor.centroDeMasa = new Punto(0, 0, 0);
                televisor.AgregarPartes("pantalla", partesCubo1);
                televisor.AgregarPartes("barra", partesBarra);
                televisor.AgregarPartes("Soporte", partesSoporte);
                televisor.AgregarPartes("Base", partesBase);




                // Coordenadas del florero
                float floreroX1 = x1 /15; // Posición X del florero a la izquierda del cubo
                float floreroX2 = x2 /4; // Posición X del florero a la derecha del cubo
                float floreroY1 = y2  + 8 ; // Posición Y del florero arriba del cubo
                float floreroY2 = y2  ; // Altura del florero
                float floreroZ1 = (z1 + z2) / 2 -1; // Posición Z del florero en el centro entre las caras frontal y trasera del cubo
                float floreroZ2 = 3; // Profundidad del florero

                // Crear las caras del florero
                Cara caraFrontalFlorero = new Cara();
                caraFrontalFlorero.addPunto(new Punto(floreroX1 -1, floreroY1, floreroZ2));
                caraFrontalFlorero.addPunto(new Punto(floreroX1  , floreroY2, floreroZ2));
                caraFrontalFlorero.addPunto(new Punto(floreroX2  , floreroY2, floreroZ2));
                caraFrontalFlorero.addPunto(new Punto(floreroX2 +1 , floreroY1, floreroZ2));
                caraFrontalFlorero.color = Color4.LightCoral; 

                Cara caraTraseraFlorero = new Cara();
                caraTraseraFlorero.addPunto(new Punto(floreroX1 -1, floreroY1, floreroZ1));
                caraTraseraFlorero.addPunto(new Punto(floreroX1  , floreroY2, floreroZ1));
                caraTraseraFlorero.addPunto(new Punto(floreroX2  , floreroY2, floreroZ1));
                caraTraseraFlorero.addPunto(new Punto(floreroX2 +1, floreroY1, floreroZ1));
                caraTraseraFlorero.color = Color4.LightCoral; // Por ejemplo, puedes asignarle un color verde

                Cara caraSuperiorFlorero = new Cara();
                caraSuperiorFlorero.addPunto(new Punto(floreroX1, floreroY2, floreroZ1));
                caraSuperiorFlorero.addPunto(new Punto(floreroX1, floreroY2, floreroZ2));
                caraSuperiorFlorero.addPunto(new Punto(floreroX2, floreroY2, floreroZ2));
                caraSuperiorFlorero.addPunto(new Punto(floreroX2, floreroY2, floreroZ1));
                caraSuperiorFlorero.color = Color4.LightSlateGray; // Por ejemplo, puedes asignarle un color verde

                Cara caraInferiorFlorero = new Cara();
                caraInferiorFlorero.addPunto(new Punto(floreroX1 -1, floreroY1, floreroZ1));
                caraInferiorFlorero.addPunto(new Punto(floreroX1 -1, floreroY1, floreroZ2));
                caraInferiorFlorero.addPunto(new Punto(floreroX2 +1, floreroY1, floreroZ2));
                caraInferiorFlorero.addPunto(new Punto(floreroX2 +1, floreroY1, floreroZ1));
                caraInferiorFlorero.color = Color4.LightSlateGray; // Por ejemplo, puedes asignarle un color verde

                Cara caraLateralIzquierdaFlorero = new Cara();
                caraLateralIzquierdaFlorero.addPunto(new Punto(floreroX1-1, floreroY1, floreroZ1));
                caraLateralIzquierdaFlorero.addPunto(new Punto(floreroX1, floreroY2, floreroZ1));
                caraLateralIzquierdaFlorero.addPunto(new Punto(floreroX1, floreroY2, floreroZ2));
                caraLateralIzquierdaFlorero.addPunto(new Punto(floreroX1-1, floreroY1, floreroZ2));
                caraLateralIzquierdaFlorero.color = Color4.Lavender; // Por ejemplo, puedes asignarle un color verde

                Cara caraLateralDerechaFlorero = new Cara();
                caraLateralDerechaFlorero.addPunto(new Punto(floreroX2+1, floreroY1, floreroZ1));
                caraLateralDerechaFlorero.addPunto(new Punto(floreroX2, floreroY2, floreroZ1));
                caraLateralDerechaFlorero.addPunto(new Punto(floreroX2, floreroY2, floreroZ2));
                caraLateralDerechaFlorero.addPunto(new Punto(floreroX2+1, floreroY1, floreroZ2));
                caraLateralDerechaFlorero.color = Color4.Lavender; // Por ejemplo, puedes asignarle un color verde

                // Crear las partes del florero con el centro de masa ajustado
                Partes partesFlorero = new Partes();
                partesFlorero.centroDeMasa = new Punto(0,0,0); // Centro de masa del florero
                partesFlorero.AgregarCara("Frontal", caraFrontalFlorero);
                partesFlorero.AgregarCara("Trasera", caraTraseraFlorero);
                partesFlorero.AgregarCara("Superior", caraSuperiorFlorero);
                partesFlorero.AgregarCara("Inferior", caraInferiorFlorero);
                partesFlorero.AgregarCara("LateralIzquierda", caraLateralIzquierdaFlorero);
                partesFlorero.AgregarCara("LateralDerecha", caraLateralDerechaFlorero);


                // Coordenadas del tallo de la flor
                float talloX = (floreroX1 + floreroX2) / 2f; // Posición X del tallo en el centro del florero
                float talloY1 = floreroY1 +5; // Posición Y del tallo abajo del florero
                float talloY2 = floreroY2 + 8; // Posición Y del tallo arriba del florero
                float talloZ = floreroZ1 +1; // Posición Z del tallo en la parte frontal del florero

                // Crear las caras del tallo de la flor
                Cara caraTalloFrontal = new Cara();
                caraTalloFrontal.addPunto(new Punto(talloX - 0.2f, talloY1, talloZ + 0.2f));
                caraTalloFrontal.addPunto(new Punto(talloX - 0.2f, talloY2, talloZ + 0.2f));
                caraTalloFrontal.addPunto(new Punto(talloX + 0.2f, talloY2, talloZ + 0.2f));
                caraTalloFrontal.addPunto(new Punto(talloX + 0.2f, talloY1, talloZ + 0.2f));
                caraTalloFrontal.color = Color4.Green;

                Cara caraTalloTrasera = new Cara();
                caraTalloTrasera.addPunto(new Punto(talloX - 0.2f, talloY1, talloZ - 0.2f));
                caraTalloTrasera.addPunto(new Punto(talloX - 0.2f, talloY2, talloZ - 0.2f));
                caraTalloTrasera.addPunto(new Punto(talloX + 0.2f, talloY2, talloZ - 0.2f));
                caraTalloTrasera.addPunto(new Punto(talloX + 0.2f, talloY1, talloZ - 0.2f));
                caraTalloTrasera.color = Color4.Green;

                Cara caraTalloSuperior = new Cara();
                caraTalloSuperior.addPunto(new Punto(talloX - 0.2f, talloY2, talloZ - 0.2f));
                caraTalloSuperior.addPunto(new Punto(talloX - 0.2f, talloY2, talloZ + 0.2f));
                caraTalloSuperior.addPunto(new Punto(talloX + 0.2f, talloY2, talloZ + 0.2f));
                caraTalloSuperior.addPunto(new Punto(talloX + 0.2f, talloY2, talloZ - 0.2f));
                caraTalloSuperior.color = Color4.Green;

                Cara caraTalloInferior = new Cara();
                caraTalloInferior.addPunto(new Punto(talloX - 0.2f, talloY1, talloZ - 0.2f));
                caraTalloInferior.addPunto(new Punto(talloX - 0.2f, talloY1, talloZ + 0.2f));
                caraTalloInferior.addPunto(new Punto(talloX + 0.2f, talloY1, talloZ + 0.2f));
                caraTalloInferior.addPunto(new Punto(talloX + 0.2f, talloY1, talloZ - 0.2f));
                caraTalloInferior.color = Color4.Green;

                Cara caraTalloLateralIzquierdo = new Cara();
                caraTalloLateralIzquierdo.addPunto(new Punto(talloX - 0.2f, talloY1, talloZ - 0.2f));
                caraTalloLateralIzquierdo.addPunto(new Punto(talloX - 0.2f, talloY2, talloZ - 0.2f));
                caraTalloLateralIzquierdo.addPunto(new Punto(talloX - 0.2f, talloY2, talloZ + 0.2f));
                caraTalloLateralIzquierdo.addPunto(new Punto(talloX - 0.2f, talloY1, talloZ + 0.2f));
                caraTalloLateralIzquierdo.color = Color4.Green;

                Cara caraTalloLateralDerecho = new Cara();
                caraTalloLateralDerecho.addPunto(new Punto(talloX + 0.2f, talloY1, talloZ - 0.2f));
                caraTalloLateralDerecho.addPunto(new Punto(talloX + 0.2f, talloY2, talloZ - 0.2f));
                caraTalloLateralDerecho.addPunto(new Punto(talloX + 0.2f, talloY2, talloZ + 0.2f));
                caraTalloLateralDerecho.addPunto(new Punto(talloX + 0.2f, talloY1, talloZ + 0.2f));
                caraTalloLateralDerecho.color = Color4.Green;

                // Crear las partes del tallo con el centro de masa ajustado
                Partes partesTallo = new Partes();
                partesTallo.centroDeMasa = new Punto(0,0,0); // Centro de masa del tallo
                partesTallo.AgregarCara("Frontal", caraTalloFrontal);
                partesTallo.AgregarCara("Trasera", caraTalloTrasera);
                partesTallo.AgregarCara("Superior", caraTalloSuperior);
                partesTallo.AgregarCara("Inferior", caraTalloInferior);
                partesTallo.AgregarCara("LateralIzquierdo", caraTalloLateralIzquierdo);
                partesTallo.AgregarCara("LateralDerecho", caraTalloLateralDerecho);



                // Coordenadas del centro de la flor
                float centroFlorX = talloX; // Posición X del centro de la flor
                float centroFlorY = talloY2 + 5; // Posición Y del centro de la flor (arriba del florero)
                float centroFlorZ = talloZ; // Posición Z del centro de la flor (adelante del florero)

                // Crear las caras del centro de la flor
                Cara caraCentroFrontal = new Cara();
                caraCentroFrontal.addPunto(new Punto(centroFlorX - 0.5f, centroFlorY, centroFlorZ + 0.5f));
                caraCentroFrontal.addPunto(new Punto(centroFlorX - 0.5f, centroFlorY + 1f, centroFlorZ + 0.5f));
                caraCentroFrontal.addPunto(new Punto(centroFlorX + 0.5f, centroFlorY + 1f, centroFlorZ + 0.5f));
                caraCentroFrontal.addPunto(new Punto(centroFlorX + 0.5f, centroFlorY, centroFlorZ + 0.5f));
                caraCentroFrontal.color = Color4.Yellow; // Color amarillo para el centro frontal de la flor

                Cara caraCentroTrasera = new Cara();
                caraCentroTrasera.addPunto(new Punto(centroFlorX - 0.5f, centroFlorY, centroFlorZ - 0.5f));
                caraCentroTrasera.addPunto(new Punto(centroFlorX - 0.5f, centroFlorY + 1f, centroFlorZ - 0.5f));
                caraCentroTrasera.addPunto(new Punto(centroFlorX + 0.5f, centroFlorY + 1f, centroFlorZ - 0.5f));
                caraCentroTrasera.addPunto(new Punto(centroFlorX + 0.5f, centroFlorY, centroFlorZ - 0.5f));
                caraCentroTrasera.color = Color4.Yellow; // Color amarillo para el centro trasero de la flor

                Cara caraCentroSuperior = new Cara();
                caraCentroSuperior.addPunto(new Punto(centroFlorX - 0.5f, centroFlorY + 1f, centroFlorZ - 0.5f));
                caraCentroSuperior.addPunto(new Punto(centroFlorX - 0.5f, centroFlorY + 1f, centroFlorZ + 0.5f));
                caraCentroSuperior.addPunto(new Punto(centroFlorX + 0.5f, centroFlorY + 1f, centroFlorZ + 0.5f));
                caraCentroSuperior.addPunto(new Punto(centroFlorX + 0.5f, centroFlorY + 1f, centroFlorZ - 0.5f));
                caraCentroSuperior.color = Color4.Yellow; // Color amarillo para el centro superior de la flor

                Cara caraCentroInferior = new Cara();
                caraCentroInferior.addPunto(new Punto(centroFlorX - 0.5f, centroFlorY, centroFlorZ - 0.5f));
                caraCentroInferior.addPunto(new Punto(centroFlorX - 0.5f, centroFlorY, centroFlorZ + 0.5f));
                caraCentroInferior.addPunto(new Punto(centroFlorX + 0.5f, centroFlorY, centroFlorZ + 0.5f));
                caraCentroInferior.addPunto(new Punto(centroFlorX + 0.5f, centroFlorY, centroFlorZ - 0.5f));
                caraCentroInferior.color = Color4.Yellow; // Color amarillo para el centro inferior de la flor

                Cara caraCentroLateralIzquierdo = new Cara();
                caraCentroLateralIzquierdo.addPunto(new Punto(centroFlorX - 0.5f, centroFlorY, centroFlorZ - 0.5f));
                caraCentroLateralIzquierdo.addPunto(new Punto(centroFlorX - 0.5f, centroFlorY + 1f, centroFlorZ - 0.5f));
                caraCentroLateralIzquierdo.addPunto(new Punto(centroFlorX - 0.5f, centroFlorY + 1f, centroFlorZ + 0.5f));
                caraCentroLateralIzquierdo.addPunto(new Punto(centroFlorX - 0.5f, centroFlorY, centroFlorZ + 0.5f));
                caraCentroLateralIzquierdo.color = Color4.Yellow; // Color amarillo para el centro lateral izquierdo de la flor

                Cara caraCentroLateralDerecho = new Cara();
                caraCentroLateralDerecho.addPunto(new Punto(centroFlorX + 0.5f, centroFlorY, centroFlorZ - 0.5f));
                caraCentroLateralDerecho.addPunto(new Punto(centroFlorX + 0.5f, centroFlorY + 1f, centroFlorZ - 0.5f));
                caraCentroLateralDerecho.addPunto(new Punto(centroFlorX + 0.5f, centroFlorY + 1f, centroFlorZ + 0.5f));
                caraCentroLateralDerecho.addPunto(new Punto(centroFlorX + 0.5f, centroFlorY, centroFlorZ + 0.5f));
                caraCentroLateralDerecho.color = Color4.Yellow; // Color amarillo para el centro lateral derecho de la flor


                Partes parteCentroFlor = new Partes();
                parteCentroFlor.centroDeMasa = new Punto(0, 0, 0); // Centro de masa del tallo
  
                // Agregar cada cara del centro de la flor a la parte
                parteCentroFlor.AgregarCara("CentroFrontal", caraCentroFrontal);
                parteCentroFlor.AgregarCara("CentroTrasera", caraCentroTrasera);
                parteCentroFlor.AgregarCara("CentroSuperior", caraCentroSuperior);
                parteCentroFlor.AgregarCara("CentroInferior", caraCentroInferior);
                parteCentroFlor.AgregarCara("CentroLateralIzquierdo", caraCentroLateralIzquierdo);
                parteCentroFlor.AgregarCara("CentroLateralDerecho", caraCentroLateralDerecho);

                // Coordenadas de los puntos base para los pétalos
                float puntoBaseX = centroFlorX;
                float puntoBaseY = centroFlorY;
                float puntoBaseZ = centroFlorZ ;

                // Crear las caras de los pétalos
                Cara caraPetalo1 = new Cara();
                caraPetalo1.addPunto(new Punto(puntoBaseX - 1.5f, puntoBaseY + 0.5f, puntoBaseZ + 0.5f));
                caraPetalo1.addPunto(new Punto(puntoBaseX - 1.5f, puntoBaseY + 2f, puntoBaseZ + 0.5f));
                caraPetalo1.addPunto(new Punto(puntoBaseX - 0.5f, puntoBaseY + 2f, puntoBaseZ + 1.5f));
                caraPetalo1.addPunto(new Punto(puntoBaseX - 0.5f, puntoBaseY + 0.5f, puntoBaseZ + 1.5f));
                caraPetalo1.color = Color4.Red; // Color rojo para el pétalo 1

                Cara caraPetalo2 = new Cara();
                caraPetalo2.addPunto(new Punto(puntoBaseX + 1.5f, puntoBaseY + 0.5f, puntoBaseZ + 0.5f));
                caraPetalo2.addPunto(new Punto(puntoBaseX + 1.5f, puntoBaseY + 2f, puntoBaseZ + 0.5f));
                caraPetalo2.addPunto(new Punto(puntoBaseX + 0.5f, puntoBaseY + 2f, puntoBaseZ + 1.5f));
                caraPetalo2.addPunto(new Punto(puntoBaseX + 0.5f, puntoBaseY + 0.5f, puntoBaseZ + 1.5f));
                caraPetalo2.color = Color4.Red; // Color rojo para el pétalo 2

                Cara caraPetalo3 = new Cara();
                caraPetalo3.addPunto(new Punto(puntoBaseX + 0.5f, puntoBaseY + 0.5f, puntoBaseZ - 1.5f));
                caraPetalo3.addPunto(new Punto(puntoBaseX + 0.5f, puntoBaseY + 2f, puntoBaseZ - 1.5f));
                caraPetalo3.addPunto(new Punto(puntoBaseX + 1.5f, puntoBaseY + 2f, puntoBaseZ - 0.5f));
                caraPetalo3.addPunto(new Punto(puntoBaseX + 1.5f, puntoBaseY + 0.5f, puntoBaseZ - 0.5f));
                caraPetalo3.color = Color4.Red; // Color rojo para el pétalo 3

                Cara caraPetalo4 = new Cara();
                caraPetalo4.addPunto(new Punto(puntoBaseX - 0.5f, puntoBaseY + 0.5f, puntoBaseZ - 1.5f));
                caraPetalo4.addPunto(new Punto(puntoBaseX - 0.5f, puntoBaseY + 2f, puntoBaseZ - 1.5f));
                caraPetalo4.addPunto(new Punto(puntoBaseX - 1.5f, puntoBaseY + 2f, puntoBaseZ - 0.5f));
                caraPetalo4.addPunto(new Punto(puntoBaseX - 1.5f, puntoBaseY + 0.5f, puntoBaseZ - 0.5f));
                caraPetalo4.color = Color4.Red; // Color rojo para el pétalo 4

                // Crear la parte para los pétalos
                Partes partePetalos = new Partes();
                partePetalos.centroDeMasa = new Punto(0, 0, 0);

                partePetalos.AgregarCara("Petalo1", caraPetalo1);
                partePetalos.AgregarCara("Petalo2", caraPetalo2);
                partePetalos.AgregarCara("Petalo3", caraPetalo3);
                partePetalos.AgregarCara("Petalo4", caraPetalo4);

               




                Objeto florero = new Objeto();
                florero.centroDeMasa = new Punto(0, 0, 0);
                florero.AgregarPartes("floreroBase", partesFlorero);
                florero.AgregarPartes("CentroFlor", parteCentroFlor);
                florero.AgregarPartes("Tallo", partesTallo);
                florero.AgregarPartes("petalos", partePetalos);



                // Coordenadas de la base del parlante
                float baseParlanteX1 = baseX2 + 10; // Ajusta la posición X de acuerdo a la base del soporte
                float baseParlanteX2 = baseParlanteX1 + 4; // Ancho del parlante
                float baseParlanteY1 = baseY1 +15 ; // Misma posición Y que la base del soporte
                float baseParlanteY2 = baseY2 - 2; // Misma posición Y que la base del soporte
                float baseParlanteZ1 = baseZ1 +5 ; // Ajusta la posición Z de acuerdo a la base del soporte
                float baseParlanteZ2 = baseParlanteZ1 + 2 ; // Profundidad del parlante

                // Crear las caras de la base del parlante
                Cara caraBaseParlanteFrontal = new Cara();
                caraBaseParlanteFrontal.addPunto(new Punto(baseParlanteX1, baseParlanteY1, baseParlanteZ2));
                caraBaseParlanteFrontal.addPunto(new Punto(baseParlanteX1, baseParlanteY2, baseParlanteZ2));
                caraBaseParlanteFrontal.addPunto(new Punto(baseParlanteX2, baseParlanteY2, baseParlanteZ2));
                caraBaseParlanteFrontal.addPunto(new Punto(baseParlanteX2, baseParlanteY1, baseParlanteZ2));
                caraBaseParlanteFrontal.color = Color4.Black; // Color negro para la base frontal del parlante

                Cara bocina = new Cara();
                bocina.addPunto(new Punto(baseParlanteX1 + 1, baseParlanteY1 / 2 - 10, baseParlanteZ2 + 0.1f));
                bocina.addPunto(new Punto(baseParlanteX1 + 1, baseParlanteY2 / 2 - 10, baseParlanteZ2 + 0.1f));
                bocina.addPunto(new Punto(baseParlanteX2 - 1, baseParlanteY2 / 2 - 10, baseParlanteZ2 + 0.1f));
                bocina.addPunto(new Punto(baseParlanteX2 - 1, baseParlanteY1 / 2 - 10, baseParlanteZ2 + 0.1f));
                bocina.color = Color4.Gray; // Color negro para la base frontal del parlante

                Cara boton = new Cara();
                boton.addPunto(new Punto(baseParlanteX1 + 0.3f  , baseParlanteY1 /2 - 3, baseParlanteZ2 +0.1f));
                boton.addPunto(new Punto(baseParlanteX1 + 0.3f, baseParlanteY2 /2  -2, baseParlanteZ2 + 0.1f));
                boton.addPunto(new Punto(baseParlanteX2 - 0.3f, baseParlanteY2 /2  - 2, baseParlanteZ2 + 0.1f));
                boton.addPunto(new Punto(baseParlanteX2 - 0.3f, baseParlanteY1 /2 - 3 , baseParlanteZ2 + 0.1f));
                boton.color = Color4.Gray; 

                Cara caraBaseParlanteTrasera = new Cara();
                caraBaseParlanteTrasera.addPunto(new Punto(baseParlanteX1 , baseParlanteY1 -2, baseParlanteZ1));
                caraBaseParlanteTrasera.addPunto(new Punto(baseParlanteX1, baseParlanteY2, baseParlanteZ1));
                caraBaseParlanteTrasera.addPunto(new Punto(baseParlanteX2, baseParlanteY2, baseParlanteZ1));
                caraBaseParlanteTrasera.addPunto(new Punto(baseParlanteX2 , baseParlanteY1 -2, baseParlanteZ1));
                caraBaseParlanteTrasera.color = Color4.Red; // Color negro para la base trasera del parlante

                Cara caraBaseParlanteSuperior = new Cara();
                caraBaseParlanteSuperior.addPunto(new Punto(baseParlanteX1, baseParlanteY2 , baseParlanteZ1));
                caraBaseParlanteSuperior.addPunto(new Punto(baseParlanteX1, baseParlanteY2 , baseParlanteZ2));
                caraBaseParlanteSuperior.addPunto(new Punto(baseParlanteX2, baseParlanteY2 , baseParlanteZ2));
                caraBaseParlanteSuperior.addPunto(new Punto(baseParlanteX2, baseParlanteY2 , baseParlanteZ1));
                caraBaseParlanteSuperior.color = Color4.Red; // Color negro para la base superior del parlante

                Cara caraBaseParlanteInferior = new Cara();
                caraBaseParlanteInferior.addPunto(new Punto(baseParlanteX1, baseParlanteY1 -2, baseParlanteZ1));
                caraBaseParlanteInferior.addPunto(new Punto(baseParlanteX1, baseParlanteY1, baseParlanteZ2));
                caraBaseParlanteInferior.addPunto(new Punto(baseParlanteX2, baseParlanteY1, baseParlanteZ2));
                caraBaseParlanteInferior.addPunto(new Punto(baseParlanteX2, baseParlanteY1 -2, baseParlanteZ1));
                caraBaseParlanteInferior.color = Color4.Gray; // Color negro para la base inferior del parlante

                Cara caraBaseParlanteLateralIzquierda = new Cara();
                caraBaseParlanteLateralIzquierda.addPunto(new Punto(baseParlanteX1, baseParlanteY1-2, baseParlanteZ1));
                caraBaseParlanteLateralIzquierda.addPunto(new Punto(baseParlanteX1, baseParlanteY2, baseParlanteZ1));
                caraBaseParlanteLateralIzquierda.addPunto(new Punto(baseParlanteX1, baseParlanteY2, baseParlanteZ2));
                caraBaseParlanteLateralIzquierda.addPunto(new Punto(baseParlanteX1, baseParlanteY1, baseParlanteZ2));
                caraBaseParlanteLateralIzquierda.color = Color4.Black; // Color negro para la base lateral izquierda del parlante

                Cara caraBaseParlanteLateralDerecha = new Cara();
                caraBaseParlanteLateralDerecha.addPunto(new Punto(baseParlanteX2, baseParlanteY1-2, baseParlanteZ1));
                caraBaseParlanteLateralDerecha.addPunto(new Punto(baseParlanteX2, baseParlanteY2, baseParlanteZ1));
                caraBaseParlanteLateralDerecha.addPunto(new Punto(baseParlanteX2, baseParlanteY2, baseParlanteZ2));
                caraBaseParlanteLateralDerecha.addPunto(new Punto(baseParlanteX2, baseParlanteY1, baseParlanteZ2));
                caraBaseParlanteLateralDerecha.color = Color4.Black; // Color negro para la base lateral derecha del parlante

                // Crear la parte para la base del parlante
                Partes parteBaseParlante = new Partes();
                parteBaseParlante.centroDeMasa = new Punto(0, 0, 0); // Centro de masa de la base del parlante

                // Agregar cada cara de la base del parlante a la parte
                parteBaseParlante.AgregarCara("Frontal", caraBaseParlanteFrontal);
                parteBaseParlante.AgregarCara("Trasera", caraBaseParlanteTrasera);
                parteBaseParlante.AgregarCara("Superior", caraBaseParlanteSuperior);
                parteBaseParlante.AgregarCara("Inferior", caraBaseParlanteInferior);
                parteBaseParlante.AgregarCara("LateralIzquierda", caraBaseParlanteLateralIzquierda);
                parteBaseParlante.AgregarCara("LateralDerecha", caraBaseParlanteLateralDerecha);
                parteBaseParlante.AgregarCara("Bocina", bocina);
                parteBaseParlante.AgregarCara("Boton", boton);


                // Coordenadas de la base del parlante izquierdo
                float baseParlanteIzquierdoX1 = baseX1 - 10; // Ajusta la posición X de acuerdo a la base del soporte
                float baseParlanteIzquierdoX2 = baseParlanteIzquierdoX1 - 4; // Ancho del parlante izquierdo
                float baseParlanteIzquierdoY1 = baseY1 + 15; // Misma posición Y que la base del soporte
                float baseParlanteIzquierdoY2 = baseY2 - 2; // Misma posición Y que la base del soporte
                float baseParlanteIzquierdoZ1 = baseZ1 + 5; // Ajusta la posición Z de acuerdo a la base del soporte
                float baseParlanteIzquierdoZ2 = baseParlanteIzquierdoZ1 + 2; // Profundidad del parlante izquierdo

                // Crear las caras de la base del parlante izquierdo
                Cara caraBaseParlanteIzquierdoFrontal = new Cara();
                caraBaseParlanteIzquierdoFrontal.addPunto(new Punto(baseParlanteIzquierdoX1, baseParlanteIzquierdoY1, baseParlanteIzquierdoZ2));
                caraBaseParlanteIzquierdoFrontal.addPunto(new Punto(baseParlanteIzquierdoX1, baseParlanteIzquierdoY2, baseParlanteIzquierdoZ2));
                caraBaseParlanteIzquierdoFrontal.addPunto(new Punto(baseParlanteIzquierdoX2, baseParlanteIzquierdoY2, baseParlanteIzquierdoZ2));
                caraBaseParlanteIzquierdoFrontal.addPunto(new Punto(baseParlanteIzquierdoX2, baseParlanteIzquierdoY1, baseParlanteIzquierdoZ2));
                caraBaseParlanteIzquierdoFrontal.color = Color4.Black; // Color negro para la base frontal del parlante izquierdo

                Cara bocinaIzquierda = new Cara();
                bocinaIzquierda.addPunto(new Punto(baseParlanteIzquierdoX1 - 1, baseParlanteIzquierdoY1 / 2 - 10, baseParlanteIzquierdoZ2 + 0.1f));
                bocinaIzquierda.addPunto(new Punto(baseParlanteIzquierdoX1 - 1, baseParlanteIzquierdoY2 / 2 - 10, baseParlanteIzquierdoZ2 + 0.1f));
                bocinaIzquierda.addPunto(new Punto(baseParlanteIzquierdoX2 + 1, baseParlanteIzquierdoY2 / 2 - 10, baseParlanteIzquierdoZ2 + 0.1f));
                bocinaIzquierda.addPunto(new Punto(baseParlanteIzquierdoX2 + 1, baseParlanteIzquierdoY1 / 2 - 10, baseParlanteIzquierdoZ2 + 0.1f));
                bocinaIzquierda.color = Color4.Gray; // Color negro para la base frontal del parlante izquierdo

                Cara botonIzquierdo = new Cara();
                botonIzquierdo.addPunto(new Punto(baseParlanteIzquierdoX1 - 0.3f, baseParlanteIzquierdoY1 / 2 - 3, baseParlanteIzquierdoZ2 + 0.1f));
                botonIzquierdo.addPunto(new Punto(baseParlanteIzquierdoX1 - 0.3f, baseParlanteIzquierdoY2 / 2 - 2, baseParlanteIzquierdoZ2 + 0.1f));
                botonIzquierdo.addPunto(new Punto(baseParlanteIzquierdoX2 + 0.3f, baseParlanteIzquierdoY2 / 2 - 2, baseParlanteIzquierdoZ2 + 0.1f));
                botonIzquierdo.addPunto(new Punto(baseParlanteIzquierdoX2 + 0.3f, baseParlanteIzquierdoY1 / 2 - 3, baseParlanteIzquierdoZ2 + 0.1f));
                botonIzquierdo.color = Color4.Gray;

                Cara caraBaseParlanteIzquierdoTrasera = new Cara();
                caraBaseParlanteIzquierdoTrasera.addPunto(new Punto(baseParlanteIzquierdoX1, baseParlanteIzquierdoY1 - 2, baseParlanteIzquierdoZ1));
                caraBaseParlanteIzquierdoTrasera.addPunto(new Punto(baseParlanteIzquierdoX1, baseParlanteIzquierdoY2, baseParlanteIzquierdoZ1));
                caraBaseParlanteIzquierdoTrasera.addPunto(new Punto(baseParlanteIzquierdoX2, baseParlanteIzquierdoY2, baseParlanteIzquierdoZ1));
                caraBaseParlanteIzquierdoTrasera.addPunto(new Punto(baseParlanteIzquierdoX2, baseParlanteIzquierdoY1 - 2, baseParlanteIzquierdoZ1));
                caraBaseParlanteIzquierdoTrasera.color = Color4.Red; // Color rojo para la base trasera del parlante izquierdo

                Cara caraBaseParlanteIzquierdoSuperior = new Cara();
                caraBaseParlanteIzquierdoSuperior.addPunto(new Punto(baseParlanteIzquierdoX1, baseParlanteIzquierdoY2, baseParlanteIzquierdoZ1));
                caraBaseParlanteIzquierdoSuperior.addPunto(new Punto(baseParlanteIzquierdoX1, baseParlanteIzquierdoY2, baseParlanteIzquierdoZ2));
                caraBaseParlanteIzquierdoSuperior.addPunto(new Punto(baseParlanteIzquierdoX2, baseParlanteIzquierdoY2, baseParlanteIzquierdoZ2));
                caraBaseParlanteIzquierdoSuperior.addPunto(new Punto(baseParlanteIzquierdoX2, baseParlanteIzquierdoY2, baseParlanteIzquierdoZ1));
                caraBaseParlanteIzquierdoSuperior.color = Color4.Red; // Color rojo para la base superior del parlante izquierdo

                Cara caraBaseParlanteIzquierdoInferior = new Cara();
                caraBaseParlanteIzquierdoInferior.addPunto(new Punto(baseParlanteIzquierdoX1, baseParlanteIzquierdoY1 - 2, baseParlanteIzquierdoZ1));
                caraBaseParlanteIzquierdoInferior.addPunto(new Punto(baseParlanteIzquierdoX1, baseParlanteIzquierdoY1, baseParlanteIzquierdoZ2));
                caraBaseParlanteIzquierdoInferior.addPunto(new Punto(baseParlanteIzquierdoX2, baseParlanteIzquierdoY1, baseParlanteIzquierdoZ2));
                caraBaseParlanteIzquierdoInferior.addPunto(new Punto(baseParlanteIzquierdoX2, baseParlanteIzquierdoY1 - 2, baseParlanteIzquierdoZ1));
                caraBaseParlanteIzquierdoInferior.color = Color4.Gray; // Color negro para la base inferior del parlante izquierdo

                Cara caraBaseParlanteIzquierdoLateralIzquierda = new Cara();
                caraBaseParlanteIzquierdoLateralIzquierda.addPunto(new Punto(baseParlanteIzquierdoX1, baseParlanteIzquierdoY1 - 2, baseParlanteIzquierdoZ1));
                caraBaseParlanteIzquierdoLateralIzquierda.addPunto(new Punto(baseParlanteIzquierdoX1, baseParlanteIzquierdoY2, baseParlanteIzquierdoZ1));
                caraBaseParlanteIzquierdoLateralIzquierda.addPunto(new Punto(baseParlanteIzquierdoX1, baseParlanteIzquierdoY2, baseParlanteIzquierdoZ2));
                caraBaseParlanteIzquierdoLateralIzquierda.addPunto(new Punto(baseParlanteIzquierdoX1, baseParlanteIzquierdoY1, baseParlanteIzquierdoZ2));
                caraBaseParlanteIzquierdoLateralIzquierda.color = Color4.Black; // Color negro para la base lateral izquierda del parlante izquierdo

                Cara caraBaseParlanteIzquierdoLateralDerecha = new Cara();
                caraBaseParlanteIzquierdoLateralDerecha.addPunto(new Punto(baseParlanteIzquierdoX2, baseParlanteIzquierdoY1 - 2, baseParlanteIzquierdoZ1));
                caraBaseParlanteIzquierdoLateralDerecha.addPunto(new Punto(baseParlanteIzquierdoX2, baseParlanteIzquierdoY2, baseParlanteIzquierdoZ1));
                caraBaseParlanteIzquierdoLateralDerecha.addPunto(new Punto(baseParlanteIzquierdoX2, baseParlanteIzquierdoY2, baseParlanteIzquierdoZ2));
                caraBaseParlanteIzquierdoLateralDerecha.addPunto(new Punto(baseParlanteIzquierdoX2, baseParlanteIzquierdoY1, baseParlanteIzquierdoZ2));
                caraBaseParlanteIzquierdoLateralDerecha.color = Color4.Black; // Color negro para la base lateral derecha del parlante izquierdo

                // Crear la parte para la base del parlante izquierdo
                Partes parteBaseParlanteIzquierdo = new Partes();
                parteBaseParlanteIzquierdo.centroDeMasa = new Punto(0, 0, 0); // Centro de masa de la base del parlante izquierdo

                // Agregar cada cara de la base del parlante izquierdo a la parte
                parteBaseParlanteIzquierdo.AgregarCara("Frontal", caraBaseParlanteIzquierdoFrontal);
                parteBaseParlanteIzquierdo.AgregarCara("Trasera", caraBaseParlanteIzquierdoTrasera);
                parteBaseParlanteIzquierdo.AgregarCara("Superior", caraBaseParlanteIzquierdoSuperior);
                parteBaseParlanteIzquierdo.AgregarCara("Inferior", caraBaseParlanteIzquierdoInferior);
                parteBaseParlanteIzquierdo.AgregarCara("LateralIzquierda", caraBaseParlanteIzquierdoLateralIzquierda);
                parteBaseParlanteIzquierdo.AgregarCara("LateralDerecha", caraBaseParlanteIzquierdoLateralDerecha);
                parteBaseParlanteIzquierdo.AgregarCara("Bocina", bocinaIzquierda);
                parteBaseParlanteIzquierdo.AgregarCara("Boton", botonIzquierdo);


                Objeto parlante = new Objeto();
                parlante.centroDeMasa = new Punto(0, 0, 0);
                parlante.AgregarPartes("base", parteBaseParlante);
                parlante.AgregarPartes("izquierda", parteBaseParlanteIzquierdo);

                Objeto parlante1 = new Objeto();
                parlante1.centroDeMasa = new Punto(1, 0, 0);
                parlante1.AgregarPartes("base", parteBaseParlante);
                parlante1.AgregarPartes("izquierda", parteBaseParlanteIzquierdo);


                // Crear el escenario y agregar los objetos

                escenario.centroDeMasa = new Punto(0, 0, 0);
                escenario.AgregarObjeto("Televisor", televisor);
                escenario.AgregarObjeto("florero", florero);
                escenario.AgregarObjeto("Parlante", parlante);

                escenario2.centroDeMasa = new Punto(0, 0, 0);
                escenario2.AgregarObjeto("Televisor", televisor);
                escenario2.AgregarObjeto("florero", florero);
                escenario2.AgregarObjeto("Parlante", parlante1);



                //string json = JsonConvert.SerializeObject(escenario);
                // GuardarEscenario(@"D:\UAGRM\Grafica\escenario.json", escenario);
                // Console.WriteLine(json);

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


                
                escenario.DibujarEscenario();
                escenario2.DibujarEscenario();
               
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
