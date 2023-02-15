using System.Collections;
using UnityEngine;

public class MainPage : MonoBehaviour
{
    // Container to attach quizzes to
    [SerializeField] private GameObject quizTableContent;
    // Prefab of a quiz table entry
    [SerializeField] private GameObject quizEntry;

    // Refreshes the UI with the new game data
    private void RefreshUi(GameData data)
    {
        foreach (Transform child in quizTableContent.transform)
        {
            Destroy(child.gameObject);
        }

        for (int i = 0; i < data.quizzes.Count; i++)
        {
            Quiz currentQuiz = data.quizzes[i];
            GameObject newEntry = Instantiate(quizEntry,
                                              new Vector3(0, 0, 0),
                                              Quaternion.identity,
                                              quizTableContent.transform) as GameObject;
            QuizEntryController entryController = newEntry.GetComponent<QuizEntryController>();
            entryController.RefreshUi(i, currentQuiz);
        }
    }

    void Awake()
    {
        GameManager.instance.AddObserver(data => RefreshUi(data));
    }
}
