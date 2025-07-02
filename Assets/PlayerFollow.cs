using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class PlayerFollow : MonoBehaviour
{
    [SerializeField] Transform player;
    [SerializeField] float followSpeed;
    [SerializeField] Vector3 offset;
    void Start()
    {
        
    }

    // Update is called once per frame
    void LateUpdate()
    {
        Vector3 desiredPosition = player.position + offset;
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, followSpeed);
        transform.position = new Vector3(smoothedPosition.x, smoothedPosition.y, transform.position.z);
    }
}
