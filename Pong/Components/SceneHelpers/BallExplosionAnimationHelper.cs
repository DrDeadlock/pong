using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace Pong.Components.SceneHelpers
{
    public static class BallExplosionAnimationHelper
    {
        private static List<Vector2> OldPoints;
        //TODO: Find better name for scaleFactor
        public static int ScaleFactor = 9;

        public static List<Vector2> GetRandomExplosionPositions(int timeStep)
        {
            if (OldPoints == null)
                OldPoints = new List<Vector2>();

            if (timeStep == 1)
                OldPoints = new List<Vector2>();

            //Initial point List is scaled from 0 to 10*(timeStep+1)
            int pointCount = 20 + 10 * timeStep;
            var resultPoints = new List<Vector2>();
            var randomizer = new Random();
            
            //Get random points in natural number scale
            for (int i = 0; i < pointCount; i++)
            {
                resultPoints.Add(new Vector2(randomizer.Next(ScaleFactor),randomizer.Next(ScaleFactor)));
            }

            //add random directions to old points
            for (int i = 0; i < OldPoints.Count - 1; i++)
            {
                OldPoints[i] = new Vector2(OldPoints[i].X + timeStep*randomizer.Next(ScaleFactor), OldPoints[i].Y + timeStep*randomizer.Next(ScaleFactor));
                resultPoints.Add(OldPoints[i]);
            }

            
            return resultPoints;
        }
    }
}
