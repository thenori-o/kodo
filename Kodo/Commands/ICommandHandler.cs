﻿using System.CommandLine;

namespace Kodo.Commands
{
    public interface ICommandHandler
    {
        Command GetCommand();
    }

    public interface ISubCommandHandler : ICommandHandler { }
}
