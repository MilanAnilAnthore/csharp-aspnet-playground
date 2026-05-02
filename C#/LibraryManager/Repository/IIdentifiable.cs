using System;
using System.Collections.Generic;
using System.Text;

namespace LibraryManager.Repository
{
    public interface IIdentifiable
    {
        string Id { get; }
    }
}
