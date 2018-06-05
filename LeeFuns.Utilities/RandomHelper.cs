using System;

namespace LeeFuns.Utilities
{
    public class RandomHelper
    {
        private Random _random = new Random();

        public void GetRandomArray<T>(T[] arr)
        {
            int length = arr.Length;
            for (int i = 0; i < length; i++)
            {
                int randomInt = this.GetRandomInt(0, arr.Length);
                int index = this.GetRandomInt(0, arr.Length);
                T local = arr[randomInt];
                arr[randomInt] = arr[index];
                arr[index] = local;
            }
        }

        public double GetRandomDouble()
        {
            return this._random.NextDouble();
        }

        public int GetRandomInt(int minNum, int maxNum)
        {
            return this._random.Next(minNum, maxNum);
        }
    }
}

