using System.Collections;
using UnityEngine;

public class BPMTestManager : MonoBehaviour
{
  const int INIT_BPM_VALUE = 90;

  //[SerializeField] ScoreManager rangeScore;
  [SerializeField] AudioClip initBPM;
  [SerializeField] AudioSource audioSource;

  float lastScore = 0;
  int currentIntervalIndex = 0;
  int lastBPM;
  int currentBPM;

  public void StartTest()
  {
    lastBPM = INIT_BPM_VALUE;
    currentBPM = lastBPM;
    audioSource.PlayOneShot(initBPM);
  }

  void SetRangeScore(float newScore)
  {
    ManipulateRangeScore(newScore);
    currentIntervalIndex++;
    lastBPM = currentBPM;
  }

  void ManipulateRangeScore(float newScore)
  {
    if (currentIntervalIndex == 0)
      TestBPMSession(lastBPM + 10);
    else
    {
      if (currentBPM > lastBPM && newScore > lastScore || currentBPM < lastBPM && newScore < lastScore)
        TestBPMSession(lastBPM + 10);
      else if (currentBPM < lastBPM && newScore > lastScore || currentBPM > lastBPM && newScore < lastScore)
        TestBPMSession(lastBPM - 10);
      else
        StopTest();
    }
    lastScore = newScore;
  }

  IEnumerator ManageSession(int secondsPerSession)
  {
    yield return new WaitForSeconds(secondsPerSession);
    audioSource.Stop();
    //SetRangeScore(rangeScore);
  }

  void TestBPMSession(int requiredBpm)
  {
    currentBPM = requiredBpm;
    audioSource.PlayOneShot(Resources.Load<AudioClip>("BPM_Clips/" + requiredBpm));
    StartCoroutine(ManageSession(30));
  }

  void StopTest()
  {
    Debug.Log("BPM: " + lastBPM);
  }

}
