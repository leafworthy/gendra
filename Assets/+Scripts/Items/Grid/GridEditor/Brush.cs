using System.Collections;
using System.Collections.Generic;
using UnityEngine;


    [ExecuteInEditMode]
    public class Brush : MonoBehaviour
    {
        [Range(0, 3)] public float brushSize = 0.5f;

        // Update is called once per frame
        void Update()
        {
            var squareCast = Physics2D.CircleCastAll(transform.position, brushSize, Vector2.zero);
            if (squareCast.Length > 0)
            {
                foreach (var hit2D in squareCast)
                {
                    var stagingItemSpace = hit2D.collider.gameObject.GetComponent<ItemGridSpace>(); 
                    if(stagingItemSpace != null) stagingItemSpace.IsEmptySpace = false;
                }
            }
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.white;
            Gizmos.DrawWireSphere(this.transform.position, brushSize);
        }
    }

