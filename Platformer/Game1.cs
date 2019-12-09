 
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;

namespace Platformer
{
    /// <summary>
    /// This is the main type for your game.
    /// 12-8-2019 REMOVE INITIAL JUMP POSITION.
    /// </summary>
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        Character player;
        Platform platform;
        List<Platform> platforms;
        Vector2 platformPosition;
        Texture2D platformImage;
        Texture2D characterImage;
        Color platformTint;
      
        Vector2 characterPosition;
       
        
        Texture2D spritesheet;
        
        List<Frame> idleFrames;
        List<Frame> runningLeftFrames;
        List<Frame> runningRightFrames;
        List<Frame> jumpingFrames;
       
        float spriteSheetFrameGap = 79;
        float idleSpriteSheetOrigin = 0;
        int spriteIdleIndex = 0;
        float runningSpriteSheetOrigin = 0;
        float jumpingSpriteSheetOrigin = 0;
        KeyboardState ks;

       
     
        //create a list of frames for each state (Idle, Run, Jump)
        //new Frame(Vector2.Zero, new Rectangle());
        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
        
           platforms = new List<Platform>();
            platformImage = Content.Load<Texture2D>("Bricks3");
            platformTint = Color.White;
            platformPosition = new Vector2(0, GraphicsDevice.Viewport.Height - platformImage.Height );
            platform = new Platform(platformImage, platformPosition, platformTint);
            spritesheet = Content.Load<Texture2D>("spritesheet");
            
          
            for (int x = 0; x < 5; x++)
            {
                for (int i = 0; i < GraphicsDevice.Viewport.Width; i += platform.image.Width)
                {

                    platforms.Add(new Platform(platformImage, platformPosition, Color.White));
                    platformPosition.X += platformImage.Width;
                }
               
                    platformPosition.X = (platforms[x+1].position.X * 3);

                
                
                platformPosition.Y -= platformImage.Height;
            }

               
         


            characterImage = Content.Load<Texture2D>("spritesheet");
            characterPosition = new Vector2(platforms[0].position.X, platforms[0].position.Y-65);
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);
            player = new Character(characterImage, characterPosition, Color.White);
            // TODO: use this.Content to load your game content here6
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        KeyboardState lastKs;
        bool onPlatform = false;
        protected override void Update(GameTime gameTime)
        {
            ks = Keyboard.GetState();
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here
           
            player.characterMovement(ks,lastKs);
            player.UpdateFrame(gameTime);


            for(int i =0;i< platforms.Count;i++)
            {
                if(player.Hitbox.Intersects(platforms[i].hitbox) && player.falling == true )
                {
                    player.falling = false;
                    player.initialPositionJumpPosition = player.Position.Y;
                    onPlatform = true;
                }
              
            }
            if (player.falling && player.Y > GraphicsDevice.Viewport.Height - 100)
            {
                player.falling = false;
            }

            //if (!onPlatform)
            //{
            //    player.falling = true;
            //}
            //   for (int i = 0; i < platforms.Count; i++)
            //    {
            //        if (player.hitbox.Intersects(platforms[i].hitbox) )
            //        {
            //            player.falling = false;
            //            player.onPlatform = true;
            //        break;
            //        }
            //        else 
            //        {
            //            player.falling = true;
            //            player.onPlatform = false;
            //        }

            //    }
            //if(player.onPlatform)
            //{
            //    player.falling = false;
            //    player.jumping = false;
            //}

            lastKs = ks;
            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
           
            GraphicsDevice.Clear(Color.CornflowerBlue);
            spriteBatch.Begin();
            // TODO: Add your drawing code here
            player.Draw(spriteBatch);
            for(int i =0;i < platforms.Count;i++)
            {
                platforms[i].Draw(spriteBatch);
            }
          
            //spriteBatch.Draw(characterImage, player.position, idleFrames[spriteIdleIndex].SourceRectangle, Color.White, 0f, idleFrames[0].Origin, Vector2.One, SpriteEffects.None, 0f);
            spriteBatch.End();
            base.Draw(gameTime);
            
        }
    }
}
