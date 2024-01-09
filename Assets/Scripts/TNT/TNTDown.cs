using UnityEngine;

public class TNTDown : MonoBehaviour
{
    private const float Duretion = 0.5f;

    [SerializeField] private TNTDeath _death;

    private CameraBorder _cameraBorder;
    private Timer _cooldown;

    public void Construct(CameraBorder cameraBorder)
    {
        _cameraBorder = cameraBorder;
    }

    private void Start()
    {
        _cooldown = new Timer(Duretion);
    }

    private void Update()
    {
        _cooldown.Tick(Time.deltaTime);

        if (_cameraBorder != null && _cooldown.CurrentDuretion <= 0)
            if (transform.position.y <= _cameraBorder.LeftBot.y)
                _death.Dead();
    }
}
