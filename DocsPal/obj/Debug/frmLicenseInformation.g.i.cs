﻿#pragma checksum "..\..\frmLicenseInformation.xaml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "3B9A1DEF13F99D021154034B65E3C11E5E662963"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
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


namespace DocWriter {
    
    
    /// <summary>
    /// frmLicenseInformation
    /// </summary>
    public partial class frmLicenseInformation : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 6 "..\..\frmLicenseInformation.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btnCancel;
        
        #line default
        #line hidden
        
        
        #line 8 "..\..\frmLicenseInformation.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox txtComCode;
        
        #line default
        #line hidden
        
        
        #line 10 "..\..\frmLicenseInformation.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox txtActivationCode;
        
        #line default
        #line hidden
        
        
        #line 11 "..\..\frmLicenseInformation.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btnActiveRemoveLicense;
        
        #line default
        #line hidden
        
        
        #line 12 "..\..\frmLicenseInformation.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btnRemoveLicense;
        
        #line default
        #line hidden
        
        
        #line 13 "..\..\frmLicenseInformation.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btnGenActiveCode;
        
        #line default
        #line hidden
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Uri resourceLocater = new System.Uri("/DocWriter;component/frmlicenseinformation.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\frmLicenseInformation.xaml"
            System.Windows.Application.LoadComponent(this, resourceLocater);
            
            #line default
            #line hidden
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
        void System.Windows.Markup.IComponentConnector.Connect(int connectionId, object target) {
            switch (connectionId)
            {
            case 1:
            this.btnCancel = ((System.Windows.Controls.Button)(target));
            
            #line 6 "..\..\frmLicenseInformation.xaml"
            this.btnCancel.Click += new System.Windows.RoutedEventHandler(this.btnCancel_Click);
            
            #line default
            #line hidden
            return;
            case 2:
            this.txtComCode = ((System.Windows.Controls.TextBox)(target));
            
            #line 8 "..\..\frmLicenseInformation.xaml"
            this.txtComCode.KeyDown += new System.Windows.Input.KeyEventHandler(this.txtComCode_KeyDown);
            
            #line default
            #line hidden
            return;
            case 3:
            this.txtActivationCode = ((System.Windows.Controls.TextBox)(target));
            
            #line 10 "..\..\frmLicenseInformation.xaml"
            this.txtActivationCode.KeyUp += new System.Windows.Input.KeyEventHandler(this.txtActivationCode_KeyUp);
            
            #line default
            #line hidden
            return;
            case 4:
            this.btnActiveRemoveLicense = ((System.Windows.Controls.Button)(target));
            
            #line 11 "..\..\frmLicenseInformation.xaml"
            this.btnActiveRemoveLicense.Click += new System.Windows.RoutedEventHandler(this.btnActiveRemoveLicense_Click);
            
            #line default
            #line hidden
            return;
            case 5:
            this.btnRemoveLicense = ((System.Windows.Controls.Button)(target));
            
            #line 12 "..\..\frmLicenseInformation.xaml"
            this.btnRemoveLicense.Click += new System.Windows.RoutedEventHandler(this.btnRemoveLicense_Click);
            
            #line default
            #line hidden
            return;
            case 6:
            this.btnGenActiveCode = ((System.Windows.Controls.Button)(target));
            
            #line 13 "..\..\frmLicenseInformation.xaml"
            this.btnGenActiveCode.Click += new System.Windows.RoutedEventHandler(this.btnGenActiveCode_Click);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}

