using System;
using Leap;

namespace GestureRecognition
{
    public abstract class BaseGesture
    {

        public virtual string GestureName
        {
            get
            {
                return GetType().Name;
            }
        }

        public event EventHandler<GestureRecognizedEventArgs> GestureRecognized;

        protected virtual void OnGestureRecognizedEventArgs(GestureRecognizedEventArgs e)
        {
            if (GestureRecognized != null)
            {
                GestureRecognized(this, e);
            }
        }

        public abstract void TestGesture(Frame frame);
    }
}