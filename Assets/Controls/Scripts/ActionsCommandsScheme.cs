using System.Collections.Generic;
using UnityEngine;

namespace Controls
{
    [CreateAssetMenu(menuName = "Controls/ActionsCommandsScheme")]
    public class ActionsCommandsScheme : ScriptableObject
    {
        public List<ActionCommandPair> actionCommandList;
    }
}
