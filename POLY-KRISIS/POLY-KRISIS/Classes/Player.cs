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

        //Private Variables and such 
        private Model model;    //player's 3D model
        private Kinect kinect; //kinect stuff

        //Constructor for the player class
        public Player(Model newmodel) {

            model = newmodel;

            //Initailze kinect if needed
            if (settings.Default.EnableKinect) {
                kinect = new Kinect();
                kinect.initialize();
            }
        }

        //Loads content method 
        public void LoadContent(ContentManager Content) {
            
            
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

        }

        ///////////////////////////////////////////////
        ///////// private helper functions ////////////
        ///////////////////////////////////////////////

        //Handle all logic for kinect controls
        private void do_kinect_controls() {

            //if there is no player on the screen
            if (kinect.player == null) return;

            
        }


        //handle all logic for pc controls
        private void do_pc_controls(){

        }
    }
}
