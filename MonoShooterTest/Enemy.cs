using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;
using System.Threading;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
namespace MonoShooterTest
{
    class EnemyTexture
    {
        private Texture2D[] m_EnemyTextureList;


        public Texture2D[] EnemyTextureList
        {
            get { return m_EnemyTextureList; }
            set { m_EnemyTextureList = value; }
        }
    }
    class Enemy
    {
        private int m_Type;
        private Vector2 m_StartPosition;
        private int m_CurrentTime;
        private int m_ExplosionStartTime;
        private int m_TransitionStartTime;
        private Rectangle m_Margins;
        private Vector2 m_Velocity;
        public Enemy(int Type, Vector2 Position, int StartTime, Rectangle Margins, Vector2 Velocity)
        {
            m_Type = Type;
            m_StartPosition = Position;
            m_CurrentTime = StartTime;
            m_TransitionStartTime = StartTime;
            m_Margins = Margins;
            m_Velocity = Velocity;
        }

        public bool Update(int NewCurrentTime)
        {
            m_CurrentTime = NewCurrentTime;
            if (Position.Y < m_Margins.Top || Position.Y > m_Margins.Height + m_Margins.Top)
            {
                m_StartPosition = Position;
                m_Velocity.Y = -m_Velocity.Y;
                m_TransitionStartTime = m_CurrentTime;
            }
            return (Position.X < m_Margins.Left || Position.X > m_Margins.Width + m_Margins.Left);
        }


        public int Type
        {
            get { return m_Type; }
            set { m_Type = value; }
        }

        public Vector2 Position
        {
            get
            {
                return m_StartPosition +
                       new Vector2(m_Velocity.X*(m_CurrentTime - m_TransitionStartTime),
                           m_Velocity.Y*(m_CurrentTime - m_TransitionStartTime));
            }
        }

        public int ExplosionStartTime
        {
            get { return m_ExplosionStartTime; }
            set { m_ExplosionStartTime = value; }
        }

        public Vector2 Velocity
        {
            get { return m_Velocity; }
            set { m_Velocity = value; }
        }
    }
}
