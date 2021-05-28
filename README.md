# GhostFunctions

GhostFunctions is a set of azure functions that handle events coming from the Ghost blogging platform such as new member creation, post and page publishing, or tag creation.

Check out the ghost documentation for a list of available [webhooks](https://ghost.org/docs/webhooks/)

# Setup and Testing

1. Clone the repo to your local machine
2. Create a local.settings.json file with the following format:

```
{
  "IsEncrypted": false,
  "Values": {
    "AzureWebJobsStorage": "UseDevelopmentStorage=true",
    "FUNCTIONS_WORKER_RUNTIME": "dotnet",
    "BaseDiscordUrl": "https://discord.com/api/webhooks",
    "DiscordUserName": "USERNAME TO DISPLAY IN DISCORD",
    "MemberDiscordChannel": "DISCORD CHANNEL WEBHOOK FOR POSTING NEW MEMBERS",
    "PageDiscordChannel": "DISCORD CHANNEL WEBHOOK FOR POSTING NEW PAGES",
    "PostDiscordChannel": "DISCORD CHANNEL WEBHOOK FOR POSTING NEW POSTS",
    "TagDiscordChannel": "DISCORD CHANNEL WEBHOOK FOR POSTING NEW TAGS"
  }
}
```

3. Run the project locally
4. Use Postman to make local requests. The request bodys should mirror the structure of the DTO events. For example, if you want to test a post being published, the body should resemble the following:

```
{
    current: {
        id: "",
        uuid: "",
        title: "",
        slug: "",
        authors: [],
        primary_author: "",
        tags: [],
        primary_tag: "",
        url: ""
    }
}
```

To get the format of the body, I setup [webhook.site](https://webhook.site) as a custom integration in ghost, and then removed it after testing the desired events.

# Installation

1. Create an Azure account
2. Create an Azure Function
3. Publish the project to the newly created Azure Function
4. Add the function urls as custom integrations in Ghost