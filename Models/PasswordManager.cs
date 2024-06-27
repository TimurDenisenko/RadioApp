
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using RadioApp.Views;
using System.Security.Cryptography;

namespace RadioApp.Models
{
    public class PasswordManager
    {
        //I don't want to complicate the pet project too much soo..
        private static byte[] globalSalt = [234, 21, 53, 231, 67, 21, 98, 74, 52, 125, 151, 182, 213, 149, 215, 43];
        public static (byte[], string) HashPassword(string password)
        {
            byte[] salt = new byte[16];
            RandomNumberGenerator.Create().GetBytes(salt);
            byte[] combinedSalt = new byte[salt.Length + globalSalt.Length];
            Buffer.BlockCopy(salt, 0, combinedSalt, 0, salt.Length);
            Buffer.BlockCopy(globalSalt, 0, combinedSalt, salt.Length, globalSalt.Length);
            return (combinedSalt, Hash(password, combinedSalt));
        }
        protected static string Hash(string password, byte[] combinedSalt) =>
            Convert.ToBase64String(KeyDerivation.Pbkdf2(
                password: password,
                salt: combinedSalt,
                prf: KeyDerivationPrf.HMACSHA256,
                iterationCount: 10000,
                numBytesRequested: 32));
        public static bool Verify(string name, string password)
        {
            UserModel? user = (App.DatabaseUser.GetElements() as UserModel[]).Where(x => x.Name == name).ToArray()[0];
            string verifyHash = Hash(password, Convert.FromBase64String(user.Salt));
            return verifyHash == user.Hash;

        }
    }
}
