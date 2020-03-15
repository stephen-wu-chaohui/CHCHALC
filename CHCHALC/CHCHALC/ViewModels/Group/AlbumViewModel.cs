﻿using System;
using CHCHALC.Models;
using CHCHALC.Services;
using Xamarin.Forms;

namespace CHCHALC.ViewModels
{
    public class AlbumViewModel : BaseViewModel
    {
        readonly IContextService contextService = DependencyService.Get<IContextService>();

        private string name;
        public string Name {
            get { return name; }
            set { SetProperty(ref name, value); }
        }

        private string image;
        public string Image {
            get { return image; }
            set { SetProperty(ref image, value); }
        }


        private string address;
        public string Address {
            get { return address; }
            set { SetProperty(ref address, value); }
        }

        private double latitude;
        public double Latitude {
            get { return latitude; }
            set { SetProperty(ref latitude, value); }
        }

        private double longitude;
        public double Longitude {
            get { return longitude; }
            set { SetProperty(ref longitude, value); }
        }

        private string missionStatement;
        public string MissionStatement {
            get { return missionStatement; }
            set { SetProperty(ref missionStatement, value); }
        }

        public Album CurrentAlbum { get; internal set; }

        public Command SaveCommand { get; set; }

        public bool IsEditable { get; }
        public string EditingButtonText { get; }

        public AlbumViewModel(Album album)
        {
            CurrentAlbum = album;
            Title = "Event";
            IsEditable = true;
            EditingButtonText = "Edit";

            if (album != null) {
                Name = album.Name ?? "";
                Image = album.Image ?? "sunday.png";
                MissionStatement = album.MissionStatement;
            }

            SaveCommand = new Command(() =>
            {
                if (contextService?.StoredDisciple == null) {
                    return;
                }
                Album copy = album?.Clone() as Album ?? new Album();
                copy.Name = Name;
                copy.Image = Image;
                copy.MissionStatement = MissionStatement;
                MessagingCenter.Send(copy, "upsert");
            });
        }
    }
}
