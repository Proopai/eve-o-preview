using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using EveOPreview.Configuration;
using EveOPreview.Mediator.Messages;
using EveOPreview.View;
using MediatR;

namespace EveOPreview.Presenters
{
    public class MainFormPresenter : Presenter<IMainFormView>, IMainFormPresenter
    {
        #region Private constants
        private const string FORUM_URL = @"https://forums.eveonline.com/t/eve-o-preview-v8-0-2-0";
        #endregion

        #region Private fields
        private readonly IMediator _mediator;
        private readonly IThumbnailConfiguration _configuration;
        private readonly IConfigurationStorage _configurationStorage;
        private readonly IDictionary<string, IThumbnailDescription> _descriptionsCache;
        private bool _suppressSizeNotifications;

        private bool _exitApplication;
        #endregion

        public MainFormPresenter(IApplicationController controller, IMainFormView view, IMediator mediator, IThumbnailConfiguration configuration, IConfigurationStorage configurationStorage)
            : base(controller, view)
        {
            this._mediator = mediator;
            this._configuration = configuration;
            this._configurationStorage = configurationStorage;

            this._descriptionsCache = new Dictionary<string, IThumbnailDescription>();

            this._suppressSizeNotifications = false;
            this._exitApplication = false;

            this.View.FormActivated = this.Activate;
            this.View.FormMinimized = this.Minimize;
            this.View.FormCloseRequested = this.Close;
            this.View.ApplicationSettingsChanged = this.SaveApplicationSettings;
            this.View.ThumbnailsSizeChanged = this.UpdateThumbnailsSize;
            this.View.ThumbnailStateChanged = this.UpdateThumbnailState;
            this.View.DocumentationLinkActivated = this.OpenDocumentationLink;
            this.View.ApplicationExitRequested = this.ExitApplication;
            this.View.ClientHotkeyChanged = this.SaveClientHotkey;
            this.View.CycleGroupSelected = this.SelectCycleGroup;
            this.View.CycleGroupAddRequested = this.AddCycleGroup;
            this.View.CycleGroupRemoveRequested = this.RemoveCycleGroup;
            this.View.CycleGroupRenameRequested = this.RenameCycleGroup;
            this.View.CycleGroupHotkeyChanged = this.SaveCycleGroupHotkey;
            this.View.CycleGroupMembershipChanged = this.SaveCycleGroupMembership;
            this.View.MinimizeAllHotkeyChanged = this.SaveMinimizeAllHotkey;
            this.View.ProfileActivateRequested = this.ActivateProfile;
            this.View.ProfileNewRequested = this.CreateProfile;
            this.View.ProfileSaveRequested = this.SaveCurrentProfile;
            this.View.ProfileDeleteRequested = this.DeleteProfile;
            this.View.ProfileResetRequested = this.ResetProfileToDefaults;
            this.View.ProfileExportRequested = this.ExportConfiguration;
            this.View.ProfileImportRequested = this.ImportConfiguration;
            this.View.ThemeChanged = this.ChangeTheme;

            this.View.IconName = this._configuration.IconName;
        }

        private void Activate()
        {
            this._suppressSizeNotifications = true;
            this.LoadApplicationSettings();

            this.RefreshProfilesView();

            // Restore the window position/state saved on last exit (startup only — not on profile switch).
            this.View.RestoreWindowPlacement(this._configuration.MainWindowLocation, this._configuration.MainWindowMinimized);

            this.View.SetDocumentationUrl(MainFormPresenter.FORUM_URL);
            this.View.SetVersionInfo(this.GetApplicationVersion());
            if (this._configuration.MinimizeToTray)
            {
                this.View.Minimize();
            }

            this._mediator.Send(new StartService());
            this._suppressSizeNotifications = false;
        }

        private void Minimize()
        {
            if (!this._configuration.MinimizeToTray)
            {
                return;
            }

            this.View.Hide();
        }

        private void Close(ViewCloseRequest request)
        {
            if (this._exitApplication || !this.View.MinimizeToTray)
            {
                this._mediator.Send(new StopService()).Wait();

                // Remember where/how the window was when closed so we can reopen there.
                // (The live config already reflects any external edits via hot-reload, so this
                // targeted save does not clobber hand edits.)
                this._configuration.MainWindowLocation = this.View.CurrentWindowLocation;
                this._configuration.MainWindowMinimized = this.View.IsCurrentlyMinimized;
                this._configurationStorage.Save();

                request.Allow = true;
                return;
            }

            request.Allow = false;
            this.View.Minimize();
        }

        private async void UpdateThumbnailsSize()
        {
            if (!this._suppressSizeNotifications)
            {
                this.SaveApplicationSettings();
                await this._mediator.Publish(new ThumbnailConfiguredSizeUpdated());
            }
        }

        private void LoadApplicationSettings()
        {
            this._configurationStorage.Load();

            this.View.ApplyTheme(this._configuration.DarkMode);

            this.View.MinimizeToTray = this._configuration.MinimizeToTray;

            this.View.ThumbnailOpacity = this._configuration.ThumbnailOpacity;

            this.View.EnableClientLayoutTracking = this._configuration.EnableClientLayoutTracking;
            this.View.HideActiveClientThumbnail = this._configuration.HideActiveClientThumbnail;
            this.View.MinimizeInactiveClients = this._configuration.MinimizeInactiveClients;
            this.View.HideCaptionOnClients = this._configuration.HideCaptionOnClients;
            this.View.WindowsAnimationStyle = ViewAnimationStyleConverter.Convert(this._configuration.WindowsAnimationStyle);
            this.View.ShowThumbnailsAlwaysOnTop = this._configuration.ShowThumbnailsAlwaysOnTop;
            this.View.PreventPreviews = this._configuration.PreventPreviews;
            this.View.HideThumbnailsOnLostFocus = this._configuration.HideThumbnailsOnLostFocus;
            this.View.EnablePerClientThumbnailLayouts = this._configuration.EnablePerClientThumbnailLayouts;

            this.View.SetThumbnailSizeLimitations(this._configuration.ThumbnailMinimumSize, this._configuration.ThumbnailMaximumSize);
            this.View.ThumbnailSize = this._configuration.ThumbnailSize;

            this.View.EnableThumbnailZoom = this._configuration.ThumbnailZoomEnabled;
            this.View.ThumbnailZoomFactor = this._configuration.ThumbnailZoomFactor;
            this.View.ThumbnailZoomAnchor = ViewZoomAnchorConverter.Convert(this._configuration.ThumbnailZoomAnchor);
            this.View.OverlayLabelAnchor = ViewZoomAnchorConverter.Convert(this._configuration.OverlayLabelAnchor);
            this.View.CycleGroupIndicatorAnchor = ViewZoomAnchorConverter.Convert(this._configuration.CycleGroupIndicatorAnchor);

            this.View.ShowThumbnailOverlays = this._configuration.ShowThumbnailOverlays;
            this.View.ShowThumbnailFrames = this._configuration.ShowThumbnailFrames;
            this.View.LockThumbnailLocation = this._configuration.LockThumbnailLocation;
            this.View.ThumbnailSnapToGrid = this._configuration.ThumbnailSnapToGrid;
            this.View.ThumbnailSnapToGridSizeX = this._configuration.ThumbnailSnapToGridSizeX;
            this.View.ThumbnailSnapToGridSizeY = this._configuration.ThumbnailSnapToGridSizeY;
            this.View.EnableActiveClientHighlight = this._configuration.EnableActiveClientHighlight;
            this.View.ActiveClientHighlightColor = this._configuration.ActiveClientHighlightColor;
            this.View.PreventPreviewColor = this._configuration.PreventPreviewColor;

            this.View.OverlayLabelColor = this._configuration.OverlayLabelColor;
            this.View.OverlayLabelFont = this._configuration.OverlayLabelFont;


            this.View.IconName = this._configuration.IconName;

            this.PushCycleGroupsToView();
        }

        private async void SaveApplicationSettings()
        {
            this._configuration.MinimizeToTray = this.View.MinimizeToTray;

            this._configuration.ThumbnailOpacity = (float)this.View.ThumbnailOpacity;

            this._configuration.EnableClientLayoutTracking = this.View.EnableClientLayoutTracking;
            this._configuration.HideActiveClientThumbnail = this.View.HideActiveClientThumbnail;
            this._configuration.MinimizeInactiveClients = this.View.MinimizeInactiveClients;

            if (this._configuration.HideCaptionOnClients != this.View.HideCaptionOnClients)
            {
                this._configuration.HideCaptionOnClients = this.View.HideCaptionOnClients;
                await this._mediator.Publish(new ThumbnailFrameSettingsUpdated());
            }
            this._configuration.WindowsAnimationStyle = ViewAnimationStyleConverter.Convert(this.View.WindowsAnimationStyle);
            this._configuration.ShowThumbnailsAlwaysOnTop = this.View.ShowThumbnailsAlwaysOnTop;

            if (this._configuration.PreventPreviews != this.View.PreventPreviews)
            {
                this._configuration.PreventPreviews = this.View.PreventPreviews;
                await this._mediator.Publish(new ThumbnailFrameSettingsUpdated());
            }

            this._configuration.HideThumbnailsOnLostFocus = this.View.HideThumbnailsOnLostFocus;
            this._configuration.EnablePerClientThumbnailLayouts = this.View.EnablePerClientThumbnailLayouts;

            this._configuration.ThumbnailSize = this.View.ThumbnailSize;

            this._configuration.ThumbnailZoomEnabled = this.View.EnableThumbnailZoom;
            this._configuration.ThumbnailZoomFactor = this.View.ThumbnailZoomFactor;
            this._configuration.ThumbnailZoomAnchor = ViewZoomAnchorConverter.Convert(this.View.ThumbnailZoomAnchor);
            this._configuration.OverlayLabelAnchor = ViewZoomAnchorConverter.Convert(this.View.OverlayLabelAnchor);

            if (this._configuration.CycleGroupIndicatorAnchor != ViewZoomAnchorConverter.Convert(this.View.CycleGroupIndicatorAnchor))
            {
                this._configuration.CycleGroupIndicatorAnchor = ViewZoomAnchorConverter.Convert(this.View.CycleGroupIndicatorAnchor);
                await this._mediator.Publish(new ThumbnailCycleGroupIndicatorUpdated());
            }

            this._configuration.ShowThumbnailOverlays = this.View.ShowThumbnailOverlays;
            if (this._configuration.ShowThumbnailFrames != this.View.ShowThumbnailFrames)
            {
                this._configuration.ShowThumbnailFrames = this.View.ShowThumbnailFrames;
                await this._mediator.Publish(new ThumbnailFrameSettingsUpdated());
            }

            this._configuration.LockThumbnailLocation = this.View.LockThumbnailLocation;
            this._configuration.ThumbnailSnapToGrid = this.View.ThumbnailSnapToGrid;
            this._configuration.ThumbnailSnapToGridSizeX = this.View.ThumbnailSnapToGridSizeX;
            this._configuration.ThumbnailSnapToGridSizeY = this.View.ThumbnailSnapToGridSizeY;

            this._configuration.EnableActiveClientHighlight = this.View.EnableActiveClientHighlight;
            this._configuration.ActiveClientHighlightColor = this.View.ActiveClientHighlightColor;

            if (this._configuration.PreventPreviewColor != this.View.PreventPreviewColor)
            {
                this._configuration.PreventPreviewColor = this.View.PreventPreviewColor;
                await this._mediator.Publish(new ThumbnailFrameSettingsUpdated());
            }

            this._configuration.OverlayLabelColor = this.View.OverlayLabelColor;
            this._configuration.OverlayLabelFont = this.View.OverlayLabelFont;

            this._configuration.IconName = this.View.IconName;

            this._configurationStorage.Save();

            this.View.RefreshZoomSettings();

            await this._mediator.Send(new SaveConfiguration());
        }


        public void AddThumbnails(IList<string> thumbnailTitles)
        {
            IList<IThumbnailDescription> descriptions = new List<IThumbnailDescription>(thumbnailTitles.Count);

            lock (this._descriptionsCache)
            {
                foreach (string title in thumbnailTitles)
                {
                    IThumbnailDescription description = this.CreateThumbnailDescription(title);
                    this._descriptionsCache[title] = description;

                    descriptions.Add(description);
                }
            }

            this.View.AddThumbnails(descriptions);
        }

        public void RemoveThumbnails(IList<string> thumbnailTitles)
        {
            IList<IThumbnailDescription> descriptions = new List<IThumbnailDescription>(thumbnailTitles.Count);

            lock (this._descriptionsCache)
            {
                foreach (string title in thumbnailTitles)
                {
                    if (!this._descriptionsCache.TryGetValue(title, out IThumbnailDescription description))
                    {
                        continue;
                    }

                    this._descriptionsCache.Remove(title);
                    descriptions.Add(description);
                }
            }

            this.View.RemoveThumbnails(descriptions);
        }

        private IThumbnailDescription CreateThumbnailDescription(string title)
        {
            bool isDisabled = this._configuration.IsThumbnailDisabled(title);
            Keys hotkey = this._configuration.GetClientHotkey(title);
            return new ThumbnailDescription(title, isDisabled, hotkey);
        }

        // Apply a per-client hotkey change from the GUI live, without a restart (issue #13).
        private async void SaveClientHotkey(string title, Keys hotkey)
        {
            this._configuration.SetClientHotkey(title, hotkey);
            this._configurationStorage.Save();
            await this._mediator.Send(new RefreshHotkeys());
        }

        // Ids of the cycle-group / minimize-all hotkey boxes on the Hotkeys tab.
        private int _selectedCycleGroupIndex = 0;

        private List<CycleGroupConfiguration> CycleGroups =>
            this._configuration.CycleGroups ?? (this._configuration.CycleGroups = new List<CycleGroupConfiguration>());

        // Push the group list + the selected group's detail (hotkeys + membership) into the view.
        private void PushCycleGroupsToView()
        {
            List<CycleGroupConfiguration> groups = this.CycleGroups;

            if (this._selectedCycleGroupIndex >= groups.Count)
            {
                this._selectedCycleGroupIndex = groups.Count - 1;
            }
            if ((this._selectedCycleGroupIndex < 0) && (groups.Count > 0))
            {
                this._selectedCycleGroupIndex = 0;
            }

            this.View.SetCycleGroups(groups.Select(group => group.Name).ToList(), this._selectedCycleGroupIndex);
            this.PushSelectedCycleGroupDetail();
            this.View.SetMinimizeAllHotkey(this.FirstHotkey(this._configuration.MinimizeAllClientsHotkeys));
        }

        private void PushSelectedCycleGroupDetail()
        {
            List<CycleGroupConfiguration> groups = this.CycleGroups;

            if ((this._selectedCycleGroupIndex < 0) || (this._selectedCycleGroupIndex >= groups.Count))
            {
                this.View.SetCycleGroupDetail(Keys.None, Keys.None, new List<string>());
                return;
            }

            CycleGroupConfiguration group = groups[this._selectedCycleGroupIndex];
            List<string> members = (group.ClientsOrder ?? new Dictionary<string, int>())
                .OrderBy(pair => pair.Value)
                .Select(pair => pair.Key)
                .ToList();

            this.View.SetCycleGroupDetail(this.FirstHotkey(group.ForwardHotkeys), this.FirstHotkey(group.BackwardHotkeys), members);
        }

        private Keys FirstHotkey(List<string> list)
        {
            return ((list != null) && (list.Count > 0))
                ? this._configuration.StringToKey(list[0])
                : Keys.None;
        }

        private void SelectCycleGroup(int index)
        {
            this._selectedCycleGroupIndex = index;
            this.PushSelectedCycleGroupDetail();
        }

        private async void AddCycleGroup(string name)
        {
            string groupName = string.IsNullOrWhiteSpace(name) ? "New Group" : name.Trim();
            this.CycleGroups.Add(new CycleGroupConfiguration { Name = groupName });
            this._selectedCycleGroupIndex = this.CycleGroups.Count - 1;
            this._configurationStorage.Save();
            await this._mediator.Send(new RefreshHotkeys());
            this.PushCycleGroupsToView();
        }

        private async void RemoveCycleGroup(int index)
        {
            List<CycleGroupConfiguration> groups = this.CycleGroups;
            if ((index < 0) || (index >= groups.Count))
            {
                return;
            }

            groups.RemoveAt(index);
            if (this._selectedCycleGroupIndex >= groups.Count)
            {
                this._selectedCycleGroupIndex = groups.Count - 1;
            }

            this._configurationStorage.Save();
            await this._mediator.Send(new RefreshHotkeys());
            this.PushCycleGroupsToView();
        }

        private void RenameCycleGroup(int index, string name)
        {
            List<CycleGroupConfiguration> groups = this.CycleGroups;
            if ((index < 0) || (index >= groups.Count) || string.IsNullOrWhiteSpace(name))
            {
                return;
            }

            groups[index].Name = name.Trim();
            this._configurationStorage.Save();
            this.PushCycleGroupsToView();
        }

        private async void SaveCycleGroupHotkey(int index, bool isForward, Keys hotkey)
        {
            List<CycleGroupConfiguration> groups = this.CycleGroups;
            if ((index < 0) || (index >= groups.Count))
            {
                return;
            }

            List<string> list = (hotkey == Keys.None)
                ? new List<string>()
                : new List<string> { this._configuration.KeyToString(hotkey) };

            if (isForward)
            {
                groups[index].ForwardHotkeys = list;
            }
            else
            {
                groups[index].BackwardHotkeys = list;
            }

            this._configurationStorage.Save();
            await this._mediator.Send(new RefreshHotkeys());
        }

        private async void SaveCycleGroupMembership(int index, string clientTitle, bool isMember)
        {
            List<CycleGroupConfiguration> groups = this.CycleGroups;
            if ((index < 0) || (index >= groups.Count) || string.IsNullOrEmpty(clientTitle))
            {
                return;
            }

            CycleGroupConfiguration group = groups[index];
            if (group.ClientsOrder == null)
            {
                group.ClientsOrder = new Dictionary<string, int>();
            }

            if (isMember)
            {
                if (!group.ClientsOrder.ContainsKey(clientTitle))
                {
                    int nextOrder = (group.ClientsOrder.Count > 0) ? group.ClientsOrder.Values.Max() + 1 : 1;
                    group.ClientsOrder[clientTitle] = nextOrder;
                }
            }
            else
            {
                group.ClientsOrder.Remove(clientTitle);
            }

            this._configurationStorage.Save();
            await this._mediator.Send(new RefreshHotkeys());
        }

        private async void SaveMinimizeAllHotkey(Keys hotkey)
        {
            this._configuration.MinimizeAllClientsHotkeys = (hotkey == Keys.None)
                ? new List<string>()
                : new List<string> { this._configuration.KeyToString(hotkey) };
            this._configurationStorage.Save();
            await this._mediator.Send(new RefreshHotkeys());
        }

        // --- Profiles ---
        private void RefreshProfilesView()
        {
            this.View.SetProfiles(this._configurationStorage.GetProfileNames(), this._configurationStorage.ActiveProfileName);
        }

        private async void ActivateProfile(string name)
        {
            if (string.IsNullOrEmpty(name)
                || string.Equals(name, this._configurationStorage.ActiveProfileName, StringComparison.OrdinalIgnoreCase))
            {
                return;
            }

            ProfileSwitchChoice choice = this.View.PromptSaveBeforeSwitch(this._configurationStorage.ActiveProfileName);
            if (choice == ProfileSwitchChoice.Cancel)
            {
                this.RefreshProfilesView(); // revert the list selection
                return;
            }

            if (choice == ProfileSwitchChoice.Save)
            {
                this._configurationStorage.Save();
            }

            if (!this._configurationStorage.SwitchProfile(name))
            {
                this.View.ShowMessage("Could not switch to profile \"" + name + "\".");
                this.RefreshProfilesView();
                return;
            }

            await this.ApplyConfigurationToUi();
            this.RefreshProfilesView();
        }

        private void CreateProfile()
        {
            string name = this.View.PromptForProfileName();
            if (string.IsNullOrWhiteSpace(name))
            {
                return;
            }

            if (!this._configurationStorage.CreateProfile(name))
            {
                this.View.ShowMessage("Could not create profile. The name may be invalid or already in use.");
                return;
            }

            this.RefreshProfilesView();
        }

        private void SaveCurrentProfile()
        {
            this._configurationStorage.Save();
            this.View.ShowMessage("Profile \"" + this._configurationStorage.ActiveProfileName + "\" saved.");
        }

        private void DeleteProfile(string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                return;
            }

            if (!this._configurationStorage.DeleteProfile(name))
            {
                this.View.ShowMessage("Could not delete profile \"" + name + "\". The active profile and the last remaining profile cannot be deleted.");
                return;
            }

            this.RefreshProfilesView();
        }

        private async void ResetProfileToDefaults()
        {
            if (!this.View.PromptResetToDefaults())
            {
                return;
            }

            this._configurationStorage.ResetActiveProfileToDefaults();
            await this.ApplyConfigurationToUi();
            this.RefreshProfilesView();
        }

        private void ChangeTheme(bool dark)
        {
            this._configuration.DarkMode = dark;
            this._configurationStorage.Save();
            this.View.ApplyTheme(dark);
        }

        private void ExportConfiguration()
        {
            string path = this.View.PromptExportPath();
            if (string.IsNullOrEmpty(path))
            {
                return;
            }

            this.View.ShowMessage(this._configurationStorage.ExportProfile(path)
                ? "Configuration exported."
                : "Could not export the configuration.");
        }

        private void ImportConfiguration()
        {
            string path = this.View.PromptImportPath();
            if (string.IsNullOrEmpty(path))
            {
                return;
            }

            string name = this._configurationStorage.ImportProfile(path);
            if (name != null)
            {
                this.RefreshProfilesView();
                this.View.ShowMessage("Imported as profile \"" + name + "\". Select it in the list and click Activate to use it.");
            }
            else
            {
                this.View.ShowMessage("Could not import that file. Make sure it is a valid EVE-O Preview configuration.");
            }
        }

        // Re-applies the whole live configuration to the UI after a profile switch.
        private async System.Threading.Tasks.Task ApplyConfigurationToUi()
        {
            this.LoadApplicationSettings();
            this.View.RefreshZoomSettings();

            await this._mediator.Publish(new ThumbnailFrameSettingsUpdated());
            await this._mediator.Publish(new ThumbnailConfiguredSizeUpdated());
            await this._mediator.Publish(new ThumbnailCycleGroupIndicatorUpdated());
            await this._mediator.Send(new RefreshHotkeys());
        }

        private async void UpdateThumbnailState(String title)
        {
            if (this._descriptionsCache.TryGetValue(title, out IThumbnailDescription description))
            {
                this._configuration.ToggleThumbnail(title, description.IsDisabled);
            }

            await this._mediator.Send(new SaveConfiguration());
        }

        public void UpdateThumbnailSize(Size size)
        {
            this._suppressSizeNotifications = true;
            this.View.ThumbnailSize = size;
            this._suppressSizeNotifications = false;
        }

        private void OpenDocumentationLink()
        {
            // funtimes
            // https://brockallen.com/2016/09/24/process-start-for-urls-on-net-core/
            // https://github.com/dotnet/runtime/issues/17938

            // TODO Move out to a separate service / presenter / message handler
#if LINUX
			Process.Start("xdg-open", new Uri(MainFormPresenter.FORUM_URL).AbsoluteUri);
#else
            ProcessStartInfo processStartInfo = new ProcessStartInfo(new Uri(MainFormPresenter.FORUM_URL).AbsoluteUri);
            processStartInfo.UseShellExecute = true;
            Process.Start(processStartInfo);
#endif
        }

        private string GetApplicationVersion()
        {
            Version version = System.Reflection.Assembly.GetEntryAssembly().GetName().Version;
            string target = "Windows";
#if LINUX
  target = "Linux";
#endif
            return $"{version.Major}.{version.Minor}.{version.Build}.{version.Revision} {target}";
        }

        private void ExitApplication()
        {
            this._exitApplication = true;
            this.View.Close();
        }
    }
}