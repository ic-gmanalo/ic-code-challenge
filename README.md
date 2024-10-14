1. This progam calls https://recruitment-test.investcloud.com/api/ to create 2 Lists of Tasks, 1000 for A and 1000 for B.
2. Then it builds a matrix by creating a list of tasks to call the api, then iterates over the list of tasks to read the response, deserialize,
   and finally creates a vector double, iterates through the data.Values and adds each row to the matrix.
3. Then it multiplies them together using MathNets Linear Algebra package.
4. Then it takes matrixC and turns it into 1 long string of the matrix's values example: "row1row2row3row4...rowN".
5. Next, it takes that string and sends it into the MD5.HashData function, 
   outputs the hex bytes and converts them to their ASCII character equivalents as a concatenated string.
6. This string is then sent in the body of the Post API as a Json object WITHOUT the brackets {}.

On average my time seems to be 8-9 seconds total, which I want to make fast, but I've run out of time.
I have found that you need a good internet connection to not run into any errors with my code, if I had some more time I would try to improve that.
Maybe batching the api calls would help.

The httpclient's were made using interfaces and dependency injection in order to make unit testing easier, but ended up being unnecessary as I couldnt figure out how to mock them properly.

The dependencies are:
MathNet.Numerics
NUnit
Microsoft.Extensions.DependencyInjection

The unit tests should be found in ic-code-challenge-tests, see the README in that project for more info on them, they definitely could be improved.

