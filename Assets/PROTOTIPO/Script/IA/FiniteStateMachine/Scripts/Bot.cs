using UnityEngine;
using System.Collections;

namespace FSM
{
    public class Bot : MonoBehaviour
    {
        private StateMachine sm;
        public BlackBoard blackRef;
        public ReferenceManager refManager;
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
            blackRef = GetComponent<BlackBoard>();
            refManager = FindObjectOfType<ReferenceManager>();
            playerRef = refManager.playerRef;
            enemyRef = blackRef.enemyRef;
            sm = new StateMachine();

            sm.stateIdle = blackRef.stateIdle;
            sm.stateFlee = blackRef.stateFlee;
            sm.stateSearch = blackRef.stateSearch;
            sm.stateFollowA = blackRef.stateFollowA;
            sm.stateFollowB = blackRef.stateFollowB;
            sm.stateFollowC = blackRef.stateFollowC;
            sm.iceTrapState = blackRef.stateIceTrap;
            sm.attractionTrapState = blackRef.stateAttractionTrap;
            sm.electricTrapState = blackRef.stateElectricTrap;
            sm.attackState = blackRef.stateAttack;

            sm.initialState = sm.stateIdle;

            sm.CreateTransitions();           
            sm.StartMachine();      
        }

        void OnEnable()
        {
            StartCoroutine(CheckInputs());
        }

        IEnumerator CheckInputs()
        {
            yield return null;
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