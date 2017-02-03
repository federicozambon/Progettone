using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Transition
{
    public Inputs input;
    public State fromState;
    public State targetState;

    public Transition(State s1, Inputs i, State s2)
    {
        fromState = s1;
        input = i;
        targetState = s2;
    }
}

public class StateMachine
{
    public State stateIdle;
    public State stateFlee;
    public State stateSearch;
    public State stateFollowA;
    public State stateFollowB;
    public State stateFollowC;
    public State attractionTrapState;
    public State iceTrapState;
    public State electricTrapState;
    public State attackState;

    public State initialState;
    public State currentState;

    public void StartMachine()
    {
        this.currentState = initialState;
        //currentState.Initialize();
    }

    public void StateHandle()
    {
        currentState.Handle();
    }

    public void HandleInput(Inputs i)
    {
        foreach (var transition in transitionList)
        {
            if (transition.input == i && transition.fromState == this.currentState)
            {
                currentState.ExitStateLogic();
                currentState = transition.targetState;
                currentState.EnterStateLogic();
            }
        }
    }

    public List<Transition> transitionList = new List<Transition>();

    public void CreateTransitions()
    {
        //idle to
        transitionList.Add(new Transition(stateIdle, Inputs.PlayerRangeA, stateFollowA));
        transitionList.Add(new Transition(stateIdle, Inputs.PlayerRangeB, stateFollowB));
        transitionList.Add(new Transition(stateIdle, Inputs.PlayerRangeC, stateFollowC));
        transitionList.Add(new Transition(stateIdle, Inputs.IceTrapped, iceTrapState));
        transitionList.Add(new Transition(stateIdle, Inputs.AttractionTrapped, attractionTrapState));
        transitionList.Add(new Transition(stateIdle, Inputs.ElectricTrapped, electricTrapState));
        transitionList.Add(new Transition(stateIdle, Inputs.Death, stateIdle));
        transitionList.Add(new Transition(stateIdle, Inputs.Attack, attackState));
        //rangeC to
        transitionList.Add(new Transition(stateFollowC, Inputs.PlayerRangeB, stateFollowB));
        transitionList.Add(new Transition(stateFollowC, Inputs.PlayerRangeA, stateFollowA));
        transitionList.Add(new Transition(stateFollowC, Inputs.IceTrapped, iceTrapState));
        transitionList.Add(new Transition(stateFollowC, Inputs.AttractionTrapped, attractionTrapState));
        transitionList.Add(new Transition(stateFollowC, Inputs.ElectricTrapped, electricTrapState));
        transitionList.Add(new Transition(stateFollowC, Inputs.Death, stateIdle));
        transitionList.Add(new Transition(stateFollowC, Inputs.Attack, attackState));
        //rangeB to
        transitionList.Add(new Transition(stateFollowB, Inputs.PlayerRangeA, stateFollowA));
        transitionList.Add(new Transition(stateFollowB, Inputs.PlayerRangeC, stateFollowC));
        transitionList.Add(new Transition(stateFollowB, Inputs.IceTrapped, iceTrapState));
        transitionList.Add(new Transition(stateFollowB, Inputs.AttractionTrapped, attractionTrapState));
        transitionList.Add(new Transition(stateFollowB, Inputs.ElectricTrapped, electricTrapState));
        transitionList.Add(new Transition(stateFollowB, Inputs.Death, stateIdle));
        transitionList.Add(new Transition(stateFollowB, Inputs.Attack, attackState));
        //rangeA to
        transitionList.Add(new Transition(stateFollowA, Inputs.PlayerRangeB, stateFollowB));
        transitionList.Add(new Transition(stateFollowA, Inputs.PlayerRangeC, stateFollowC));
        transitionList.Add(new Transition(stateFollowA, Inputs.IceTrapped, iceTrapState));
        transitionList.Add(new Transition(stateFollowA, Inputs.AttractionTrapped, attractionTrapState));
        transitionList.Add(new Transition(stateFollowA, Inputs.ElectricTrapped, electricTrapState));
        transitionList.Add(new Transition(stateFollowA, Inputs.Death, stateIdle));
        transitionList.Add(new Transition(stateFollowA, Inputs.Attack, attackState));
        //attractionTrap to
        transitionList.Add(new Transition(attractionTrapState, Inputs.PlayerRangeA, stateFollowA));
        transitionList.Add(new Transition(attractionTrapState, Inputs.PlayerRangeB, stateFollowB));
        transitionList.Add(new Transition(attractionTrapState, Inputs.PlayerRangeC, stateFollowC));
        transitionList.Add(new Transition(attractionTrapState, Inputs.IceTrapped, iceTrapState));
        transitionList.Add(new Transition(attractionTrapState, Inputs.ElectricTrapped, electricTrapState));
        transitionList.Add(new Transition(attractionTrapState, Inputs.Death, stateIdle));
        transitionList.Add(new Transition(attractionTrapState, Inputs.Attack, attackState));
        //iceTrap to
        transitionList.Add(new Transition(iceTrapState, Inputs.PlayerRangeA, stateFollowA));
        transitionList.Add(new Transition(iceTrapState, Inputs.PlayerRangeB, stateFollowB));
        transitionList.Add(new Transition(iceTrapState, Inputs.PlayerRangeC, stateFollowC));
        transitionList.Add(new Transition(iceTrapState, Inputs.AttractionTrapped, attractionTrapState));
        transitionList.Add(new Transition(iceTrapState, Inputs.ElectricTrapped, electricTrapState));
        transitionList.Add(new Transition(iceTrapState, Inputs.Death, stateIdle));
        transitionList.Add(new Transition(iceTrapState, Inputs.Attack, attackState));
        //electricTrap to
        transitionList.Add(new Transition(electricTrapState, Inputs.PlayerRangeA, stateFollowA));
        transitionList.Add(new Transition(electricTrapState, Inputs.PlayerRangeB, stateFollowB));
        transitionList.Add(new Transition(electricTrapState, Inputs.PlayerRangeC, stateFollowC));
        transitionList.Add(new Transition(electricTrapState, Inputs.IceTrapped, iceTrapState));
        transitionList.Add(new Transition(electricTrapState, Inputs.AttractionTrapped, attractionTrapState));
        transitionList.Add(new Transition(electricTrapState, Inputs.Death, stateIdle));
        transitionList.Add(new Transition(electricTrapState, Inputs.Attack, attackState));
        //attack to
        transitionList.Add(new Transition(attackState, Inputs.PlayerRangeA, stateFollowA));
        transitionList.Add(new Transition(attackState, Inputs.PlayerRangeB, stateFollowB));
        transitionList.Add(new Transition(attackState, Inputs.PlayerRangeC, stateFollowC));
        transitionList.Add(new Transition(attackState, Inputs.IceTrapped, iceTrapState));
        transitionList.Add(new Transition(attackState, Inputs.AttractionTrapped, attractionTrapState));
        transitionList.Add(new Transition(attackState, Inputs.Death, stateIdle));
    }
}
