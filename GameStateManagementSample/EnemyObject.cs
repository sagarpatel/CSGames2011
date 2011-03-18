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
    class EnemyObject:GameObject
    {

        public Vector2 targetDirection;
        public float targetSpeed;

        public EnemyObject()
        {

            targetDirection = new Vector2(0, 0);
            targetSpeed = 0.05f;
        }


        public void TargetPlayer(Vector2 playerPosition)
        {

            targetDirection = (playerPosition - position);
            targetDirection.Normalize();
            velocity += targetDirection * targetSpeed;
            
        }



    }
}
