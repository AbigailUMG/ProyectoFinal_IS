using System.Text;
using System;
using System.Security.Cryptography;


namespace BackendApi.Services
{
    public class Encriptacion
    {
        public static string EncripContra(string contra)
        {
            using (SHA256 sha256Hash = SHA256.Create())
            {
                // ComputeHash - devuelve un arreglo de bytes
                byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(contra));

                // Convertir arreglo de bytes a una cadena hexadecimal
                StringBuilder stringBuilder = new StringBuilder();
                for (int i = 0; i < bytes.Length; i++)
                {
                    // Convertir cada byte en su representación hexadecimal de dos caracteres y concatenarlo
                    stringBuilder.Append(bytes[i].ToString("x2"));
                }

                return stringBuilder.ToString();
            }
        }
    }
}
