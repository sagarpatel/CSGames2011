using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Input.Touch;
using Microsoft.Xna.Framework.Media;

namespace GameStateManagement
{
    public class GameObject
    {

        public bool isAlive;

        public Texture2D texture;
        public Vector2 position;
        public Vector2 velocity;

        public float friction;
        public float speed;

        public bool isFlip;

        public GameObject()
        {
            isAlive = false;
            position = new Vector2(0, 0);
            velocity = new Vector2(0, 0);
            speed = 1;
            friction = 0;
            isFlip = false;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            
            if(isFlip)
            {
                spriteBatch.Draw(texture, position,null, Color.White, 0, new Vector2(0, 0),1f, SpriteEffects.FlipVertically, 0);
                
            }
            else
                spriteBatch.Draw(texture, position, Color.White);
        }

        public void UpdatePV()
        {
            position += velocity;

        }

        public bool CheckCollision(Vector2 otherpos, int W, int H)
        {

            Rectangle myRec = new Rectangle((int)position.X,(int)position.Y, texture.Width, texture.Height);
            Rectangle otherRec = new Rectangle((int)otherpos.X, (int)otherpos.Y, W, H);
            
            if (myRec.Intersects(otherRec))
                return true;
            else
                return false;


        }


    }
}
