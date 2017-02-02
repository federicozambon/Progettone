using UnityEngine;
using System.Collections;
using System;

namespace FSM
{
    public class StateIdle : State
    {
        public override void Handle()
        {
            switch (blackRef.enemyType)
            {
                case "furia":
                    blackRef.botMovement.destination = transform.position;
                    break;
                case "fante":
                    blackRef.botMovement.destination = transform.position;
                    break;
                case "furiaesplosiva":
                    blackRef.botMovement.destination = transform.position;
                    break;
                case "predatore":
                    blackRef.botMovement.destination = transform.position;
                    break;
                case "titano":
                    blackRef.botMovement.destination = transform.position;
                    break;
                case "cecchino":
                    blackRef.botMovement.destination = transform.position;
                    break;
            }
        }
    }
}
