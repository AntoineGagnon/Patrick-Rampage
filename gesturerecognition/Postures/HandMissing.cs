using Leap;

namespace GestureRecognition.Postures
{
    class HandMissing : Posture
    {
        public override bool TestPosture(Frame frame)
        {
            return (HandGetter.GetRightHand(frame) == null || HandGetter.GetLeftHand(frame) == null);
        }
    }
}
