using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace FormApp.Dtos.UserDto
{
    public class AuthReponseDto
    {
        [JsonPropertyName("access_token")]
        public string Token { get; set; }

        [JsonPropertyName("token_type")]
        public string TokenType { get; set; }

        [JsonPropertyName("expired_in")]
        public int TotalSecond { get; set; }
    }
}
