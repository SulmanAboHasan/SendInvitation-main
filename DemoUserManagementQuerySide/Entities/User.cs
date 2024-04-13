﻿namespace DemoUserManagementQuerySide.Entities
{
    
        public class User
        {
            private User(string id, string name)
            {
                Id = id;
                Name = name;
            }
            public string Id { get; private set; }
            public string Name { get; private set; }
        }
    
}
