using UnityEngine;
using UnityEngine.UI;
using NB.ColorExt;

public class NBColorExtExample : MonoBehaviour {
    public Slider RedSlider;
    public InputField RedInput;

    public Slider GreenSlider;
    public InputField GreenInput;

    public Slider BlueSlider;
    public InputField BlueInput;

    public InputField HexInput;

    public Slider HueSlider;
    public InputField HueInput;

    public Slider SaturationSlider;
    public InputField SaturationInput;

    public Slider VibranceSlider;
    public InputField VibranceInput;

    public Image ColorImage;
     
    //Private color
    Color _mainColor;
    

    //Set a default color and refresh the UI
    //On Start
    void Start() {
        _mainColor = Color.red;
        RefreshUI();
    }


    //Updates the UI elements from the current _mainColor values
    void RefreshUI() {        
        ColorImage.color = _mainColor;        

        //Set HSV Sliders
        UpdateSliderValue(HueSlider, _mainColor.Hue());
        UpdateSliderValue(SaturationSlider, _mainColor.Saturation());
        UpdateSliderValue(VibranceSlider, _mainColor.Vibrance());

        //Set Hex Field
        UpdateHexField();

        //Set RGB Sliders
        UpdateSliderValue(RedSlider, _mainColor.r);
        UpdateSliderValue(GreenSlider, _mainColor.g);
        UpdateSliderValue(BlueSlider, _mainColor.b);        

        //Update Texts
        RedInput.text = Mathf.RoundToInt(_mainColor.r * 255.0f).ToString();
        GreenInput.text = Mathf.RoundToInt(_mainColor.g * 255.0f).ToString();
        BlueInput.text = Mathf.RoundToInt(_mainColor.b * 255.0f).ToString();

        HueInput.text = Mathf.RoundToInt(_mainColor.Hue()).ToString();
        SaturationInput.text = Mathf.RoundToInt(_mainColor.Saturation() * 100f).ToString();
        VibranceInput.text = Mathf.RoundToInt(_mainColor.Vibrance() * 100f).ToString();        
    }

    //Callback attached to RGB sliders
    public void OnRGBSlidersChanged() {
        //Update Main Color From RGB Sliders        
        _mainColor.r = RedSlider.value;
        _mainColor.g = GreenSlider.value;
        _mainColor.b = BlueSlider.value;

        RefreshUI();    
    }
    
    //Callback attached to HSV sliders
    public void OnHSVSlidersChanged() {
        //Update Main Color From HSV Sliders           
        _mainColor = _mainColor.FromHSV(HueSlider.value, SaturationSlider.value, VibranceSlider.value);

        RefreshUI();
    }

    //Callback attached to HexInputField
    public void OnHexFieldEditComplete() {        
        _mainColor = _mainColor.FromHexStr(HexInput.text);

        RefreshUI();
    }

    //Utility function to update slider from code without
    //triggering the callback
    Slider.SliderEvent emptySliderEvent = new Slider.SliderEvent();
    void UpdateSliderValue(Slider s, float v) {
        var originalEvent = s.onValueChanged;
        s.onValueChanged = emptySliderEvent;
        s.value = v;
        s.onValueChanged = originalEvent;
    }

    InputField.OnChangeEvent emptyInputChangeEvent = new InputField.OnChangeEvent();
    void UpdateHexField() {
        var originalEvent = HexInput.onValueChanged;
        HexInput.onValueChanged = emptyInputChangeEvent;
        HexInput.text = "#" + _mainColor.ToHexStr();
        HexInput.onValueChanged = originalEvent;
    }
}