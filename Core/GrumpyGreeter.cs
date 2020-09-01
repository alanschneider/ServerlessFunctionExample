using System;
using System.Collections.Generic;
using System.Text;

namespace Core
{
    public class GrumpyGreeter : IGreeter
    {
        public string ReturnCannedResponse() => "Don't call me.";

        public string SayHello(string name)
        {
            var fixedName = !string.IsNullOrWhiteSpace(name) ? name : "loser";
            return $"Get lost, {fixedName}.";
        }
    }
}
