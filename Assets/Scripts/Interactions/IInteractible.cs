using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IInteractible
{
    public bool interaction(GereInteractions i);
    public void finInteraction();
}
