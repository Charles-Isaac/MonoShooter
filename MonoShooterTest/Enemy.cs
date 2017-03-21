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
        private int m_LeftMargin;
        public Enemy(int Type, Vector2 Position, int StartTime, int LeftMargin)
        {
            m_Type = Type;
            m_StartPosition = Position;
            m_CurrentTime = StartTime;
            m_TransitionStartTime = StartTime;
            m_LeftMargin = LeftMargin;
        }

        public bool Update(int NewCurrentTime)
        {
            m_CurrentTime = NewCurrentTime;
            return (Position.X <= m_LeftMargin);
        }


        public int Type
        {
            get { return m_Type; }
            set { m_Type = value; }
        }

        public Vector2 Position
        {
            get { return m_StartPosition + new Vector2(-0.1f * (m_CurrentTime - m_TransitionStartTime), 0); }
        }

        public int ExplosionStartTime
        {
            get { return m_ExplosionStartTime; }
            set { m_ExplosionStartTime = value; }
        }
    }
}
