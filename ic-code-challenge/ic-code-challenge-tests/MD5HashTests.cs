using System.Security.Cryptography;
using System.Text;
using icCodeChallenge;
//simulates what happens when hashing, not commenting every line for the unit tests

namespace icCodeChallengeTests
{
    [TestFixture]
    public class MD5HashTests
    {
        [Test]
        public void GenerateMD5Hash_Correct()
        {
            string input = "3636";
            byte[] data = MD5.HashData(Encoding.UTF8.GetBytes(input));
            string hash = data.Aggregate(new StringBuilder(), (s, i) => s.Append(i)).ToString();
            
            string expectedHash = "7244329174738025424218942175160185113161";

            
            Assert.That(hash, Is.EqualTo(expectedHash));
        }

        [Test]
        public void GenerateMD5Hash_Error()
        {
            
            string? input = null;

            Assert.Throws<ArgumentNullException>(() => MD5Hash.GenerateMD5Hash(input));
        }
    }
}