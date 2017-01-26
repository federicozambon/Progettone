using UnityEngine;
using System.Collections;
using System;

namespace FSM
{
    public class StateIdle : State
    {
        public override void Handle()
        {
            switch (enemyType)
            {
                case "furia":
                    botMovement.destination = transform.position;
                    break;
                case "fante":
                    botMovement.destination = transform.position;
                    break;
                case "furiaesplosiva":
                    botMovement.destination = transform.position;
                    break;
                case "predatore":
                    botMovement.destination = transform.position;
                    break;
                case "titano":
                    botMovement.destination = transform.position;
                    break;
                case "cecchino":
                    botMovement.destination = transform.position;
                    break;
            }
        }
    }
}
