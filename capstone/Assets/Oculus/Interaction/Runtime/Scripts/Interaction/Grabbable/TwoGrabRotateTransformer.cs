/*
 * Copyright (c) Meta Platforms, Inc. and affiliates.
 * All rights reserved.
 *
 * Licensed under the Oculus SDK License Agreement (the "License");
 * you may not use the Oculus SDK except in compliance with the License,
 * which is provided at the time of installation or download, or which
 * otherwise accompanies this software in either electronic or hard copy form.
 *
 * You may obtain a copy of the License at
 *
 * https://developer.oculus.com/licenses/oculussdk/
 *
 * Unless required by applicable law or agreed to in writing, the Oculus SDK
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 */

using System;
using UnityEngine;
using UnityEngine.Serialization;

namespace Oculus.Interaction
{
    /// <summary>
    /// A Transformer that rotates the target about an axis, given two grab points.
    /// Updates apply relative rotational changes, relative to the angle change between the two
    /// grab points each frame.
    /// The axis is defined by a pivot transform: a world position and up vector.
    /// </summary>
    public class TwoGrabRotateTransformer : MonoBehaviour, ITransformer
    {
        public enum Axis
        {
            Right = 0,
            Up = 1,
            Forward = 2
        }

        //돌아가는 기준 축
        [SerializeField, Optional]
        private Transform _pivotTransform = null;

        private Transform PivotTransform =>
            _pivotTransform != null ? _pivotTransform : _grabbable.Transform;

        //enum을 SerializeField로 만들면 드롭다운 된다.
        [SerializeField]
        private Axis _rotationAxis = Axis.Up;

        //회전 임계치 각도
        [Serializable]
        public class TwoGrabRotateConstraints
        {
            public FloatConstraint MinAngle; //bool과 임계치
            public FloatConstraint MaxAngle;
        }

        [SerializeField]
        private TwoGrabRotateConstraints _constraints;

        private float _relativeAngle = 0.0f;
        private float _constrainedRelativeAngle = 0.0f;

        private IGrabbable _grabbable;

        // 대충 첫번째 손과 두번째 손의 투영된 벡터 계산 값
        // vector from the hand at the first grab point to the hand on the second grab point,
        // projected onto the plane of the rotation.
        private Vector3 _previousHandsVectorOnPlane;

        public void Initialize(IGrabbable grabbable)
        {
            _grabbable = grabbable;
        }

        public void BeginTransform()
        {
            Vector3 rotationAxis = CalculateRotationAxisInWorldSpace();
            Debug.Log("축 : " + rotationAxis);
            _previousHandsVectorOnPlane = CalculateHandsVectorOnPlane(rotationAxis);
            Debug.Log("핸들 벡터 : " + _previousHandsVectorOnPlane);
            _relativeAngle = _constrainedRelativeAngle;
        }

        public void UpdateTransform()
        {
            Vector3 rotationAxis = CalculateRotationAxisInWorldSpace();
            Vector3 handsVector = CalculateHandsVectorOnPlane(rotationAxis);
            float angleDelta =
                Vector3.SignedAngle(_previousHandsVectorOnPlane, handsVector, rotationAxis);

            float previousAngle = _constrainedRelativeAngle;
            _relativeAngle += angleDelta;
            _constrainedRelativeAngle = _relativeAngle;

            //임계값
            if (_constraints.MinAngle.Constrain)
            {
                _constrainedRelativeAngle =
                    Mathf.Max(_constrainedRelativeAngle, _constraints.MinAngle.Value);
            }

            if (_constraints.MaxAngle.Constrain)
            {
                _constrainedRelativeAngle =
                    Mathf.Min(_constrainedRelativeAngle, _constraints.MaxAngle.Value);
            }

            angleDelta = _constrainedRelativeAngle - previousAngle;

            // Apply this angle rotation about the axis to our transform
            _grabbable.Transform.RotateAround(PivotTransform.position, rotationAxis, angleDelta);

            _previousHandsVectorOnPlane = handsVector;
        }

        public void EndTransform() { }

        //회전축? [검토]
        private Vector3 CalculateRotationAxisInWorldSpace()
        {
            Vector3 worldAxis = Vector3.zero;
            worldAxis[(int)_rotationAxis] = 1f;
            return PivotTransform.TransformDirection(worldAxis);
        }

        //핸들 차이 계산 함수
        private Vector3 CalculateHandsVectorOnPlane(Vector3 planeNormal)
        {
            Vector3[] grabPointsOnPlane =
            {
                Vector3.ProjectOnPlane(_grabbable.GrabPoints[0].position, planeNormal),
                Vector3.ProjectOnPlane(_grabbable.GrabPoints[1].position, planeNormal),
            };
            //둘중 하나가 임계값차이만큼 안바뀐 경우

            return grabPointsOnPlane[1] - grabPointsOnPlane[0];
        }

        #region Inject

        public void InjectOptionalPivotTransform(Transform pivotTransform)
        {
            _pivotTransform = pivotTransform;
        }

        public void InjectOptionalRotationAxis(Axis rotationAxis)
        {
            _rotationAxis = rotationAxis;
        }

        public void InjectOptionalConstraints(TwoGrabRotateConstraints constraints)
        {
            _constraints = constraints;
        }

        #endregion


        #region getset

        public void SetRelativeAngle(float _relativeAngle)
        {
            this._relativeAngle = _relativeAngle;
        }
        public float GetRelativeAngle()
        {
            return _relativeAngle;
        }
        #endregion
    }
}
