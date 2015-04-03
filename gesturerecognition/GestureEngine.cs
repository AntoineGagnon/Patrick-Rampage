//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.34014
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using GestureRecognition.Gestures;
using GestureRecognition.Postures;
using Leap;

namespace GestureRecognition
{
	public static class GestureEngine
	{
		public static List<BaseGesture> gestures = new List<BaseGesture>(){
            new HandMissing(),
            new HandLifted(),
            new Shoot(),
            new InitPosture(),
            new InitShoot(),
        };

		public static void TestGesture(Frame frame){
			foreach (var gest in gestures) {
                gest.TestGesture(frame);
			}
		}

		public static event EventHandler<GestureRecognizedEventArgs> GestureRecognized {
			add {
				foreach (var gest in gestures) {
					gest.GestureRecognized += value;
				}
			}
			remove {
				foreach (var gest in gestures) {
					gest.GestureRecognized -= value;
				}
			}
		}
	}
}

