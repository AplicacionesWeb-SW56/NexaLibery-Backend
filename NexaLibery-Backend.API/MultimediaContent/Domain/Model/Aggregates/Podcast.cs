using NexaLibery_Backend.API.MultimediaContent.Domain.Model.Commands;

namespace NexaLibery_Backend.API.MultimediaContent.Domain.Model.Entities;

public class Podcast
{
    public int id { get;  private set ; }
    public string title {get; private set;}
    public string description { get;private  set; }
    
    public DateTime date { get;private set; }
    public string pic { get;private set; }
    public string url { get;private set; }
    public string premium {get; private set; }
    
    public Podcast(CreatePodcastCommand command)
    {
        this.title = command.title;
        this.description = command.description;
        this.pic = command.pic;
        this.url = command.url;
        this.premium = command.premium;
    }
    
}