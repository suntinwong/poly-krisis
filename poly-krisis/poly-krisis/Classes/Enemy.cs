using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace poly_krisis.Classes
{
    enum EnemyState { Fire, Wait, Dead };
    class Enemy
    {
        public Vector3 pos;
        public Model shape;
        EnemyState STATE;
        int timer_ms;
        int firerate_ms;
        const double FIGHTDISTANCE = 40;

        public Enemy(Model shape, Vector3 pos, int firerate)
        {
            this.shape = shape;
            this.pos = pos;
            firerate_ms = firerate;
        }

        public void Update(GameTime time, Vector3 hero_pos)
        {
            switch (STATE) // transitions
            {
                case EnemyState.Wait:
                    timer_ms += time.ElapsedGameTime.Milliseconds;
                    double distance = ( hero_pos - pos ).Length();
                    if( distance < FIGHTDISTANCE )
                    if (timer_ms > firerate_ms)
                    {
                        STATE = EnemyState.Fire;
                    }
                    break;
                case EnemyState.Fire:
                    STATE = EnemyState.Wait;
                    break;
                case EnemyState.Dead:
                    break;
                default:
                    STATE = EnemyState.Wait;
                    break;
            }

            switch (STATE)
            {
                case EnemyState.Wait:
                    break;
                case EnemyState.Fire:
                    break;
                case EnemyState.Dead:
                    break;
            }
        }

    }
}
