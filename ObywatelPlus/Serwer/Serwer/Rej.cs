using System;
using System.Collections.Generic;
using System.Text;

namespace Serwer
{
    class Rej
    {
        public string RejNumber { get; set; }
        public string RejComment{ get; set; }
        public Rej(string rejNumber, string rejComment)
        {
            RejComment = rejComment;
            RejNumber = rejNumber;
        }
    }
}
