

using Asteroids.Scripts.Core.Progress.Interfeces;

namespace Asteroids.Scripts.Core.Progress.Models
{
    public sealed class ProgressModel : IProgressModel
    {
        private int _score;

        public int GetScore => _score;
        public int AddScore(int value = 1) => _score += value > 0 ? value : 0;
        public int ResetScore() => _score = 0;
    }
}