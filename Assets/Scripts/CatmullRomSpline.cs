using UnityEngine;
using System.Collections;

//Interpolation between points with a Catmull-Rom spline
public class CatmullRomSpline : MonoBehaviour
{
	//Has to be at least 4 points
	ArrayList controlPointsList;

	public ArrayList path = new ArrayList();

	void Awake(){
		controlPointsList = new ArrayList();
		Vector2 posLB = Camera.main.ViewportToWorldPoint (new Vector2 (0, 0));
		Vector2 posRU = Camera.main.ViewportToWorldPoint (new Vector2 (1, 1));
		Vector2 pointStartOffset = new Vector2 (posLB.x-10,posLB.y/2);
		controlPointsList.Add (pointStartOffset);
		Vector2 pointStart = new Vector2 (posLB.x,Random.Range(posLB.y , posRU.y));
		controlPointsList.Add (pointStart);
		//add sorted random list of  points
		Vector2 pointEnd = new Vector2 (posRU.x,Random.Range(posLB.y , posRU.y));
		controlPointsList.Add (pointEnd);
		Vector2 pointEndOffset = new Vector2 (posRU.x+10,posLB.y/2);
		controlPointsList.Add (pointEndOffset);
		CreateLine ();
	}

	void OnDrawGizmos() {
		Gizmos.color = Color.yellow;
		Gizmos.DrawLine((Vector2) controlPointsList [1], (Vector2) path[0]);
		
		for (int i = 1; i < path.Count; i++) {
			//Draw this line segment
			Gizmos.DrawLine((Vector2) path[i - 1], (Vector2) path[i]);
		}
	}

	//Display without having to press play
	void CreateLine()
	{
		Gizmos.color = Color.red;


		//Draw the Catmull-Rom spline between the points
		for (int i = 0; i < controlPointsList.Count; i++)
		{
			//Cant draw between the endpoints
			//Neither do we need to draw from the second to the last endpoint
			//...if we are not making a looping line
			if ((i == 0 || i == controlPointsList.Count - 2 || i == controlPointsList.Count - 1))
			{
				continue;
			}

			DisplayCatmullRomSpline(i);
		}
	}
	//Display a spline between 2 points derived with the Catmull-Rom spline algorithm
	void DisplayCatmullRomSpline(int pos)
	{
		//The 4 points we need to form a spline between p1 and p2
		Vector2 p0 = (Vector2) controlPointsList[ClampListPos(pos - 1)];
		Vector2 p1 = (Vector2) controlPointsList [pos];
		Vector2 p2 =  (Vector2)controlPointsList[ClampListPos(pos + 1)];
		Vector2 p3 =  (Vector2)controlPointsList[ClampListPos(pos + 2)];

		//The start position of the line
		Vector2 lastPos = p1;

		//The spline's resolution
		//Make sure it's is adding up to 1, so 0.3 will give a gap, but 0.2 will work
		float resolution = 0.2f;

		//How many times should we loop?
		int loops = Mathf.FloorToInt(1f / resolution);

		for (int i = 1; i <= loops; i++)
		{
			//Which t position are we at?
			float t = i * resolution;

			//Find the coordinate between the end points with a Catmull-Rom spline
			Vector2 newPos = GetCatmullRomPosition(t, p0, p1, p2, p3);

			Debug.Log(newPos);
			path.Add(newPos);
			//Save this pos so we can draw the next line segment
			lastPos = newPos;
		}
	}

	//Clamp the list positions to allow looping
	int ClampListPos(int pos)
	{
		if (pos < 0)
		{
			pos = controlPointsList.Count - 1;
		}

		if (pos > controlPointsList.Count)
		{
			pos = 1;
		}
		else if (pos > controlPointsList.Count - 1)
		{
			pos = 0;
		}

		return pos;
	}

	//Returns a position between 4 Vector3 with Catmull-Rom spline algorithm
	//http://www.iquilezles.org/www/articles/minispline/minispline.htm
	Vector2 GetCatmullRomPosition(float t, Vector2 p0, Vector2 p1, Vector2 p2, Vector2 p3)
	{
		//The coefficients of the cubic polynomial (except the 0.5f * which I added later for performance)
		Vector2 a = 2f * p1;
		Vector2 b = p2 - p0;
		Vector2 c = 2f * p0 - 5f * p1 + 4f * p2 - p3;
		Vector2 d = -p0 + 3f * p1 - 3f * p2 + p3;

		//The cubic polynomial: a + b * t + c * t^2 + d * t^3
		Vector2 pos = 0.5f * (a + (b * t) + (c * t * t) + (d * t * t * t));

		return pos;
	}
}