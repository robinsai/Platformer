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
        
        enum movement
        {
            StandStill,
            MoveLeft,
            MoveRight,
            Jump

        };
        enum States
        {
            Moving,
            Jumping,
            Idle
        };  
        List<Frame> idleFrames;
        List<Frame> runningFrames;
        List<Frame> jumpingFrames;
        States currentState;
        float initialSpeed
        {
            get
            {
                return 0.05f;
            }
        }
        float CharSpeed = 0;
        
        float velocity
        {
            get
            {
                return velocity;
            }
            set
            {
                velocity = value;
            }
        }

        float initialVelocity
        {
            get
            {
                return 4f;
            }
        } 
        
      //Animations[currentState][currentFram].sourceRectangle
     
      //Draw() -> draws current frame
        public bool jumping = false;
        public bool falling = false;
        float gravity = 0.1f;
        public float initialPositionJumpPosition;
        bool doOnce = true;
        public bool onPlatform = true;
        int FrameIndex = 0;
        TimeSpan updateTime = TimeSpan.FromMilliseconds(100);
        TimeSpan elapsedTime = TimeSpan.Zero;
        Vector2 Position { get; set; }
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
        Texture2D Image { get; set; }
        public Character(Texture2D image, Vector2 position, Color color) 
        {
            Initialize();
        }
        void Initialize()
        {
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
        void UpdateFrame(GameTime gameTime)
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

            
               Y -= velocity;
               
                        if(velocity>0)
                        {
                            velocity -=gravity;
                          //  position.Y -= velocity;
                        }
                        else
                        {
                            
                            falling = true;
                            jumping = false;
                        }
              
        }
        void Falling()
        {
           
                Y += velocity;
                if(Y < initialPositionJumpPosition)
                {
                    velocity += gravity;
                }
                else 
                {
                    falling = false;
                
                }
        }
           
              
           
        
        public void characterMovement(KeyboardState ks)
        {


            //use if statements


            if (ks.IsKeyDown(Keys.Up))
            {     //make sure they are floats
                  //y -= velocity
                  //velocity -= gravity
                  //initial velocity
                  //velocity = speed of char
                  //gravity = .1
                jumping = true;
                if (doOnce)
                {
                    initialPositionJumpPosition = Y;
                    doOnce = false;
                }
                if(jumping)
                { 
                    Jumping();
                    Falling();
                }
                
            }
            if (ks.IsKeyDown(Keys.Left))
            {
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
            CharSpeed = 0;
               CharSpeed = 0;
            }
            if (jumping && !falling)
            {

                Jumping();

            }
            else if (!jumping && falling)
            {
                Falling();
            }

        }

    }


}

    

