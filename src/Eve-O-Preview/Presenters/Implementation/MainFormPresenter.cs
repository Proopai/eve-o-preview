using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
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

			this.View.IconName = this._configuration.IconName;
		}

		private void Activate()
		{
			this._suppressSizeNotifications = true;
			this.LoadApplicationSettings();
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
			this.View.OnlyRegisterCycleHotkeysWhenEveFocused = this._configuration.OnlyRegisterCycleHotkeysWhenEveFocused;
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
		}

		private async void SaveApplicationSettings()
		{
			this._configuration.MinimizeToTray = this.View.MinimizeToTray;

			this._configuration.ThumbnailOpacity = (float)this.View.ThumbnailOpacity;

			this._configuration.EnableClientLayoutTracking = this.View.EnableClientLayoutTracking;
			this._configuration.HideActiveClientThumbnail = this.View.HideActiveClientThumbnail;
			this._configuration.MinimizeInactiveClients = this.View.MinimizeInactiveClients;

			if (this._configuration.HideCaptionOnClients != this.View.HideCaptionOnClients ) {
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
			this._configuration.OnlyRegisterCycleHotkeysWhenEveFocused = this.View.OnlyRegisterCycleHotkeysWhenEveFocused;
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
			return new ThumbnailDescription(title, isDisabled);
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