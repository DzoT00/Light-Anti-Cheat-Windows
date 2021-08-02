using System;

namespace LightAnti_CheatWindows
{
    public class SpeedHackDetector //Does not work
    {
        public static int timeDiff = 0;
        private static int previousTime = 0;
        private static int realTime = 0;
        private static float gameTime = 0;
        public static bool detected = false;

        public static void Initialize()
        {
            previousTime = DateTime.Now.Second;
            gameTime = 1;
        }

        public static void UpdateDetection()
        {
            if (previousTime != DateTime.Now.Second)
            {
                realTime++;
                previousTime = DateTime.Now.Second;

                timeDiff = (int)gameTime - realTime;
                if (timeDiff > 7)
                {
                    if (!detected)
                    {
                        detected = true;
                        SpeedhackDetected();
                    }
                }
                else
                {
                    detected = false;
                }
            }
            gameTime += Program.deltaTime;
        }

        private static void SpeedhackDetected()
        {
            //Speedhack was detected, do something here (kick player from the game etc.)
            Console.WriteLine("Speedhack detected.");
        }
    }
}
