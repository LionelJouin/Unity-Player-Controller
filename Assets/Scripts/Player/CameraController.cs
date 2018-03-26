using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Player
{
    public class CameraController : MonoBehaviour
    {

        [SerializeField]
        private List<Transform> _playersToFollow;

        private float _minHeight = 15f;

        private void FixedUpdate()
        {
            transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, 0, 0);
            FixedCameraFollowSmooth(GetComponent<Camera>(), _playersToFollow);
        }

        // Follow two transform
        public void FixedCameraFollowSmooth(Camera cam, List<Transform> tfs)
        {
            const float zoomFactor = 1.4f;
            const float followTimeDelta = 0.8f;

            Vector3 midpoint;
            float distance;

            //Search for the farthest transforms from each other
            if (tfs.Count > 2)
            {
                var t1 = tfs[0];
                var t2 = tfs[1];
                var maxDistance = -1f;

                for (var i = 0; i < tfs.Count - 1; i++)
                {
                    for (var j = i + 1; j < tfs.Count; j++)
                    {
                        distance = (tfs[i].position - tfs[j].position).magnitude;
                        if (maxDistance < distance)
                        {
                            maxDistance = distance;
                            t1 = tfs[i];
                            t2 = tfs[j];
                        }
                    }
                }

                var averagePos = Vector3.zero;
                foreach (var t in tfs)
                {
                    averagePos += t.position;
                }

                midpoint = averagePos / tfs.Count;
                distance = maxDistance;
            }
            else
            {
                midpoint = (tfs[0].position + tfs[1].position) / 2f;
                distance = (tfs[0].position - tfs[1].position).magnitude;
            }

            // Move camera a certain distance
            var cameraDestination = midpoint - cam.transform.forward * distance * zoomFactor;

            // Avoid zooming too close
            if (cameraDestination.y < _minHeight)
            {
                cameraDestination.y = _minHeight;
            }

            // Move camera lower to balance the x rotation
            cameraDestination.z -= 2;

            // You specified to use MoveTowards instead of Slerp
            cam.transform.position = Vector3.Slerp(cam.transform.position, cameraDestination, followTimeDelta);

            // Snap when close enough to prevent annoying slerp behavior
            if ((cameraDestination - cam.transform.position).magnitude <= 0.05f)
                cam.transform.position = cameraDestination;
        }

    }
}
