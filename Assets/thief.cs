using UnityEngine;
using UnityEngine.InputSystem;

public class Thief : MonoBehaviour
{
    [SerializeField] private float speed = 3f;

    void Update()
    {
        Vector3 direction = Vector3.zero;
        if (Keyboard.current.wKey.isPressed)
            direction.y += 1;
        if (Keyboard.current.sKey.isPressed)
            direction.y -= 1;
        if (Keyboard.current.aKey.isPressed)
            direction.x -= 1;
        if (Keyboard.current.dKey.isPressed)
            direction.x += 1;

        transform.position += direction.normalized * speed * Time.deltaTime;

        // Keep the thief inside the room.
        Vector3 position = transform.position;
        position.x = Mathf.Clamp(position.x, -7f, 7f);
        position.y = Mathf.Clamp(position.y, -4f, 4f);

        transform.position = position;
    }
}