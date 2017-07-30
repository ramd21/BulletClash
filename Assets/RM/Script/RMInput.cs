using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;

namespace RM
{
	public class RMInput : Singleton<RMInput>
{
	public bool _Log;

	public Camera _TargetCam;
	public EventSystem _EventSystem;


	public enum UpdateMode
	{
		default_update,
		late_update,
	}

	public UpdateMode _UpdateMode;

	public enum InputState
	{
		none,
		start,
		input,
		end,
	}

	public enum InputSeq
	{
		none,
		drag_start,
		drag,
		drag_end,
		short_tap,
		long_tap_start,
		long_tap,
		long_tap_end,
		double_tap,
		short_tap_with_no_double_tap,
	}

	public bool _IsPinching;
	public float _PinchCur;
	public float _PinchDelta;
	public float _PinchLast;

	public float _ShortTap = 0.3f;
	public float _LongTap = 0.5f;
	public float _DoubleTap = 0.2f;

	public float _DragDistThresholdWorld = 0.25f;
    public float _DoubleTapDistThresholdWorld = 0.5f;
	
	public float _WorldBaseDist = 50;

	public GameObject _CurSelect;


	[Serializable]
	public struct InputInfo
	{
		public bool _IsPointerOverUI;

		public InputSeq _InputSeq;
		public InputState _InputState;

		public float _CurTime;
		public float _DoubleTapTimer;

		public Vector2 _V2ScreenInputStart;
		public Vector2 _V2ScreenInputCur;
		public Vector2 _V2ScreenInputDelta;
		public Vector2 _V2ScreenInputLast;
		public Vector2 _V2ScreenInputEnd;
		public Vector2 _V2ScreenInputStartEndDiff;

		public Vector2 _V2ViewInputStart;
		public Vector2 _V2ViewInputCur;
		public Vector2 _V2ViewInputDelta;
		public Vector2 _V2ViewInputLast;
		public Vector2 _V2ViewInputEnd;
		public Vector2 _V2ViewInputStartEndDiff;

		public Vector3 _V3WorldInputStart;
		public Vector3 _V3WorldInputCur;
		public Vector3 _V3WorldInputDelta;
		public Vector3 _V3WorldInputLast;
		public Vector3 _V3WorldInputEnd;
		public Vector3 _V3WorldInputStartEndDiff;

		public Vector2 _V2ScreenLastShortTap;
		public Vector2 _V2ViewLastShortTap;
		public Vector3 _V3WorldLastShortTap;
	}

	InputInfo[] _InputInfoArr = new InputInfo[2];

	public InputSeq GetInptSeq(int aInputId)
	{
		return _InputInfoArr[aInputId]._InputSeq;
	}

		public InputState GetInptState(int aInputId)
		{
			return _InputInfoArr[aInputId]._InputState;
		}

		public InputInfo GetInputInfo(int aInputId)
	{
		return _InputInfoArr[aInputId];
	}

	bool IsPointerOverUI(int aInputId)
	{
		bool chk = Input.touchCount < aInputId + 1;

		if (
			_EventSystem.IsPointerOverGameObject() || 
			(!chk && Input.GetTouch(aInputId).phase == TouchPhase.Began && 
			_EventSystem.IsPointerOverGameObject(Input.touches[aInputId].fingerId))
		)
		{
			return true;
		}
		else
		{
			return false;
		}
	}

	private void Start()
	{
		if (_Log)
		{
			this.StartObsserve(() => GetInptSeq(0),
			(cur, last) =>
			{
				Debug.Log("GetInptState(0) = " + cur + "\n" + Time.frameCount);
			}, true);

			this.StartObsserve(() => _EventSystem.currentSelectedGameObject,
			(cur, last) =>
			{
				Debug.Log("_EventSystem.currentSelectedGameObject = " + cur + "\n" + Time.frameCount);
			}, true);

			this.StartObsserve(() => _EventSystem.firstSelectedGameObject,
			(cur, last) =>
			{
				Debug.Log("_EventSystem.firstSelectedGameObject = " + cur + "\n" + Time.frameCount);
			}, true);

			this.StartObsserve(() => IsPointerOverUI(0),
			(cur, last) =>
			{
				Debug.Log("IsPointerOverUI(0) = " + cur + "\n" + Time.frameCount);
			}, true);
		}
	}


	public GameObject GetCurrentSelectedGameObject(int aTouchId)
	{
		if (IsPointerOverUI(aTouchId))
		{
			return _EventSystem.currentSelectedGameObject;
		}
		else
		{
			return null;
		}
	}

	void Update()
	{
		if (_UpdateMode == UpdateMode.default_update)
		{
			_CurSelect = _EventSystem.currentSelectedGameObject;
			InputUpdate(0);
#if !UNITY_EDITOR && !UNITY_STANDALONE && (UNITY_IOS || UNITY_ANDROID)
			InputUpdate(1);
			Pinch();
#endif
		}
	}

	void LateUpdate()
	{
		if (_UpdateMode == UpdateMode.late_update)
		{
			_CurSelect = _EventSystem.currentSelectedGameObject;
			InputUpdate(0);
#if !UNITY_EDITOR && !UNITY_STANDALONE && (UNITY_IOS || UNITY_ANDROID)
			InputUpdate(1);
			Pinch();
#endif
		}
	}

	void Pinch()
	{
		if (_InputInfoArr[0]._InputState == InputState.input && _InputInfoArr[1]._InputState == InputState.input)
		{
			if (_InputInfoArr[0]._InputSeq == InputSeq.drag && _InputInfoArr[1]._InputSeq == InputSeq.drag)
			{
				_PinchCur = (_InputInfoArr[0]._V3WorldInputCur - _InputInfoArr[1]._V3WorldInputCur).magnitude;
				if (!_IsPinching)
					_PinchLast = _PinchCur;

				_PinchDelta = _PinchCur - _PinchLast;

				_IsPinching = true;
			}
			else
			{
				_PinchCur = 0;
				_PinchDelta = 0;
				_PinchLast = 0;
				_IsPinching = false;
			}

		}
		else
		{
			_PinchCur = 0;
			_PinchDelta = 0;
			_PinchLast = 0;
			_IsPinching = false;
		}

		_PinchLast = _PinchCur;
	}

	void InputUpdate(int aTouchId)
	{
		if (_TargetCam == null)
			_TargetCam = Camera.main;

#if UNITY_EDITOR || UNITY_STANDALONE
		if (!Input.GetMouseButton(0))
		{
			if (Input.GetMouseButtonUp(0))
				_InputInfoArr[aTouchId]._InputState = InputState.end;
			else
				_InputInfoArr[aTouchId]._InputState = InputState.none;
		}
		else
		{
			if (Input.GetMouseButtonDown(0))
			{
				_InputInfoArr[aTouchId]._InputState = InputState.start;
			}
			else
			{
				_InputInfoArr[aTouchId]._InputState = InputState.input;
			}
		}

		switch (_InputInfoArr[aTouchId]._InputState)
		{
			case InputState.none:
				break;
			case InputState.start:
				_InputInfoArr[aTouchId]._V2ScreenInputStart = Input.mousePosition;
				_InputInfoArr[aTouchId]._V2ScreenInputCur = Input.mousePosition;
				_InputInfoArr[aTouchId]._V2ScreenInputLast = Input.mousePosition;
				break;
			case InputState.input:
				_InputInfoArr[aTouchId]._V2ScreenInputCur = Input.mousePosition;
				break;
			case InputState.end:
				_InputInfoArr[aTouchId]._V2ScreenInputEnd = Input.mousePosition;
				_InputInfoArr[aTouchId]._V2ScreenInputCur = Input.mousePosition;
				break;
		}

#elif UNITY_IOS || UNITY_ANDROID
		if (Input.touchCount < aTouchId + 1)
		{
			_TouchInfoArr[aTouchId]._InputStateSub = InputStateSub.none;
		}
		else
		{
			if (Input.touches[aTouchId].phase == TouchPhase.Began)
			{
				_TouchInfoArr[aTouchId]._InputStateSub = InputStateSub.start;
			}
			else
			{
				if (Input.touches[aTouchId].phase == TouchPhase.Ended)
					_TouchInfoArr[aTouchId]._InputStateSub = InputStateSub.end;
				else
					_TouchInfoArr[aTouchId]._InputStateSub = InputStateSub.input;
			}
		}

		switch (_TouchInfoArr[aTouchId]._InputStateSub)
		{
			case InputStateSub.none:
				break;
			case InputStateSub.start:
				_TouchInfoArr[aTouchId]._V2ScreenInputStart = Input.touches[aTouchId].position;
				_TouchInfoArr[aTouchId]._V2ScreenInputCur = Input.touches[aTouchId].position;
				_TouchInfoArr[aTouchId]._V2ScreenInputLast = Input.touches[aTouchId].position;
				break;
			case InputStateSub.input:
				_TouchInfoArr[aTouchId]._V2ScreenInputCur = Input.touches[aTouchId].position;
				break;
			case InputStateSub.end:
				_TouchInfoArr[aTouchId]._V2ScreenInputEnd = Input.touches[aTouchId].position;
				_TouchInfoArr[aTouchId]._V2ScreenInputCur = Input.touches[aTouchId].position;
				break;
		}
#endif

		if (_InputInfoArr[aTouchId]._InputState == InputState.none)
		{
			_InputInfoArr[aTouchId]._V2ScreenInputStart = Vector2.zero;
			_InputInfoArr[aTouchId]._V2ScreenInputCur = Vector2.zero;
			_InputInfoArr[aTouchId]._V2ScreenInputDelta = Vector2.zero;
			_InputInfoArr[aTouchId]._V2ScreenInputLast = Vector2.zero;
			_InputInfoArr[aTouchId]._V2ScreenInputStartEndDiff = Vector2.zero;

			_InputInfoArr[aTouchId]._V2ViewInputStart = Vector2.zero;
			_InputInfoArr[aTouchId]._V2ViewInputCur = Vector2.zero;
			_InputInfoArr[aTouchId]._V2ViewInputDelta = Vector2.zero;
			_InputInfoArr[aTouchId]._V2ViewInputLast = Vector2.zero;
			_InputInfoArr[aTouchId]._V2ViewInputStartEndDiff = Vector2.zero;

			_InputInfoArr[aTouchId]._V3WorldInputStart = Vector2.zero;
			_InputInfoArr[aTouchId]._V3WorldInputCur = Vector2.zero;
			_InputInfoArr[aTouchId]._V3WorldInputDelta = Vector2.zero;
			_InputInfoArr[aTouchId]._V3WorldInputLast = Vector2.zero;
			_InputInfoArr[aTouchId]._V3WorldInputStartEndDiff = Vector3.zero;

			_InputInfoArr[aTouchId]._InputSeq = InputSeq.none;
			_InputInfoArr[aTouchId]._CurTime = 0;
		}
		else
		{
			_InputInfoArr[aTouchId]._V2ScreenInputDelta = _InputInfoArr[aTouchId]._V2ScreenInputCur - _InputInfoArr[aTouchId]._V2ScreenInputLast;
			_InputInfoArr[aTouchId]._V2ScreenInputStartEndDiff = _InputInfoArr[aTouchId]._V2ScreenInputCur - _InputInfoArr[aTouchId]._V2ScreenInputStart;


			_InputInfoArr[aTouchId]._V2ViewInputStart = _TargetCam.ScreenToViewportPoint(_InputInfoArr[aTouchId]._V2ScreenInputStart);
			_InputInfoArr[aTouchId]._V2ViewInputCur = _TargetCam.ScreenToViewportPoint(_InputInfoArr[aTouchId]._V2ScreenInputCur);
			_InputInfoArr[aTouchId]._V2ViewInputDelta = _InputInfoArr[aTouchId]._V2ViewInputCur - _InputInfoArr[aTouchId]._V2ViewInputLast;
			_InputInfoArr[aTouchId]._V2ViewInputEnd = _TargetCam.ScreenToViewportPoint(_InputInfoArr[aTouchId]._V2ScreenInputEnd);
			_InputInfoArr[aTouchId]._V2ViewInputStartEndDiff = _InputInfoArr[aTouchId]._V2ViewInputCur - _InputInfoArr[aTouchId]._V2ViewInputStart;


			_InputInfoArr[aTouchId]._V3WorldInputStart = _TargetCam.ScreenToWorldPoint(new Vector3(_InputInfoArr[aTouchId]._V2ScreenInputStart.x, _InputInfoArr[aTouchId]._V2ScreenInputStart.y, _WorldBaseDist));
			_InputInfoArr[aTouchId]._V3WorldInputStart = _TargetCam.transform.InverseTransformPoint(_InputInfoArr[aTouchId]._V3WorldInputStart);

			_InputInfoArr[aTouchId]._V3WorldInputCur = _TargetCam.ScreenToWorldPoint(new Vector3(_InputInfoArr[aTouchId]._V2ScreenInputCur.x, _InputInfoArr[aTouchId]._V2ScreenInputCur.y, _WorldBaseDist));
			_InputInfoArr[aTouchId]._V3WorldInputCur = _TargetCam.transform.InverseTransformPoint(_InputInfoArr[aTouchId]._V3WorldInputCur);

			_InputInfoArr[aTouchId]._V3WorldInputDelta = _InputInfoArr[aTouchId]._V3WorldInputCur - _InputInfoArr[aTouchId]._V3WorldInputLast;

			_InputInfoArr[aTouchId]._V3WorldInputEnd = _TargetCam.ScreenToWorldPoint(new Vector3(_InputInfoArr[aTouchId]._V2ScreenInputEnd.x, _InputInfoArr[aTouchId]._V2ScreenInputEnd.y, _WorldBaseDist));
			_InputInfoArr[aTouchId]._V3WorldInputEnd = _TargetCam.transform.InverseTransformPoint(_InputInfoArr[aTouchId]._V3WorldInputEnd);

			_InputInfoArr[aTouchId]._V3WorldInputStartEndDiff = _InputInfoArr[aTouchId]._V3WorldInputCur - _InputInfoArr[aTouchId]._V3WorldInputStart;
		}

		

		switch (_InputInfoArr[aTouchId]._InputState)
		{
			case InputState.none:
				_InputInfoArr[aTouchId]._IsPointerOverUI = false;
				break;
			case InputState.start:
				_InputInfoArr[aTouchId]._IsPointerOverUI = IsPointerOverUI(aTouchId);
				break;
			case InputState.input:

				if (_InputInfoArr[aTouchId]._V3WorldInputStartEndDiff.magnitude > _DragDistThresholdWorld)
				{
					if (_InputInfoArr[aTouchId]._InputSeq != InputSeq.drag_start && _InputInfoArr[aTouchId]._InputSeq != InputSeq.drag)
					{
						_InputInfoArr[aTouchId]._InputSeq = InputSeq.drag_start;
					}
					else
					{
						_InputInfoArr[aTouchId]._InputSeq = InputSeq.drag;
					}
				}

				if (_InputInfoArr[aTouchId]._InputSeq != InputSeq.drag_start && _InputInfoArr[aTouchId]._InputSeq != InputSeq.drag)
				{
					if (_InputInfoArr[aTouchId]._CurTime > _LongTap)
					{
						if (_InputInfoArr[aTouchId]._InputSeq != InputSeq.long_tap_start && _InputInfoArr[aTouchId]._InputSeq != InputSeq.long_tap)
						{
							_InputInfoArr[aTouchId]._InputSeq = InputSeq.long_tap_start;
						}
						else
						{
							_InputInfoArr[aTouchId]._InputSeq = InputSeq.long_tap;
						}
					}
				}

				_InputInfoArr[aTouchId]._CurTime += Time.deltaTime;
				break;
			case InputState.end:
				if (_InputInfoArr[aTouchId]._InputSeq != InputSeq.drag_start && _InputInfoArr[aTouchId]._InputSeq != InputSeq.drag)
				{
					if (_InputInfoArr[aTouchId]._InputSeq == InputSeq.long_tap_start || _InputInfoArr[aTouchId]._InputSeq == InputSeq.long_tap)
					{
						_InputInfoArr[aTouchId]._InputSeq = InputSeq.long_tap_end;
					}
					else
					{
						if (_InputInfoArr[aTouchId]._CurTime < _ShortTap)
						{
							if (_InputInfoArr[aTouchId]._DoubleTapTimer != 0)
							{
								float dist = Vector3.Distance(_InputInfoArr[aTouchId]._V3WorldLastShortTap, _InputInfoArr[aTouchId]._V3WorldInputCur);

								if (dist < _DoubleTapDistThresholdWorld)
								{
									_InputInfoArr[aTouchId]._InputSeq = InputSeq.double_tap;
									_InputInfoArr[aTouchId]._DoubleTapTimer = 0;
								}
							}
							else
							{
								_InputInfoArr[aTouchId]._InputSeq = InputSeq.short_tap;
								_InputInfoArr[aTouchId]._DoubleTapTimer = _DoubleTap;
								_InputInfoArr[aTouchId]._V2ScreenLastShortTap = _InputInfoArr[aTouchId]._V2ScreenInputEnd;
								_InputInfoArr[aTouchId]._V2ViewLastShortTap = _InputInfoArr[aTouchId]._V2ViewInputEnd;
								_InputInfoArr[aTouchId]._V3WorldLastShortTap = _InputInfoArr[aTouchId]._V3WorldInputEnd;
							}
						}
					}
				}
				else
				{
					float dist = Vector3.Distance(_InputInfoArr[aTouchId]._V3WorldInputStart, _InputInfoArr[aTouchId]._V3WorldInputCur);
					_InputInfoArr[aTouchId]._InputSeq = InputSeq.drag_end;
                }
				break;
		}

		

		if (_InputInfoArr[aTouchId]._DoubleTapTimer != 0)
		{
			_InputInfoArr[aTouchId]._DoubleTapTimer -= Time.deltaTime;
			if (_InputInfoArr[aTouchId]._DoubleTapTimer <= 0)
			{
				_InputInfoArr[aTouchId]._DoubleTapTimer = 0;
				_InputInfoArr[aTouchId]._InputSeq = InputSeq.short_tap_with_no_double_tap;
			}
		}

		_InputInfoArr[aTouchId]._V2ScreenInputLast = _InputInfoArr[aTouchId]._V2ScreenInputCur;
		_InputInfoArr[aTouchId]._V2ViewInputLast = _InputInfoArr[aTouchId]._V2ViewInputCur;
		_InputInfoArr[aTouchId]._V3WorldInputLast = _InputInfoArr[aTouchId]._V3WorldInputCur;
	}

#if UNITY_EDITOR
	void OnDrawGizmos()
	{
	}
#endif

#if UNITY_EDITOR
#endif

}

}

