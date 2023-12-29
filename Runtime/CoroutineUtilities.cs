using System.Collections;
using UnityEngine;

namespace Peg.Systems
{
    /// <summary>
    /// Note: This uses a really bad method of summing time and does not accurately reflect error.
    /// 
    /// TODO: Fixing this with proper error correction!!!
    /// </summary>
    public static class CoroutineUtilities
    {
        public static IEnumerator WaitForRealTime(double delay)
        {
            while (true)
            {
                double pauseEndTime = Time.realtimeSinceStartupAsDouble + delay; //BUG! FIXME!
                while (Time.realtimeSinceStartupAsDouble < pauseEndTime)
                    yield return 0;
                break;
            }
        }

        public static IEnumerator WaitForTime(double delay)
        {
            while (true)
            {
                double pauseEndTime = Time.timeSinceLevelLoadAsDouble + delay; //BUG! FIXME!
                while (Time.timeSinceLevelLoadAsDouble < pauseEndTime)
                    yield return 0;
                break;
            }
        }
    }
}
