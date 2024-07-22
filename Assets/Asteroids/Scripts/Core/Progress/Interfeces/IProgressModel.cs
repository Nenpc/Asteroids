namespace Asteroids.Scripts.Core.Progress.Interfeces
{
    public interface IProgressModel
    {
        int GetScore { get; }
        int AddScore(int value = 1);
        int ResetScore();
    }
}