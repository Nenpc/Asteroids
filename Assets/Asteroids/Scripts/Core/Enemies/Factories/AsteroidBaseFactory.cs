using Asteroids.Scripts.Core.Enemies.Config;
using Asteroids.Scripts.Core.Progress.Interfeces;
using Asteroids.Scripts.Infrastructure.UpdateProvider;
using UnityEngine;

namespace Asteroids.Scripts.Core.Enemies.Models
{
    public abstract class AsteroidBaseFactory<T> : EnemyFactoryAbstract<T> where T : BaseEnemy
    {
        public override Enums.Enemies EnemyType => Enums.Enemies.AsteroidSmall;
        public AsteroidBaseFactory(IEnemiesConfig enemiesConfig, 
            IUpdateProvider updateProvider, 
            IProgressModel progressModel) : 
            base(enemiesConfig, updateProvider, progressModel)
        {

        }
        
        protected float GetStartRotation(int startBorder, Vector2 startPosition)
        {
            var leftAngleBorder = 360f;
            var rightAngleBorder = 0f;
            switch (startBorder)
            {
                case 0:
                    // Top
                    if (startPosition.x > 0)
                    {
                        leftAngleBorder = 90;
                        rightAngleBorder = 270 - GetAngleShift(startPosition.x, _bottomRight.x);
                    }
                    else
                    {
                        leftAngleBorder = 90 + GetAngleShift(Mathf.Abs(startPosition.x), _bottomRight.x);
                        rightAngleBorder = 270;
                    }
                    break;
                case 1:
                    // Right
                    if (startPosition.y > 0)
                    {
                        leftAngleBorder = 0;
                        rightAngleBorder = 180 - GetAngleShift(startPosition.y, _topLeft.y);
                    }
                    else
                    {
                        leftAngleBorder = 0 + GetAngleShift(Mathf.Abs(startPosition.y), _topLeft.y);
                        rightAngleBorder = 180;
                    }
                    break;
                case 2:
                    // Bottom
                    if (startPosition.x > 0)
                    {
                        leftAngleBorder = -90;
                        rightAngleBorder = 90 - GetAngleShift(startPosition.x, _bottomRight.x);
                    }
                    else
                    {
                        leftAngleBorder = -90 + GetAngleShift(Mathf.Abs(startPosition.x), _bottomRight.x);
                        rightAngleBorder = 90;
                    }
                    break;
                case 3:
                    // Left
                    if (startPosition.y > 0)
                    {
                        leftAngleBorder = 180;
                        rightAngleBorder = 360 - GetAngleShift(startPosition.y, _topLeft.y);

                    }
                    else
                    {
                        leftAngleBorder = 180 + GetAngleShift(Mathf.Abs(startPosition.y), _topLeft.y);
                        rightAngleBorder = 360;
                    }
                    break;
            }
            return Random.Range(leftAngleBorder, rightAngleBorder);
        }

        protected const float AnglePercent = 90 * 0.01f;
        protected float GetAngleShift(float position, float length)
        {
            var lenghtPercent = length * 0.01f;
            var positionPercent =  position / lenghtPercent;
            return positionPercent * AnglePercent;
        }
    }
}