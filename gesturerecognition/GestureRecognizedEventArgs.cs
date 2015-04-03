using System;

namespace GestureRecognition
{
    public class GestureRecognizedEventArgs : EventArgs
    {

        public string GestureName
        {
            get;
            private set;
        }

        public GestureRecognizedEventArgs(string gestureName)
        {
            GestureName = gestureName;
        }
    }

}