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
    class Character : Sprite
    {
      enum movement
        {
            StandStill,
            MoveLeft,
            MoveRight,
            Jump

        };
        float initialLeftSpeed = 0.1f;
        float initialRightSpeed = 0.1f;
        float leftCharSpeed = 0;
        float rightCharSpeed = 0;
       
        public Character(Texture2D image, Vector2 position, Color color) : base(image, position, color)
        {
        }
       
        public void characterMovement(KeyboardState ks)
        {
            movement CurrentMovment;
            
            //use if statements
            if(ks.IsKeyDown(Keys.Up))
            {
                CurrentMovment = movement.Jump;
            }
            if(ks.IsKeyDown(Keys.Left))
            {
                CurrentMovment = movement.MoveLeft;
            }
            else if(ks.IsKeyDown(Keys.Right))
            {
                CurrentMovment = movement.MoveRight;
            }
            else if(ks.IsKeyUp(Keys.Up)&& ks.IsKeyUp(Keys.Left)&& ks.IsKeyUp(Keys.Right))
            {
                CurrentMovment = movement.StandStill;
            }
            CurrentMovment = movement.StandStill;
            switch(CurrentMovment)
            {
                case movement.StandStill:
                    leftCharSpeed = 0;
                    rightCharSpeed = 0;

                    break;
                case movement.MoveLeft:
                    if(leftCharSpeed < 3)
                    {
                        leftCharSpeed += initialLeftSpeed;
                    }
                  else
                    {
                        leftCharSpeed = 3;
                    }
                    position.X += leftCharSpeed;
                    break;
                case movement.MoveRight:
                    if(rightCharSpeed < 3)
                    {
                        leftCharSpeed += initialRightSpeed;
                    }
                    else
                    {
                        rightCharSpeed = 3;
                    }
                    position.X += -Math.Abs(rightCharSpeed);
                    break;
                case movement.Jump:
                    //make sure they are floats
                    //y -= velocity
                    //velocity -= gravity
                    //initial velocity
                    //velocity = speed of char
                    //gravity = .1
                    break;
                default:
                    CurrentMovment = movement.StandStill;
                   
                    break;
            }
        }
        

        }

    }
}
