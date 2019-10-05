using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CadastroProdutos.Models
{
    public class Token
    {
        public string CurrentToken { get; set; }
        public string RefreshToken { get; set; }
        public string TokenType { get; set; } = "bearer";
        public long ExpiresIn { get; set; }
    }

    public class RefreshToken
    {
        public string UserName { get; set; }
        public string Token { get; set; }
        public DateTime ExpirationDate { get; set; }
    }
}
