using UnityEngine; // Lets us use Unity's tools

public class CameraFollow : MonoBehaviour // Our script's name. It gets attached to the camera
{
    public Transform target; // Who the camera should chase. Drag the player in here!
    public Vector3 offset = new Vector3(0f, 12f, -9f); // Where the camera sits compared to the player: 12 up and 9 behind
    public float smooth = 5f; // How gently the camera catches up. Bigger number = quicker

    void LateUpdate() // Runs every frame, but AFTER the player has moved. That way the camera never chases an old spot
    {
        if (target == null) // Did we forget to drag the player in?
        {
            return; // Then stop right here, or the game will complain
        }

        Vector3 want = target.position + offset; // The spot the camera WISHES it was at: the player's spot, plus our up-and-behind numbers

        transform.position = Vector3.Lerp(transform.position, want,
            smooth * Time.deltaTime); // Glide a little bit toward that spot each frame instead of teleporting. That's what makes it feel smooth

        transform.LookAt(target.position); // Always point the camera right at the player
    }
}