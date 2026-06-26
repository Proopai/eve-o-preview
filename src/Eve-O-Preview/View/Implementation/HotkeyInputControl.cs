using System;
using System.ComponentModel;
using System.Windows.Forms;

namespace EveOPreview.View
{
    /// <summary>
    /// A single-line control that captures a keyboard shortcut (modifier(s) + key) while
    /// focused, instead of letting the user type free text. It exposes the captured
    /// combination as a <see cref="Keys"/> value, which round-trips cleanly through
    /// <c>ThumbnailConfiguration.SetClientHotkey</c> / <c>StringToKey</c> (both use
    /// <see cref="KeysConverter"/> under the hood) — so the GUI is just the *write* side
    /// of the same canonical string contract the config loader reads. This removes the
    /// hand-edited-JSON workflow that produces the lowercase-keybind crash (issue #15).
    ///
    /// Drop-in: subclass of TextBox, so no .Designer.cs entry is required — add it in code,
    /// or promote a TextBox to this type in the designer.
    /// </summary>
    public sealed class HotkeyInputControl : TextBox
    {
        private Keys _hotkey = Keys.None;
        private bool _suppressChangeEvent;

        public HotkeyInputControl()
        {
            this.ReadOnly = true;          // capture only — no free-text typing
            this.ShortcutsEnabled = false; // no cut/paste context menu hijacking the combo
            this.Cursor = Cursors.Hand;
            this.Text = FormatHotkey(Keys.None);
        }

        /// <summary>Raised whenever the captured hotkey changes (including being cleared).</summary>
        public event EventHandler HotkeyChanged;

        // Assigns the hotkey without raising HotkeyChanged. Used when the presenter pushes
        // stored values into the control so it doesn't echo back as a user edit.
        public void SetHotkeySilently(Keys hotkey)
        {
            this._suppressChangeEvent = true;
            this.Hotkey = hotkey;
            this._suppressChangeEvent = false;
        }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public Keys Hotkey
        {
            get => _hotkey;
            set
            {
                if (_hotkey == value)
                {
                    return;
                }

                _hotkey = value;
                this.Text = FormatHotkey(value);
                if (!this._suppressChangeEvent)
                {
                    this.HotkeyChanged?.Invoke(this, EventArgs.Empty);
                }
            }
        }

        // Capture everything (including Tab/arrows/Enter) while focused so navigation keys
        // don't get swallowed by the dialog instead of being recorded as part of the combo.
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (this.Focused)
            {
                HandleKey(keyData);
                return true;
            }

            return base.ProcessCmdKey(ref msg, keyData);
        }

        protected override void OnKeyDown(KeyEventArgs e)
        {
            HandleKey(e.KeyData);
            e.SuppressKeyPress = true;
            e.Handled = true;
        }

        private void HandleKey(Keys keyData)
        {
            Keys keyCode = keyData & Keys.KeyCode;     // the "real" key
            Keys modifiers = keyData & Keys.Modifiers; // Alt / Control / Shift only

            // Backspace or Delete clears the binding.
            if (keyCode == Keys.Back || keyCode == Keys.Delete)
            {
                this.Hotkey = Keys.None;
                return;
            }

            // A lone modifier isn't a complete combo yet — show it as a pending prefix.
            // NOTE: the Win key is intentionally not supported here, because the existing
            // HotkeyHandler only registers Alt/Control/Shift via RegisterHotKey.
            if (keyCode == Keys.None
                || keyCode == Keys.ControlKey || keyCode == Keys.ShiftKey || keyCode == Keys.Menu
                || keyCode == Keys.LWin || keyCode == Keys.RWin)
            {
                this.Text = FormatHotkey(modifiers) + " ...";
                return;
            }

            this.Hotkey = modifiers | keyCode;
        }

        private static string FormatHotkey(Keys keys)
        {
            if (keys == Keys.None)
            {
                return "(click and press a key — Backspace to clear)";
            }

            // KeysConverter yields exactly the text the config stores, so what the user sees
            // here matches what gets written to disk.
            return new KeysConverter().ConvertToInvariantString(keys);
        }
    }
}