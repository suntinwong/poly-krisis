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
        private Vector3 position, target;

		public DollyCam() {
			view = new Matrix();
			project = new Matrix();
		}
        //Constructor for dollycam
        public DollyCam(Matrix view, Matrix project){
            this.view = view;
            this.project = project;
        }

        //Update the viewing matrix
        public void UpdateView() {
            view = Matrix.CreateLookAt(position, target, Vector3.UnitY);
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
            get { return position; }
            set { position = value; }
        }
        public Vector3 Target {
            get { return target; }
            set { target = value; }
        }
    }
}
