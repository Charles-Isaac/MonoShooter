using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace MonoShooterTest
{
    class Bullet
    {
        private int m_TransitionStartTime;
        private int m_CurrentTime;
        private Vector2 m_StartPosition;
        private int m_RightMargin;

        public Bullet(int TransitionStartTime, Vector2 Position, int RightMargin)
        {
            m_TransitionStartTime = TransitionStartTime;
            m_CurrentTime = TransitionStartTime;
            m_StartPosition = Position;
            m_RightMargin = RightMargin;
        }

        public bool Update(int NewCurrentTime)
        {
            m_CurrentTime = NewCurrentTime;
            return (Position.X >= m_RightMargin);

        }
        public int Transition
        {
            get
            {
                if (((m_CurrentTime - m_TransitionStartTime) / 100) % 4 > 4 || ((m_CurrentTime - m_TransitionStartTime) / 100) % 4 < 0)
                {
                    int i = 0;
                }
                return (((m_CurrentTime - m_TransitionStartTime) / 100) % 4);
            }
        }

        public Vector2 Position
        {
            get { return m_StartPosition + new Vector2(0.3f *(m_CurrentTime - m_TransitionStartTime), 0); }
        }

        public int CurrentTime
        {
            get { return m_CurrentTime; }
            set { m_CurrentTime = value; }
        }
    }
}
