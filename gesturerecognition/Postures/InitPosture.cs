//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.34014
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using Leap;

namespace GestureRecognition.Postures
{
    public class InitPosture : Posture
    {
        public override bool TestPosture(Frame frame)
        {
            if (frame.Hands.Count != 2)
            {
                return false;
            }

            Hand rightHand = HandGetter.GetRightHand(frame);
            Hand leftHand = HandGetter.GetLeftHand(frame);
            if (rightHand == null || leftHand == null) return false;
            if (rightHand.PalmPosition.x > leftHand.PalmPosition.x) return true;
            return false;

        }

    }
}

