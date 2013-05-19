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
     * Handles a Dolly cam that can travel between
     * different viewing matrices
     */
    public class DollyCam {
        private Matrix view, project;
		private CameraCue currentCue, nextCue;
		private float lerpSpeed;
		private bool arrived;

		public DollyCam() {
			arrived = true;
		}
        //Constructor for dollycam
        public DollyCam(CameraCue cue, Matrix project){
			this.currentCue = cue;
            this.project = project;
			arrived = true;
			UpdateView();
        }
		//Update the camera motion
		public void Update(GameTime time) {
			//position += movedir * 2.0f * (time.ElapsedGameTime.Milliseconds / 1000.0f);
			//UpdateView();
			if (!arrived) {
				float amt = lerpSpeed * (time.ElapsedGameTime.Milliseconds / 1000.0f);
				currentCue.pos = Vector3.Lerp(currentCue.pos, nextCue.pos, amt);
				currentCue.look = Vector3.Lerp(currentCue.look, nextCue.look, amt);
				UpdateView();
				if ((currentCue.pos - nextCue.pos).LengthSquared() < 0.2
					&& (currentCue.look - nextCue.look).LengthSquared() < 0.2) 
						arrived = true;
			}
		}
        //Update the viewing matrix
        public void UpdateView() {
            view = Matrix.CreateLookAt(currentCue.pos, currentCue.pos + currentCue.look, Vector3.UnitY);
        }
		//Set the camera cue to transition too and the speed to do it at
		public void TransitionTo(CameraCue nextCue, float speed) {
			this.nextCue = nextCue;
			lerpSpeed = speed;
			arrived = false;
		}

        public Matrix View {
            get { return view; }
            set { view = value; }
        }
        public Matrix Projection {
            get { return project; }
            set { project = value; }
        }
        public Vector3 Position {
            get { return currentCue.pos; }
			set { currentCue.pos = value; }
        }
        public Vector3 Look {
            get { return currentCue.look; }
			set { currentCue.look = value; }
        }
    }
}
