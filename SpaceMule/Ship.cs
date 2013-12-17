using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace SpaceMule
{
    class Ship
    {
        Texture2D tex;

        Vector2 position;
        Vector2 endPosition;
        Vector2 velocity = Vector2.Zero;
        

        Vector2 origin = Vector2.Zero;

        List<Planet> planets;

        Vector2 engineAcceleration;
        Vector2 retroAcceleration;
        Vector2 gravityAcceleration;

        public float angMom = 0;
        public float angle = 0;
        bool hit = false;

        int nAnims = 2;
        int anim = 0;

        Rectangle rect;

        public Ship(Texture2D texture, Vector2 startPosition, List<Planet> planetList)
        {
            this.position = startPosition;

            this.tex = texture;
            this.rect.Height = tex.Height/nAnims;
            this.rect.Width  = tex.Width;
            this.rect.X = 0;
            this.rect.Y = 0;

            this.planets = planetList;
        }

        public Vector2 RotationPositionAdjust(float angle, Vector2 position)
        {
           Vector2 endPosition = position + new Vector2(-(rect.Width / 2) * (float)Math.Cos((double)angle) + (rect.Height / 2) * (float)Math.Sin((double)angle),
                                                    -(rect.Width / 2) * (float)Math.Sin((double)angle) - (rect.Height / 2) * (float)Math.Cos((double)angle));

           return endPosition;
        }

        public void Update(GameTime gameTime)
        {
            //----- control the ship! ----------------------

            KeyboardState kstate = Keyboard.GetState(); // get keyboard input
            
            // turn CCW
            if (kstate.IsKeyDown(Microsoft.Xna.Framework.Input.Keys.Left))
            {
                angMom -= 0.001f;
            }

            // turn CW
            else if (kstate.IsKeyDown(Microsoft.Xna.Framework.Input.Keys.Right))
            {
                angMom += 0.001f;
            }                     

            // accelerate in direction of engine
            if (kstate.IsKeyDown(Microsoft.Xna.Framework.Input.Keys.Up))
            {
                engineAcceleration = new Vector2((float)Math.Sin((double)angle) * 0.1f, - (float)Math.Cos((double)angle) * 0.1f);
                anim = 1;
            }

            else
            {
                engineAcceleration = Vector2.Zero;
                retroAcceleration = Vector2.Zero;
                anim = 0;
            }

            // deccelerate in direction of retros
            if (kstate.IsKeyDown(Microsoft.Xna.Framework.Input.Keys.Down))
            {
                retroAcceleration = - new Vector2((float)Math.Sin((double)angle) * 0.01f, -(float)Math.Cos((double)angle) * 0.01f);
                anim = 0;
            }

            

            // rotation due to angular momentum
            angle += angMom;

            // acceleration due to gravity
            gravityAcceleration = Vector2.Zero;

            foreach (Planet planet in planets)
            {
                Vector2 acceleration = (planet.position - position);

                float magnitude = planet.mass / acceleration.LengthSquared();

                if (acceleration.Length() < planet.radius)
                {
                    hit = true;
                }
                else
                {
                    hit = false;
                }

                acceleration.Normalize();

                acceleration = new Vector2(acceleration.X * magnitude, acceleration.Y * magnitude);

                gravityAcceleration += acceleration;


                
            }

            if (hit == true)
            {
                //velocity = Vector2.Zero;
                gravityAcceleration = Vector2.Zero;   
            }

            // adjust velocity vector according to acceleration
            velocity += engineAcceleration;
            velocity += retroAcceleration;
            velocity += gravityAcceleration;

            

            // update position
            position += velocity;

            // account for rotation
            endPosition = RotationPositionAdjust(angle, position);

            // update animation
            rect.Y = rect.Height * anim;

        }

        public void Render(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(tex,endPosition,rect,Color.White,angle,origin,1.0f,SpriteEffects.None,0.5f);
        }
    }
}
