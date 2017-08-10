using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pong.Constants
{
    /*
     * const - can only be set at compile time
     * static readonly - can be set once in runtime
     */
    public static class SysConstants
    {
        //System 

        //How many seconds needs the ball to move from one player to the other one.
        public const float REACHTIME = 1.4f;
        public static readonly int FRAMERATE = 60;
    }
}
