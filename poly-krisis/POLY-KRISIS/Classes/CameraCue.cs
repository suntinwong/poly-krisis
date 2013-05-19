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
		public Vector3 pos, target;

		public CameraCue(Vector3 p, Vector3 t) {
			pos = p;
			target = t;
		}
	}
}
