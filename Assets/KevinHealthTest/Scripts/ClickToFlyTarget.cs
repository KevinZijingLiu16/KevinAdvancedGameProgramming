using System.Collections;
using UnityEngine;

public class ClickToFlyTarget : MonoBehaviour
{
    //just for prototype testing. 
    [Header("Target Positions")]
    public Transform headTarget;  
    public Transform chestTarget; 
    public Transform armTarget;   

    private Vector3 startPosition; 
    private bool isFlying = false;

    private void Start()
    {
        startPosition = transform.position; 
    }

    public void FlyToTarget(int targetType)
    {
        if (isFlying) return; 

        Transform targetPosition = GetTargetTransform(targetType);
        if (targetPosition == null) return;

        isFlying = true;
        StartCoroutine(FlyToAndReturn(targetPosition));
    }

    private IEnumerator FlyToAndReturn(Transform target)
    {
        float speed = 10f;

      
        while (Vector3.Distance(transform.position, target.position) > 0.1f)
        {
            transform.position = Vector3.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
            yield return null;
        }

        yield return new WaitForSeconds(0.2f); 

       
        while (Vector3.Distance(transform.position, startPosition) > 0.1f)
        {
            transform.position = Vector3.MoveTowards(transform.position, startPosition, speed * Time.deltaTime);
            yield return null;
        }

        isFlying = false;
    }

    private Transform GetTargetTransform(int targetType)
    {
        return targetType switch
        {
            0 => headTarget,
            1 => chestTarget,
            2 => armTarget,
            _ => null
        };
    }
}
