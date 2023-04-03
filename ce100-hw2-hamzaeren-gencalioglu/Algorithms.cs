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
        /**
          *@name Matrix Chain Multiplication Order DP
          *
          *@brief Finds the optimal order of matrix multiplication using dynamic programming.
          *
          *@param matrixDimensionArray The array containing the dimensions of the matrices to be multiplied.
          *
          *@param matrixOrder The output string that stores the optimal order of matrix multiplication.
          *
          *@param operationCount The output integer that stores the minimum number of operations required for multiplication.
          *
          *@param n The number of matrices to be multiplied (derived from the length of matrixDimensionArray).
          *
          *@retval Returns 0 if the operation is successful, -1 if there are invalid inputs.
      **/


        public static int McmDP(int[] matrixDimensionArray, ref string matrixOrder, ref int operationCount, bool enableDebug = false)
        {
            // Find the number of matrices
            int n = matrixDimensionArray.Length - 1;

            // Check for invalid inputs.
            if (matrixDimensionArray == null || n < 2)
            {
                // Return -1 for invalid inputs
                return -1;
            }

            // Create the two matrices required for matrix multiplication.
            int[,] m = new int[n + 1, n + 1];  // Stores the minimum number of operations for multiplying sub-chains.
            int[,] s = new int[n + 1, n + 1];  // Stores the order of matrix multiplication.

            // Initialize the matrices to zero.
            for (int i = 1; i <= n; i++)
            {
                m[i, i] = 0;
            }

            // Fill the lower triangle
            for (int L = 2; L <= n; L++)  // Chain length
            {
                for (int i = 1; i <= n - L + 1; i++)  // Start matrix
                {
                    int j = i + L - 1;  // End matrix
                    m[i, j] = int.MaxValue;  // Set to maximum value initially.
                    for (int k = i; k <= j - 1; k++)  // Chain split point
                    {
                        int q = m[i, k] + m[k + 1, j] + matrixDimensionArray[i - 1] * matrixDimensionArray[k] * matrixDimensionArray[j];
                        if (q < m[i, j])  // Update the minimum number of operations and the order of matrix multiplication.
                        {
                            m[i, j] = q;
                            s[i, j] = k;
                        }

                        if (enableDebug)
                        {
                            Console.WriteLine($"i = {i}, j = {j}, k = {k}");
                            Console.WriteLine($"m[{i},{j}] = {m[i, j]}");
                            Console.WriteLine($"s[{i},{j}] = {s[i, j]}");
                            Console.WriteLine();
                        }
                    }
                }
            }

            // Assign the operation count and matrix multiplication order.
            operationCount = m[1, n];
            matrixOrder = PrintOrder(s, 1, n, enableDebug);

            // Return 0 for success.
            return 0;
        }


        /**
        *@name PrintOrder
        *@param s The table that contains the dimensions and multiplication order of the matrix.
        *@param i The index of the first matrix to be multiplied.
        *@param j The index of the last matrix to be multiplied.
        *@retval The multiplication order of the matrices in a string format.
        **/


        /* This code calculates the order of multiplication of a given matrix.
           The "s" parameter is the table that contains the dimensions and multiplication order of the matrix.
           The "i" and "j" parameters determine the indices of the matrices to be multiplied.*/
        private static string PrintOrder(int[,] s, int i, int j, bool enableDebug = false)
        {
            if (i == j)
            {
                return $"A{i}";
            }

            string leftMatrixOrder = PrintOrder(s, i, s[i, j], enableDebug);
            string rightMatrixOrder = PrintOrder(s, s[i, j] + 1, j, enableDebug);
            string multiplicationOrder = $"({leftMatrixOrder} x {rightMatrixOrder})";

            if (enableDebug)
            {
                Console.WriteLine($"i = {i}, j = {j}");
                Console.WriteLine($"s[{i},{j}] = {s[i, j]}");
                Console.WriteLine($"leftMatrixOrder = {leftMatrixOrder}");
                Console.WriteLine($"rightMatrixOrder = {rightMatrixOrder}");
                Console.WriteLine($"multiplicationOrder = {multiplicationOrder}");
                Console.WriteLine();
            }

            return multiplicationOrder;
        }

        /**
        * @name Matrix Chain Multiplication Order – Memorized Recursive Multiplication 
        * @brief Finds the optimal order of matrix multiplication using Memorized Recursive Multiplication.
        * @param matrixDimensionArray An array of integers representing the dimensions of the matrices to be multiplied.
        * The dimensions of matrix i is represented by the ith and (i+1)th elements in the array.
        * @param matrixOrder A reference string that will be populated with the optimal multiplication order.
        * @param operationCount A reference integer that will be populated with the total number of scalar multiplications needed.
        * The total number of scalar multiplications needed to multiply the chain of matrices.
        * @retval Returns 0 if the operation is successful, and -1 otherwise.                  
        **/
        public int Mcmrem(int[] matrixDimensionArray, ref string matrixOrder, ref int operationCount)
        {
            // obtains the number of matrices.
            int n = matrixDimensionArray.Length - 1;
            // Matrices m and s are created for the DP table.
            int[,] m = new int[n, n];
            int[,] s = new int[n, n];
            // The diagonal of the m matrix is set to 0, i.e. it becomes an upper triangular matrix.
            for (int i = 0; i < n; i++)
                m[i, i] = 0;
            // The DP table is filled.
            for (int l = 2; l <= n; l++)
            {
                for (int i = 1; i <= n - l + 1; i++)
                {
                    int j = i + l - 1;
                    m[i - 1, j - 1] = int.MaxValue;
                    // All matrix orders are compared.
                    for (int k = i; k <= j - 1; k++)
                    {
                        operationCount++;
                        // Calculate the cost of multiplication.
                        int q = m[i - 1, k - 1] + m[k, j - 1] + matrixDimensionArray[i - 1] * matrixDimensionArray[k] * matrixDimensionArray[j];
                        // The optimal matrix order is selected.
                        if (q < m[i - 1, j - 1])
                        {
                            m[i - 1, j - 1] = q;
                            s[i - 1, j - 1] = k;
                        }
                    }
                }
            }
            // The optimal matrix order is obtained.
            matrixOrder = PrintOptimalParens(s, 0, n - 1);
            // Matrix multiplication operation number is returned.
            return 0;
        }

        // Calculates the optimal matrix order.
        string PrintOptimalParens(int[,] s, int i, int j)
        {
            if (i == j)
                return "A" + (i + 1);
            else
            {
                string result = "(";
                // The matrix order is calculated recursively
                result += PrintOptimalParens(s, i, s[i, j] - 1);
                result += PrintOptimalParens(s, s[i, j], j);
                result += ")";
                return result;
            }
        }
    }
}




