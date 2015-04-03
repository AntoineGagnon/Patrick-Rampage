using System;
using GestureRecognition.Postures;
using Leap;

namespace GestureRecognition.Gestures
{
    class HandLifted : Gesture
    {
        protected override bool TestInitialConditions(Frame frame)
        {
            RightHandDown r = new RightHandDown();
            return r.TestPosture(frame);
        }

        protected override bool TestEndingConditions(Frame frame)
        {
            RightHandUp r = new RightHandUp();
            return r.TestPosture(frame);
        }

        protected override bool TestPosture(Frame frame)
        {
            throw new NotImplementedException();
        }

        protected override bool TestDynamicGesture(Frame frame)
        {
            throw new NotImplementedException();
        }
    }
}
