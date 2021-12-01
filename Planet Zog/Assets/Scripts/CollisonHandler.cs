
using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisonHandler : MonoBehaviour
{
    [SerializeField] float delayLevel = 1f;
    [SerializeField] AudioClip finishClip;
    [SerializeField] AudioClip crashClip;

    bool isTransitioning = false;

    AudioSource source;
    bool collEnabled = true;

    void Start()
    {
        source = GetComponent<AudioSource>();
    }
    void Update()
    {
        DebugKey();
    }
    void OnCollisionEnter(Collision coll)
    {
        if (collEnabled == true)
        {
            if (isTransitioning) { return; }
            switch (coll.gameObject.tag)
            {
                case ("Friendly"):
                    Debug.Log("Collided into a friendly GameObject");
                    break;
                case ("Finish"):
                    isTransitioning = true;
                    source.PlayOneShot(finishClip);
                    Invoke("LoadNextLevel", delayLevel);
                    break;
                default:
                    source.PlayOneShot(crashClip);
                    isTransitioning = true;
                    StartCrashSequence();
                    break;

            }
        }
    }
    void RestartLevel()
    {
        Debug.Log("Restarting Level");
        int sceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(sceneIndex);
    }
    void LoadNextLevel()
    {
        Debug.Log("Loading Next Level");
        int sceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(sceneIndex + 1);
    }
    void StartCrashSequence()
    {
      
        Invoke("RestartLevel", delayLevel);
        GetComponent<Movement>().enabled = false;
    }
    void DebugKey()
    {
        DebugKeyCheck();
        void DebugKeyCheck()
        {
            if (Input.GetKeyDown(KeyCode.L))
            {
                LoadNextLevel();
            }
            if (Input.GetKeyDown(KeyCode.C))
            {
                CollisionEnabler();
            }
        }
        void CollisionEnabler()
        {
            if (!collEnabled) { collEnabled = true; }
            else { collEnabled = false; }
            Debug.Log(collEnabled);
        }
    }


}
