using ScriptableObjects;
using UnityEngine;
using UnityEngine.Events;

public class DialogsManager : MonoBehaviour
{
    [SerializeField] private Dialog[] dialogs;
    [SerializeField] private UnityEvent[] events;
    [SerializeField] private NPCManager npcManager;
    
    private int index = 0;

    public void CycleDialog()
    {
        index++;
        if (index < dialogs.Length)
        {
            npcManager.Phrases = dialogs[index];
        }
        
        if (index < events.Length)
        {
            npcManager.OnDialogFinished = events[index];
        }
    }
}
