using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
	public Transform target;
	public static Vector3 offset;
	public float sensitivity = 3; // чувствительность мышки
	float limit = 80; // ограничение вращения по Y
	public static float zoom = 0.5f; // чувствительность при увеличении, колесиком мышки
	float zoomMax = 20; // макс. увеличение
	float zoomMin = 0; // мин. увеличение
	float sensitiveMove = 0.1f;
	float X, Y;
	Vector3 startPos;
	Vector3 startPosWorldPoint;
	Vector3 lastPos;
	Vector3 lastPosWorldPoint;
	Vector3 posDelta;
	Vector3 posToward;
	bool stopCheck;
	bool saveStartPos;

	public bool stopMouse;

	public static bool scrollUp;
	public static bool scrollDown;

	public float zoomPos;
	Vector3 endPos;
	Vector3 pos;
	public static float distance;



	void Awake()
	{
		restartPos();


	}


	void restartPos()
	{
		X = 0;
		Y = 0;
		offset = new Vector3(0, 0, 0);

		limit = Mathf.Abs(limit);
		if (limit > 90) limit = 90;
		offset = new Vector3(offset.x, offset.y, -Mathf.Abs(zoomMax) / 2);

	}


	void Update()
	{


		pos = transform.localRotation * offset + target.position;
		transform.position = Vector3.Lerp(transform.position, pos, 10 * Time.deltaTime);


		if (Input.GetMouseButton(2))
		{

			stopCheck = true;

			if (!saveStartPos)
			{
				startPos = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 50);
				startPosWorldPoint = Camera.main.ScreenToWorldPoint(startPos);
				saveStartPos = true;
			}

			lastPos = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 50);
			lastPosWorldPoint = Camera.main.ScreenToWorldPoint(lastPos);
			posDelta = lastPosWorldPoint - startPosWorldPoint;

			target.transform.position = endPos - posDelta * (zoom / 10);
		}
		else
		{
			endPos = target.transform.position;
			if (stopCheck)
			{
				saveStartPos = false;
				stopCheck = false;
				return;
			}
		}

		zoomPos = offset.z;

		if (Input.GetAxis("Mouse ScrollWheel") > 0 || scrollUp)
		{
			offset.z += zoom;
			//if (zoom > 0.2f)
			//{
			//	zoom -= 0.125f;
			//}
			zoomPos = offset.z; //дляп проверки какой сейчас зум
			transform.position = transform.localRotation * offset + target.position;
		}
		else if (Input.GetAxis("Mouse ScrollWheel") < 0 || scrollDown)
		{
			offset.z -= zoom;
			//zoom += 0.125f;
			zoomPos = offset.z;
			transform.position = transform.localRotation * offset + target.position;
		}


		if (Input.GetMouseButton(1))
		{
			X = transform.localEulerAngles.y + Input.GetAxis("Mouse X") * sensitivity;
			Y -= Input.GetAxis("Mouse Y") * sensitivity;
			Y = Mathf.Clamp(Y, -limit, limit);
			transform.localEulerAngles = new Vector3(Y, X, 0);

			pos = transform.rotation * offset + target.position;
			transform.position = transform.rotation * offset + target.position;
		}

	}
}
