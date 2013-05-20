using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace poly_krisis.Classes
{
    
    class AABB
    {
        public Vector3 min, max;
        AABB(Vector3 min, Vector3 max)
        {
            this.min = min;
            this.max = max;
        }
    }

    enum colobjectType{Player, Enemy, Static, PlayerAttack, EnemyAttack}
    
    class collisionObject
    {
        AABB boundingBox;
        colobjectType type;

        Int32 collisionMask;
        collisionObject(AABB box, colobjectType collisionType)
        {
            boundingBox = box;
            type = collisionType;
        }

        public void collide(collisionObject object2)
        {
            return;
        }
    }

    class ColMan
    {

        List<collisionObject> objects;

        ColMan()
        {
            objects = new List<collisionObject>();
        }


        void addObject(collisionObject obj)
        {
            objects.Add(obj);
        }

        void Update(GameTime time)
        {
            for (int i = 0 ; i < objects.Count ; i++ )
            {
                for ( int j = i+1 ; j < objects.Count ; j++)
                {

                    objects[i].collide( objects[j] );
                }
            }
        }
    }
}
