using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Kinect;
using KinectTracking;


namespace poly_krisis
{
    public class Player{

        public Vector3 position; //current position of the player in the game world
        public Vector2 crosshair_position; //position of the crosshair on the screen
        public float rotation; //player's current rotation
        public int health;      //players health


        //Private Variables and such 
        private Texture2D crosshair; //player's aiming crosshair
        private Model model;    //player's 3D model
        private Kinect kinect; //kinect stuff

        //Constructor for the player class
        public Player(Model newmodel = null) {
            
            model = newmodel;
            position = new Vector3(0,0,0);
            crosshair_position = new Vector2(settings.Default.ScreenWidth/2,settings.Default.ScreenHeight/2);

            //Initailze kinect if needed
            if (settings.Default.EnableKinect) {
                kinect = new Kinect();
                kinect.initialize();
            }
        }

        //Loads content method 
        public void LoadContent(ContentManager Content) {
            crosshair = Content.Load<Texture2D>("2D_Art/crosshair"); //load the crosshair texture
          
        }

        //Unload content mehod
        public void UnloadContent() {

        }

        //Update mehtod
        public void Update(GameTime Gametime) { 

            //Do logic for player's controls and act accordinly
            if (settings.Default.EnableKinect) do_kinect_controls();
            else do_pc_controls();
        }

        //Draw mehtod
        public void Draw(SpriteBatch spritebatch) {
            
            spritebatch.Draw(crosshair,crosshair_position,Color.White);
        }

        ///////////////////////////////////////////////
        ///////// private helper functions ////////////
        ///////////////////////////////////////////////

        //Handle all logic for kinect controls
        private void do_kinect_controls() {

            //if there is no player on the screen
            if (kinect.player == null) return;

            //get and set the position of the crosshair
            Joint joint = kinect.player.Joints[JointType.HandRight];
            Vector2 jointPosition = new Vector2(joint.Position.X,joint.Position.Y);
            crosshair_position.X = Math.Abs((jointPosition.X *2)* (settings.Default.ScreenWidth) + settings.Default.ScreenWidth/2);
            crosshair_position.Y = Math.Abs((jointPosition.Y *2 )* (settings.Default.ScreenHeight) - settings.Default.ScreenHeight/2);
            Console.WriteLine(jointPosition.X + "," + jointPosition.Y + "||" + crosshair_position.X + "," + crosshair_position.Y);

            //figure out if the player is shooting or not

        }
        

        //handle all logic for pc controls
        private void do_pc_controls(){

            //get and set the position of the crosshair
            MouseState mousestate = Mouse.GetState();
            crosshair_position.X = mousestate.X;
            crosshair_position.Y = mousestate.Y;

            //

        }
    }
}
