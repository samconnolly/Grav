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
    class Planet
    {
        Vector2 origin = Vector2.Zero;

        Texture2D tex;
        public Vector2 position;
        public Vector2 velocity;
        public Vector2 acceleration;


        Microsoft.Xna.Framework.Rectangle rect = new Microsoft.Xna.Framework.Rectangle();

        public float mass;
        public float radius;


        public Planet(Texture2D texture, Vector2 startPosition, float planetMass, float planetRadius, Vector2 initialVelocity)
        {
            this.tex = texture;
            this.rect.Height = tex.Height;
            this.rect.Width = tex.Width;

            this.position = startPosition;
            this.mass = planetMass*100;
            this.radius = planetRadius;
            this.velocity = initialVelocity;

        }

        public void Update(List<Planet> planets)
        {
            // acceleration due to gravity
            acceleration = Vector2.Zero;

            foreach (Planet planet in planets)
            {
                if (planet != this)
                {
                    Vector2 thisAcceleration = (planet.position - position);

                    float magnitude = planet.mass / thisAcceleration.LengthSquared();

                    thisAcceleration.Normalize();

                    thisAcceleration = new Vector2(thisAcceleration.X * magnitude, thisAcceleration.Y * magnitude);

                    acceleration += thisAcceleration;
                }
                
            }
            
            // adjust velocity vector according to acceleration
            velocity += acceleration;
            
            // update position
            position += velocity;
        }


        public void Render(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(tex, position, rect, Color.White, 0, origin, 3.0f, SpriteEffects.None, 0.7f);
            
        }

    }
}
