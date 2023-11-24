using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Controls
{   
    [System.Serializable]
    public class ActionCommandPair
    {
        public InputAction key;
        public BaseCommand val;
    }

    public class InputHandler : MonoBehaviour
    {

        List<ActionCommandPair> actionCommandList = new List<ActionCommandPair>();
        public Dictionary<InputAction, BaseCommand> bindActions = new Dictionary<InputAction, BaseCommand>();
        public Dictionary<BaseCommand, InputAction> reversedBindActions = new Dictionary<BaseCommand, InputAction>();

        GameObject player;

        public static InputHandler instance;

        void Awake()
        {
            if (instance == null)
            {
                instance = this;
                DontDestroyOnLoad(this);
            }
            else
            {
                Destroy(this.gameObject);
            }
        }

        void Start()
        {
            player = GameObject.FindGameObjectWithTag("Player");
        }

        void Update()
        {
            foreach (var action in bindActions)
                action.Value.Execute(action.Key, player);
        }

        void FixedUpdate()
        {
            foreach (var action in bindActions)
                action.Value.FixedExecute(action.Key, player);
        }

        void OnEnable()
        {
            UpdateActionsCommandsBindings();
        }

        void OnDisable()
        {
            foreach (var action in bindActions)
                action.Key.Disable();
        }

        public void UpdateActionsCommandsBindings()
        {
            bindActions.Clear();
            reversedBindActions.Clear();
            foreach (var acp in actionCommandList)
            {
                bindActions[acp.key] = acp.val;
                reversedBindActions[acp.val] = acp.key;
                acp.key.Enable();
            }
        }

        public void UpdateActionsCommandsList(List<ActionCommandPair> aList)
        {
            actionCommandList = aList;
        }
    }
}


