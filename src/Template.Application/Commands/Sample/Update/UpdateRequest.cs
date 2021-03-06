﻿using Kit.Application.Models.Requests;

namespace Template.Application.Commands.Sample.Update
{
    public class UpdateRequest : CommandRequest
    {
        public UpdateRequest(string id, string description)
        {
            Id = id;
            Description = description;

            AddNotifications(new UpdateRequestValidator().Validate(this));
        }

        public string Id { get; }

        public string Description { get; }
    }
}