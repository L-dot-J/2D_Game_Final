using Lana;
using UnityEditorInternal;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndPoint : MonoBehaviour
{
    [SerializeField] private string nextScene;
    [SerializeField] private string curScene;

    private GameMenager gameMenager;
    [SerializeField] PlayerController playerController;
    void Start()
    {
      gameMenager = FindAnyObjectByType<GameMenager>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
      int potions = playerController.GetPotionCollected();
      if(potions == 5)
      {
        EndLevel();
      }
    }

    public void EndLevel()
   {
     gameMenager.LoadAndUnloadScene(nextScene, curScene);
   }
}
