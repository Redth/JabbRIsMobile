using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cirrious.CrossCore.Platform;
using Cirrious.MvvmCross.ViewModels;
using Cirrious.MvvmCross.WindowsStore.Platform;
using Windows.UI.Xaml.Controls;

namespace JabbrIsMobile.Store
{
    public class Setup : MvxStoreSetup
    {
        public Setup(Frame rootFrame)
            : base(rootFrame)
        {
        }

        public class MvxTrace : IMvxTrace
        {
            public void Trace(MvxTraceLevel level, string tag, Func<string> message)
            {
                Debug.WriteLine(tag + ":" + level + ":" + message());
            }

            public void Trace(MvxTraceLevel level, string tag, string message)
            {
                Debug.WriteLine(tag + ": " + level + ": " + message);
            }

            public void Trace(MvxTraceLevel level, string tag, string message, params object[] args)
            {
                Debug.WriteLine(tag + ": " + level + ": " + message, args);
            }
        }
        protected override IMvxTrace CreateDebugTrace()
        {
            return new MvxTrace();
        }

        protected override IMvxApplication CreateApp()
        {
            return new JabbRIsMobile.App();
        }
    }
}
