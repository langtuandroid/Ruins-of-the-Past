using System;
using UnityEngine;

namespace Scenes
{
    public class Billboard : MonoBehaviour
    {
        private enum Type
        {
            LookAtCamera,
            CameraForward
        }

        [SerializeField] private Type _type;

        private Camera _camera;

        private void Awake()
        {
            _camera = Camera.main;
        }

        private void LateUpdate()
        {
            if (!_camera)
                return;

            switch (_type)
            {
                case Type.LookAtCamera:
                    transform.LookAt(_camera.transform, Vector3.up);
                    break;
                case Type.CameraForward:
                    transform.forward = _camera.transform.forward;
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}