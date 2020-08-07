using Application.Response;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Commands
{
    public class CreateInteractionCommand : IRequest<CreateInteractionResponse>
    {
        public CreateInteractionCommand()
        {
            CreatedAt = DateTime.UtcNow;
        }
        public string Browser { get; set; }
        public string Language { get; set; }
        public string Referrer { get; set; }
        public string Path { get; set; }
        public string Title { get; set; }
        public string ScreenSizeAttr { get; set; }
        public int IsBot { get; set; }
        public string Session { get; set; }
        public string Key { get; set; }
        public string RemoteIpAddress { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
