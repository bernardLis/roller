using UnityEngine;

public enum ColliderDirection { PositiveX, PositiveZ, NegativeX, NegativeZ, CornerNegXNegZ, CornerPosXNegZ, CornerNegXPosZ, CornerPosXPosZ }

public class FloorCollider : MonoBehaviour
{
    public ColliderDirection colDirection;
}
