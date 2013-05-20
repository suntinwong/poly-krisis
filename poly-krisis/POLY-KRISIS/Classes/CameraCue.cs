using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace poly_krisis {
	/*
	 * Tells the camera where it should be and what it should
	 * be looking at
	 */
	public class CameraCue {
		public Vector3 pos, look;
		public float speed;
		//Time to wait at this cue in milliseconds
		public int waitMS;
		//Scene callback type to tell us if the scene is done
		public delegate bool CueDone(int elapsedMS);
		CueDone isDone;

		/*
		 * Create a camera cue, giving a position, look direction, speed to 
		 * travel too and time to wait
		 */
		public CameraCue(Vector3 p, Vector3 l, float s, int wait = 0) {
			pos = p;
			look = l;
			speed = s;
			waitMS = wait;
		}
		/*
		 * Create a camera cue giving a position, look direction, 
		 * speed to travel too and scene signal to wait for
		 */
		public CameraCue(Vector3 p, Vector3 l, float s, CueDone done) {
			pos = p;
			look = l;
			speed = s;
			isDone = done;
		}
	}
}
