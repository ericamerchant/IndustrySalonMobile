﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MobileApp
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class ContactSalon : ContentPage
	{
		public ContactSalon ()
		{
			InitializeComponent ();

            // The TapViewModel contains the TapCommand which is wired up in Xaml
            BindingContext = new TapViewModel();
		}
	}
}