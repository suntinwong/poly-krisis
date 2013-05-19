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
		//The list of cue transitions to work through
		private List<CameraCue> cues;
		private CameraCue currentCue;
		private bool arrived;
		//Amount of time we've been waiting at the cue, in milliseconds
		private int waitElapsed;

        //Constructor for dollycam
        public DollyCam(CameraCue cue, Matrix project){
			this.currentCue = cue;
            this.project = project;
			cues = new List<CameraCue>();
			arrived = true;
			waitElapsed = 0;			
			UpdateView();
        }
		//Update the camera motion
		public void Update(GameTime time) {
			if (!arrived && cues.Count != 0) {
				float amt = cues.First().speed * (time.ElapsedGameTime.Milliseconds / 1000.0f);
				currentCue.pos = Vector3.SmoothStep(currentCue.pos, cues.First().pos, amt);
				currentCue.look = Vector3.SmoothStep(currentCue.look, cues.First().look, amt);
				//Say we've arrived if we're close enough
				if ((currentCue.pos - cues.First().pos).LengthSquared() < 0.15
					&& (currentCue.look - cues.First().look).LengthSquared() < 0.15) {
						arrived = true;
				}

				UpdateView();
			}
			else if (cues.Count != 0) {
				waitElapsed += time.ElapsedGameTime.Milliseconds;
				if (waitElapsed > cues.First().waitMS) {
					cues.RemoveAt(0);
					arrived = false;
					waitElapsed = 0;
				}
			}
		}
        //Update the viewing matrix
        public void UpdateView() {
            view = Matrix.CreateLookAt(currentCue.pos, currentCue.pos + currentCue.look, Vector3.UnitY);
        }
		//Set the camera cue to transition too and the speed to do it at
		public void TransitionTo(CameraCue nextCue) {
			cues.Insert(0, nextCue);
			arrived = false;
		}
		//Add a transition to the cue list
		public void AddCue(CameraCue cue) {
			if (cues.Count == 0)
				arrived = false;
			cues.Add(cue);
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
			set { 
				currentCue.look = value;
				currentCue.look.Normalize();
			}
        }
		public bool Arrived {
			get { return arrived; }
		}
    }
}
