using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateSub : MonoBehaviour
{
    Subscription<RotateEvent> rotate_subscription;

    void Start()
    {
        rotate_subscription = EventBus.Subscribe<RotateEvent>(_OnRotateSelected);
    }

    // Function that will actually rotate the map
    void _OnRotateSelected(RotateEvent r)
    {

    }

    // Unsubscribe on the destruction
    private void OnDestroy()
    {
        EventBus.Unsubscribe(rotate_subscription);
    }
}
