﻿#pragma checksum "C:\Users\xvsnf\source\repos\CMP307\CMP307\Admin\EditRoom.xaml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "5088C0CF8A82F904C824396051C5825583B22476ED5FA386862BF8A41A498C2F"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace CMP307.Admin
{
    partial class EditRoom : 
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
            case 2: // Admin\EditRoom.xaml line 23
                {
                    this.createPanel = (global::Windows.UI.Xaml.Controls.StackPanel)(target);
                }
                break;
            case 3: // Admin\EditRoom.xaml line 25
                {
                    this.txtCurrentName = (global::Windows.UI.Xaml.Controls.TextBox)(target);
                }
                break;
            case 4: // Admin\EditRoom.xaml line 26
                {
                    this.txtNewName = (global::Windows.UI.Xaml.Controls.TextBox)(target);
                }
                break;
            case 5: // Admin\EditRoom.xaml line 27
                {
                    this.txtCapacity = (global::Windows.UI.Xaml.Controls.TextBox)(target);
                }
                break;
            case 6: // Admin\EditRoom.xaml line 28
                {
                    this.btnUpdate = (global::Windows.UI.Xaml.Controls.Button)(target);
                    ((global::Windows.UI.Xaml.Controls.Button)this.btnUpdate).Click += this.btnUpdate_Click;
                }
                break;
            case 7: // Admin\EditRoom.xaml line 29
                {
                    this.btnBack = (global::Windows.UI.Xaml.Controls.Button)(target);
                    ((global::Windows.UI.Xaml.Controls.Button)this.btnBack).Click += this.btnBack_Click;
                }
                break;
            case 8: // Admin\EditRoom.xaml line 30
                {
                    this.txtErr = (global::Windows.UI.Xaml.Controls.TextBlock)(target);
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

