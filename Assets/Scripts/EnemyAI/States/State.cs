
public abstract class State
{
    protected readonly EnemyController enemy;
    protected readonly NavMesh2D navMesh;

    protected State(EnemyController enemy, NavMesh2D navMesh)
    {
        this.enemy = enemy;
        this.navMesh = navMesh;
    }

    public abstract void OnStart();

    public abstract void OnUpdate();

    public abstract void OnFinalize();

    public virtual UnityEngine.Vector2 CalculateMovementPosition()
    {
        return UnityEngine.Vector2.zero;
    }
}
