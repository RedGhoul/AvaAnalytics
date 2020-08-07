using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Application.Response
{
    public class CreateInteractionResponse
    {
        public MemoryStream Image { get; set; }
        public string ContentType { get; set; }
    }
}
