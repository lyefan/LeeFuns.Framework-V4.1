using System;
using System.IO;

namespace LeeFuns.Utilities
{
    public class FileDateSorter
    {
        private static int Partition(FileInfo[] arr, int low, int high)
        {
            FileInfo info = arr[low];
            while (low < high)
            {
                while ((low < high) && (arr[high].CreationTime <= info.CreationTime))
                {
                    high--;
                }
                Swap(ref arr[high], ref arr[low]);
                while ((low < high) && (arr[low].CreationTime >= info.CreationTime))
                {
                    low++;
                }
                Swap(ref arr[high], ref arr[low]);
            }
            arr[low] = info;
            return low;
        }

        public static void QuickSort(FileInfo[] arr, int low, int high)
        {
            if (low <= (high - 1))
            {
                int num = Partition(arr, low, high);
                QuickSort(arr, low, num - 1);
                QuickSort(arr, num + 1, high);
            }
        }

        private static void Swap(ref FileInfo i, ref FileInfo j)
        {
            FileInfo info = i;
            i = j;
            j = info;
        }
    }
}

