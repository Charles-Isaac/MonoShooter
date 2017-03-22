using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace MonoShooterTest
{
    class Missile
    {
        private int m_TransitionStartTime;
        private int m_CurrentTime;
        private Vector2 m_Position;
        private Vector2 m_Velocity;
        private Vector2 m_OldPosition;
        private Rectangle m_Margins;

        public Missile(int TransitionStartTime, Vector2 Position, Vector2 Velocity, Rectangle Margins)
        {
            m_TransitionStartTime = TransitionStartTime;
            m_CurrentTime = TransitionStartTime;
            m_Position = Position;
            m_Margins = Margins;
            m_Velocity = Velocity;
            m_OldPosition = Position;
        }

        public bool Update(int NewCurrentTime, Vector2 Target)
        {
            int Delta = NewCurrentTime - m_CurrentTime;
            m_CurrentTime = NewCurrentTime;
            Target -= m_Position;
            Target.Normalize();
            m_Velocity += Target * Delta / 100.0f;
            m_OldPosition = m_Position;
            
            if (m_Velocity.Length() > 75)
            {
                m_Velocity /= m_Velocity.Length() / 75;
            }
            m_Position += m_Velocity;
            return (m_Position.X < m_Margins.Left || m_Position.X > m_Margins.Width + m_Margins.Left || m_Position.Y < m_Margins.Top || m_Position.Y > m_Margins.Height + m_Margins.Top);

        }
        public int Transition
        {
            get
            {
                return (((m_CurrentTime - m_TransitionStartTime) / 100) % 4);
            }
        }

        public Vector2 Position
        {
            get { return m_Position; }
        }

        public int CurrentTime
        {
            get { return m_CurrentTime; }
            set { m_CurrentTime = value; }
        }
        public Vector2 OldPosition
        {
            get { return m_OldPosition; }
            set { m_OldPosition = value; }
        }

        public Vector2 Velocity
        {
            get { return m_Velocity; }
            set { m_Velocity = value; }
        }
    }
}
