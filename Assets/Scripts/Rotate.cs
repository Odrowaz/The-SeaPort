using System.Collections;
using Unity.AI.Navigation;
using UnityEngine;
using UnityEngine.AI;

public class Rotate : MonoBehaviour
{
    [SerializeField] private float time = 1f;
    [SerializeField] private Vector3 rotation; // Rotazione finale in eulero
    
    private Quaternion _startRotation;
    private Quaternion _targetRotation;

    public void StartRotation()
    {
        _startRotation = transform.rotation;
        _targetRotation = Quaternion.Euler(rotation);
        StartCoroutine(UpdateRotation());
    }

    private IEnumerator UpdateRotation()
    {
        float elapsed = 0f;

        while (elapsed < time)
        {
            float t = elapsed / time;
            transform.rotation = Quaternion.Slerp(_startRotation, _targetRotation, t);
            elapsed += Time.deltaTime;
            yield return null;
        }

        transform.rotation = _targetRotation;
    }
}
