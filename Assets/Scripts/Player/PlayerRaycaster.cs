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

            if (Physics.CapsuleCast(p1, p2,
                charContr.radius,
                transform.forward,
                out hit,
                RayDistance,
                LayerMask.GetMask("RayReceiver")))
            {
                colliderGameObject = hit.collider.gameObject;
            }

            if (Input.GetButtonDown("Action"))
            {
                if (_takenObject != null)
                {
                    Drop();
                }
                else if (colliderGameObject != null &&
                         colliderGameObject.tag == "Catchable")
                {
                    Catch(colliderGameObject);
                }
            }
        }

        private void Drop()
        {
            Debug.Log("Drop : " + _takenObject.name);
            _takenObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
            _takenObject.transform.parent = _interactive.transform;
            _takenObject = null;
        }

        private void Catch(GameObject obj)
        {
            obj.transform.parent = this.transform;
            obj.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
            var position = transform.forward + transform.position;
            obj.transform.rotation = new Quaternion();
            obj.transform.position = position;
            _takenObject = obj;
            Debug.Log("Take : " + _takenObject.name);
        }
    }
}
