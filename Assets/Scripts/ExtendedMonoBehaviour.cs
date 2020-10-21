using UnityEngine;
using System.Collections;

public enum InputAxis {
    horizontal,
    vertical
}

public class ExtendedMonoBehaviour : MonoBehaviour {
    protected string TagFor(EditorTag tag) {
        switch (tag) {
            case EditorTag.Ground:
                return "Ground";
            default:
                return "Default";
        }
    }
    protected string GetAxisStringName(InputAxis axis) {
        switch (axis) {
            case InputAxis.horizontal:
                return "Horizontal";
            case InputAxis.vertical:
                return "Vertical";
            default:
                return null;
        }
    }
}
