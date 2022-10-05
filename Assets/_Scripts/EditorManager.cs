using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class EditorManager : MonoBehaviour
{
    public static EditorManager instance;
    PlayerAction inputAction;

    public Camera mainCam;
    public Camera editorCam;

    public GameObject prefab1;
    public GameObject prefab2;

    public GameObject item;

    public bool editorMode = false;
    public bool instanciated = false;

    Vector3 mousePos;

    Subject subject = new Subject();

    ICommand command;

    UIManager ui;

    // Start is called before the first frame update
    void Start()
    {
        if(instance == null)
        {
            instance = this;
        }
        inputAction = PlayerInputController.controller.inputAction;

        inputAction.Editor.EnableEditor.performed += cntxt => SwitchCamera();
        inputAction.Editor.AddItem1.performed += cntxt => AddItem(1);
        inputAction.Editor.AddItem2.performed += cntxt => AddItem(2);
        inputAction.Editor.DropItem.performed += cntxt => DropItem();

        mainCam.enabled = true;
        editorCam.enabled = false;

        ui = GetComponent<UIManager>();
    }


    //private void OnEnable()
    //{
    //    inputAction.Editor.Enable();
    //}

    //private void OnDisable()
    //{
    //    inputAction.Editor.Disable();
    //}
    void SwitchCamera()
    {
        mainCam.enabled = !mainCam.enabled;
        editorCam.enabled = !editorCam.enabled;

        ui.ToggleEditorUI();
    }

    private void AddItem(int ItemId)
    {
        if (editorMode&& !instanciated)
        {
            switch (ItemId)
            {
                case 1:
                    item = Instantiate(prefab1);
                    SpikeBall spike1 = new SpikeBall(item, new GreenMat());
                    subject.AddObserver(spike1);
                    break;

                case 2:
                    item = Instantiate(prefab2);
                    SpikeBall spike2 = new SpikeBall(item, new YellowMat());
                    subject.AddObserver(spike2);
                    break;

                default:
                    break;
            }
            subject.Notify();
            instanciated = true;
        }
    }

    void DropItem()
    {
        if (editorMode && instanciated)
        {
            item.GetComponent<Rigidbody>().useGravity = true;
            item.GetComponent<Collider>().enabled = true;

            command = new PlaceItemCommand(item.transform.position, item.transform);
            CommandInvoker.AddCommand(command);

            instanciated = false;

            
        }
    }


    // Update is called once per frame
    void Update()
    {
        if (mainCam.enabled == false && editorCam.enabled == true)
        {
            editorMode = true;
            Time.timeScale = 0;
            Cursor.lockState = CursorLockMode.None;
        }
        else
        {
            editorMode = false;
            Time.timeScale = 1;
            Cursor.lockState = CursorLockMode.Locked;
        }

        if (instanciated)
        {
            mousePos = Mouse.current.position.ReadValue();
            mousePos = new Vector3(mousePos.x, mousePos.y, 10f);

            item.transform.position = editorCam.ScreenToWorldPoint(mousePos);
        }
    }
}
