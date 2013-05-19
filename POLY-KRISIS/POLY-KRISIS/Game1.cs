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

        private Vector3 cameraPosition, cameraTarget;
        private float fovAngle, aspectRatio, near, far;
        private Matrix world, view, projection;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        protected override void Initialize(){

            //Initialize 3d camera stuffs
            cameraPosition = new Vector3(0.0f, 0.0f, 10.0f);
            cameraTarget = new Vector3(0.0f, 0.0f, 0.0f);       // Look back at the origin
            fovAngle = MathHelper.ToRadians(45);                // convert 45 degrees to radians
            aspectRatio = 800f / 480f; // graphics.PreferredBackBufferWidth / graphics.PreferredBackBufferHeight;
            near = 0.1f;                                       // the near clipping plane distance
            far = 100f;                                          // the far clipping plane distance
            world = Matrix.CreateTranslation(0f, 0f, 0f);
            view = Matrix.CreateLookAt(cameraPosition, cameraTarget, Vector3.UnitY);
            projection = Matrix.CreatePerspectiveFieldOfView(fovAngle, aspectRatio, near, far);
            
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
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();

            // TODO: Add your update logic here

            base.Update(gameTime);
        }

        /// This is called when the game should draw itself.
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            DrawModel(Content.Load<Model>("Models/Cube/cube"), world, view, projection);
            

            base.Draw(gameTime);
        }

        //////////////////////////////////////////////////////////
        /////////////// Private Helper Functions /////////////////
        //////////////////////////////////////////////////////////

        //Draw the model
        private void DrawModel(Model model, Matrix world, Matrix view, Matrix projection){
            foreach (ModelMesh mesh in model.Meshes){
                foreach (BasicEffect effect in mesh.Effects){
                    effect.World = world;
                    effect.View = view;
                    effect.Projection = projection;
                }
                mesh.Draw();
            }
        }



    }
}
