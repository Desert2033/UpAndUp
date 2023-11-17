using UnityEngine;

public class HeroPutBlock
{
    private IGameFactory _gameFactory;
    private HeroAnimator _heroAnimator;
    private GameObject _prevBlockEarth;

    public HeroPutBlock(IGameFactory gameFactory, HeroAnimator heroAnimator)
    {
        _gameFactory = gameFactory;
        _heroAnimator = heroAnimator;
    }

    public Vector3 PutBlock(Directions direction)
    {
        Vector3 positionBlock = new Vector3(0, 0, 0);

        _heroAnimator.OnPutBlockTrigger();


        if (!IsFirstBlockSpawned())
        {
            SpawnBlock(positionBlock);
        }
        else if (direction == Directions.Left)
        {
            positionBlock = SpawnBlockLeft();
        }
        else if (direction == Directions.Right)
        {
            positionBlock = SpawnBlockRight();
        }
        else
        {
            SpawnBlockUp();
        }

        return positionBlock;
    }

    public bool IsFirstBlockSpawned() =>
        _prevBlockEarth != null;

    private void SpawnBlockUp()
    {
        Vector3 positionBlock = _prevBlockEarth.transform.position;
        positionBlock.y += Constants.BlocksDistanceY;

        SpawnBlock(positionBlock);
    }

    private Vector3 SpawnBlockRight()
    {
        Vector3 positionBlock = _prevBlockEarth.transform.position;
        positionBlock.x += Constants.BlocksDistanceX;

        SpawnBlock(positionBlock);

        return positionBlock;
    }

    private Vector3 SpawnBlockLeft()
    {
        Vector3 positionBlock = _prevBlockEarth.transform.position;
        positionBlock.x -= Constants.BlocksDistanceX;

        SpawnBlock(positionBlock);

        return positionBlock;
    }

    private void SpawnBlock(Vector3 position) =>
       _prevBlockEarth = _gameFactory.CreateBlockEarth(position);
}
