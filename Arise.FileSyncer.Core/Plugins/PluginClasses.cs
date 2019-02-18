﻿using System.Collections.Generic;
using Arise.FileSyncer.FileSync;

namespace Arise.FileSyncer.Plugins
{
    public abstract partial class Plugin
    {
        public class MSD_IN
        {
            public ISyncerConnection Connection;
            public SyncProfile Profile;
            public FileSystemItem[] LocalState;
            public FileSystemItem[] RemoteState;
        }

        public class MSD_OUT
        {
            public string[] Directories;
            public IList<string> Files;
            public IList<string> Redirects;
        }
    }
}