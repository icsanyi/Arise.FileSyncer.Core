using System;
using System.Collections.Generic;
using System.IO;
using Arise.FileSyncer.Core.FileSync;
using Arise.FileSyncer.Core.Helpers;
using Arise.FileSyncer.Serializer;

namespace Arise.FileSyncer.Core.Messages
{
    internal class DeleteDirectoriesMessage : NetMessage
    {
        public Guid ProfileId { get; set; }
        public Guid Key { get; set; }
        public IList<string> Directories { get; set; }

        public override NetMessageType MessageType => NetMessageType.DeleteDirectories;

        public DeleteDirectoriesMessage() { }

        public DeleteDirectoriesMessage(Guid profileId, SyncProfile syncProfile, IList<string> directories)
        {
            ProfileId = profileId;
            Key = syncProfile.Key;
            Directories = directories;
        }

        public override void Process(SyncerConnection con)
        {
            if (con.Owner.Profiles.GetProfile(ProfileId, out var profile))
            {
                if (profile.AllowDelete && profile.Key == Key)
                {
                    for (int i = 0; i < Directories.Count; i++)
                    {
                        Utility.DirectoryDelete(profile.RootDirectory, PathHelper.GetCorrect(Directories[i], true));
                    }

                    if (Directories.Count > 0)
                    {
                        profile.UpdateLastSyncDate(con.Owner.Profiles, ProfileId);
                    }
                }
            }
        }

        public override void Deserialize(Stream stream)
        {
            ProfileId = stream.ReadGuid();
            Key = stream.ReadGuid();
            Directories = stream.ReadStringArray();
        }

        public override void Serialize(Stream stream)
        {
            stream.WriteAFS(ProfileId);
            stream.WriteAFS(Key);
            stream.WriteAFS(Directories);
        }
    }
}
