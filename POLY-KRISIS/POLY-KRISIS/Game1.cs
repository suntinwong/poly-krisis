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
        Player player;
		Model level;

        private Matrix world;

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

			CameraCue camCue = new CameraCue(new Vector3(0, 2, 23), new Vector3(0, 0, -1), 0);
			Matrix proj = Matrix.CreatePerspectiveFieldOfView(MathHelper.ToRadians(45),
				(float)settings.Default.ScreenWidth / (float)settings.Default.ScreenHeight,
				0.1f, 100.0f);

			camera = new DollyCam(camCue, proj);
            BuildCamPath();
            player = new Player();

			world = Matrix.CreateTranslation(0f, 0f, 0f);
			world *= Matrix.CreateScale(new Vector3(0.8f, 0.8f, 0.5f)) * Matrix.CreateRotationX(-(float)Math.PI / 2.0f);

			level = Content.Load<Model>("Models/Level/level1");

            base.Initialize();
        }

        //Build up the camera cue path
		private void BuildCamPath() {
 			camera.AddCue(new CameraCue(new Vector3(0, 2, 15), new Vector3(0, 0, -1), 3));
 			camera.AddCue(new CameraCue(new Vector3(0, 2, 9), new Vector3(-1, 0, 0), 3, 5000));
 			camera.AddCue(new CameraCue(new Vector3(-5, 2, 9), new Vector3(-1, 0, 0), 3, 1000));
 			camera.AddCue(new CameraCue(new Vector3(2, 2, 9), new Vector3(0, 0, 1), 4, 0));
 			camera.AddCue(new CameraCue(new Vector3(5, 2, 9), new Vector3(1, 0, 0), 4, 0));
 			camera.AddCue(new CameraCue(new Vector3(10, 2, 9), new Vector3(1, 0, 0), 3, 2000));
 			camera.AddCue(new CameraCue(new Vector3(10, 2, 9), -Vector3.UnitZ, 4));
 			camera.AddCue(new CameraCue(new Vector3(12, 2, 9), -Vector3.UnitZ, 3));
 		}

        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);
            player.LoadContent(Content);

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

			camera.Update(gameTime);
			player.Update(gameTime);

            base.Update(gameTime);
        }

        /// This is called when the game should draw itself.
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            //Before you draw anything in 3D you will probably want to reset these states:
            GraphicsDevice.BlendState = BlendState.Opaque;
            GraphicsDevice.DepthStencilState = DepthStencilState.Default;

            //Draw my test cube
			DrawModel(level, world);

            //draw everyting in 2d now
            spriteBatch.Begin();
			player.Draw(spriteBatch);
            spriteBatch.End();

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
