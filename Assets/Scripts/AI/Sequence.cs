
using System.Collections;
using UnityEngine;
public class Sequence : Node
{
    private Node[] children;

    public Sequence(Node[] nodes)
    {
        children = nodes;
    }

    public override IEnumerator Execute(MonoBehaviour mono)
    {
        foreach (Node child in children)
        {
            CoroutineWithData cd = new CoroutineWithData(mono, child.Execute( mono) );
            NodeStatus status = (NodeStatus)cd.result;
            
            if (status == NodeStatus.Failure )
            {
               yield return status; // Return failure if any child fails
            }
        }
        yield return NodeStatus.Success; // All children succeeded
    }
}