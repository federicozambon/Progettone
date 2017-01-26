using UnityEngine;
using System.Collections;

namespace FSM
{
    public class Bot : MonoBehaviour
    {
        private StateMachine sm;
        public Player playerRef;
        public Enemy enemyRef;
        public float attackTimer= 1.5f;

        void Update()
        {
            if (sm.currentState == sm.attackState)
            {
                sm.attackState.timer += Time.deltaTime;
            }
        }

        void Start()
        {
            playerRef = FindObjectOfType<Player>();
            enemyRef = GetComponent<Enemy>();
            sm = new StateMachine();

            sm.stateIdle = GetComponentInChildren<StateIdle>();
            sm.stateFlee = GetComponentInChildren<StateFlee>();
            sm.stateSearch = GetComponentInChildren<StateSearch>();
            sm.stateFollowA = GetComponentInChildren<StateFollowA>();
            sm.stateFollowB = GetComponentInChildren<StateFollowB>();
            sm.stateFollowC = GetComponentInChildren<StateFollowC>();
            sm.iceTrapState = GetComponentInChildren<StateIceTrapped>();
            sm.attractionTrapState = GetComponentInChildren<StateAttractionTrapped>();
            sm.electricTrapState = GetComponentInChildren<StateElectricTrapped>();
            sm.attackState = GetComponentInChildren<StateAttack>();

            sm.initialState = sm.stateIdle;

            sm.CreateTransitions();
           
            sm.StartMachine();
            StartCoroutine(CheckInputs());
        }

        IEnumerator CheckInputs()
        {
            switch (enemyRef.enemyType)
            {
                //furia statechoose
                case "furia":
                    if (enemyRef.hPoints > 0)
                    {
                        if (enemyRef.trapped)
                        {
                            if (enemyRef.isActiveAttractionTrap)
                            {
                                sm.HandleInput(Inputs.AttractionTrapped);
                            }
                            if (enemyRef.isActiveIceTrap)
                            {
                                sm.HandleInput(Inputs.IceTrapped);
                            }
                            if (enemyRef.isActiveElectricTrap)
                            {
                                sm.HandleInput(Inputs.ElectricTrapped);
                            }
                        }
                        else
                        {
                            if (sm.currentState == sm.attackState)
                            {
                                enemyRef.GetComponent<MeleeEnemy>().StartAttack();
                            }
                            else if (Vector3.Distance(this.transform.position, playerRef.transform.position) < 3f && sm.currentState != sm.attackState)
                            {
                                sm.HandleInput(Inputs.Attack);
                            }
                            if (Vector3.Distance(this.transform.position, playerRef.transform.position) >= 3f && Vector3.Distance(this.transform.position, playerRef.transform.position) <= 10)
                            {
                                sm.HandleInput(Inputs.PlayerRangeB);
                                //  Debug.LogError("PlayerMedium");
                            }
                            if (Vector3.Distance(this.transform.position, playerRef.transform.position) > 10)
                            {
                                sm.HandleInput(Inputs.PlayerRangeC);
                                //  Debug.LogError("PlayerFar");
                            }
                        }
                    }
                    else
                    {
                        sm.HandleInput(Inputs.Death);
                    }
                    sm.StateHandle();

                    yield return new WaitForSeconds(0.5f);
                    StartCoroutine(CheckInputs());
                    break;
                //trooper statechoose
                case "fante":
                    if (enemyRef.hPoints > 0)
                    {
                        if (enemyRef.trapped)
                        {
                            if (enemyRef.isActiveAttractionTrap)
                            {
                                sm.HandleInput(Inputs.AttractionTrapped);
                            }
                            if (enemyRef.isActiveIceTrap)
                            {
                                sm.HandleInput(Inputs.IceTrapped);
                            }
                            if (enemyRef.isActiveElectricTrap)
                            {
                                sm.HandleInput(Inputs.ElectricTrapped);
                            }
                        }
                        else
                        {                   
                            if (Vector3.Distance(this.transform.position, playerRef.transform.position) >= 3f && Vector3.Distance(this.transform.position, playerRef.transform.position) <= 10)
                            {
                                sm.HandleInput(Inputs.Attack);
                            }
                            if (Vector3.Distance(this.transform.position, playerRef.transform.position) < 3f)
                            {
                                sm.HandleInput(Inputs.PlayerRangeA);
                            }
                            if (Vector3.Distance(this.transform.position, playerRef.transform.position) > 10)
                            {
                                sm.HandleInput(Inputs.PlayerRangeC);
                            }                     
                        }
                    }
                    else
                    {
                        sm.HandleInput(Inputs.Death);
                    }
                    sm.StateHandle();

                    yield return new WaitForSeconds(0.5f);
                    StartCoroutine(CheckInputs());
                    break;
                //furiaesplosiva statechoose
                case "furiaesplosiva":
                    if (enemyRef.hPoints > 0)
                    {
                        if (enemyRef.trapped)
                        {
                            if (enemyRef.isActiveAttractionTrap)
                            {
                                sm.HandleInput(Inputs.AttractionTrapped);
                            }
                            if (enemyRef.isActiveIceTrap)
                            {
                                sm.HandleInput(Inputs.IceTrapped);
                            }
                            if (enemyRef.isActiveElectricTrap)
                            {
                                sm.HandleInput(Inputs.ElectricTrapped);
                            }
                        }
                        else
                        {
                            if (sm.currentState == sm.attackState)
                            {
                                enemyRef.GetComponent<MeleeExplosiveEnemy>().StartAttack();
                            }
                            else if (Vector3.Distance(this.transform.position, playerRef.transform.position) < 2.5f && sm.currentState != sm.attackState)
                            {
                                sm.HandleInput(Inputs.Attack);
                            }
                            if (Vector3.Distance(this.transform.position, playerRef.transform.position) >= 2.5f && Vector3.Distance(this.transform.position, playerRef.transform.position) <= 10)
                            {
                                sm.HandleInput(Inputs.PlayerRangeB);
                                //  Debug.LogError("PlayerMedium");
                            }
                            if (Vector3.Distance(this.transform.position, playerRef.transform.position) > 10)
                            {
                                sm.HandleInput(Inputs.PlayerRangeC);
                                //  Debug.LogError("PlayerFar");
                            }
                        }
                    }
                    else
                    {
                        sm.HandleInput(Inputs.Death);
                    }
                    sm.StateHandle();

                    yield return new WaitForSeconds(0.5f);
                    StartCoroutine(CheckInputs());
                    break;
                //predatore statechoose
                case "predatore":
                    if (enemyRef.hPoints > 0)
                    {
                        if (enemyRef.trapped)
                        {
                            if (enemyRef.isActiveAttractionTrap)
                            {
                                sm.HandleInput(Inputs.AttractionTrapped);
                            }
                            if (enemyRef.isActiveIceTrap)
                            {
                                sm.HandleInput(Inputs.IceTrapped);
                            }
                            if (enemyRef.isActiveElectricTrap)
                            {
                                sm.HandleInput(Inputs.ElectricTrapped);
                            }
                        }
                        else
                        {
                            if (sm.currentState.timer >= attackTimer && sm.currentState == sm.attackState)
                            {
                                //enemyRef.Attack();
                            }
                            else if (Vector3.Distance(this.transform.position, playerRef.transform.position) < 3f && sm.currentState != sm.attackState)
                            {
                                sm.HandleInput(Inputs.Attack);
                            }
                            if (Vector3.Distance(this.transform.position, playerRef.transform.position) >= 3f && Vector3.Distance(this.transform.position, playerRef.transform.position) <= 10)
                            {
                                sm.HandleInput(Inputs.PlayerRangeB);
                                //  Debug.LogError("PlayerMedium");
                            }
                            if (Vector3.Distance(this.transform.position, playerRef.transform.position) > 10)
                            {
                                sm.HandleInput(Inputs.PlayerRangeC);
                                //  Debug.LogError("PlayerFar");
                            }
                        }
                    }
                    else
                    {
                        sm.HandleInput(Inputs.Death);
                    }
                    sm.StateHandle();

                    yield return new WaitForSeconds(0.5f);
                    StartCoroutine(CheckInputs());
                    break;
                //titano statechoose
                case "titano":
                    if (enemyRef.hPoints > 0)
                    {
                        if (enemyRef.trapped)
                        {
                            if (enemyRef.isActiveAttractionTrap)
                            {
                                sm.HandleInput(Inputs.AttractionTrapped);
                            }
                            if (enemyRef.isActiveIceTrap)
                            {
                                sm.HandleInput(Inputs.IceTrapped);
                            }
                            if (enemyRef.isActiveElectricTrap)
                            {
                                sm.HandleInput(Inputs.ElectricTrapped);
                            }
                        }
                        else
                        {
                            if (sm.currentState == sm.attackState)
                            {
                                if (sm.currentState.timer >= attackTimer)
                                {
                                    //enemyRef.Attack();
                                }
                            }
                            else if (Vector3.Distance(this.transform.position, playerRef.transform.position) >= 3f && Vector3.Distance(this.transform.position, playerRef.transform.position) <= 10)
                            {
                                sm.HandleInput(Inputs.Attack);
                                //  Debug.LogError("PlayerMedium");
                            }
                            if (Vector3.Distance(this.transform.position, playerRef.transform.position) < 3f)
                            {
                                sm.HandleInput(Inputs.PlayerRangeA);
                                //  Debug.LogError("PlayerClose");
                            }
                   
                            if (Vector3.Distance(this.transform.position, playerRef.transform.position) > 10)
                            {
                                sm.HandleInput(Inputs.PlayerRangeC);
                                //  Debug.LogError("PlayerFar");
                            }
                        }
                    }
                    else
                    {
                        sm.HandleInput(Inputs.Death);
                    }
                    sm.StateHandle();

                    yield return new WaitForSeconds(0.5f);
                    StartCoroutine(CheckInputs());
                    break;
                //sniper statechoose
                case "cecchino":
                    if (enemyRef.hPoints > 0)
                    {
                        if (enemyRef.trapped)
                        {
                            if (enemyRef.isActiveAttractionTrap)
                            {
                                sm.HandleInput(Inputs.AttractionTrapped);
                            }
                            if (enemyRef.isActiveIceTrap)
                            {
                                sm.HandleInput(Inputs.IceTrapped);
                            }
                            if (enemyRef.isActiveElectricTrap)
                            {
                                sm.HandleInput(Inputs.ElectricTrapped);
                            }
                        }
                        else
                        {
                            if (sm.currentState == sm.attackState)
                            {
                                if (sm.currentState.timer >= attackTimer)
                                {
                                    //enemyRef.Attack();
                                }
                            }
                            else
                            {
                                if (Vector3.Distance(this.transform.position, playerRef.transform.position) > 15)
                                {
                                    sm.HandleInput(Inputs.Attack);
                                    //  Debug.LogError("PlayerFar");
                                }
                                if (Vector3.Distance(this.transform.position, playerRef.transform.position) < 3f)
                                {
                                    sm.HandleInput(Inputs.PlayerRangeA);
                                    //  Debug.LogError("PlayerClose");
                                }
                                if (Vector3.Distance(this.transform.position, playerRef.transform.position) >= 3f && Vector3.Distance(this.transform.position, playerRef.transform.position) <= 15)
                                {
                                    sm.HandleInput(Inputs.PlayerRangeB);
                                    //  Debug.LogError("PlayerMedium");
                                }
                            }                             
                        }
                    }
                    else
                    {
                        sm.HandleInput(Inputs.Death);
                    }
                    sm.StateHandle();

                    yield return new WaitForSeconds(0.5f);
                    StartCoroutine(CheckInputs());
                    break;
            }
        }
    }
}