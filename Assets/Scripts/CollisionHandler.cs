using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{
	[SerializeField] float delay = 1f;

	void OnCollisionEnter(Collision other)
	{
		switch (other.gameObject.tag)
		{
			case "Start":
				Debug.Log("You are at the start");
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
		StopMovement();
		Invoke("ReloadLevel", delay);
	}

	void StartNextLevelSequence()
	{
		StopMovement();
		Invoke("LoadNextLevel", delay);
	}

	void ReloadLevel()
    {
    	int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);
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
