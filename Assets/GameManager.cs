using UnityEngine;
using TMPro;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject exhibit;
    [SerializeField] private GameObject exit;
    [SerializeField] private TMP_Text gameText;
    [SerializeField] private GameObject guardVision;
    private bool hasExhibit = false;
    private bool gameEnded = false;

    void Update()
    {
        if (gameEnded)
        {
            // Restart the current scene when R is pressed.
            if (Keyboard.current.rKey.wasPressedThisFrame)
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            }
            return;
        }

        //stealing the exhibit
        if (!hasExhibit && Keyboard.current.eKey.wasPressedThisFrame) 
        {
            float distance = Vector3.Distance(player.transform.position, exhibit.transform.position);

            if (distance <= 0.5f)
            {
                hasExhibit = true;
                exhibit.SetActive(false);
                gameText.text = "Exhibit stolen! Get to the exit.";
            }
        }


        // Win only after stealing the exhibit and reaching the exit.
        if (hasExhibit && Vector3.Distance(player.transform.position, exit.transform.position) <= 0.25f)
        {
            gameEnded = true;
            gameText.text = "You win! You escaped with the exhibit.";

            // Stop the player's movement after winning.
            player.GetComponent<Thief>().enabled = false;
        }

        // Check whether the player's center is inside the guard's vision.
        Vector3 localPosition =guardVision.transform.InverseTransformPoint(player.transform.position);

        float x = localPosition.x;
        float y = localPosition.y;

        // The triangle becomes narrower toward its tip.
        if (y >= -0.5f && y <= 0.5f && Mathf.Abs(x) <= (0.5f - y) / 2f)
        {
            gameEnded = true;
            gameText.text = "Caught! You lose. Press R to restart.";
            player.GetComponent<Thief>().enabled = false;
            return;
        }
    }


}