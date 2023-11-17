using System;

[Serializable]
public class PlayerProgress
{
    public GameData GameData;

    public PlayerProgress()
    {
        GameData = new GameData(0);
    }
}
