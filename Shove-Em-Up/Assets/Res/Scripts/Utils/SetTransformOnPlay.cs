using UnityEngine;

public class SetTransformOnPlay : MonoBehaviour
{
    [SerializeField] private Transform initTransform;

    void Awake() {
        transform.SetPositionAndRotation(initTransform.position, initTransform.rotation);
    }
}
