using Leap;

namespace GestureRecognition
{
    public abstract class Gesture : BaseGesture
    {

        protected int CurrentNbOfFrames
        {
            get;
            set;
        }

        protected int MinNbOfFrame
        {
            get { return 10; }
        }

        protected int MaxNbOfFrame
        {
            get { return 60; }
        }

        public bool IsRunning;

        protected abstract bool TestInitialConditions(Frame frame);

        protected abstract bool TestEndingConditions(Frame frame);

        protected abstract bool TestPosture(Frame frame);

        protected abstract bool TestDynamicGesture(Frame frame);

        public override void TestGesture(Frame frame)
        {
            if (!IsRunning)
            {
                if (TestInitialConditions(frame))
                {
                    IsRunning = true;
                    CurrentNbOfFrames = 0;
                }
            }
            else
            {
                CurrentNbOfFrames++;
                if (TestEndingConditions(frame))
                {
                    IsRunning = false;
                    OnGestureRecognizedEventArgs(new GestureRecognizedEventArgs(GestureName));
                }
            }
        }

    }
}