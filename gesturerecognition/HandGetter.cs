using System.Linq;
using Leap;

namespace GestureRecognition
{
    public static class HandGetter
    {

        public static Hand GetRightHand(Frame frame)
        {
            return frame.Hands.FirstOrDefault(hand => hand.IsRight);
        }

        public static Hand GetLeftHand(Frame frame)
        {
            return frame.Hands.FirstOrDefault(hand => hand.IsLeft);
        }
    }
}
