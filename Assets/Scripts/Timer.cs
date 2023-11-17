public class Timer
{
    private float _baseDuretion;
    private float _currentDuretion;

    public float CurrentDuretion => _currentDuretion;

    public Timer(float duretion)
    {
        _baseDuretion = duretion;
        _currentDuretion = _baseDuretion;
    }

    public void ChangeDuretion(float duretion)
    {
        _baseDuretion = duretion;
    }

    public void Tick(float step)
    {
        _currentDuretion -= step;
    }

    public void Restart()
    {
        _currentDuretion = _baseDuretion;
    }
}
