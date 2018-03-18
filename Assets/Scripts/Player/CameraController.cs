using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

  [SerializeField]
  private List<Transform> playersToFollow;

  private float minHeight = 15f;

  private void FixedUpdate() {
    transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, 0, 0);
    FixedCameraFollowSmooth(GetComponent<Camera>(), playersToFollow);
  }

  // Follow two transform
  public void FixedCameraFollowSmooth(Camera cam, List<Transform> tfs) {
    float zoomFactor = 1.4f;
    float followTimeDelta = 0.8f;

    Vector3 midpoint;
    float distance;

    //Search for the farthest transforms from each other
    if (tfs.Count > 2) {
      Transform t1 = tfs[0];
      Transform t2 = tfs[1];
      float maxDistance = -1f;

      for (int i = 0; i < tfs.Count - 1; i++) {
        for (int j = i + 1; j < tfs.Count; j++) {
          distance = (tfs[i].position - tfs[j].position).magnitude;
          if (maxDistance < distance) {
            maxDistance = distance;
            t1 = tfs[i];
            t2 = tfs[j];
          }
        }
      }

      Vector3 averagePos = Vector3.zero;
      foreach (Transform t in tfs) {
        averagePos += t.position;
      }

      midpoint = averagePos / tfs.Count;
      distance = maxDistance;
    } else {
      midpoint = (tfs[0].position + tfs[1].position) / 2f;
      distance = (tfs[0].position - tfs[1].position).magnitude;
    }
    
    // Move camera a certain distance
    Vector3 cameraDestination = midpoint - cam.transform.forward * distance * zoomFactor;
    
    // Avoid zooming too close
    if(cameraDestination.y < minHeight) {
      cameraDestination.y = minHeight;
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
