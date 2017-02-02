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
        public bool canSeePlayer = false;

        void Update()
        {
            if (sm.currentState == sm.attackState)
            {
                sm.attackState.blackRef.timer += Time.deltaTime;
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
                            else if (Vector3.Distance(this.transform.position, playerRef.transform.position) >= 3f && Vector3.Distance(this.transform.position, playerRef.transform.position) <= 10)
                            {
                                sm.HandleInput(Inputs.PlayerRangeB);
                                //  Debug.LogError("PlayerMedium");
                            }
                            else if (Vector3.Distance(this.transform.position, playerRef.transform.position) > 10)
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

                    yield return new WaitForSeconds(0.25f);
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

                            RaycastHit losRayOut;
                            if (Physics.Linecast(enemyRef.GetComponent<RangedEnemyFire>().weapon.position, playerRef.transform.position, out losRayOut))
                            {
                                if (losRayOut.collider.tag == "Player")
                                {
                                    canSeePlayer = true;
                                }
                                else
                                {
                                    canSeePlayer = false;
                                }
                            }
           
                            if (Vector3.Distance(this.transform.position, playerRef.transform.position) > 15)
                            {
                                sm.HandleInput(Inputs.PlayerRangeC);
                            }
                            if (Vector3.Distance(this.transform.position, playerRef.transform.position) < 4f)
                            {
                                sm.HandleInput(Inputs.PlayerRangeA);
                            }
                            if (Vector3.Distance(this.transform.position, playerRef.transform.position) >= 4f && Vector3.Distance(this.transform.position, playerRef.transform.position) <= 15 && canSeePlayer)
                            {
                                sm.HandleInput(Inputs.Attack);
                            }
                            else if (Vector3.Distance(this.transform.position, playerRef.transform.position) >= 4f && Vector3.Distance(this.transform.position, playerRef.transform.position) <= 15 && !canSeePlayer)
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

                    yield return new WaitForSeconds(0.25f);
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

                    yield return new WaitForSeconds(0.25f);
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
                            if (sm.currentState.blackRef.timer >= attackTimer && sm.currentState == sm.attackState)
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

                    yield return new WaitForSeconds(0.25f);
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

                            RaycastHit losRayOut;
                            if (Physics.Linecast(enemyRef.GetComponent<TitanoEnemyFire>().weapon.position, playerRef.transform.position, out losRayOut))
                            {
                                if (losRayOut.collider.tag == "Player")
                                {
                                    canSeePlayer = true;
                                }
                                else
                                {
                                    canSeePlayer = false;
                                }
                            }

                            if (Vector3.Distance(this.transform.position, playerRef.transform.position) > 30)
                            {
                                sm.HandleInput(Inputs.PlayerRangeC);
                            }
                            if (Vector3.Distance(this.transform.position, playerRef.transform.position) < 20f)
                            {
                                sm.HandleInput(Inputs.PlayerRangeA);
                            }
                            if (Vector3.Distance(this.transform.position, playerRef.transform.position) >= 15f && Vector3.Distance(this.transform.position, playerRef.transform.position) <= 30 && canSeePlayer)
                            {
                                sm.HandleInput(Inputs.Attack);
                            }
                            else if (Vector3.Distance(this.transform.position, playerRef.transform.position) >= 15f && Vector3.Distance(this.transform.position, playerRef.transform.position) <= 30 && !canSeePlayer)
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

                    yield return new WaitForSeconds(0.25f);
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
                                if (sm.currentState.blackRef.timer >= attackTimer)
                                {
                                    enemyRef.Attack();
                                }
                            }
                            else
                            {
                                if (Vector3.Distance(this.transform.position, playerRef.transform.position) > 10)
                                {
                                    sm.HandleInput(Inputs.Attack);
                                    //  Debug.LogError("PlayerFar");
                                }
                            }                             
                        }
                    }
                    else
                    {
                        sm.HandleInput(Inputs.Death);
                    }
                    sm.StateHandle();

                    yield return new WaitForSeconds(0.25f);
                    StartCoroutine(CheckInputs());
                    break;
            }
        }
    }
}