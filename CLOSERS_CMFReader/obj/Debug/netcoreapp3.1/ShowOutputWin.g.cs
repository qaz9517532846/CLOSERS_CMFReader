#pragma checksum "..\..\..\ShowOutputWin.xaml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "9372941688003EC87CADF58AA14BA34FC663B387"
//------------------------------------------------------------------------------
// <auto-generated>
//     這段程式碼是由工具產生的。
//     執行階段版本:4.0.30319.42000
//
//     對這個檔案所做的變更可能會造成錯誤的行為，而且如果重新產生程式碼，
//     變更將會遺失。
// </auto-generated>
//------------------------------------------------------------------------------

using CLOSERS_CMFReader;
using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Controls.Ribbon;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Effects;
using System.Windows.Media.Imaging;
using System.Windows.Media.Media3D;
using System.Windows.Media.TextFormatting;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Shell;


namespace CLOSERS_CMFReader {
    
    
    /// <summary>
    /// ShowOutputWin
    /// </summary>
    public partial class ShowOutputWin : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 1 "..\..\..\ShowOutputWin.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal CLOSERS_CMFReader.ShowOutputWin OutputWindows;
        
        #line default
        #line hidden
        
        
        #line 22 "..\..\..\ShowOutputWin.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label TermLable;
        
        #line default
        #line hidden
        
        
        #line 24 "..\..\..\ShowOutputWin.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox PathTextbox;
        
        #line default
        #line hidden
        
        
        #line 34 "..\..\..\ShowOutputWin.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button Browse_btn;
        
        #line default
        #line hidden
        
        
        #line 35 "..\..\..\ShowOutputWin.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label BrowseLable;
        
        #line default
        #line hidden
        
        
        #line 36 "..\..\..\ShowOutputWin.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button Cancle_btn;
        
        #line default
        #line hidden
        
        
        #line 37 "..\..\..\ShowOutputWin.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button OK_btn;
        
        #line default
        #line hidden
        
        
        #line 38 "..\..\..\ShowOutputWin.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label Processed_Lable;
        
        #line default
        #line hidden
        
        
        #line 39 "..\..\..\ShowOutputWin.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button Close_btn;
        
        #line default
        #line hidden
        
        
        #line 41 "..\..\..\ShowOutputWin.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ProgressBar progressbar_motion;
        
        #line default
        #line hidden
        
        
        #line 42 "..\..\..\ShowOutputWin.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.RichTextBox ShowProcessingText;
        
        #line default
        #line hidden
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.8.1.0")]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Uri resourceLocater = new System.Uri("/CLOSERS_CMFReader;component/showoutputwin.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\ShowOutputWin.xaml"
            System.Windows.Application.LoadComponent(this, resourceLocater);
            
            #line default
            #line hidden
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.8.1.0")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
        void System.Windows.Markup.IComponentConnector.Connect(int connectionId, object target) {
            switch (connectionId)
            {
            case 1:
            this.OutputWindows = ((CLOSERS_CMFReader.ShowOutputWin)(target));
            return;
            case 2:
            this.TermLable = ((System.Windows.Controls.Label)(target));
            return;
            case 3:
            this.PathTextbox = ((System.Windows.Controls.TextBox)(target));
            return;
            case 4:
            this.Browse_btn = ((System.Windows.Controls.Button)(target));
            
            #line 34 "..\..\..\ShowOutputWin.xaml"
            this.Browse_btn.Click += new System.Windows.RoutedEventHandler(this.Browse_btn_Click);
            
            #line default
            #line hidden
            return;
            case 5:
            this.BrowseLable = ((System.Windows.Controls.Label)(target));
            return;
            case 6:
            this.Cancle_btn = ((System.Windows.Controls.Button)(target));
            
            #line 36 "..\..\..\ShowOutputWin.xaml"
            this.Cancle_btn.Click += new System.Windows.RoutedEventHandler(this.Cancle_btn_Click);
            
            #line default
            #line hidden
            return;
            case 7:
            this.OK_btn = ((System.Windows.Controls.Button)(target));
            
            #line 37 "..\..\..\ShowOutputWin.xaml"
            this.OK_btn.Click += new System.Windows.RoutedEventHandler(this.OK_btn_Click);
            
            #line default
            #line hidden
            return;
            case 8:
            this.Processed_Lable = ((System.Windows.Controls.Label)(target));
            return;
            case 9:
            this.Close_btn = ((System.Windows.Controls.Button)(target));
            
            #line 39 "..\..\..\ShowOutputWin.xaml"
            this.Close_btn.Click += new System.Windows.RoutedEventHandler(this.Close_btn_Click);
            
            #line default
            #line hidden
            return;
            case 10:
            this.progressbar_motion = ((System.Windows.Controls.ProgressBar)(target));
            return;
            case 11:
            this.ShowProcessingText = ((System.Windows.Controls.RichTextBox)(target));
            return;
            }
            this._contentLoaded = true;
        }
    }
}

