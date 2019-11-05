using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using XUEventUGUI;
using System.Collections;
using System;
using Random = UnityEngine.Random;

public class XUEventHandlerTest : MonoBehaviour
{
    public Image image;
    public Button button;
    public Toggle toggle;
    public Slider slider;
    public Scrollbar scrollbar;
    public Dropdown dropdown;
    public InputField inputField;

    private void Awake()
    {
        XUEventHandler.eventDelegate = OnUIEvent;
        XUEventHandler.eventDelegateXU = OnUIEventXU;
    }

    IEnumerator Start()
    {
        image.InsertEvent(EventTriggerType.PointerClick, "imageClick", "image");
        image.InsertEvent(EventTriggerType.PointerUp, "imageUp", "image");

        button.InsertEvent(EventTriggerType.PointerClick, "buttonPointerClick", "button");
        button.InsertEventClick("buttonClick", "button");

        toggle.InsertEventChange("toggleClick", "toggle");
        toggle.InsertEvent(EventTriggerType.PointerClick, "togglePointerClick", "toggle");

        slider.InsertEvent(EventTriggerType.PointerClick, "sliderPointerClick", "slider");
        slider.InsertEventChange("sliderClick", "slider");

        scrollbar.InsertEventChange("scrollbarClick", "scrollbar");
        scrollbar.InsertEvent(EventTriggerType.PointerClick, "scrollbarPointerClick", "scrollbar");

        dropdown.InsertEventChange("dropdownClick", "dropdown");
        dropdown.InsertEvent(EventTriggerType.PointerClick, "dropdownPointerClick", "dropdown");

        inputField.gameObject.GetComponent<Image>().InsertEvent(EventTriggerType.PointerDown, "inputFieldPointerDown", "inputField");
        inputField.InsertEventChange("inputFieldChange", "inputField");
        inputField.InsertEventSubmit("inputFieldSubmit", "inputField");
        inputField.gameObject.GetComponent<Image>().InsertEvent(EventTriggerType.PointerClick, "inputFieldPointerClick", "inputField");


        while (true)
        {
            if (button != null)
            {
                string newButtonName = "Button" + Random.Range(2, 50);
                Transform newButton = transform.Find(newButtonName);
                if (newButton == null)
                {
                    newButton = GameObject.Instantiate(button).transform;
                    newButton.name = newButtonName;
                    newButton.SetParent(transform);
                    newButton.localPosition = Vector3.zero;
                    newButton.localScale = Vector3.one;
                }
                int random = Random.Range(1, 4);
                switch (random)
                {
                    case 1:
                        string strEvent = "EVT_BUTTON_CLICK_" + Random.Range(1, 300000);
                        object objParam = null;
                        int randomParam = Random.Range(1, 3);
                        switch (randomParam)
                        {
                            case 1:
                                objParam = Random.Range(1, 1000);
                                break;
                            case 2:
                                objParam = Random.Range(1, 1000).ToString();
                                break;
                            case 3:
                                objParam = Random.Range(1.0f, 1000.0f);
                                break;
                        }
                        if (Random.Range(1, 3) == 1)
                        {
                            newButton.GetComponent<Button>().InsertEvent((EventTriggerType)Random.Range(0, Enum.GetValues(typeof(EventTriggerType)).Length), strEvent, objParam);
                        }
                        else
                        {
                            newButton.GetComponent<Button>().InsertEventClick(strEvent, objParam);
                        }
                        break;
                    case 2:
                        if (Random.Range(1, 3) == 1)
                        {
                            newButton.GetComponent<Button>().DeleteEvent((EventTriggerType)Random.Range(0, Enum.GetValues(typeof(EventTriggerType)).Length));
                        }
                        else
                        {
                            newButton.GetComponent<Button>().DeleteEventClick();
                        }
                        break;
                    case 3:
                        Destroy(newButton.gameObject);
                        break;
                }
            }
            yield return Random.Range(1, 300);
            if (toggle != null)
            {
                string newToggleName = "Toggle" + Random.Range(2, 50);
                Transform newToggle = transform.Find(newToggleName);
                if (newToggle == null)
                {
                    newToggle = GameObject.Instantiate(toggle).transform;
                    newToggle.name = newToggleName;
                    newToggle.SetParent(transform);
                    newToggle.localPosition = Vector3.zero;
                    newToggle.localScale = Vector3.one;
                }
                int random = Random.Range(1, 4);
                switch (random)
                {
                    case 1:
                        string strEvent = "EVT_Toggle_CLICK_" + Random.Range(1, 300000);
                        object objParam = null;
                        int randomParam = Random.Range(1, 3);
                        switch (randomParam)
                        {
                            case 1:
                                objParam = Random.Range(1, 1000);
                                break;
                            case 2:
                                objParam = Random.Range(1, 1000).ToString();
                                break;
                            case 3:
                                objParam = Random.Range(1.0f, 1000.0f);
                                break;
                        }
                        if (Random.Range(1, 3) == 1)
                        {
                            newToggle.GetComponent<Toggle>().InsertEvent((EventTriggerType)Random.Range(0, Enum.GetValues(typeof(EventTriggerType)).Length), strEvent, objParam);
                        }
                        else
                        {
                            newToggle.GetComponent<Toggle>().InsertEventChange(strEvent, objParam);
                        }
                        break;
                    case 2:
                        if (Random.Range(1, 3) == 1)
                        {
                            newToggle.GetComponent<Toggle>().DeleteEvent((EventTriggerType)Random.Range(0, Enum.GetValues(typeof(EventTriggerType)).Length));
                        }
                        else
                        {
                            newToggle.GetComponent<Toggle>().DeleteEventChange();
                        }
                        break;
                    case 3:
                        Destroy(newToggle.gameObject);
                        break;
                }
            }
            yield return Random.Range(1, 300);
            if (slider != null)
            {
                string newSliderName = "Slider" + Random.Range(2, 50);
                Transform newSlider = transform.Find(newSliderName);
                if (newSlider == null)
                {
                    newSlider = GameObject.Instantiate(slider).transform;
                    newSlider.name = newSliderName;
                    newSlider.SetParent(transform);
                    newSlider.localPosition = Vector3.zero;
                    newSlider.localScale = Vector3.one;
                }
                int random = Random.Range(1, 4);
                switch (random)
                {
                    case 1:
                        string strEvent = "EVT_Slider_CLICK_" + Random.Range(1, 300000);
                        object objParam = null;
                        int randomParam = Random.Range(1, 3);
                        switch (randomParam)
                        {
                            case 1:
                                objParam = Random.Range(1, 1000);
                                break;
                            case 2:
                                objParam = Random.Range(1, 1000).ToString();
                                break;
                            case 3:
                                objParam = Random.Range(1.0f, 1000.0f);
                                break;
                        }
                        if (Random.Range(1, 3) == 1)
                        {
                            newSlider.GetComponent<Slider>().InsertEvent((EventTriggerType)Random.Range(0, Enum.GetValues(typeof(EventTriggerType)).Length), strEvent, objParam);
                        }
                        else
                        {
                            newSlider.GetComponent<Slider>().InsertEventChange(strEvent, objParam);
                        }
                        break;
                    case 2:
                        if (Random.Range(1, 3) == 1)
                        {
                            newSlider.GetComponent<Slider>().DeleteEvent((EventTriggerType)Random.Range(0, Enum.GetValues(typeof(EventTriggerType)).Length));
                        }
                        else
                        {
                            newSlider.GetComponent<Slider>().DeleteEventChange();
                        }
                        break;
                    case 3:
                        Destroy(newSlider.gameObject);
                        break;
                }
            }
            yield return Random.Range(1, 300);
            if (scrollbar != null)
            {
                string newScrollbarName = "Scrollbar" + Random.Range(2, 50);
                Transform newScrollbar = transform.Find(newScrollbarName);
                if (newScrollbar == null)
                {
                    newScrollbar = GameObject.Instantiate(scrollbar).transform;
                    newScrollbar.name = newScrollbarName;
                    newScrollbar.SetParent(transform);
                    newScrollbar.localPosition = Vector3.zero;
                    newScrollbar.localScale = Vector3.one;
                }
                int random = Random.Range(1, 4);
                switch (random)
                {
                    case 1:
                        string strEvent = "EVT_Scrollbar_CLICK_" + Random.Range(1, 300000);
                        object objParam = null;
                        int randomParam = Random.Range(1, 3);
                        switch (randomParam)
                        {
                            case 1:
                                objParam = Random.Range(1, 1000);
                                break;
                            case 2:
                                objParam = Random.Range(1, 1000).ToString();
                                break;
                            case 3:
                                objParam = Random.Range(1.0f, 1000.0f);
                                break;
                        }
                        if (Random.Range(1, 3) == 1)
                        {
                            newScrollbar.GetComponent<Scrollbar>().InsertEvent((EventTriggerType)Random.Range(0, Enum.GetValues(typeof(EventTriggerType)).Length), strEvent, objParam);
                        }
                        else
                        {
                            newScrollbar.GetComponent<Scrollbar>().InsertEventChange(strEvent, objParam);
                        }
                        break;
                    case 2:
                        if (Random.Range(1, 3) == 1)
                        {
                            newScrollbar.GetComponent<Scrollbar>().DeleteEvent((EventTriggerType)Random.Range(0, Enum.GetValues(typeof(EventTriggerType)).Length));
                        }
                        else
                        {
                            newScrollbar.GetComponent<Scrollbar>().DeleteEventChange();
                        }
                        break;
                    case 3:
                        Destroy(newScrollbar.gameObject);
                        break;
                }
            }
            yield return Random.Range(1, 300);
            if (dropdown != null)
            {
                string newDropdownName = "Dropdown" + Random.Range(2, 50);
                Transform newDropdown = transform.Find(newDropdownName);
                if (newDropdown == null)
                {
                    newDropdown = GameObject.Instantiate(dropdown).transform;
                    newDropdown.name = newDropdownName;
                    newDropdown.SetParent(transform);
                    newDropdown.localPosition = Vector3.zero;
                    newDropdown.localScale = Vector3.one;
                }
                int random = Random.Range(1, 4);
                switch (random)
                {
                    case 1:
                        string strEvent = "EVT_Dropdown_CLICK_" + Random.Range(1, 300000);
                        object objParam = null;
                        int randomParam = Random.Range(1, 3);
                        switch (randomParam)
                        {
                            case 1:
                                objParam = Random.Range(1, 1000);
                                break;
                            case 2:
                                objParam = Random.Range(1, 1000).ToString();
                                break;
                            case 3:
                                objParam = Random.Range(1.0f, 1000.0f);
                                break;
                        }
                        XUEventHandler.InsertEventChange(newDropdown.GetComponent<Dropdown>(), strEvent, objParam);
                        break;
                    case 2:
                        XUEventHandler.DeleteEventChange(newDropdown.GetComponent<Dropdown>());
                        break;
                    case 3:
                        Destroy(newDropdown.gameObject);
                        break;
                }
            }
            yield return Random.Range(1, 300);
            if (inputField != null)
            {
                string newInputFieldName = "InputField" + Random.Range(2, 50);
                Transform newInputField = transform.Find(newInputFieldName);
                if (newInputField == null)
                {
                    newInputField = GameObject.Instantiate(inputField).transform;
                    newInputField.name = newInputFieldName;
                    newInputField.SetParent(transform);
                    newInputField.localPosition = Vector3.zero;
                    newInputField.localScale = Vector3.one;
                }
                int random = Random.Range(1, 4);
                switch (random)
                {
                    case 1:
                        string strEvent = "EVT_InputField_CLICK_" + Random.Range(1, 300000);
                        object objParam = null;
                        int randomParam = Random.Range(1, 3);
                        switch (randomParam)
                        {
                            case 1:
                                objParam = Random.Range(1, 1000);
                                break;
                            case 2:
                                objParam = Random.Range(1, 1000).ToString();
                                break;
                            case 3:
                                objParam = Random.Range(1.0f, 1000.0f);
                                break;
                        }
                        if (Random.Range(1, 3) == 1)
                        {
                            XUEventHandler.InsertEventChange(newInputField.GetComponent<InputField>(), strEvent, objParam);
                        }
                        else
                        {
                            XUEventHandler.InsertEventSubmit(newInputField.GetComponent<InputField>(), strEvent, objParam);
                        }
                        break;
                    case 2:
                        if (Random.Range(1, 3) == 1)
                        {
                            XUEventHandler.DeleteEventChange(newInputField.GetComponent<InputField>());
                        }
                        else
                        {
                            XUEventHandler.DeleteEventSubmit(newInputField.GetComponent<InputField>());
                        }
                        break;
                    case 3:
                        Destroy(newInputField.gameObject);
                        break;
                }
            }
            yield return Random.Range(1, 300);
        }
    }

    void OnUIEvent(EventTriggerType eventType, string strEvent, object objParam, GameObject sender, BaseEventData eventData)
    {
        Debug.Log("OnUIEvent = " + eventType + "  " + strEvent + "  " + objParam + "  " + sender.name + "  " + eventData);
    }

    void OnUIEventXU(XUEventType eventType, string strEvent, object objParam, UIBehaviour sender, object senderParam)
    {
        Debug.Log("OnUIEventXU = " + eventType + "  " + strEvent + "  " + objParam + "  " + sender.name + "(" + sender.GetType() + ")  " + senderParam);
    }
}
