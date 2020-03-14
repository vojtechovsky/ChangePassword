using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ChangePassword
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            SetExceptionHandler();

            //add THINFINITY VIRTUALUI
            new Cybele.Thinfinity.VirtualUI().Start();

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new FormChangePasswordDialog());
        }

        public static void SetExceptionHandler()
        {
            // Setup unhandled exception handlers
            //CLR
            AppDomain.CurrentDomain.UnhandledException += OnUnhandledException;

            // Windows Forms
            Application.ThreadException += OnGuiUnhandedException;
        }

        // CLR unhandled exception
        private static void OnUnhandledException(object sender, UnhandledExceptionEventArgs e)
        {

            //this.log.Error(e.ExceptionObject);
            HandleUnhandledException(e.ExceptionObject);
        }

        // Windows Forms unhandled exception
        private static void OnGuiUnhandedException(object sender, ThreadExceptionEventArgs e)
        {
            //log.Error(e.Exception);
            HandleUnhandledException(e.Exception);
        }

        static void HandleUnhandledException(object exceptionObject)
        {
            Exception exception = exceptionObject as Exception;
            string message;

            if (exception != null)
            { // Report System.Exception info
                message = exception.GetType() + Environment.NewLine + exception.Message + Environment.NewLine + exception;
            }
            else
            { // Report exception Object info
                message = exceptionObject.GetType() + Environment.NewLine + exceptionObject;
            }
            
            FlexibleMessageBox.Show(message);
        }
    }
}
