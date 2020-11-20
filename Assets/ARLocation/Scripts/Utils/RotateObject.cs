using UnityEngine;

namespace ARLocation.Utils
{
    public class RotateObject : MonoBehaviour
    {
        public float Speed = 10.0f;
        public Vector3 Axis;

        private float angle;

        // Update is called once per frame
        public void Start()
        {
            Axis = this.gameObject.transform.position;
        }
        void Update()
        {
            
            angle += Speed * Time.deltaTime;
            transform.localRotation = Quaternion.AngleAxis(angle, Axis);
        }
    }
}
