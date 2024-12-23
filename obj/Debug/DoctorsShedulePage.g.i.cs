﻿#pragma checksum "..\..\DoctorsShedulePage.xaml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "F17AA45F755DF62552EFF1F6B84442B9CFD5821A9455CA4A742CC938E0541CE8"
//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан программой.
//     Исполняемая версия:4.0.30319.42000
//
//     Изменения в этом файле могут привести к неправильной работе и будут потеряны в случае
//     повторной генерации кода.
// </auto-generated>
//------------------------------------------------------------------------------

using MaterialDesignThemes.Wpf;
using MaterialDesignThemes.Wpf.Converters;
using MaterialDesignThemes.Wpf.Transitions;
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
using VeterinaryClinic;


namespace VeterinaryClinic {
    
    
    /// <summary>
    /// DoctorsShedulePage
    /// </summary>
    public partial class DoctorsShedulePage : System.Windows.Controls.Page, System.Windows.Markup.IComponentConnector {
        
        
        #line 25 "..\..\DoctorsShedulePage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ComboBox DoctorsComboBox;
        
        #line default
        #line hidden
        
        
        #line 29 "..\..\DoctorsShedulePage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.DataGrid ScheduleDataGrid;
        
        #line default
        #line hidden
        
        
        #line 43 "..\..\DoctorsShedulePage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ComboBox WorkDayComboBox;
        
        #line default
        #line hidden
        
        
        #line 54 "..\..\DoctorsShedulePage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox StartTimeTextBox;
        
        #line default
        #line hidden
        
        
        #line 55 "..\..\DoctorsShedulePage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox EndTimeTextBox;
        
        #line default
        #line hidden
        
        
        #line 56 "..\..\DoctorsShedulePage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox DurationTextBox;
        
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
            System.Uri resourceLocater = new System.Uri("/VeterinaryClinic;component/doctorsshedulepage.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\DoctorsShedulePage.xaml"
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
            this.DoctorsComboBox = ((System.Windows.Controls.ComboBox)(target));
            
            #line 25 "..\..\DoctorsShedulePage.xaml"
            this.DoctorsComboBox.SelectionChanged += new System.Windows.Controls.SelectionChangedEventHandler(this.DoctorsComboBox_SelectionChanged);
            
            #line default
            #line hidden
            return;
            case 2:
            this.ScheduleDataGrid = ((System.Windows.Controls.DataGrid)(target));
            
            #line 29 "..\..\DoctorsShedulePage.xaml"
            this.ScheduleDataGrid.SelectionChanged += new System.Windows.Controls.SelectionChangedEventHandler(this.ScheduleDataGrid_SelectionChanged);
            
            #line default
            #line hidden
            return;
            case 3:
            this.WorkDayComboBox = ((System.Windows.Controls.ComboBox)(target));
            return;
            case 4:
            this.StartTimeTextBox = ((System.Windows.Controls.TextBox)(target));
            return;
            case 5:
            this.EndTimeTextBox = ((System.Windows.Controls.TextBox)(target));
            return;
            case 6:
            this.DurationTextBox = ((System.Windows.Controls.TextBox)(target));
            return;
            case 7:
            
            #line 57 "..\..\DoctorsShedulePage.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.AddOrUpdateScheduleButton_Click);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}

