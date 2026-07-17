using UnityEngine; // Lets us use Unity's tools, like moving things around

public class PlayerMove : MonoBehaviour // Our script's name. It gets attached to the player
{
    public float speed = 5f;      // How fast the player walks
    public float turnSpeed = 12f; // How quickly the player spins to face a new way

    private CharacterController controller; // The thing that actually moves the player and stops it from walking through walls
    private float gravity = -20f;   // How hard the player gets pulled down. Negative means "downward"
    private float fallSpeed = 0f;   // How fast we're falling right now

    void Start() // Runs one time, when the game begins
    {
        controller = GetComponent<CharacterController>(); // Go find the CharacterController on the player and remember it
    }

    void Update() // Runs over and over, every single frame
    {
        float x = Input.GetAxis("Horizontal"); // Left/right keys. -1 is left, 1 is right, 0 is nothing
        float z = Input.GetAxis("Vertical");   // Forward/back keys. -1 is back, 1 is forward, 0 is nothing

        Vector3 move = new Vector3(x, 0f, z); // Turn those two numbers into a direction. The 0 means "no up or down yet"

        if (move.magnitude > 0.1f) // Only turn if we're actually pushing a key. Tiny wiggles don't count
        {
            Quaternion look = Quaternion.LookRotation(move); // Figure out which way we SHOULD be facing
            transform.rotation = Quaternion.Slerp(transform.rotation, look,
                turnSpeed * Time.deltaTime); // Smoothly spin toward that direction instead of snapping instantly
        }

        if (controller.isGrounded) // Are our feet touching the ground?
        {
            fallSpeed = -1f; // Yes! Push down just a tiny bit so we stay stuck to the floor
        }
        else
        {
            fallSpeed = fallSpeed + gravity * Time.deltaTime; // No! Fall faster and faster, like a real drop
        }

        Vector3 final = move * speed; // Take our direction and make it as fast as our speed says
        final.y = fallSpeed;          // Add the falling part into the up/down slot

        controller.Move(final * Time.deltaTime); // Actually move! Time.deltaTime keeps it fair on fast and slow computers
    }
}