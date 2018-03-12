using UnityEngine;

namespace Assets.Scripts.Player
{
    public class PlayerRaycaster : MonoBehaviour
    {
        public float RayDistance = 1;

        private GameObject _takenObject;
        private GameObject _interactive;

        private void Start()
        {
            _takenObject = null;
            _interactive = GameObject.Find("/Interactive");
        }

        private void FixedUpdate()
        {
            RaycastHit hit;
            GameObject colliderGameObject = null;

            var charContr = GetComponent<CharacterController>();
            var p1 = transform.position + charContr.center + Vector3.up * -charContr.height * 0.5F;
            var p2 = p1 + Vector3.up * charContr.height;

            // Cast character controller shape 10 meters forward to see if it is about to hit anything.
            if (Physics.CapsuleCast(p1, p2,
                charContr.radius,
                transform.forward,
                out hit,
                RayDistance,
                LayerMask.GetMask("RayReceiver")))
            {
                colliderGameObject = hit.collider.gameObject;
            }

            //var rayPosition = transform.forward + transform.position;
            //rayPosition.y -= 1;
            //Debug.DrawRay(rayPosition, transform.up * RayDistance, Color.green);

            //Debug.DrawRay(transform.position, transform.forward * RayDistance, Color.green);

            //if (Physics.Raycast(transform.position, transform.forward * RayDistance,
            //    out hit, RayDistance, LayerMask.GetMask("RayReceiver")))
            //{
            //    colliderGameObject = hit.collider.gameObject;
            //}

            if (Input.GetButtonDown("Action"))
            {
                if (_takenObject != null)
                {
                    Debug.Log("Drop : " + _takenObject.name);
                    _takenObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
                    _takenObject.transform.parent = _interactive.transform;
                    _takenObject = null;
                }
                else if (colliderGameObject != null &&
                         colliderGameObject.tag == "Catchable")
                {
                    colliderGameObject.transform.parent = this.transform;
                    colliderGameObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
                    _takenObject = colliderGameObject;
                    Debug.Log("Take : " + _takenObject.name);
                }
            }
        }
    }
}
