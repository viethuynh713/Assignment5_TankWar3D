using BehaviorTree;

public class TankBT : Tree
{
    public static float speed = 12f;

    protected override Node SetupTree()
    {
        Node root = new TaskTank(transform);

        return root;
    }
}
