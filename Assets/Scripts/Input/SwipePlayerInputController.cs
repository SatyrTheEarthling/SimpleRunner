using UnityEngine;
using UnityEngine.EventSystems;
using Zenject;

/*
 * PlayerInputController for mobile devises.
 * Implements IBeginDragHandler, IDragHandler, IEndDragHandler to handling swipes.
 * There to "thresholds".
 * LongMovementLength for long movement. If players wants to move from left edge to right one, he should make long horizontal swipe. 
 * If current swap (drag) longer than this threshold - rest of drag not interesting to as - we start to moving.
 * And controller ignores tiny (finished) swipes that is shorter than second threshold MinMovementLength.
 * 
 * This thresholds defined in relative to Screen.width mesurments. So MinMovementLength = 0.1 says that swipe should be longer then 10% of screen width.
 * Not optimal solution, should be rewrited to define this values in physical mesurments. Swipe length is depends on finger length so have similar size for different players.
 */
public class SwipePlayerInputController : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    [Inject] private SwipeInputSettings _swipeSettings;
    [Inject] private SignalBus _signalBus;

    private bool _isDraged = false;
    private Vector2 _startDragPos;

    private float _longMovementLengthSqr = 0;
    private float _minMovementLengthSqr = 0;

    private void Start()
    {
        _longMovementLengthSqr = _swipeSettings.LongMovementLength * _swipeSettings.LongMovementLength;
        _minMovementLengthSqr = _swipeSettings.MinMovementLength * _swipeSettings.MinMovementLength;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        _isDraged = true;
        _startDragPos = eventData.position;
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (!_isDraged)
            return;

        var delta = eventData.position - _startDragPos;
        var relativeDelta = delta / Screen.width;

        if (relativeDelta.sqrMagnitude > _longMovementLengthSqr)
        {
            ProcessSwipe(relativeDelta);
            return;
        }

        if (relativeDelta.sqrMagnitude > _minMovementLengthSqr && relativeDelta.y > Mathf.Abs(relativeDelta.x))
            ProcessSwipe(relativeDelta);
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        _isDraged = false;

        var delta = eventData.position - _startDragPos;
        var relativeDelta = delta / Screen.width;

        if (relativeDelta.sqrMagnitude < _minMovementLengthSqr)
            return;

        ProcessSwipe(relativeDelta);
    }

    private void ProcessSwipe(Vector2 relativeDelta)
    {
        var movement = ParseSwipeDelta(relativeDelta);

        _signalBus.Fire(new PlayerInputSignals.NewAction(movement));
    }

    private Vector2Int ParseSwipeDelta(Vector2 relativeDelta)
    {
        if (relativeDelta.y > Mathf.Abs(relativeDelta.x))
            return Vector2Int.up;

        if (relativeDelta.sqrMagnitude > _longMovementLengthSqr)
            return new Vector2Int(relativeDelta.x > 0 ? 2 : -2, 0);

        return new Vector2Int(relativeDelta.x > 0 ? 1 : -1, 0);
    }
}
