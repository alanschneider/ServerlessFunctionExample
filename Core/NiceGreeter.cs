using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Core
{
    public class NiceGreeter : IGreeter
    {
        public string ReturnCannedResponse() => "Call me with your name";

        public string SayHello(string name)
        {
            var fixedName = !string.IsNullOrWhiteSpace(name) ? name : "noname";
            return $"Hello, {fixedName}!";
        }
    }
}
