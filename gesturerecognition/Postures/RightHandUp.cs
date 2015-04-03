using Leap;

namespace GestureRecognition.Postures
{
    class RightHandUp : Posture
    {
        public override bool TestPosture(Frame frame)
        {
            Hand rightHand = HandGetter.GetRightHand(frame);
            if (rightHand == null)
            {
                return false;
            }

            return rightHand.PalmPosition.y > 200;
        }
    }
}
