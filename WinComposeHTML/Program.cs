using System.Runtime.InteropServices;
using System.Windows.Forms;
using System.Windows.Input;
using System;

namespace WinComposeHTML
{
    internal static class Program
    {
        static Form1? formLock = null;
        public static string lastText = "&amp;";

        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.
            ApplicationConfiguration.Initialize();

            using (NotifyIcon icon = new NotifyIcon())
            using (KeyboardHook hook = new KeyboardHook())
            {
                icon.Visible = true;
                icon.Icon = SystemIcons.Information;
                icon.Text = "WinComposeHTML";
                icon.ContextMenuStrip = new ContextMenuStrip();
                icon.ContextMenuStrip.Items.Add("Exit WinComposeHTML", null, (object? sender, EventArgs args) => { Application.Exit(); });
                hook.RegisterHotKey(ModifierKeys.Win | ModifierKeys.Shift, Keys.I);
                hook.KeyPressed += (object? sender, KeyPressedEventArgs args) =>
                {
                    if (formLock != null)
                        return;
                    formLock = new Form1();
                    formLock.Disposed += (object? sender, EventArgs args) => formLock = null;
                    formLock.Show();
                };

                Application.Run();
            }
        }
    }
}

public class KeyboardHook : IDisposable
{
    // Registers a hot key with Windows.
    [DllImport("user32.dll")]
    private static extern bool RegisterHotKey(IntPtr hWnd, int id, uint fsModifiers, uint vk);
    // Unregisters the hot key with Windows.
    [DllImport("user32.dll")]
    private static extern bool UnregisterHotKey(IntPtr hWnd, int id);

    /// <summary>
    /// Represents the window that is used internally to get the messages.
    /// </summary>
    private class Window : NativeWindow, IDisposable
    {
        private static int WM_HOTKEY = 0x0312;

        public Window()
        {
            // create the handle for the window.
            this.CreateHandle(new CreateParams());
        }

        /// <summary>
        /// Overridden to get the notifications.
        /// </summary>
        /// <param name="m"></param>
        protected override void WndProc(ref Message m)
        {
            base.WndProc(ref m);

            // check if we got a hot key pressed.
            if (m.Msg == WM_HOTKEY)
            {
                // get the keys.
                Keys key = (Keys)(((int)m.LParam >> 16) & 0xFFFF);
                ModifierKeys modifier = (ModifierKeys)((int)m.LParam & 0xFFFF);

                // invoke the event to notify the parent.
                if (KeyPressed != null)
                    KeyPressed(this, new KeyPressedEventArgs(modifier, key));
            }
        }

        public event EventHandler<KeyPressedEventArgs>? KeyPressed;

        public void Dispose()
        {
            this.DestroyHandle();
        }
    }

    private Window _window = new Window();
    private int _currentId = 0;

    public KeyboardHook()   
    {
        // register the event of the inner native window.
        _window.KeyPressed += delegate (object? sender, KeyPressedEventArgs args)
        {
            if (KeyPressed != null)
                KeyPressed(this, args);
        };
    }

    /// <summary>
    /// Registers a hot key in the system.
    /// </summary>
    /// <param name="modifier">The modifiers that are associated with the hot key.</param>
    /// <param name="key">The key itself that is associated with the hot key.</param>
    public void RegisterHotKey(ModifierKeys modifier, Keys key)
    {
        // increment the counter.
        _currentId = _currentId + 1;

        // register the hot key.
        if (!RegisterHotKey(_window.Handle, _currentId, (uint)modifier, (uint)key))
            throw new InvalidOperationException("Couldn’t register the hot key.");
    }

    /// <summary>
    /// A hot key has been pressed.
    /// </summary>
    public event EventHandler<KeyPressedEventArgs>? KeyPressed;

    public void Dispose()
    {
        // unregister all the registered hot keys.
        for (int i = _currentId; i > 0; i--)
        {
            UnregisterHotKey(_window.Handle, i);
        }

        // dispose the inner native window.
        _window.Dispose();
    }
}

/// <summary>
/// Event Args for the event that is fired after the hot key has been pressed.
/// </summary>
public class KeyPressedEventArgs : EventArgs
{
    private ModifierKeys _modifier;
    private Keys _key;

    internal KeyPressedEventArgs(ModifierKeys modifier, Keys key)
    {
        _modifier = modifier;
        _key = key;
    }

    public ModifierKeys Modifier
    {
        get { return _modifier; }
    }

    public Keys Key
    {
        get { return _key; }
    }
}

/// <summary>
/// The enumeration of possible modifiers.
/// </summary>
[Flags]
public enum ModifierKeys : uint
{
    Alt = 1,
    Control = 2,
    Shift = 4,
    Win = 8
}