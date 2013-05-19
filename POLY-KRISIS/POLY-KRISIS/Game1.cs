using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace poly_krisis
{
    /// This is the main type for your game
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        DollyCam camera;

        private Matrix world, world_rotated;

        public Game1(){
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";


            //Set some basic stuff for my game
            graphics.IsFullScreen = false; //set it to full screen no
            graphics.PreferredBackBufferWidth = settings.Default.ScreenWidth;//set the screen dimension width
            graphics.PreferredBackBufferHeight = settings.Default.ScreenHeight; //set the screen dimension height
            this.Window.Title = "POLY-KRISIS"; //set window title
        }

        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        protected override void Initialize(){
            world = Matrix.CreateTranslation(0f, 0f, 0f);
			CameraCue camCue = new CameraCue(new Vector3(0, 1, 50), new Vector3(0, 0, -1));
			Matrix proj = Matrix.CreatePerspectiveFieldOfView(MathHelper.ToRadians(45),
				(float)settings.Default.ScreenWidth / (float)settings.Default.ScreenHeight,
				0.1f, 100.0f);

			camera = new DollyCam(camCue, proj);

			//Set a location to move cam to
			camCue = new CameraCue(new Vector3(0, 20, 0), new Vector3(0, 0, 0));
			camera.TransitionTo(camCue, 0.5f);

			world *= Matrix.CreateRotationX(-(float)Math.PI / 2.0f) * Matrix.CreateScale(new Vector3(0.5f, 0.5f, 0.5f));
            
            base.Initialize();
        }

        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
        }

        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        protected override void Update(GameTime gameTime)
        {
            // Allows the game to exit
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed) this.Exit();

            // TODO: Add your update logic here
			camera.Update(gameTime);

            base.Update(gameTime);
        }

        /// This is called when the game should draw itself.
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            //Draw my test cube
			//DrawModel(Content.Load<Model>("Models/Cube/cube"), world_rotated);
			DrawModel(Content.Load<Model>("Models/Level/level1"), world);
            

            base.Draw(gameTime);
        }

        //////////////////////////////////////////////////////////
        /////////////// Private Helper Functions /////////////////
        //////////////////////////////////////////////////////////

        //Draw the model
        private void DrawModel(Model model, Matrix world){
            foreach (ModelMesh mesh in model.Meshes){
                foreach (BasicEffect effect in mesh.Effects){
                    
                    //Lighting stuffs
					//effect.EnableDefaultLighting();
					//effect.LightingEnabled = true; // Turn on the lighting subsystem.
					//effect.DirectionalLight0.DiffuseColor = new Vector3(1f, 0.2f, 0.2f); // a reddish light
					//effect.DirectionalLight0.Direction = new Vector3(1, 0, 0);  // coming along the x-axis
					//effect.DirectionalLight0.SpecularColor = new Vector3(0, 1, 0); // with green highlights
					//effect.AmbientLightColor = new Vector3(0.2f, 0.2f, 0.2f); // Add some overall ambient light.
					//effect.EmissiveColor = new Vector3(1, 0, 0); // Sets some strange emmissive lighting.  This just looks weird.

                    //Fog stuffs
                    /*effect.FogEnabled = true;
                    effect.FogColor = Color.CornflowerBlue.ToVector3(); // For best results, ake this color whatever your background is.
                    effect.FogStart = 9.75f;
                    effect.FogEnd = 10.25f;*/

                    //Shows the model
                    effect.World = world;
                    effect.View = camera.View;
                    effect.Projection = camera.Projection;
                }
                mesh.Draw();
            }
        }
    }
}
