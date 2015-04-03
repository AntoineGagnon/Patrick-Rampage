using System;
using GestureRecognition.Postures;
using Leap;

namespace GestureRecognition.Gestures
{
	class Shoot : Gesture
	{
		protected override bool TestInitialConditions(Frame frame)
		{
			InitShoot init = new InitShoot ();
			return init.TestPosture(frame);
		}
		
		protected override bool TestEndingConditions(Frame frame)
		{
			EndShoot end = new EndShoot ();
			return end.TestPosture(frame);
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
