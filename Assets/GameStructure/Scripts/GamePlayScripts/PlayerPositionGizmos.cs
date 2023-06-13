using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPositionGizmos : MonoBehaviour
{
    [SerializeField]
    private Color m_GroupColor = new Color(0f, 1f, .2f, 1f);
    [Range(0f, 2f)]
    public float m_GroupRadius = .5f;

    
    private void OnDrawGizmosSelected()
    {
        Color prevCol = Gizmos.color;

        Gizmos.color = m_GroupColor;
        Gizmos.DrawSphere(transform.position, m_GroupRadius);

        Gizmos.color = prevCol;
    }
}
