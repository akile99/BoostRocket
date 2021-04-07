using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{
	[SerializeField] float delay = 1f;
	[SerializeField] AudioClip crashAudio;
	[SerializeField] AudioClip successAudio;
	[SerializeField] ParticleSystem crashParticles;
	[SerializeField] ParticleSystem successParticles;

    AudioSource audioSource;

    bool isTransitioning = false;
    bool collisionDisable = false;
    
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        RespondToDebugKeys();
    }

    void RespondToDebugKeys()
    {
        if (Input.GetKey(KeyCode.R))
        {
            ReloadLevel();
        }

        if (Input.GetKeyDown(KeyCode.L))
        {
            LoadNextLevel();
        }

        if (Input.GetKeyDown(KeyCode.C))
        {
            collisionDisable = !collisionDisable;
        }
    }

    void OnCollisionEnter(Collision other)
	{
		if (isTransitioning) { return; }
        if (collisionDisable) { return; }

		switch (other.gameObject.tag)
		{
			case "Start":
				// Debug.Log("You are at the start");
				break;
			case "Finish":
				StartNextLevelSequence();
				break;
			default:
				StartCrashSequence();
				break;
		}
	}

	void StartCrashSequence()
 	{
 		isTransitioning = true;
        crashParticles.Play();
 		audioSource.Stop();
		StopMovement();
		audioSource.PlayOneShot(crashAudio);
		Invoke("ReloadLevel", delay);
	}

	void StartNextLevelSequence()
	{
		isTransitioning = true; 
        successParticles.Play();		
		audioSource.Stop();
		StopMovement();
		audioSource.PlayOneShot(successAudio);
		Invoke("LoadNextLevel", delay);
	}

	void ReloadLevel()
    {
    	int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);
        // isTransitioning = false;
    }

    void LoadNextLevel()
    {
    	int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        int nextSceneIndex = currentSceneIndex + 1;
        if (nextSceneIndex == SceneManager.sceneCountInBuildSettings)
        {
        	nextSceneIndex = 0;
        }
        SceneManager.LoadScene(nextSceneIndex);
        // isTransitioning = false;
    }

    void HandleLevel(int level)
    {
    	int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex + level);
    }

    void StopMovement()
    {
    	GetComponent<Movement>().enabled = false;
    }
}
