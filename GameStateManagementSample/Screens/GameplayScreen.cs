#region File Description
//-----------------------------------------------------------------------------
// GameplayScreen.cs
//
// Microsoft XNA Community Game Platform
// Copyright (C) Microsoft Corporation. All rights reserved.
//-----------------------------------------------------------------------------
#endregion

#region Using Statements
using System;
using System.Threading;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

#endregion

namespace GameStateManagement
{
    /// <summary>
    /// This screen implements the actual game logic. It is just a
    /// placeholder to get the idea across: you'll probably want to
    /// put some more interesting gameplay in here!
    /// </summary>
    class GameplayScreen : GameScreen
    {
        #region Fields

        ContentManager content;
        SpriteFont gameFont;

        Vector2 playerPosition = new Vector2(100, 100);
        Vector2 enemyPosition = new Vector2(100, 100);


        Random random = new Random();

        //////My stuff

        PlayerObject player1;

        EnemyObject[] enemies1;
        int max_enemies1;
        int spawnRate;
        int spawntimeCounter;

        Vector2 spawnPosition;

        SpriteFont GameOverFont;


        #endregion

        #region Initialization


        /// <summary>
        /// Constructor.
        /// </summary>
        public GameplayScreen()
        {
            TransitionOnTime = TimeSpan.FromSeconds(1.5);
            TransitionOffTime = TimeSpan.FromSeconds(0.5);
        }


        /// <summary>
        /// Load graphics content for the game.
        /// </summary>
        public override void LoadContent()
        {
            if (content == null)
                content = new ContentManager(ScreenManager.Game.Services, "Content");

            gameFont = content.Load<SpriteFont>("gamefont");
            GameOverFont = content.Load<SpriteFont>("GameOver");
            /////My stuff

            player1 = new PlayerObject();

            player1.texture = content.Load<Texture2D>("Sprites/semicircleV4");
            player1.position = new Vector2(0, 780);
            foreach (GameObject w in player1.weapon1)
            {

                w.texture = content.Load<Texture2D>("Sprites/w1V2");
            }

            max_enemies1 = 10;
            enemies1 = new EnemyObject[max_enemies1];
            for (int i = 0; i < max_enemies1; i++)
            {
                enemies1[i] = new EnemyObject();
                enemies1[i].texture = content.Load<Texture2D>("Sprites/enemy1V1");
                enemies1[i].speed = 1;

            }

            spawnRate = 10;
            spawntimeCounter = 0;
            spawnPosition = new Vector2(200, 400);



            // A real game would probably have more content than this sample, so
            // it would take longer to load. We simulate that by delaying for a
            // while, giving you a chance to admire the beautiful loading screen.
            Thread.Sleep(1000);

            // once the load has finished, we use ResetElapsedTime to tell the game's
            // timing mechanism that we have just finished a very long frame, and that
            // it should not try to catch up.
            ScreenManager.Game.ResetElapsedTime();
        }


        /// <summary>
        /// Unload graphics content used by the game.
        /// </summary>
        public override void UnloadContent()
        {
            content.Unload();
        }


        #endregion

        #region Update and Draw


        /// <summary>
        /// Updates the state of the game. This method checks the GameScreen.IsActive
        /// property, so the game will stop updating when the pause menu is active,
        /// or if you tab away to a different application.
        /// </summary>
        public override void Update(GameTime gameTime, bool otherScreenHasFocus,
                                                       bool coveredByOtherScreen)
        {
            base.Update(gameTime, otherScreenHasFocus, coveredByOtherScreen);

            if (IsActive)
            {
                //// Apply some random jitter to make the enemy move around.
                //const float randomization = 10;

                //enemyPosition.X += (float)(random.NextDouble() - 0.5) * randomization;
                //enemyPosition.Y += (float)(random.NextDouble() - 0.5) * randomization;

                //// Apply a stabilizing force to stop the enemy moving off the screen.
                //Vector2 targetPosition = new Vector2(
                //    ScreenManager.GraphicsDevice.Viewport.Width / 2 - gameFont.MeasureString("Insert Gameplay Here").X / 2, 
                //    200);

                //enemyPosition = Vector2.Lerp(enemyPosition, targetPosition, 0.05f);

                //// TODO: this game isn't very fun! You could probably improve
                //// it by inserting something more interesting in this space :-)


                /////My stuff here


                ///Fire weapins
                //if (gameTime.TotalGameTime.Milliseconds % 100 == 0) ////player1.fireRate == 0)
                //{
                //    player1.FireWeapon(1);


                //}

                player1.WtimeCount++;
                if (player1.WtimeCount == player1.fireRate)
                {
                    player1.FireWeapon(1);
                    player1.WtimeCount = 0;
                }

                ////Update Weapins
                foreach (GameObject w in player1.weapon1)
                {
                    if (w.isAlive)
                    {

                        w.UpdatePV();
                    }

                }


                ////Enemies

                SpawnEnemies();

                foreach (EnemyObject e in enemies1)
                {
                    if (e.isAlive)
                    {
                        e.UpdatePV();
                        e.TargetPlayer(player1.position);
                    }

                }



                HandleCollisions();

            }

        }


        /// <summary>
        /// Lets the game respond to player input. Unlike the Update method,
        /// this will only be called when the gameplay screen is active.
        /// </summary>
        public override void HandleInput(InputState input)
        {
            if (input == null)
                throw new ArgumentNullException("input");

            // Look up inputs for the active player profile.
            int playerIndex = (int)ControllingPlayer.Value;

            
            // if the user pressed the back button, we return to the main menu
            PlayerIndex player;
            if (input.IsNewButtonPress(Buttons.Back, ControllingPlayer, out player))
            {
                LoadingScreen.Load(ScreenManager, false, ControllingPlayer, new BackgroundScreen(), new MainMenuScreen());
            }
            else
            {
                // Otherwise move the player position.
                Vector2 movement = Vector2.Zero;

            
                if (movement.Length() > 1)
                    movement.Normalize();

                playerPosition += movement * 2;
            }




            ////My stuff
            var mouseState = Mouse.GetState();

            if (mouseState.LeftButton == ButtonState.Pressed)
            {
                
                
                ///MathHelper.Clamp(player1.position.Y, 0, 10f);

                if (mouseState.Y < 400)
                {
                    player1.isFlip = true;
                    player1.position.Y = 5;
                    player1.position.X = mouseState.X - player1.texture.Width / 2;

                }
                else
                {
                    player1.position.X = mouseState.X - player1.texture.Width / 2;
                    player1.position.Y = 770f;
                    player1.isFlip = false;


                }


            }

            





        }


        /// <summary>
        /// Draws the gameplay screen.
        /// </summary>
        public override void Draw(GameTime gameTime)
        {
            // This game has a blue background. Why? Because!
            ScreenManager.GraphicsDevice.Clear(ClearOptions.Target,
                                               Color.Black, 0, 0);

            // Our player and enemy are both actually just text strings.
            SpriteBatch spriteBatch = ScreenManager.SpriteBatch;

            spriteBatch.Begin();

            //spriteBatch.DrawString(gameFont, "// TODO", playerPosition, Color.Green);

            //spriteBatch.DrawString(gameFont, "Insert Gameplay Here",
            //                       enemyPosition, Color.DarkRed);


            /////My stuff

            

 
            foreach (GameObject w in player1.weapon1)
            {
                if (w.isAlive)
                {
                    spriteBatch.Draw(w.texture, w.position, Color.White);
                }
            }


            //enemies
            foreach (EnemyObject e in enemies1)
            {
                if (e.isAlive)
                {
                    spriteBatch.Draw(e.texture, e.position, Color.White);
                }
            }


            if (player1.isAlive)
            {
                player1.Draw(spriteBatch);
                spriteBatch.DrawString(gameFont, player1.score.ToString(), new Vector2(400, 0), Color.Green);
                
            }
            else
            {

                spriteBatch.DrawString(gameFont, player1.score.ToString(), new Vector2(400, 0), Color.Green);
                spriteBatch.DrawString(GameOverFont, "Game Over", new Vector2(100, 300), Color.Red);
                
                //Thread.Sleep(2000);
              //  LoadingScreen.Load(ScreenManager, false, ControllingPlayer, new BackgroundScreen(), new MainMenuScreen());
            }

            spriteBatch.End();

            // If the game is transitioning on or off, fade it out to black.
            if (TransitionPosition > 0)
                ScreenManager.FadeBackBufferToBlack(1f - TransitionAlpha);
        }
        #endregion


        private void SpawnEnemies()
        {
            spawntimeCounter++;
            if(spawntimeCounter == spawnRate )
            {
                spawntimeCounter = 0;

                foreach (EnemyObject e in enemies1)
                {
                    if (e.isAlive == false)
                    {

                        e.isAlive = true;
                        e.position = spawnPosition;
                        e.velocity = new Vector2((float)random.NextDouble(), (float)random.NextDouble());
                        e.TargetPlayer(player1.position);

                    }

                }

            }


        }


        private void HandleCollisions()
        {

            foreach (EnemyObject e in enemies1)
            {
                if(e.isAlive)
                {
                    ////Check vs weapons

                    foreach( GameObject w in player1.weapon1)
                    {
                        if(w.isAlive)
                        {
                            if(e.CheckCollision(w.position,w.texture.Width,w.texture.Height))
                            {
                                player1.score++;
                                e.isAlive = false;
                                w.isAlive = false;

                            }

                        }


                    }






                    ///Chewck vs player
                    ///
                    if(e.CheckCollision(player1.position,player1.texture.Width,player1.texture.Height))
                    {
                        e.isAlive = false;
                        player1.isAlive =false;
                    }




                }
            }



        }




    }
}
