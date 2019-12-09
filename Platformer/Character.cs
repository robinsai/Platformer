using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Platformer
{
    
    class Character 
    {
        Dictionary<States, List<Frame>> Animations;
        #region Enums
        enum movement
        {
            StandStill,
            MoveLeft,
            MoveRight,
            Jump

        };
        #region States
        enum States
        {
            Moving,
            Jumping,
            Idle
        };
        States currentState;
        #endregion
        #endregion

        #region JumpLogic
        public bool jumping = false;
        public bool falling = false;
        float gravity = 0.1f;
      
        bool doOnce = false;
        public bool onPlatform = true;
      
        #endregion

#region Frames
        List<Frame> idleFrames;
        List<Frame> runningFrames;
        List<Frame> jumpingFrames;
        #endregion
        #region Properties


        float initialSpeed
        {
            get
            {
                return 0.05f;
            }
        }
        float CharSpeed = 0;
        
        float velocity { get; set; }
      

        float initialVelocity
        {
            get
            {
                return 3f;
            }
        }

       
        //Animations[currentState][currentFram].sourceRectangle

        //Draw() -> draws current frame

       public Vector2 Position { get; set; }


       
        public float Y
        {
            get
            {
                return Position.Y;
            }
            set
            {
                Position = new Vector2(Position.X, value);
            }
        }
        public float X
        {
            get
            {
                return Position.X;
            }
            set
            {
                Position = new Vector2(value, Position.Y);
            }


        }
        #endregion
        public Rectangle Hitbox
        {
            get
            {
                return Animations[currentState][FrameIndex].SourceRectangle;
            }
       
        }


        Texture2D Image { get;}
        TimeSpan updateTime = TimeSpan.FromMilliseconds(100);
        TimeSpan elapsedTime = TimeSpan.Zero;
        int FrameIndex { get; set; }

        SpriteEffects Effect = SpriteEffects.None;
        public Character(Texture2D image, Vector2 position, Color color) 
        {
            Animations = new Dictionary<States, List<Frame>>();
            Image = image;
            Position = position;
            Initialize();
        }
        void Initialize()
        {
            currentState = States.Idle;
            CreateFrames();
            Animations.Add(States.Moving, runningFrames);
            Animations.Add(States.Jumping, jumpingFrames);
            Animations.Add(States.Idle, idleFrames);
        }
        //UpdateFrame() -> changes current frame by update time
        void FrameIndexer(int amountOfFrames)
        {
            FrameIndex++;
            if (FrameIndex == amountOfFrames)
            {
                FrameIndex = 0;
            }

        }
       public void UpdateFrame(GameTime gameTime)
        {
            elapsedTime += gameTime.ElapsedGameTime;
            if(elapsedTime > updateTime)
            {
                elapsedTime = TimeSpan.Zero;
                FrameIndexer(Animations[currentState].Count);
            }
        }
        void CreateFrames()
        {
            float spriteSheetFrameGap = 79;
            float idleSpriteSheetOrigin = 0;
            float runningSpriteSheetOrigin = 0;
            float jumpingSpriteSheetOrigin = 0;
            idleFrames = new List<Frame>();
            runningFrames = new List<Frame>();
           
            jumpingFrames = new List<Frame>();
            for (int i = 0; i < 6; i++)
            {
                idleFrames.Add(new Frame(Vector2.Zero, new Rectangle((int)idleSpriteSheetOrigin, 567, 79, 63)));
                idleSpriteSheetOrigin += spriteSheetFrameGap;
            }
            for (int i = 0; i < 8; i++)
            {
                runningFrames.Add(new Frame(Vector2.Zero, new Rectangle((int)runningSpriteSheetOrigin, 693, 79, 63)));
                runningSpriteSheetOrigin += spriteSheetFrameGap;
            }
            for (int i = 0; i < 6; i++)
            {
                jumpingFrames.Add(new Frame(Vector2.Zero, new Rectangle((int)jumpingSpriteSheetOrigin, 630, 79, 63)));
                jumpingSpriteSheetOrigin += spriteSheetFrameGap;
            }
        }
        void Jumping()
        {
            if(!doOnce )
            {
                velocity = initialVelocity;
          
                doOnce = true;
            }
            Y -= velocity;
               
                        if(velocity>0)
                        {
                            velocity -=gravity;
                          //  position.Y -= velocity;
                        }
                        else
                        {
                            
                            falling = true;
                            velocity = initialVelocity;
                            jumping = false;
                        }
              
        }
        void Falling()
        {
            if (jumping == false)
            { 
                Y += velocity;
            }
            if(falling == false)
            {
                SwitchState(States.Idle);
            }

              
        }
           
              //SwitchState(State)
              //switches currentState , sets FrameIndex and elapsedTime = 0
           
          void SwitchStateProperties(States state)
        {
            currentState = state;
            FrameIndex = 0;
            elapsedTime = TimeSpan.Zero; 
        }
        void SwitchState(States state)
        {
            if(currentState != state)
            {
             if(state == States.Idle)
             {
                    CharSpeed = 0;
                    CharSpeed = 0;
             }
                SwitchStateProperties(state);
            }
        }
        public void characterMovement(KeyboardState ks, KeyboardState lastks)
        {


            //use if statements


            if (ks.IsKeyDown(Keys.Up) && !lastks.IsKeyDown(Keys.Up) && !jumping && !falling)
            {     //make sure they are floats
                  //y -= velocity
                  //velocity -= gravity
                  //initial velocity
                  //velocity = speed of char
                  //gravity = .1
                jumping = true;
                SwitchState(States.Jumping);
                //if(jumping)
                //{ 
                //    Jumping();
                //    Falling();
                //}
                
            }
            if (ks.IsKeyDown(Keys.Left))
            {
                Effect = SpriteEffects.FlipHorizontally;
                SwitchState(States.Moving);
                X -= CharSpeed;
                if (CharSpeed < 3)
                {
                    CharSpeed += initialSpeed;
                }
                else
                {
                   CharSpeed = 3;
                }
                X += -Math.Abs(CharSpeed);

            }
            else if (ks.IsKeyDown(Keys.Right))
            {
                Effect = SpriteEffects.None;
                SwitchState(States.Moving);

                X += CharSpeed;
                if (CharSpeed < 3)
                {
                   CharSpeed += initialSpeed;
                }
                else
                {
                    CharSpeed = 3;
                }
                X += CharSpeed;


            }
            else if (ks.IsKeyUp(Keys.Up) && ks.IsKeyUp(Keys.Left) && ks.IsKeyUp(Keys.Right))
            {
                SwitchState(States.Idle);
            
            }
            if (jumping )
            {

                Jumping();

            }
            if (falling)
            {
                Falling();
            }

        }

        public void Draw(SpriteBatch spriteBatch)
        {
            var currentFrame = Animations[currentState][FrameIndex];
            spriteBatch.Draw(Image, Position, currentFrame.SourceRectangle, Color.White, 0f,currentFrame.Origin, Vector2.One, Effect, 0f);
        }
        //Draw function that draws current frame of current animation
        //Animations[currentState][FrameIndex].SourceRectanlge

    }


}

    

