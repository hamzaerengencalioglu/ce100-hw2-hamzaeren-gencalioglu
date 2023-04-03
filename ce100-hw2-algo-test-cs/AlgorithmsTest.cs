﻿using ce100_hw2_algo_lib_cs;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace ce100_hw2_algo_test_cs
{
    [TestClass]
    public class AlgorithmsTest
    {

        [TestMethod]
        public void HeapSort_BestCase()
        {
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            // Create a sorted 1000 element array
            int[] inputArray = new int[1000];
            for (int i = 0; i < inputArray.Length; i++)
                inputArray[i] = i + 1; // Assign each element to its index plus one

            // Create an output array of the same size
            int[] outputArray = new int[1000];

            // Call the HeapSort method and check the result
            int result = Algorithms.HeapSort(inputArray, ref outputArray);
            Assert.AreEqual(0, result); // Check if the operation succeeded

            // Check if the output array is equal to the input array
            for (int i = 0; i < outputArray.Length; i++)
                Assert.AreEqual(inputArray[i], outputArray[i]); // Check if each element is equal to the corresponding element in the input array

            stopwatch.Stop();
            Console.WriteLine($"Time elapsed: {stopwatch.Elapsed}");

            long memoryUsed = GC.GetTotalMemory(true);
            Console.WriteLine($"Memory used: {memoryUsed}");
        }

        [TestMethod]
        public void HeapSort_AverageCase()
        {
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();

            // Create a random 1000 element array
            Random random = new Random();
            int[] inputArray = new int[1000];
            for (int i = 0; i < inputArray.Length; i++)
                inputArray[i] = random.Next(1, 1001);

            // Create an output array of the same size
            int[] outputArray = new int[1000];

            // Call the HeapSort method and check the result
            int result = Algorithms.HeapSort(inputArray, ref outputArray);
            Assert.AreEqual(0, result); // Check if the operation succeeded

            // Check if the output array is sorted in ascending order
            for (int i = 0; i < outputArray.Length - 1; i++)
                Assert.IsTrue(outputArray[i] <= outputArray[i + 1]); // Check if each element is less than or equal to the next element

            stopwatch.Stop();
            Console.WriteLine($"Time elapsed: {stopwatch.Elapsed}");

            long memoryUsed = GC.GetTotalMemory(true);
            Console.WriteLine($"Memory used: {memoryUsed}");
        }


        [TestMethod]
        public void HeapSort_WorstCase()
        {
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            // Create a reverse sorted 1000 element array
            int[] inputArray = new int[1000];
            for (int i = 0; i < inputArray.Length; i++)
                inputArray[i] = 1000 - i; // Assign each element to 1000 minus its index

            // Create an output array of the same size
            int[] outputArray = new int[1000];

            // Call the HeapSort method and check the result
            int result = Algorithms.HeapSort(inputArray, ref outputArray);
            Assert.AreEqual(0, result); // Check if the operation succeeded

            // Check if the output array is sorted in ascending order
            for (int i = 0; i < outputArray.Length - 1; i++)
                Assert.IsTrue(outputArray[i] <= outputArray[i + 1]); // Check if each element is less than or equal to the next element

            stopwatch.Stop();
            Console.WriteLine($"Time elapsed: {stopwatch.Elapsed}");

            long memoryUsed = GC.GetTotalMemory(true);
            Console.WriteLine($"Memory used: {memoryUsed}");
        }
        [TestMethod]
        public void Mcmdp_Test()
        {
            // Start the stopwatch to measure the elapsed time of the function.
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();

            // Define the dimensions of the input matrices and the expected result.
            int[] matrixDimensions = new int[] { 30, 35, 15, 5, 10, 20, 25 };
            int expectedOperationCount = 15125;
            string expectedMatrixOrder = "((A1 x (A2 x A3)) x ((A4 x A5) x A6))";

            // Call the McmDP function and get the output.
            string matrixOrder = "";
            int operationCount = 0;
            int result = Algorithms.McmDP(matrixDimensions, ref matrixOrder, ref operationCount);

            // Verify that the output is correct.
            Assert.AreEqual(0, result);
            Assert.AreEqual(expectedOperationCount, operationCount);
            Assert.AreEqual(expectedMatrixOrder, matrixOrder);

            // Stop the stopwatch and print the elapsed time.
            stopwatch.Stop();
            Console.WriteLine($"Time elapsed: {stopwatch.Elapsed}");

            // Get the total memory usage of the program and print it.
            long memoryUsed = GC.GetTotalMemory(true);
            Console.WriteLine($"Memory used: {memoryUsed}");
        }
    }
}