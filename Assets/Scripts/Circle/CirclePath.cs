using System;
using UnityEngine;

namespace Circle
{
    class CirclePath
    {
        private struct PathEdge
        {
            public readonly Vector3 Start;
            public readonly Vector3 End;
            public readonly float Length;

            public PathEdge(Vector3 start, Vector3 end)
            {
                Start = start;
                End = end;
                Length = Vector3.Distance(start, end);
            }
        }

        private int _partId;
        private float _localPosition;
        private float _length;
        private float _position;
        private PathEdge[] _edges;

        public bool Complete => _position >= _length;
        public float Position => _position;
        public float RemainingDistance => _length - _position;
        public float Length => _length;

        public Vector3 GetPosition(float delta)
        {
            if (_edges == null || _edges.Length == 0)
                return Vector3.zero;

            _position = Mathf.Min(_position + delta, _length);
            _localPosition += delta;
            while (_partId < _edges.Length && _localPosition >= _edges[_partId].Length)
            {
                _localPosition -= _edges[_partId].Length;
                _partId++;
            }

            if (_partId >= _edges.Length)
                return _edges[^1].End;

            return Vector3.Lerp(_edges[_partId].Start, _edges[_partId].End, _localPosition / _edges[_partId].Length);
        }

        public CirclePath(Vector3 start, Vector3[] corners)
        {
            if (corners == null || corners.Length == 0)
                throw new ArgumentException();

            _length = 0f;
            _edges = new PathEdge[corners.Length];
            for (int i = 0; i < corners.Length; i++)
            {
                _edges[i] = new PathEdge(start, corners[i]);
                _length += _edges[i].Length;
                start = corners[i];
            }

            _partId = 0;
            _localPosition = 0f;
            _position = 0f;
        }
    }
}
