using System;
using SteamInventoryServiceTool.Utility;

namespace SteamInventoryServiceTool.Workspace;

public class WorkspaceManager : Singleton<WorkspaceManager>
{
    private Workspace _activeWorkspace;

    public event Action<Workspace> WorkspaceChanged;
    public Workspace ActiveWorkspace => _activeWorkspace;

    public WorkspaceManager()
    {
        SetActiveWorkspace(new Workspace(0, "New Workspace"));
    }

    private void SetActiveWorkspace(Workspace workspace)
    {
        _activeWorkspace = workspace;
        WorkspaceChanged?.Invoke(_activeWorkspace);
    }
}