﻿#pragma checksum "..\..\..\ForEveryone\AddProd.xaml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "FEF68054F5D79990EFECC03CC7AD8B4F3D96855B7C3C69EBE56A7DF9E39C6FB6"
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


namespace Castle.ForEveryone {
    
    
    /// <summary>
    /// AddProd
    /// </summary>
    public partial class AddProd : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 42 "..\..\..\ForEveryone\AddProd.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox PriceTextBox;
        
        #line default
        #line hidden
        
        
        #line 49 "..\..\..\ForEveryone\AddProd.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock PriceHint;
        
        #line default
        #line hidden
        
        
        #line 62 "..\..\..\ForEveryone\AddProd.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox QuantityTextBox;
        
        #line default
        #line hidden
        
        
        #line 69 "..\..\..\ForEveryone\AddProd.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock QuantityHint;
        
        #line default
        #line hidden
        
        
        #line 84 "..\..\..\ForEveryone\AddProd.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ComboBox CategoryComboBox;
        
        #line default
        #line hidden
        
        
        #line 92 "..\..\..\ForEveryone\AddProd.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ComboBox SupplierComboBox;
        
        #line default
        #line hidden
        
        
        #line 121 "..\..\..\ForEveryone\AddProd.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button LoadImageButton;
        
        #line default
        #line hidden
        
        
        #line 122 "..\..\..\ForEveryone\AddProd.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock ImageStatusText;
        
        #line default
        #line hidden
        
        
        #line 125 "..\..\..\ForEveryone\AddProd.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Image ProductImage;
        
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
            System.Uri resourceLocater = new System.Uri("/Castle;component/foreveryone/addprod.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\ForEveryone\AddProd.xaml"
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
            this.PriceTextBox = ((System.Windows.Controls.TextBox)(target));
            
            #line 46 "..\..\..\ForEveryone\AddProd.xaml"
            this.PriceTextBox.GotFocus += new System.Windows.RoutedEventHandler(this.PriceTextBox_GotFocus);
            
            #line default
            #line hidden
            
            #line 47 "..\..\..\ForEveryone\AddProd.xaml"
            this.PriceTextBox.LostFocus += new System.Windows.RoutedEventHandler(this.PriceTextBox_LostFocus);
            
            #line default
            #line hidden
            
            #line 48 "..\..\..\ForEveryone\AddProd.xaml"
            this.PriceTextBox.TextChanged += new System.Windows.Controls.TextChangedEventHandler(this.PriceTextBox_TextChanged);
            
            #line default
            #line hidden
            return;
            case 2:
            this.PriceHint = ((System.Windows.Controls.TextBlock)(target));
            return;
            case 3:
            this.QuantityTextBox = ((System.Windows.Controls.TextBox)(target));
            
            #line 66 "..\..\..\ForEveryone\AddProd.xaml"
            this.QuantityTextBox.GotFocus += new System.Windows.RoutedEventHandler(this.QuantityTextBox_GotFocus);
            
            #line default
            #line hidden
            
            #line 67 "..\..\..\ForEveryone\AddProd.xaml"
            this.QuantityTextBox.LostFocus += new System.Windows.RoutedEventHandler(this.QuantityTextBox_LostFocus);
            
            #line default
            #line hidden
            
            #line 68 "..\..\..\ForEveryone\AddProd.xaml"
            this.QuantityTextBox.TextChanged += new System.Windows.Controls.TextChangedEventHandler(this.QuantityTextBox_TextChanged);
            
            #line default
            #line hidden
            return;
            case 4:
            this.QuantityHint = ((System.Windows.Controls.TextBlock)(target));
            return;
            case 5:
            this.CategoryComboBox = ((System.Windows.Controls.ComboBox)(target));
            return;
            case 6:
            this.SupplierComboBox = ((System.Windows.Controls.ComboBox)(target));
            return;
            case 7:
            this.LoadImageButton = ((System.Windows.Controls.Button)(target));
            
            #line 120 "..\..\..\ForEveryone\AddProd.xaml"
            this.LoadImageButton.Click += new System.Windows.RoutedEventHandler(this.UploadPhoto_Click);
            
            #line default
            #line hidden
            return;
            case 8:
            this.ImageStatusText = ((System.Windows.Controls.TextBlock)(target));
            return;
            case 9:
            this.ProductImage = ((System.Windows.Controls.Image)(target));
            return;
            case 10:
            
            #line 131 "..\..\..\ForEveryone\AddProd.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.Save_Click);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}

