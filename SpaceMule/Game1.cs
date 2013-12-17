using System;
using System.Collections.Generic;
using System.Linq;
using System.Drawing;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace SpaceMule
{
 
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        
        SpriteBatch spriteBatch;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            
            Content.RootDirectory = "Content";

            graphics.PreferredBackBufferHeight = 1080;
            graphics.PreferredBackBufferWidth = 1920;
        }

        protected override void Initialize()
        {
           

            base.Initialize();
        }

        SpriteFont font;

        Planet planet;
        Planet planet2;

        List<Planet> planets;

        Ship ship;
        
      
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            

            // fonts
            font = Content.Load<SpriteFont>("font");

            // planets
            planet = new Planet(Content.Load<Texture2D>("gball"), new Vector2(960, 540), 10, 25, new Vector2(0,1));
            planet2 = new Planet(Content.Load<Texture2D>("rball"), new Vector2(560, 540), 10, 25, new Vector2(0,-1));

            // list of plaents
            planets = new List<Planet> { planet,planet2 };

            // spaceship

            ship = new Ship(Content.Load<Texture2D>("spaceship"), new Vector2(200, 200), planets);
           
        }

    
        protected override void UnloadContent()
        {
           
        }

      
        protected override void Update(GameTime gameTime)
        {
            // check for exit key press
            KeyboardState kstate = Keyboard.GetState();

            if (kstate.IsKeyDown(Microsoft.Xna.Framework.Input.Keys.Escape))
            {
                this.Exit();
            }
           
            // update ship

            ship.Update(gameTime);

            foreach (Planet p in planets)
            {
                p.Update(planets);
            }

            base.Update(gameTime);
        }

     
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Microsoft.Xna.Framework.Color.Black);

            spriteBatch.Begin(SpriteSortMode.BackToFront, BlendState.AlphaBlend);

            foreach (Planet p in planets)
            {
                p.Render(spriteBatch);
            }
            
            ship.Render(spriteBatch);


            //spriteBatch.DrawString(font, ship.angle.ToString(), new Vector2(300, 10), Color.White);

            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
