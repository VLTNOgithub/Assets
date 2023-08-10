using UnityEngine;

public class LocalTimeController : MonoBehaviour
{
    public float rewindSpeed = 0.5f; // Speed at which the object rewinds (0.5f means half speed)

    public Material rewindMaterial;

    private LocalTimeRecorder selectedRecorder;

    private void Update()
    {
        // Rewind when "R" key is pressed
        if (Input.GetKeyDown(KeyCode.R))
        {
            RaycastHit hit;
            if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit))
            {
                LocalTimeRecorder timeRecorder = hit.transform.GetComponent<LocalTimeRecorder>();
                if (timeRecorder != null)
                {
                    selectedRecorder = timeRecorder;
                    StartRewindTime();
                }
            }
        }

        // Stop rewinding when "R" key is released
        if (Input.GetKeyUp(KeyCode.R))
        {
            StopRewindTime();
        }
    }

    private void StartRewindTime()
    {
        if (selectedRecorder != null)
        {
            selectedRecorder.rewindSpeed = rewindSpeed;
            selectedRecorder.StartRewind(rewindMaterial);
        }
    }

    private void StopRewindTime()
    {
        if (selectedRecorder != null)
        {
            selectedRecorder.StopRewind();
            selectedRecorder = null;
        }
    }
}
