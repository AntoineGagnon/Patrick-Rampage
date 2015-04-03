using Leap;

namespace GestureRecognition
{
    public abstract class Posture : BaseGesture
    {

        public abstract bool TestPosture(Frame frame);


        public override void TestGesture(Frame frame)
        {
            if (TestPosture(frame))
            {
                OnGestureRecognizedEventArgs(new GestureRecognizedEventArgs(GestureName));
            }
        }

    }
}
