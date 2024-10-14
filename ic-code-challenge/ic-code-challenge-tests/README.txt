These unit tests are very basic, I did not cover all possible scenarios.
The BuildMatricesTests.cs attempts to naively mock the httpclient but returns the same response for each success for fail condition.
The tests were build to simulate a [1,2] matrixA and [1,2] matrixB which would result in a [3,6] matrixC.
                                   [1,2]             [1,2]                                 [3,6]
I confirmed what the hash would be for the above matrixC and made sure that the MD5 hash correctly returns that value.

There is lots of room for improvement of these unit tests, and some that may be missing.
I ran out of time to get more into them, but I covered the major components.