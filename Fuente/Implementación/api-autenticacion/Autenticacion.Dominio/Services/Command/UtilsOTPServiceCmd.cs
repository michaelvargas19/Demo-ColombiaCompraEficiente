using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Autenticacion.Dominio.Services.Command
{
    public class UtilsOTPServiceCmd
    {

        #region Numérico
            public static string tokenNumerico(int tokenLenght)
            {
                var token = string.Empty;
                while (token.Length != tokenLenght)
                {
                    if (token.Length > tokenLenght)
                    {
                        var order = token.ToCharArray();

                        token = string.Empty;
                        order.ToList().ForEach(x => { if (token.Length < tokenLenght) token += x; });
                    }
                    else
                    {
                        token = (new Random().Next(1, 5000) * DateTime.Now.Millisecond * new Random().Next(1, 100)).ToString();
                    }
                }
                return token;
            }

        #endregion



        #region Alfanumérico

            public static string tokenAlfanumerico(int tokenLenght)
            {
                var token = string.Empty;
                string[] arrayLetters = lettersArray();
                var rd = new Random();

                while (token.Length < tokenLenght)
                {
                    var valid = rd.Next(1, 10) % 2;
                    if (valid == 0)
                    {
                        //number
                        token += rd.Next(0, 9);
                    }
                    else
                    {
                        //letter
                        token += arrayLetters[rd.Next(0, 51)];
                    }
                }
                return token;
            }

        #endregion



        #region letras
            private static string[] lettersArray()
            {
            
                return new string[52] { "a", "A", "b", "B", "c", "C", "d", "D", "e", "E", "f", "F", "g", "G", "h", "H", "i", "I", "j", "J", "k", "K", "l", "L", "m", "M", "n", "N", "o", "O", "p", "P", "q", "Q", "r", "R", "s", "S", "t", "T", "u", "U", "v", "V", "w", "W", "x", "X", "y", "Y", "z", "Z" };
            
            }
        #endregion


    }
}
