using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Changelnput : MonoBehaviour
{
    EventSystem system;
    public Selectable firstInput;
    public Button subminButton;
    void Start()
    {
        system = EventSystem.current;
        firstInput.Select();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab) && Input.GetKey(KeyCode.LeftShift))
        {
            Selectable previous = system.currentSelectedGameObject.GetComponent<Selectable>().FindSelectableOnUp();
            if (previous != null)
            {
                previous.Select();
            }
        }
        else if (Input.GetKeyUp(KeyCode.Tab))
        {
            Selectable next = system.currentSelectedGameObject.GetComponent<Selectable>().FindSelectableOnDown();
            if (next != null)
            {
                next.Select();
            }
            else if (Input.GetKeyDown(KeyCode.Return))
            {
                subminButton.onClick.Invoke();
                Debug.Log("Button pressed!");
            }
        }
    }
}
