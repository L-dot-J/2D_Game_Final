using UnityEditorInternal;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndPoint : MonoBehaviour
{
    [SerializeField] private string nextScene;
    [SerializeField] private string curScene;

    private GameMenager gameMenager;
    void Start()
    {
      gameMenager = FindAnyObjectByType<GameMenager>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
      EndLevel();
    }

    public void EndLevel()
   {
     gameMenager.LoadAndUnloadScene(nextScene, curScene);
   }
}
