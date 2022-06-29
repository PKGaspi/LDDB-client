using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;

public class ClapGestureListener : MonoBehaviour, KinectGestures.GestureListenerInterface
{
	// GUI Text to display the gesture messages.
	public Text GestureInfo;
	
	// private bool to track if progress message has been displayed
	private bool progressDisplayed;

	private int n_claps = 0;
	
	
	public void UserDetected(uint userId, int userIndex)
	{
		// as an example - detect these user specific gestures
		KinectManager manager = KinectManager.Instance;

		manager.DetectGesture(userId, KinectGestures.Gestures.Clap);

		n_claps = 0;

		if(GestureInfo != null)
		{
			GestureInfo.text = n_claps + " claps";
		}
	}
	
	public void UserLost(uint userId, int userIndex)
	{

	}

	public void GestureInProgress(uint userId, int userIndex, KinectGestures.Gestures gesture, 
	                              float progress, KinectWrapper.NuiSkeletonPositionIndex joint, Vector3 screenPos)
	{

	}

	public bool GestureCompleted (uint userId, int userIndex, KinectGestures.Gestures gesture, 
	                              KinectWrapper.NuiSkeletonPositionIndex joint, Vector3 screenPos)
	{
		if (gesture != KinectGestures.Gestures.Clap) {
			print(gesture);
			return false;
		}
			n_claps++;
			GestureInfo.text = n_claps + " claps";
			return true;
	}

	public bool GestureCancelled (uint userId, int userIndex, KinectGestures.Gestures gesture, 
	                              KinectWrapper.NuiSkeletonPositionIndex joint)
	{
		if (gesture == KinectGestures.Gestures.Clap) {
			print("clap cancelled");
		}
		return true;
	}
	
}
