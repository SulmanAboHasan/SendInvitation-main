﻿namespace DemoUserManagementQuerySide.EventsHandler.Removed
{
    public record MemberRemovedData(
       string Id,
       string AccountId,
       string SubscriptionId,
       string MemberId
        );

   
}
