using System;
using System.Collections.Generic;
using System.Text;
using Stateless;

namespace StalelessDemo
{
    class FsmOnOff
    {
        #region State & Trigger Declaration

        public const string ON_State = "ON";
        public const string OFF_State = "OFF";

        public const char SWITCH_ON_Trigger = '=';
        public const char SWITCH_OFF_Trigger = '-';

        #endregion

        private readonly StateMachine<string, char> _stateMachine;

        private bool _isOnReentry;

        public FsmOnOff(string initialState)
        {
            _stateMachine = new StateMachine<string, char>(initialState);

            ConfigStateMachine();
        }

        private void ConfigStateMachine()
        {
            _stateMachine.Configure(ON_State)
                .Permit(SWITCH_OFF_Trigger, OFF_State)
                .InternalTransition(SWITCH_ON_Trigger, transition => { _isOnReentry = true; })
                .OnExit(() => { _isOnReentry = false; })
                ;

            _stateMachine.Configure(OFF_State)
                .Permit(SWITCH_ON_Trigger, ON_State)
                .InternalTransition(SWITCH_OFF_Trigger, transition => { _isOnReentry = true; })
                .OnExit(() => { _isOnReentry = false; })
                ;
        }

        public string DisplayCurrentState()
        {
            if (_isOnReentry)
            {
                return $"\n[Switch is already in state [{_stateMachine.State}]!";
            }

            return $"\n[Switch is in state {_stateMachine.State}]";
        }

        public void Input(char inputKey)
        {
            //NOTE: should use if(_stateMachine.CanFire(inputKey)) to check if it is valid input.

            _stateMachine.Fire(inputKey);
        }

    }
}
