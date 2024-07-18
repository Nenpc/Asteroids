namespace Asteroids.Scripts.Core.Enemies.Models
{
    public interface IEnemiesCreator
    {
        void Start();
        void FixedUpdate();
        void End();
    }
}