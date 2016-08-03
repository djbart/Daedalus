﻿using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace TheDaedalusSentenceCompanion
{
	public partial class HomePage : ContentPage
	{
		public HomePage()
		{
			InitializeComponent();
		}

		void OnButtonClicked(object sender, EventArgs args)
		{
			Button button = (Button)sender;

			Page settingsPage = new SettingsPage();

			button.Navigation.PushModalAsync(settingsPage);
		}
	}
}

