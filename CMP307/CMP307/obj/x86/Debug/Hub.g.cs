﻿#pragma checksum "C:\Users\xvsnf\source\repos\CMP307\CMP307\Hub.xaml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "51D99472DBFB3680377F51FF546A3607CA3EE3EAAE23A411344C4CAF9A9E1815"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace CMP307
{
    partial class Hub : 
        global::Windows.UI.Xaml.Controls.Page, 
        global::Windows.UI.Xaml.Markup.IComponentConnector,
        global::Windows.UI.Xaml.Markup.IComponentConnector2
    {
        /// <summary>
        /// Connect()
        /// </summary>
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Windows.UI.Xaml.Build.Tasks"," 10.0.19041.685")]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public void Connect(int connectionId, object target)
        {
            switch(connectionId)
            {
            case 2: // Hub.xaml line 23
                {
                    this.meetingPanel = (global::Windows.UI.Xaml.Controls.StackPanel)(target);
                }
                break;
            case 3: // Hub.xaml line 26
                {
                    this.btnCreate = (global::Windows.UI.Xaml.Controls.Button)(target);
                    ((global::Windows.UI.Xaml.Controls.Button)this.btnCreate).Click += this.btnCreate_Click;
                }
                break;
            case 4: // Hub.xaml line 27
                {
                    this.btnEdit = (global::Windows.UI.Xaml.Controls.Button)(target);
                    ((global::Windows.UI.Xaml.Controls.Button)this.btnEdit).Click += this.btnEdit_Click;
                }
                break;
            case 5: // Hub.xaml line 28
                {
                    this.btnView = (global::Windows.UI.Xaml.Controls.Button)(target);
                    ((global::Windows.UI.Xaml.Controls.Button)this.btnView).Click += this.btnView_Click;
                }
                break;
            case 6: // Hub.xaml line 29
                {
                    this.btnPrevious = (global::Windows.UI.Xaml.Controls.Button)(target);
                    ((global::Windows.UI.Xaml.Controls.Button)this.btnPrevious).Click += this.btnPrevious_Click;
                }
                break;
            case 7: // Hub.xaml line 30
                {
                    this.btnLogout = (global::Windows.UI.Xaml.Controls.Button)(target);
                    ((global::Windows.UI.Xaml.Controls.Button)this.btnLogout).Click += this.btnLogout_Click;
                }
                break;
            default:
                break;
            }
            this._contentLoaded = true;
        }

        /// <summary>
        /// GetBindingConnector(int connectionId, object target)
        /// </summary>
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Windows.UI.Xaml.Build.Tasks"," 10.0.19041.685")]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public global::Windows.UI.Xaml.Markup.IComponentConnector GetBindingConnector(int connectionId, object target)
        {
            global::Windows.UI.Xaml.Markup.IComponentConnector returnValue = null;
            return returnValue;
        }
    }
}

