using System.Linq;
using Leap;

namespace GestureRecognition.Postures
{
	class EndShoot : Posture
	{
		public override bool TestPosture(Frame frame)
		{
		    Hand leftHand = HandGetter.GetLeftHand(frame);
		    if (leftHand == null)
		    {
		        return false;
		    }

		    Finger pinky = leftHand.Fingers.FingerType(Finger.FingerType.TYPE_PINKY).First ();
            Finger middle = leftHand.Fingers.FingerType(Finger.FingerType.TYPE_MIDDLE).First();
            Finger index = leftHand.Fingers.FingerType(Finger.FingerType.TYPE_INDEX).First();

            return !pinky.IsExtended
                   && (index.IsExtended || middle.IsExtended)
			       && middle.Bone(Bone.BoneType.TYPE_PROXIMAL).Center.z >= pinky.Bone(Bone.BoneType.TYPE_PROXIMAL).PrevJoint.z;
		}
	}
}
