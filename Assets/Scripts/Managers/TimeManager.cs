using UnityEngine;


public class TimeManager : MonoBehaviour {

  private float slowdownFactor = 0.05f;
  private float slowdownLength = 1.0f;

  private static float frameTime = 0.02f;//1.0f/50.f;

  void Update()
  {
    Time.timeScale += (1.0f/ slowdownLength)  *  Time.unscaledDeltaTime;
    Time.timeScale = Mathf.Clamp(Time.timeScale, 0.0f, 1.0f);
    if (Time.timeScale == 1.0f) {
      Time.fixedDeltaTime = Time.deltaTime;
    }

  }

  public void DoSlowMotion()
  {
    Time.timeScale = slowdownFactor;
    Time.fixedDeltaTime = Time.timeScale * frameTime;
  }
}