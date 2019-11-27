using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Platformer
{
    class Frame
    {
       public Vector2 Origin;
       public Rectangle SourceRectangle; 

        public Frame(Vector2 origin, Rectangle source)
        {
            Origin = origin;
            SourceRectangle = source;
        }
    }
}
