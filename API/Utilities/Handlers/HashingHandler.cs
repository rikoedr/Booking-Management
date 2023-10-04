namespace API.Utilities.Handlers;


/*
 * Hashing Handler adalah class yang berfungsi untuk melakukan Hashing
 * terhadap password yang diterima.
 */

public class HashingHandler
{
    private static string GetRandomSalt()
    {
        return BCrypt.Net.BCrypt.GenerateSalt(12);
    }

    public static string HashPassword(string password)
    {
        return BCrypt.Net.BCrypt.HashPassword(password, GetRandomSalt());
    }

    public static bool VerifyPassword(string password, string hashedPassword)
    {
        return BCrypt.Net.BCrypt.Verify(password, hashedPassword);
    }
}