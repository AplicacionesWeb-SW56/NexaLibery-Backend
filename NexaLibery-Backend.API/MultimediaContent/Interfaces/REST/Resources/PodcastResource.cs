﻿namespace NexaLibery_Backend.API.MultimediaContent.Interfaces.REST.Resources;

public record  PodcastResource(int id,string title, string description ,DateTime date,string pic, string url, string premium);