using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using KevinCastejon.FiniteStateMachine;

namespace Born2Fish
{
    public class FishAbstractState : AbstractState
    {
        public FeeshManager fmanRef;
    }

    public class PlayerSM : AbstractFiniteStateMachine
    {
        public FeeshManager FManagerRef;
        // Declaring the states Enum
        public enum PlayerState
        {
            Idle,
            Fishing,
            Catching
        }
        // Before Start
        public void Init()
        {
            // Initializes the state machine with the default state enum, then all the states (order does not matter)
            Init(PlayerState.Idle,
                AbstractState.Create<IdleState, PlayerState>(PlayerState.Idle, this),
                AbstractState.Create<FishingState, PlayerState>(PlayerState.Fishing, this),
                AbstractState.Create<CatchingState, PlayerState>(PlayerState.Catching, this)
            );
            
            foreach (FishAbstractState state in GetStates())
            {
                switch (state)
                {
                    case FishAbstractState fish:
                    {
                        fish.fmanRef = FManagerRef;
                        break;
                    }
                    default:
                    {
                        Debug.LogError("Unhandled fish type of thing!");
                        break;
                    }
                }
            }
        }

        // Declaring the states
        public class IdleState : FishAbstractState
        {
            // Will be called on state entry
            public override void OnEnter()
            {
            }
            // Will be called on state update
            public override void OnUpdate()
            {
                foreach (Feesh spot in fmanRef.FSpots)
                {
                    if (spot.InRange)
                    {

                        if (Input.GetKeyDown(KeyCode.E))
                        {
                            fmanRef.LookAtFish = true;
                            fmanRef.TurnToFish = spot.transform;
                            TransitionToState(PlayerState.Fishing);
                            //Debug.Log("Caught ya a big'un!");
                        }


                    }

                }
                
              
            }
            // Will be called on state exit
            public override void OnExit()
            {
            }
        }
        public class FishingState : FishAbstractState
        {
            public override void OnEnter()
            {
                Debug.Log("You're is fishin!");
            }
            public override void OnUpdate()
            {
               // if (Input.anyKeyDown)
               // {
               //     TransitionToState(PlayerState.Catching);
               // }
            }
            public override void OnExit()
            {
            }
        }
        public class CatchingState : FishAbstractState
        {
            public override void OnEnter()
            {
            }
            public override void OnUpdate()
            {
               // if (Input.anyKeyDown)
                //{
                //    TransitionToState(PlayerState.Idle);
                //}
            }
            public override void OnExit()
            {
            }
        }
    }
}