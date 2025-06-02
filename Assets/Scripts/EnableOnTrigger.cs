using UnityEngine;

public class EnableOnTrigger : MonoBehaviour
{
   [SerializeField] private GameObject gObject;
   void OnTriggerEnter(Collider other)
   {
      gObject.SetActive(true);
   }
}
