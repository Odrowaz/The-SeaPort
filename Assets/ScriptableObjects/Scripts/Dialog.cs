using UnityEngine;

namespace ScriptableObjects
{
    [CreateAssetMenu(fileName = "Dialog", menuName = "ScriptableObjects/Dialog", order = 0)]
    public class Dialog : ScriptableObject
    {
        [SerializeField] private string[] dialogs;
        
        public string[] Dialogs {get => dialogs;}
    }
}