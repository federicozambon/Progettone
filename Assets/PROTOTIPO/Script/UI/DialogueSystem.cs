using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class DialogueSystem : MonoBehaviour
{
    public string[] Dialogues; 
    public Text text;
    int numeroDialoghi = 0;
	
	void Start ()
    {
        SetDialogue(1);
        StartCoroutine(HideDialogue());
	}
	
	IEnumerator HideDialogue()
    {
        yield return new WaitForSeconds(2);
        Hide();    
    }

    void Hide()
    {
        
        text.gameObject.SetActive(false);
    }

    void Active()
    {
        
        text.gameObject.SetActive(true);

        StartCoroutine(HideDialogue());
    }

    void ActiveMultiple()
    {
        
        text.gameObject.SetActive(true);
    }

    public void SetDialogue(int nDialogo)
    {
        
        text.text = Dialogues[nDialogo];
        StartCoroutine(SetMonologue(nDialogo));

    }

    IEnumerator SetMonologue(int Monologue)
    {
        text.text = Dialogues[Monologue];

        yield return new WaitForSeconds(1.5f);

        text.text = Dialogues[0];
    }

    IEnumerator SetArrayDialogue(int[] arrayDialogue)
    {
        ActiveMultiple();

        for (int i = 0; i < arrayDialogue.Length; i++)
        {
            text.text = Dialogues[arrayDialogue[i]];
            yield return new WaitForSeconds(1.5f);
            
        }
        text.text = Dialogues[0];
    }

    public void DialogoMultiplo(int[] arrayD)
    {
        int[] dialogue = arrayD;
        StartCoroutine(SetArrayDialogue(dialogue));
    }
}
