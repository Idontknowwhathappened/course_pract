﻿#pragma checksum "..\..\PaymentsPage.xaml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "24C3D1D59BA0F54434FC0FF164FFCD441ED2B73BDE012538DB88866660E556B7"
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
    /// PaymentsPage
    /// </summary>
    public partial class PaymentsPage : System.Windows.Controls.Page, System.Windows.Markup.IComponentConnector {
        
        
        #line 25 "..\..\PaymentsPage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ListView AppointmentsList;
        
        #line default
        #line hidden
        
        
        #line 47 "..\..\PaymentsPage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock NameAppoinment;
        
        #line default
        #line hidden
        
        
        #line 51 "..\..\PaymentsPage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock Veterinarian;
        
        #line default
        #line hidden
        
        
        #line 56 "..\..\PaymentsPage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock AppointmentDateTextBlock;
        
        #line default
        #line hidden
        
        
        #line 60 "..\..\PaymentsPage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock AppointmentTimeTextBlock;
        
        #line default
        #line hidden
        
        
        #line 65 "..\..\PaymentsPage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock ComplaintsTextBlock;
        
        #line default
        #line hidden
        
        
        #line 70 "..\..\PaymentsPage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock TotalAmount;
        
        #line default
        #line hidden
        
        
        #line 73 "..\..\PaymentsPage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ComboBox PaymentMethodComboBox;
        
        #line default
        #line hidden
        
        
        #line 76 "..\..\PaymentsPage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button GenerateInvoice;
        
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
            System.Uri resourceLocater = new System.Uri("/VeterinaryClinic;component/paymentspage.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\PaymentsPage.xaml"
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
            this.AppointmentsList = ((System.Windows.Controls.ListView)(target));
            
            #line 25 "..\..\PaymentsPage.xaml"
            this.AppointmentsList.SelectionChanged += new System.Windows.Controls.SelectionChangedEventHandler(this.AppointmentsList_SelectionChanged);
            
            #line default
            #line hidden
            return;
            case 2:
            this.NameAppoinment = ((System.Windows.Controls.TextBlock)(target));
            return;
            case 3:
            this.Veterinarian = ((System.Windows.Controls.TextBlock)(target));
            return;
            case 4:
            this.AppointmentDateTextBlock = ((System.Windows.Controls.TextBlock)(target));
            return;
            case 5:
            this.AppointmentTimeTextBlock = ((System.Windows.Controls.TextBlock)(target));
            return;
            case 6:
            this.ComplaintsTextBlock = ((System.Windows.Controls.TextBlock)(target));
            return;
            case 7:
            this.TotalAmount = ((System.Windows.Controls.TextBlock)(target));
            return;
            case 8:
            this.PaymentMethodComboBox = ((System.Windows.Controls.ComboBox)(target));
            return;
            case 9:
            this.GenerateInvoice = ((System.Windows.Controls.Button)(target));
            
            #line 76 "..\..\PaymentsPage.xaml"
            this.GenerateInvoice.Click += new System.Windows.RoutedEventHandler(this.GenerateInvoice_Click);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}

