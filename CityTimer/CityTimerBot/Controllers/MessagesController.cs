﻿using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using Microsoft.Bot.Connector;
using Newtonsoft.Json;
using Microsoft.Bot.Builder.Dialogs;
using CityTimerBot.Models;
using System.Diagnostics;

namespace CityTimerBot
{
    [BotAuthentication]
    public class MessagesController : ApiController
    {
        /// <summary>
        /// POST: api/Messages
        /// receive a message from a user and send replies
        /// </summary>
        /// <param name="activity"></param>
        [ResponseType(typeof(void))]
        public virtual async Task<HttpResponseMessage> Post([FromBody] Activity activity)
        {
            if (activity != null)
            {
                // one of these will have an interface and process it
                switch (activity.GetActivityType())
                {
                    case ActivityTypes.Message:
                        await Conversation.SendAsync(activity, () => EchoChainDialog.dialog);
                        break;

                    case ActivityTypes.ConversationUpdate:
                        //IConversationUpdateActivity update = activity;
                        //using (var scope = DialogModule.BeginLifetimeScope(Conversation.Container, activity))
                        //{
                        //    var client = scope.Resolve<IConnectorClient>();
                        //    if (update.MembersAdded.Any())
                        //    {
                        //        var reply = activity.CreateReply();
                        //        foreach (var newMember in update.MembersAdded)
                        //        {
                        //            if (newMember.Id != activity.Recipient.Id)
                        //            {
                        //                reply.Text = $"Welcome {newMember.Name}!";
                        //            }
                        //            else
                        //            {
                        //                reply.Text = $"Welcome {activity.From.Name}";
                        //            }
                        //            await client.Conversations.ReplyToActivityAsync(reply);
                        //        }
                        //    }
                        //}
                        //break;
                    case ActivityTypes.ContactRelationUpdate:
                    case ActivityTypes.Typing:
                    case ActivityTypes.DeleteUserData:
                    case ActivityTypes.Ping:
                    default:
                        Trace.TraceError($"Unknown activity type ignored: {activity.GetActivityType()}");
                        break;
                }
            }
            return new HttpResponseMessage(System.Net.HttpStatusCode.Accepted);
        }
    }
}