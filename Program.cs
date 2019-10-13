﻿using System;

namespace Console_BookExamples
{
    //***************************** Adapter principle *****************************//
    // Implementing an Adapter is easy
    // Determine the API you have and the API you need
    // Create a component which aggregates (has a reference to, ...) the adaptee
    // Intermediate representations can pile up: use caching and other optmizations

    public interface IInteger
    {
        int Value { get; }
    }

    public static class Dimensions
    {
        public class Two : IInteger
        {
            public int Value => 2;
        }

        public class Three : IInteger
        {
            public int Value => 3;
        }
    }

    public class Vector<TSelf, T, D>
        where D : IInteger, new()
        where TSelf : Vector<TSelf, T, D>, new()
    {
        protected T[] Data;

        public Vector()
        {
            Data = new T[new D().Value];
        }

        public Vector(params T[] values)
        {
            var requiredSize = new D().Value;
            Data = new T[requiredSize];

            var providedSize = values.Length;

            for (int i = 0; i < Math.Min(requiredSize, providedSize); ++i)
                Data[i] = values[i];
        }

        public static TSelf Create(params T[] values)
        {
            var result = new TSelf();
            var requiredSize = new D().Value;
            result.Data = new T[requiredSize];

            var providedSize = values.Length;

            for (int i = 0; i < Math.Min(requiredSize, providedSize); ++i)
                result.Data[i] = values[i];

            return result;
        }

        public T this[int index]
        {
            get => Data[index];
            set => Data[index] = value;
        }
    }

    public class VectorOfFloat<TSelf, D>
        : Vector<TSelf, float, D>
        where D : IInteger, new()
        where TSelf : Vector<TSelf, float, D>, new()
    {
    }

    public class VectorOfInt<D> : Vector<VectorOfInt<D>, int, D>
        where D : IInteger, new()
    {
        public VectorOfInt()
        {
        }

        public VectorOfInt(params int[] values) : base(values)
        {
        }

        public static VectorOfInt<D> operator + (VectorOfInt<D> lhs, VectorOfInt<D> rhs)
        {
            var result = new VectorOfInt<D>();
            var dim = new D().Value;
            for (int i = 0; i < dim; i++)
            {
                result[i] = lhs[i] + rhs[i];
            }

            return result;
        }
    }

    public class Vector2i : VectorOfInt<Dimensions.Two>
    {
        public Vector2i()
        {
        }

        public Vector2i(params int[] values) : base(values)
        {
        }
    }

    public class Vector3f : VectorOfFloat<Vector3f, Dimensions.Three>
    {
        public override string ToString()
        {
            return $"{string.Join(",", Data)}";
        }
    }

    class Demo
    {
        public static void Main(string[] args)
        {
            var v = new Vector2i(1, 2);
            v[0] = 0;

            var vv = new Vector2i(3, 2);

            var result = v + vv;

            Vector3f u = Vector3f.Create(3.5f, 2.2f, 1);
        }
    }
}