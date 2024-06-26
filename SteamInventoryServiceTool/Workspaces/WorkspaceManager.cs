using System;
using SteamInventoryServiceTool.Utility;

namespace SteamInventoryServiceTool.Workspaces;

public class WorkspaceManager : Singleton<WorkspaceManager>
{
    private Workspace _activeWorkspace;

    public event Action<Workspace> WorkspaceChanged;
    public Workspace ActiveWorkspace => _activeWorkspace;

    public WorkspaceManager()
    {
        NewWorkspace();
    }

    public void NewWorkspace()
    {
        SetActiveWorkspace(new Workspace(0, "New Workspace"));
    }

    public void OpenWorkspace(Workspace workspace)
    {
        if(workspace == null)
            return;
        SetActiveWorkspace(workspace);   
    }

    private void SetActiveWorkspace(Workspace workspace)
    {
        if (_activeWorkspace != null)
        {
            _activeWorkspace.Updated -= OnActiveWorkspaceUpdated;
        }
        _activeWorkspace = workspace;
        _activeWorkspace.Updated += OnActiveWorkspaceUpdated;
        WorkspaceChanged?.Invoke(_activeWorkspace);
    }

    private void OnActiveWorkspaceUpdated()
    {
        WorkspaceChanged?.Invoke(_activeWorkspace);
    }
}