using Services_Industry_Simulation.Simulation;
using System;
using System.Collections.Generic;
namespace Services_Industry_Simulation.Imports
{
    // Minheap was taken from https://egorikas.com/max-and-min-heap-implementation-with-csharp/ , made by egor grishechko
    public class MinHeap
    {
        private readonly List<Event> _elements;
        private int _size;

        public MinHeap()
        {
            _elements = new List<Event>();
        }

        private int GetLeftChildIndex(int elementIndex) => 2 * elementIndex + 1;
        private int GetRightChildIndex(int elementIndex) => 2 * elementIndex + 2;
        private int GetParentIndex(int elementIndex) => (elementIndex - 1) / 2;

        private bool HasLeftChild(int elementIndex) => GetLeftChildIndex(elementIndex) < _size;
        private bool HasRightChild(int elementIndex) => GetRightChildIndex(elementIndex) < _size;
        private bool IsRoot(int elementIndex) => elementIndex == 0;

        private Event GetLeftChild(int elementIndex) => _elements[GetLeftChildIndex(elementIndex)];
        private Event GetRightChild(int elementIndex) => _elements[GetRightChildIndex(elementIndex)];
        private Event GetParent(int elementIndex) => _elements[GetParentIndex(elementIndex)];

        private void Swap(int firstIndex, int secondIndex)
        {
            var temp = _elements[firstIndex];
            _elements[firstIndex] = _elements[secondIndex];
            _elements[secondIndex] = temp;
        }

        public bool IsEmpty()
        {
            return _size == 0;
        }

        public Event Peek()
        {
            if (_size == 0)
                throw new IndexOutOfRangeException();

            return _elements[0];
        }

        public Event Pop()
        {
            if (_size == 0)
                throw new IndexOutOfRangeException();

            var result = _elements[0];
            _elements[0] = _elements[_size - 1];
            _size--;

            ReCalculateDown();

            return result;
        }

        public void Add(Event element)
        {
            if (_elements.Count == _size) _elements.Add(Event.Empty);
            _elements[_size] = element;
            _size++;

            ReCalculateUp();
        }

        private void ReCalculateDown()
        {
            int index = 0;
            while (HasLeftChild(index))
            {
                var smallerIndex = GetLeftChildIndex(index);
                if (HasRightChild(index) && GetRightChild(index).Position < GetLeftChild(index).Position)
                {
                    smallerIndex = GetRightChildIndex(index);
                }

                if (_elements[smallerIndex].Position >= _elements[index].Position)
                {
                    break;
                }

                Swap(smallerIndex, index);
                index = smallerIndex;
            }
        }

        private void ReCalculateUp()
        {
            var index = _size - 1;
            while (!IsRoot(index) && _elements[index].Position < GetParent(index).Position)
            {
                var parentIndex = GetParentIndex(index);
                Swap(parentIndex, index);
                index = parentIndex;
            }
        }
    }
}
