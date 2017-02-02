using UnityEngine;
using System.Collections;

public abstract class State : MonoBehaviour
{
    public abstract void Handle();

    //public Enemy enemyRef;
    //protected Movement botMovement;
    //protected Transform playerTr;
    //protected Transform botTr;
    //public NavMeshAgent navRef;
    //public string enemyType;
    //public Transform attractionTransform;
    //public float timer;
    public BlackBoard blackRef;

    public virtual void OnEnable()
    {
        blackRef = GetComponentInParent<BlackBoard>();
        //enemyRef = GetComponentInParent<Enemy>();
        //enemyType = enemyRef.enemyType;
        //botMovement = GetComponentInParent<Movement>();
        //navRef = GetComponentInParent<NavMeshAgent>();
        //playerTr = FindObjectOfType<Player>().transform;
        //botTr = transform.parent.transform;
        //attractionTransform = enemyRef.attractionTrap;
    }

    public virtual void EnterStateLogic()
    {
    }

    public virtual void ExitStateLogic()
    {

    }

    public virtual void Initialize()
    {
    }
}
