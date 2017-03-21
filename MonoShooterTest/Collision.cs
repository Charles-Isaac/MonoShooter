using System.Drawing;
using Microsoft.Xna.Framework;

namespace MonoShooterTest
{
    static class Collision
    {
        public static bool IsIntersecting(Vector2 VecteurA1, Vector2 VecteurA2, Vector2 VecteurB1, Vector2 VecteurB2) //Retourne un booléen indiquant si la ligne tracé entre VecteurA1 et VecteurA2 intersectionne la ligne tracé entre VecteurB1 et VecteurB2 
        {
            float Denominator = ((VecteurA2.X - VecteurA1.X) * (VecteurB2.Y - VecteurB1.Y)) - ((VecteurA2.Y - VecteurA1.Y) * (VecteurB2.X - VecteurB1.X));
            float Numerator1 = ((VecteurA1.Y - VecteurB1.Y) * (VecteurB2.X - VecteurB1.X)) - ((VecteurA1.X - VecteurB1.X) * (VecteurB2.Y - VecteurB1.Y));
            float Numerator2 = ((VecteurA1.Y - VecteurB1.Y) * (VecteurA2.X - VecteurA1.X)) - ((VecteurA1.X - VecteurB1.X) * (VecteurA2.Y - VecteurA1.Y));

            if (Denominator == 0) return Numerator1 == 0 && Numerator2 == 0;

            float R = Numerator1 / Denominator;
            float S = Numerator2 / Denominator;

            return (R >= 0 && R <= 1) && (S >= 0 && S <= 1);
        }

        
    }
}
