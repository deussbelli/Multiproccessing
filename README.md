# Multiproccessing
A collection of problems solving problems of parallel computing and the use of multiprocessors

[![GitHub repo](https://img.shields.io/badge/GitHub-Multiprocessing-blue)](https://github.com/deussbelli/Multiproccessing.git)

## Overview
This repository contains a collection of problems and solutions related to parallel computing and the use of multiprocessor techniques. The projects focus on various aspects of parallel programming, from thread synchronization to complex mathematical operations and task management.

## Contents

### Laboratory 1: Matrix Sum Calculation
**Task**:
- Calculate the sum of the elements in a matrix by adding the elements of the upper triangular and lower triangular submatrices. 
- The matrix is generated randomly, and its size is input from the console.

### Laboratory 2: Thread Synchronization Using Semaphores
**Task**:
- A thread execution graph is provided where each task in the graph runs in its own thread.
- Synchronization between threads is implemented using semaphores.
- Global variables `x`, `y`, and `z` are initialized as follows: `x = 1`, `y = 5`, `z = 4`.

### Laboratory 3: Parallel Computation of Mathematical Expressions
**Task 1**:
- Write a parallel program that calculates the value of the expression `F` with each arithmetic operation performed in a separate thread.
- Variables are initialized in the thread where they are first used.
- Initial variable values: `x1 = 1`, `x2 = 2`, `x3 = 3`, `x4 = 4`, `x5 = 5`, `x6 = 6`.

**Expression**: F = x1 * x2 + x3 + x4 * x5 + x6


**Task 2**:
- Create a parallel program that computes a matrix expression.
- All matrices are square with dimension `N` and filled with random integers in the range `[-10, 10]`.
- The user specifies the number of worker threads `P` for parallel computation.
- Plot a graph showing the dependency of execution time on matrix size (`N = 10^3, 10^4, 10^5, ...`) for single-threaded mode and for the number of threads equal to the logical cores of the computer.

**Expression**: (M1 + M2) * (M3 + M4) + M5 * M6


### Laboratory 4: Thread Classes for Number Writing
**Task**:
- Create two new thread classes `OddWriter` and `NonOddWriter` (derived from the `Thread` class) that print an ordered sequence of unique random integers from `0` to `100`, sorted in ascending order.
- The `OddWriter` thread should print only odd numbers, while the `NonOddWriter` thread should print only even numbers.
- Additional threads may be created as needed to achieve the task.

### Laboratory 5: Custom Multithreaded Program
**Task**:
- Develop a multithreaded program that creates an array of 10 threads.
- Each thread's constructor should receive a function with three arguments:
  - First argument: first name
  - Second argument: last name
  - Third argument: a random integer in the range `0` to `9`

**Function Logic**:
- Print only the first name if the third argument is even, and only the last name if the third argument is odd.

## How to Run
1. Clone the repository:
   ```bash
   git clone https://github.com/deussbelli/Multiproccessing.git

