using UnityEngine;
using UnityEngine.UI;

/*
 * UI Component that changes the width of _barRect in accordance with provided value.
 */
public class ProgressBarComponent : MonoBehaviour
{
    #region public fields and properties
    #endregion

    #region protected and private fields and properties
    [SerializeField] private float _minWidth;
    [SerializeField] private float _maxWidth;
    [SerializeField] private RectTransform _barRect;
    [SerializeField] private Gradient _hpGradient;
    [SerializeField] private Image _image;
    #endregion

    #region Initializing
    #endregion

    #region public methods
    /// <summary>
    /// Set value of progress bar
    /// </summary>
    /// <param name="value">0..1</param>
    public void SetValue(float value)
    {
        var size = _barRect.sizeDelta;
        size.x = (_maxWidth - _minWidth) * value + _minWidth;
        _barRect.sizeDelta = size;

        _image.color = _hpGradient.Evaluate(value);
    }
    #endregion

    #region protected and private methods
    #endregion
}

