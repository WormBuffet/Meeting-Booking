﻿#pragma checksum "C:\Users\xvsnf\source\repos\CMP307\CMP307\CreateMeeting.xaml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "B9E3A8282874C35EFE84CBBE82F2A528F041697DCBEFB817A5E67A2455363153"
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
    partial class CreateMeeting : 
        global::Windows.UI.Xaml.Controls.Page, 
        global::Windows.UI.Xaml.Markup.IComponentConnector,
        global::Windows.UI.Xaml.Markup.IComponentConnector2
    {
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Windows.UI.Xaml.Build.Tasks"," 10.0.19041.685")]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        private static class XamlBindingSetters
        {
            public static void Set_Windows_UI_Xaml_Controls_ItemsControl_ItemsSource(global::Windows.UI.Xaml.Controls.ItemsControl obj, global::System.Object value, string targetNullValue)
            {
                if (value == null && targetNullValue != null)
                {
                    value = (global::System.Object) global::Windows.UI.Xaml.Markup.XamlBindingHelper.ConvertValue(typeof(global::System.Object), targetNullValue);
                }
                obj.ItemsSource = value;
            }
        };

        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Windows.UI.Xaml.Build.Tasks"," 10.0.19041.685")]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        private class CreateMeeting_obj1_Bindings :
            global::Windows.UI.Xaml.Markup.IDataTemplateComponent,
            global::Windows.UI.Xaml.Markup.IXamlBindScopeDiagnostics,
            global::Windows.UI.Xaml.Markup.IComponentConnector,
            ICreateMeeting_Bindings
        {
            private global::CMP307.CreateMeeting dataRoot;
            private bool initialized = false;
            private const int NOT_PHASED = (1 << 31);
            private const int DATA_CHANGED = (1 << 30);

            // Fields for each control that has bindings.
            private global::Windows.UI.Xaml.Controls.ListView obj7;
            private global::Windows.UI.Xaml.Controls.ComboBox obj14;

            // Static fields for each binding's enabled/disabled state
            private static bool isobj7ItemsSourceDisabled = false;
            private static bool isobj14ItemsSourceDisabled = false;

            public CreateMeeting_obj1_Bindings()
            {
            }

            public void Disable(int lineNumber, int columnNumber)
            {
                if (lineNumber == 66 && columnNumber == 164)
                {
                    isobj7ItemsSourceDisabled = true;
                }
                else if (lineNumber == 39 && columnNumber == 172)
                {
                    isobj14ItemsSourceDisabled = true;
                }
            }

            // IComponentConnector

            public void Connect(int connectionId, global::System.Object target)
            {
                switch(connectionId)
                {
                    case 7: // CreateMeeting.xaml line 66
                        this.obj7 = (global::Windows.UI.Xaml.Controls.ListView)target;
                        break;
                    case 14: // CreateMeeting.xaml line 39
                        this.obj14 = (global::Windows.UI.Xaml.Controls.ComboBox)target;
                        break;
                    default:
                        break;
                }
            }

            // IDataTemplateComponent

            public void ProcessBindings(global::System.Object item, int itemIndex, int phase, out int nextPhase)
            {
                nextPhase = -1;
            }

            public void Recycle()
            {
                return;
            }

            // ICreateMeeting_Bindings

            public void Initialize()
            {
                if (!this.initialized)
                {
                    this.Update();
                }
            }
            
            public void Update()
            {
                this.Update_(this.dataRoot, NOT_PHASED);
                this.initialized = true;
            }

            public void StopTracking()
            {
            }

            public void DisconnectUnloadedObject(int connectionId)
            {
                throw new global::System.ArgumentException("No unloadable elements to disconnect.");
            }

            public bool SetDataRoot(global::System.Object newDataRoot)
            {
                if (newDataRoot != null)
                {
                    this.dataRoot = (global::CMP307.CreateMeeting)newDataRoot;
                    return true;
                }
                return false;
            }

            public void Loading(global::Windows.UI.Xaml.FrameworkElement src, object data)
            {
                this.Initialize();
            }

            // Update methods for each path node used in binding steps.
            private void Update_(global::CMP307.CreateMeeting obj, int phase)
            {
                if (obj != null)
                {
                    if ((phase & (NOT_PHASED | (1 << 0))) != 0)
                    {
                        this.Update_persons(obj.persons, phase);
                        this.Update_rooms(obj.rooms, phase);
                    }
                }
            }
            private void Update_persons(global::System.Collections.ObjectModel.ObservableCollection<global::MeetingLib.Person> obj, int phase)
            {
                if ((phase & ((1 << 0) | NOT_PHASED )) != 0)
                {
                    // CreateMeeting.xaml line 66
                    if (!isobj7ItemsSourceDisabled)
                    {
                        XamlBindingSetters.Set_Windows_UI_Xaml_Controls_ItemsControl_ItemsSource(this.obj7, obj, null);
                    }
                }
            }
            private void Update_rooms(global::System.Collections.ObjectModel.ObservableCollection<global::MeetingLib.Room> obj, int phase)
            {
                if ((phase & ((1 << 0) | NOT_PHASED )) != 0)
                {
                    // CreateMeeting.xaml line 39
                    if (!isobj14ItemsSourceDisabled)
                    {
                        XamlBindingSetters.Set_Windows_UI_Xaml_Controls_ItemsControl_ItemsSource(this.obj14, obj, null);
                    }
                }
            }
        }
        /// <summary>
        /// Connect()
        /// </summary>
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Windows.UI.Xaml.Build.Tasks"," 10.0.19041.685")]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public void Connect(int connectionId, object target)
        {
            switch(connectionId)
            {
            case 2: // CreateMeeting.xaml line 24
                {
                    this.btnMBack = (global::Windows.UI.Xaml.Controls.Button)(target);
                    ((global::Windows.UI.Xaml.Controls.Button)this.btnMBack).Click += this.btnMBack_Click;
                }
                break;
            case 3: // CreateMeeting.xaml line 26
                {
                    this.meetingPanel = (global::Windows.UI.Xaml.Controls.StackPanel)(target);
                }
                break;
            case 4: // CreateMeeting.xaml line 58
                {
                    this.addParticipants = (global::Windows.UI.Xaml.Controls.StackPanel)(target);
                }
                break;
            case 5: // CreateMeeting.xaml line 61
                {
                    this.txtPName = (global::Windows.UI.Xaml.Controls.TextBox)(target);
                }
                break;
            case 6: // CreateMeeting.xaml line 63
                {
                    this.btnPInvite = (global::Windows.UI.Xaml.Controls.Button)(target);
                    ((global::Windows.UI.Xaml.Controls.Button)this.btnPInvite).Click += this.btnPInvite_Click;
                }
                break;
            case 7: // CreateMeeting.xaml line 66
                {
                    this.lstParticipants = (global::Windows.UI.Xaml.Controls.ListView)(target);
                    ((global::Windows.UI.Xaml.Controls.ListView)this.lstParticipants).ItemClick += this.lstParticipants_ItemClick;
                }
                break;
            case 8: // CreateMeeting.xaml line 73
                {
                    this.txtPErr = (global::Windows.UI.Xaml.Controls.TextBlock)(target);
                }
                break;
            case 9: // CreateMeeting.xaml line 74
                {
                    this.txtPHint = (global::Windows.UI.Xaml.Controls.TextBlock)(target);
                }
                break;
            case 11: // CreateMeeting.xaml line 30
                {
                    this.txtMStartTime = (global::Windows.UI.Xaml.Controls.TimePicker)(target);
                }
                break;
            case 12: // CreateMeeting.xaml line 33
                {
                    this.txtMEndTime = (global::Windows.UI.Xaml.Controls.TimePicker)(target);
                }
                break;
            case 13: // CreateMeeting.xaml line 35
                {
                    this.txtMName = (global::Windows.UI.Xaml.Controls.TextBox)(target);
                }
                break;
            case 14: // CreateMeeting.xaml line 39
                {
                    this.cmbMRoom = (global::Windows.UI.Xaml.Controls.ComboBox)(target);
                }
                break;
            case 15: // CreateMeeting.xaml line 53
                {
                    this.btnMCont = (global::Windows.UI.Xaml.Controls.Button)(target);
                    ((global::Windows.UI.Xaml.Controls.Button)this.btnMCont).Click += this.btnMCont_Click;
                }
                break;
            case 16: // CreateMeeting.xaml line 54
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
            switch(connectionId)
            {
            case 1: // CreateMeeting.xaml line 1
                {                    
                    global::Windows.UI.Xaml.Controls.Page element1 = (global::Windows.UI.Xaml.Controls.Page)target;
                    CreateMeeting_obj1_Bindings bindings = new CreateMeeting_obj1_Bindings();
                    returnValue = bindings;
                    bindings.SetDataRoot(this);
                    this.Bindings = bindings;
                    element1.Loading += bindings.Loading;
                    global::Windows.UI.Xaml.Markup.XamlBindingHelper.SetDataTemplateComponent(element1, bindings);
                }
                break;
            }
            return returnValue;
        }
    }
}

