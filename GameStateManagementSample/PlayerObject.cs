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
    class PlayerObject:GameObject
    {

        public int HP;


        public GameObject[] weapon1;
        public int max_ammo1;

        public int fireRate;
        public int Windex;

        public int WtimeCount;

        public int score;

        public PlayerObject()
        {
            isAlive = true;
            HP = 100;
            score = 0;
            max_ammo1 = 500;

            Windex = 1;

            fireRate = 10; ////500 shoots every half second
            weapon1 = new GameObject[max_ammo1];
            for (int i = 0; i < max_ammo1; i++)
            {
                weapon1[i] = new GameObject();
                weapon1[i].isAlive = false;
                weapon1[i].position = new Vector2(0, 0);
                weapon1[i].velocity = new Vector2(0, 0);
                weapon1[i].speed = 10f;
            }

            WtimeCount = 0;

        }



        public void FireWeapon(int index)
        {

            if (index == 1)
            {

                foreach (GameObject w in weapon1)
                {

                    if (w.isAlive == false)
                    {

                        w.isAlive = true;
                        w.position = position;
                        w.position.X +=  texture.Width/2 -w.texture.Width/2;
                        w.velocity = new Vector2(0, -1)*w.speed;
                        if (isFlip)
                        {
                            w.velocity = new Vector2(0, -1) * -w.speed;
                        }
                        else
                            w.velocity = new Vector2(0, -1) * w.speed;
                        break;
                    }

                }



            }


        }


    }
}
