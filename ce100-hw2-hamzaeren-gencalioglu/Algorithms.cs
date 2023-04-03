using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ce100_hw2_algo_lib_cs
{
    public class Algorithms
    {
        /**
        *@name HeapSort
        *
        *@brief Sorts an input array using the HeapSort algorithm.
        *
        *@param inputArray The input array to be sorted.
        *
        *@param outputArray The output array to store the sorted elements.
        *
        *@retval Returns 0 if the operation is successful, and -1 otherwise.
         **/

        public static int HeapSort(int[] inputArray, ref int[] outputArray, bool enableDebug = false)
        {
            try
            {
                // Check if the length of input and output arrays are the same.
                if (inputArray.Length != outputArray.Length)
                    throw new ArgumentException("The length of input and output arrays are not the same.");

                // Copy the input array to the output array.
                for (int i = 0; i < inputArray.Length; i++)
                    outputArray[i] = inputArray[i];

                // Apply the HeapSort algorithm on the output array.
                int n = outputArray.Length;
                for (int i = n / 2 - 1; i >= 0; i--)
                {
                    if (enableDebug)
                        Console.WriteLine($"Heapify up: outputArray={string.Join(",", outputArray)}, n={n}, i={i}");
                    Heapify(ref outputArray, n, i);
                }

                for (int i = n - 1; i >= 0; i--)
                {
                    int temp = outputArray[0];
                    outputArray[0] = outputArray[i];
                    outputArray[i] = temp;
                    if (enableDebug)
                        Console.WriteLine($"Heapify down: outputArray={string.Join(",", outputArray)}, i={i}");
                    Heapify(ref outputArray, i, 0);
                }

                return 0; // Returns 0 if the operation is successful.
            }
            catch (Exception ex)
            {
                // Handle the exception if an error occurs.
                Console.WriteLine(ex.Message);
                return -1; // Returns -1 if the operation is unsuccessful.
            }
        }


        /**
            *@name  Heapify
            *
            *@brief Reorders a subtree to maintain the max heap property.
            *
            *@param array The array to be reorganized.
            *
            *@param n The size of the heap.
            *
            *@param i The index of the subtree's root node.
            **/
        static void Heapify(ref int[] array, int n, int i)
        {
            // Select the root element as the largest element initially.
            int largest = i;

            // Calculate the position of the left child of the root element in the array.
            int left = 2 * i + 1;

            // Calculate the position of the right child of the root element in the array.
            int right = 2 * i + 2;

            /* If the left child is smaller than the size of the array and greater than the largest element,
               set the largest element as the left child. */
            if (left < n && array[left] > array[largest])
                largest = left;

            /* If the right child is smaller than the size of the array and greater than the largest element,
               set the largest element as the right child. */
            if (right < n && array[right] > array[largest])
                largest = right;

            /* If the largest element is not the root element, swap the largest element with the root element
               and repeat the Heapify process on the lower sections. */
            if (largest != i)
            {
                int swap = array[i];
                array[i] = array[largest];
                array[largest] = swap;
                Heapify(ref array, n, largest);
            }

        }



    }
}




