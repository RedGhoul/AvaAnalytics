using System.IO;

namespace Application.Response
{
    public class CreateInteractionResponse
    {
        public MemoryStream Image { get; set; }
        public string ContentType { get; set; }
    }
}
